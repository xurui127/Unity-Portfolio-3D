using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character.Enemy.Animation;

public abstract class IEnemyMovementHandler : MonoBehaviour
{

    [SerializeField] protected GameObject player;
    protected NavMeshAgent agent;
    protected EnemyAnimationHandler anim;
    protected CharacterStatus characterStatus;
    protected CharacterStatus targetStatus;
    [SerializeField]protected CharacterStates enemyStates;

    [Header("Speed Settings")]
    [Range(0, 1f)]
    [SerializeField] protected float runSpeed;
    [Range(0, 0.8f)]
    [SerializeField] protected float walkSpeed;


    [Header("Radius Settings")]
    [Range(0, 10)]
    [SerializeField] protected float targetLockRadius;

    [Range(0, 10)]
    [SerializeField] protected float patrolRaduis;
    [Range(0, 10)]
    [SerializeField] protected float attackRadius;
    [Range(0, 10)]
    [SerializeField] protected float shottingRadius;


    //[Header("Attack Cool Down")]
    //[Range(0, 3f)]
    //[SerializeField] protected float attackTimer;
    //[Range(0, 100f)]
    //[SerializeField] protected float attackCoolDown;

    protected float restTimer;
    protected Vector3 patrolWayPoint;

    protected Vector3 originalPosition;
    public Collider[] Collider { get; private set; }
    
    
    protected abstract void EnemyBehaviours();

    protected abstract bool FindTarget();
    protected abstract void MoveToTarget(Vector3 destination, float startSpeed, float endSpeed);

    protected abstract IEnumerator AttackTarget();
    protected abstract void SetSpeed(float startSpeed, float endSpeed);
    protected abstract IEnumerator IsDead();

    protected abstract void PatrolRoute();
    protected abstract void RandomRoutPoints();
    protected abstract void ChaseTarget();
    protected abstract bool IdleTimeCounter();
    protected abstract void Hit();
   // protected abstract bool AttackCoolDownTimer();

}
