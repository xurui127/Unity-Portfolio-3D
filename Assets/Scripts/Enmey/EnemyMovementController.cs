using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character.Enemy.Animation;

namespace Character.Enemy
{

    public class EnemyMovementController : IEnemyMovementHandler
    {
        private void Awake()
        {
            originalPosition = gameObject.transform.position;

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, targetLockRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, patrolRaduis);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, shottingRadius);
        }
       

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<EnemyAnimationHandler>();
            agent = GetComponent<NavMeshAgent>();
            characterStatus = GetComponent<CharacterStatus>();
            enemyStates = CharacterStates.Idle;

        }
        void Update()
        {
            FindTarget();
            if (FindTarget() == true)
            {
                //enemyStates = CharacterStates.Chase;
                enemyStates = CharacterStates.Attack;
            }

            // TO BE FIX Later
            if (characterStatus.CurrentHealth <= 0)
            {
                enemyStates = CharacterStates.Death;
            }
            EnemyBehaviours();
        }
        /// <summary>
        /// Enemy can change different States --- Idle, Guard , Attack, Patorl , and Death 
        /// </summary>
        protected override void EnemyBehaviours()
        {
            switch (enemyStates)
            {

                case CharacterStates.Idle:
                    //Debug.Log("In Idle");
                    anim.Speed = 0;
                    agent.isStopped = true;
                    restTimer -= Time.deltaTime;
                    if (IdleTimeCounter() == true)
                    {
                        agent.isStopped = false;
                        SetSpeed(walkSpeed, 0);
                        enemyStates = CharacterStates.Patrol;
                    }
                    break;
                case CharacterStates.Guard:
                    //  Debug.Log("In Guard");

                    if (transform.position != originalPosition)
                    {
                        MoveToTarget(originalPosition, 0, walkSpeed);
                    }
                    if (Vector3.Distance(transform.position, originalPosition) >= agent.stoppingDistance)
                    {
                        enemyStates = CharacterStates.Idle;
                    }

                    break;
                case CharacterStates.Attack:
                    // Debug.Log("In Attack");

                    StartCoroutine(AttackTarget());
                    if (FindTarget() == false)
                    {
                        enemyStates = CharacterStates.Guard;
                    }

                    break;
                case CharacterStates.Patrol:
                    // Debug.Log("In Patrol");
                    PatrolRoute();
                    break;
                case CharacterStates.Chase:
                    ChaseTarget();
                    break;
                case CharacterStates.Death:
                    StartCoroutine(IsDead());
                    break;
            }
        }
        /// <summary>
        /// Character can find Play with Tags
        /// if tag is Player, Enemy will cache player's Status script
        /// </summary>
        protected override bool FindTarget()
        {
            var collider = Physics.OverlapSphere(transform.position, targetLockRadius);

            foreach (var target in collider)
            {
                if (target.CompareTag("Player"))
                {
                    targetStatus = player.GetComponent<CharacterStatus>();
                    agent.destination = player.transform.position;
                    // MoveToTarget(player.transform.position, 0, runSpeed);
                    //enemyStates = CharacterStates.Chase;
                    return true;
                }
                else if (target.CompareTag("Projectiles"))
                {
                    enemyStates = CharacterStates.Chase;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        ///  Enemy can chase the target
        ///  if the distance is close to targetLockRadius is Enemy will in Chase state
        /// </summary>
        protected override void ChaseTarget()
        {
               agent.destination = player.transform.position;
             // MoveToTarget(player.transform.position, 0, runSpeed);
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= targetLockRadius)
            {
                enemyStates = CharacterStates.Attack;
            }
        }
        /// <summary>
        /// Attack Target can check the distance between Enemy and Player,
        /// if they are not close, the function will can MoveToTarget
        /// if they are close enough, it will can attack.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator AttackTarget()
        {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackRadius && anim.IsAttacking(anim.meleeAttack) == false)
            {
                MoveToTarget(player.transform.position, 0, runSpeed);
                yield return null;
            }
            else
            {
                if (anim.IsAttacking(anim.meleeAttack) == false )
                {
                    gameObject.transform.LookAt(player.transform);
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    anim.SetAttack();
                    yield return new WaitForSeconds(3);
                }
            }
        }
        /// <summary>
        /// set enemy speed function 
        /// </summary>
        /// <param name="startSpeed">from idle to run speed </param>
        /// <param name="endSpeed">runnung speed</param>
        protected override void SetSpeed(float startSpeed, float endSpeed)
        {
            anim.Speed = Mathf.Clamp(agent.velocity.magnitude, startSpeed, endSpeed);
            anim.SetMove(anim.Speed);
        }
        /// <summary>
        /// if enemy currentHP is loser than 0, 
        /// this function will call death animation, 
        /// and destroy gameobject
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator IsDead()
        {
            if (characterStatus.CurrentHealth <= 0)
            {
                agent.isStopped = true;
                anim.SetDeath();
                yield return new WaitForSeconds(4);
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// set Enemy Patrol behaviour
        /// </summary>
        protected override void PatrolRoute()
        {
            if (Vector3.Distance(transform.position, patrolWayPoint) >= agent.stoppingDistance)
            {
                restTimer = 1;
                MoveToTarget(patrolWayPoint, 0, walkSpeed);
            }
            else
            {
                agent.velocity = Vector3.zero;
                anim.SetMove(0);

                enemyStates = CharacterStates.Idle;
            }
        }
        /// <summary>
        /// set random points in enemy patrolRaduis
        /// </summary>
        protected override void RandomRoutPoints()
        {
            float randomX = Random.Range(-patrolRaduis, patrolRaduis);
            float randomZ = Random.Range(-patrolRaduis, patrolRaduis);

            patrolWayPoint = new Vector3(originalPosition.x + randomX, originalPosition.y, transform.position.z + randomZ);
        }
        /// <summary>
        /// set enemy destination. 
        /// </summary>
        /// <param name="destination">Target</param>
        /// <param name="startSpeed">from Idel to run speed</param>
        /// <param name="endSpeed">set running speed</param>
        protected override void MoveToTarget(Vector3 destination, float startSpeed, float endSpeed)
        {
            agent.isStopped = false;
            agent.destination = destination;
            SetSpeed(startSpeed, endSpeed);
        }
        /// <summary>
        /// in this function, 
        /// if restTimer is loser than 0 
        /// enemy will move to next partal point
        /// </summary>
        /// <returns></returns>
        protected override bool IdleTimeCounter()
        {
            if (restTimer <= 0)
            {
                RandomRoutPoints();
                return true;
            }
            return false;
        }
        /// <summary>
        /// this function bind will Player Attack Animation, 
        /// if player attacked, it will be called and enemy will lose HP
        /// </summary>
        protected override void Hit()
        {
            targetStatus.TakeDamage(characterStatus);
          
        }

    }
}
//[SerializeField] private GameObject player;
//private NavMeshAgent agent;
//private EnemyAnimationHandler anim;
//private CharacterStatus characterStatus;
//private CharacterStates enemyStates;

//[Header("Speed Settings")]
//[Range(0, 1f)]
//[SerializeField] private float runSpeed;
//[Range(0, 0.8f)]
//[SerializeField] private float walkSpeed;


//[Header("Radius Settings")]
//[Range(0, 10)]
//[SerializeField] private float targetLockRadius;

//[Range(0, 10)]
//[SerializeField] private float patrolRaduis;
//[Range(0, 5)]
//[SerializeField] private float attackRadius;

//[SerializeField] private float restTimer;
//[SerializeField] Vector3 patrolWayPoint;

//private Vector3 originalPosition;
//public Collider[] Collider { get; private set; }
//private bool IdleTimeCounter()
//{

//    if (restTimer <= 0)
//    {
//        RandomRoutePoints();
//        return true;
//    }
//    return false;
//}
//private bool FindTarget()
//{
//    var collider = Physics.OverlapSphere(transform.position, targetLockRadius);

//    foreach (var target in collider)
//    {
//        if (target.CompareTag("Player"))
//        {
//            agent.destination = player.transform.position;

//            return true;
//        }

//    }
//    return false;
//}


//private void EnemyBehaviors()
//{

//    switch (enemyStates)
//    {

//        case CharacterStates.Idle:
//            //Debug.Log("In Idle");
//            anim.Speed = 0;
//            agent.isStopped = true;
//            restTimer -= Time.deltaTime;
//            if (IdleTimeCounter() == true)
//            {
//                agent.isStopped = false;
//                SetSpeed(walkSpeed, 0);
//                enemyStates = CharacterStates.Patrol;
//            }
//            break;
//        case CharacterStates.Guard:
//          //  Debug.Log("In Guard");

//            if (transform.position != originalPosition)
//            {
//                MoveToTarget(originalPosition, 0, walkSpeed);
//            }
//            if (Vector3.Distance(transform.position, originalPosition) >= agent.stoppingDistance)
//            {
//                enemyStates = CharacterStates.Idle;
//            }

//            break;
//        case CharacterStates.Attack:
//           // Debug.Log("In Attack");

//            StartCoroutine(AttackTarget());
//            if (FindTarget() == false)
//            {
//                enemyStates = CharacterStates.Guard;
//            }

//            break;
//        case CharacterStates.Patrol:
//           // Debug.Log("In Patrol");
//            PatrolRoute();
//            break;
//        case CharacterStates.Death:
//            StartCoroutine(IsDead());
//            break;
//    }
//}

//void MoveToTarget(Vector3 destination, float startSpeed, float endSpeed)
//{

//    agent.isStopped = false;
//    agent.destination = destination;
//    SetSpeed(startSpeed, endSpeed);
//}

//private void SetSpeed(float startSpeed, float endSpeed)
//{
//    anim.Speed = Mathf.Clamp(agent.velocity.magnitude, startSpeed, endSpeed);
//    anim.SetMove(anim.Speed);
//}

//IEnumerator AttackTarget()
//{
//    if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackRadius && anim.IsAttacking(anim.meleeAttack) == false)
//    {
//        MoveToTarget(player.transform.position, 0, runSpeed);
//        yield return null;
//    }
//    else
//    {
//        if (anim.IsAttacking(anim.meleeAttack) == false)
//        {
//            gameObject.transform.LookAt(player.transform);
//            agent.isStopped = true;
//            anim.SetAttack();
//            yield return new WaitForSeconds(5);
//        }
//    }


//}

// TO BE Fix Later
//IEnumerator IsDead()
//{
//    if (characterStatus.CurrentHealth<= 0 )
//    {
//        agent.isStopped = true;
//        anim.SetDeath();
//        yield return new WaitForSeconds(4);
//        Destroy(gameObject);
//    }
//}
//private void PatrolRoute()
//{
//    if (Vector3.Distance(transform.position, patrolWayPoint) >= agent.stoppingDistance)
//    {
//        restTimer = 1;
//        MoveToTarget(patrolWayPoint, 0, walkSpeed);
//    }
//    else
//    {
//        agent.velocity = Vector3.zero;
//        anim.SetMove(0);

//        enemyStates = CharacterStates.Idle;
//    }

//}

//private void RandomRoutePoints()
//{
//    float randomX = Random.Range(-patrolRaduis, patrolRaduis);
//    float randomZ = Random.Range(-patrolRaduis, patrolRaduis);

//    patrolWayPoint = new Vector3(originalPosition.x + randomX, originalPosition.y, transform.position.z + randomZ);


//}