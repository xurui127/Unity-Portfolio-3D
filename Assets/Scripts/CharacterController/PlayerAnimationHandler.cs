using Character.Animation;
using UnityEngine;

namespace Character.Player.Animation
{
    public class PlayerAnimationHandler : CharacterAnimationHandler
    {
        [SerializeField] public string[] meleeAttack;
        [SerializeField] public string[]  shootingAction;
        [SerializeField] public string dashing;
        [SerializeField] public bool isDashing;
        [SerializeField] public GameObject[] bow;
        [SerializeField] public bool coolDown;
        [SerializeField] public bool isShooting;
        [SerializeField] public float dashCoolDown;
        [SerializeField] public float CoolDown;
        private CharacterMovementHandler handler;

        public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
        public void SetDash()
        {
            anim.SetTrigger("dodge");
        }

        public void SetShootArrow()
        {
            anim.SetTrigger("arrow");
        }
        public bool IsDash(string dash)
        {
            isDashing = false;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(dash))
            {
               
                return isDashing = true ;
            }
            return isDashing = false;
           
        }
    
        public void ActiveBow()
        {
            foreach (var item in bow)
            {
                item.SetActive(true);
            }
          
        }
        public void DisActiveBow()
        {
            foreach (var item in bow)
            {
                item.SetActive(false);
            }
        }
        public void CoolDownCounter()
        {
            if (handler.dashCoolDown == true)
            {
                dashCoolDown += Time.deltaTime;
                if (dashCoolDown >= CoolDown)
                {
                    dashCoolDown = 0;
                    handler.dashCoolDown = false;
                    Debug.Log(handler.dashCoolDown);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            handler = GetComponent<CharacterMovementHandler>();
        }

        // Update is called once per frame
        void Update()
        {
            SetAttackStep();
            CoolDownCounter();
            IsDash(dashing);
            //Debug.Log(IsShooting(shootingAction));
            //if (Input.GetKeyDown("1"))
            //{
            //    SetShootArrow();
            //}
        }

    }
}

//public void IsBusy()
//{
//    busy = true;

//}
//public void NotBusy()
//{
//    busy = false;

//}