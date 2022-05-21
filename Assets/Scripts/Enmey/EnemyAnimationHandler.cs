using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Animation;


namespace Character.Enemy.Animation
{
    public class EnemyAnimationHandler : CharacterAnimationHandler
    {
        [SerializeField] public  string meleeAttack;
        [SerializeField] public string[] shootingAction;
    }
}
