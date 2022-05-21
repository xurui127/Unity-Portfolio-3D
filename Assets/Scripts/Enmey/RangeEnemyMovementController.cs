using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Enemy;
using UnityEngine.AI;
using Character.Enemy.Animation;

public class RangeEnemyMovementController : EnemyMovementController
{
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<EnemyAnimationHandler>();
        agent = GetComponent<NavMeshAgent>();
        characterStatus = GetComponent<CharacterStatus>();
        enemyStates = CharacterStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        EnemyBehaviours();
    }
    protected override void EnemyBehaviours()
    {
        switch (enemyStates)
        {
            case CharacterStates.Idle:

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
                StartCoroutine(AttackTarget());
                break;
            case CharacterStates.Shoot:
                StartCoroutine(ShootTarget());
                break;
            case CharacterStates.Patrol:
                FindTarget();
                PatrolRoute();
                break;
            case CharacterStates.Death:
                StartCoroutine(IsDead());
                break;
        }
    }
    protected virtual IEnumerator ShootTarget()
    {
        if (anim.IsShooting(anim.shootingAction) == false)
        {
            gameObject.transform.LookAt(player.transform);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            anim.SetShooting();
            yield return new WaitForSeconds(3);
        }

    }
    protected override bool FindTarget()
    {
        var collider = Physics.OverlapSphere(transform.position, targetLockRadius);

        foreach (var target in collider)
        {
            if (target.CompareTag("Player"))
            {
                targetStatus = player.GetComponent<CharacterStatus>();
                if (CheckDistance(attackRadius))
                {
                    enemyStates = CharacterStates.Attack;
                    return true;
                }
                else
                {
                    if (anim.IsAttacking(anim.meleeAttack) == false)
                    {
                        enemyStates = CharacterStates.Shoot;
                        return true;
                    }

                }


            }
            else if (target.CompareTag("Projectiles"))
            {
                enemyStates = CharacterStates.Shoot;
                return true;
            }
        }
        return false;
    }

    private bool CheckDistance(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }


}

