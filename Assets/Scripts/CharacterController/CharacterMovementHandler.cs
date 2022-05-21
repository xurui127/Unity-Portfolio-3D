using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Character.Player.Animation;



namespace Character.Player
{
    //[System.Serializable]
    //public class EventVector3 : UnityEvent<Vector3> { }


    /// <summary>
    /// The script "PlayerController" controls Main character movements 
    /// such as move, attack , spells, by using New input system and NavMeshAgent
    /// </summary>
    public class CharacterMovementHandler : MonoBehaviour
    {

        public static CharacterMovementHandler instance;
        private PlayerAnimationHandler anim;     // Cache Player Animator
        private CharacterStatus characterStatus;
        private CharacterStatus targetStatus;
        private CharacterStates playerStates;
        private NavMeshAgent agent;         // Cache PlayerNavMeshAgent
        private Ray mouseRay;               // Cache the ray when right mouse clicked
        private RaycastHit hit;             // Cache the ray shoot from camera to mouseclick ray position
        private float speed = 0;            // switch the player Locomotion blend tree animation  
        private bool isClicked = false;     // Cache new Input system right mouse click for walk
        private bool isAttack = false;      // Cache new Input system right mouse click for attack
        private bool isDash = false;       // Cache new Input system  space bar
        private bool isShoot = false;
        public bool dashCoolDown = false;
        [SerializeField] public LayerMask layerMask;         // filt layermasks  
        [SerializeField] private float dashSpeed;
        [SerializeField] private float coolDown;
        private bool isCoolDown = false;
        [SerializeField] public float arrowRange;
        [SerializeField] public GameObject arrow;
        [SerializeField] Transform arrowStartPos;
        [SerializeField] PoolManager poolManager;
     


        bool isDashing;


        public int comboStep = 0;
        public bool comboReceived = false;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, arrowRange);
        }

        //private void Awake()
        //{
        //    if (instance == null)
        //    {
        //        instance = this;
        //    }
        //    else if (instance != null)
        //    {
        //        Destroy(gameObject);
        //    }
        //}

        #region Bind Character Input System 


        public void IsClicked(InputAction.CallbackContext context)
        {


            isClicked = context.ReadValueAsButton();
           


        }



        // NEED FIXED
        public void IsDash(InputAction.CallbackContext context)
        {
            isDash = context.ReadValueAsButton();
        }

        public void IsShoot(InputAction.CallbackContext context)
        {
            isShoot = context.ReadValueAsButton();
        }

        #endregion
        public void IsShoot()
        {
            if (isShoot == true)
            {
                // StartCoroutine( Shoot());
                gameObject.transform.LookAt(hit.collider.transform);
                Shoot();
            }
            else
            {
                agent.updatePosition = true;
                //agent.isStopped = false;
            }
        }

        private void Shoot()
        {
            transform.LookAt(hit.point);
            agent.isStopped = true;
           // isClicked = false;
            isShoot = false;
            anim.SetShootArrow();

        }
        #region Character Dash 
        public void IsDash()
        {
            if (isDash == true)
            {
                if (dashCoolDown == false)
                {
                    agent.autoRepath = true;
                    isDashing = false;
                    StartCoroutine(Dashing());
                }
                else
                {
                    //Debug.Log("CD");
                }
            }
        }

        IEnumerator Dashing()
        {

            dashCoolDown = true;
            Dash();
            anim.SetDash();
            yield return new WaitForSeconds(1);

        }


        /// <summary>
        /// Set character dash dirction and dash speed 
        /// </summary>
        void Dash()
        {
            Vector3 direction = transform.forward;
            Vector3 destination = transform.position + direction * dashSpeed;

            agent.SetDestination(destination);
            if (Vector3.Distance(transform.position, destination) <= 0.2)
            {
                isDashing = false;

            }
            else
            {
                isDashing = true;

            }
        }

        #endregion
        #region Mouse Control
        public void ReadMouseInfo()
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out hit ))
            {
               
            }
        }
        public void MouseControl()
        {
            if (isClicked == true && hit.collider != null)
            {
                if (isShoot == false)
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        MoveToTarget(hit.point);
                    }
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        targetStatus = hit.collider.GetComponent<CharacterStatus>();
                        StartCoroutine(AttackTarget());
                    }
                }
             

            }
            if (isShoot == true && hit.collider != null)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    IsShoot();
                }
                if (hit.collider.CompareTag("Enemy"))
                {
                  
                    IsShoot();
                }
            }
            anim.Speed = Mathf.Clamp(agent.velocity.magnitude, 0, 1);
            anim.SetMove(anim.Speed);
        }


        private void MoveToTarget(Vector3 destination)
        {
            //anim.IsDashing == false
            if (anim.IsAttacking(anim.meleeAttack) == false  )
            {
                if (anim.IsShooting(anim.shootingAction) == false)
                {
                    agent.isStopped = false;
                    agent.destination = destination;
                }
                else
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                }

            }
            
        }
        #endregion
        #region Character Attack 
        IEnumerator AttackTarget()
        {
            if (Vector3.Distance(transform.position, hit.collider.gameObject.transform.position) > 2)
            {

                MoveToTarget(hit.collider.gameObject.transform.position);
                yield return null;
            }
            else
            {
                if (anim.IsAttacking(anim.meleeAttack) == false )
                {
                   
                    gameObject.transform.LookAt(hit.collider.transform);
                    agent.isStopped = true;
                    anim.SetAttack();
                    yield return new WaitForSeconds(1);
                }

            }
        }

        public void PlayAttack()
        {
            anim.SetAttack();
        }
        public void ComboReset()
        {
            comboStep = 0;
        }

        public void ShootArrow()
        {

            poolManager.ReleasePrefab(arrow, arrowStartPos.position ,arrowStartPos.rotation);
        }
        #endregion

        public void Hit()
        {
            targetStatus.TakeDamage(characterStatus);
            targetStatus.FloatingDamageText(characterStatus);
        }
        void Start()
        {
            anim = GetComponent<PlayerAnimationHandler>();
            agent = GetComponent<NavMeshAgent>();
            characterStatus = GetComponent<CharacterStatus>();

        }
        // Update is called once per frame
        void Update()
        {
            
            ReadMouseInfo();
            MouseControl();
            //IsShoot();
        }
        private void FixedUpdate()
        {
            IsDash();
        }
    }
}
//mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//Physics.Raycast(mouseRay, out hit, 1000f, layerMask);
        //private void IsInCoolDown()
        //{
        //    if (isCoolDown == true)
        //    {
        //        coolDown -= Time.deltaTime;
        //        if (coolDown <= 0)
        //        {
        //            coolDown = 3f;
        //            isCoolDown = false;

        //        }
        //    }

        //}