using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character.Player;

namespace Character.Animation
{

    public class CharacterAnimationHandler : MonoBehaviour
    {
        #region // Propertities
        //public static CharacterAnimationHandler instance;
        protected Animator anim;

        private float speed; // character speed property
        private bool attack;
        private bool death;
        private bool isAttack;
        private bool isShoot;
        //private bool isDashing;
        protected int comboNum;

        public bool isBusy;


        public float Speed { get { return speed; } set { speed = value; } }
        public bool Attack { get { return attack; } set { attack = value; } }
        public bool IsShoot { get { return isShoot; } set { isShoot = value; } }
        public bool Death { get { return death; } set { death = value; } }
        public int ComboNum { get { return comboNum; } set { comboNum = value; } }
        #endregion

        private void Awake()
        {
            anim = GetComponent<Animator>();
        //    if (instance == null)
        //    {
        //        instance = this;
        //    }
        //    else if (instance != null)
        //    {
        //        Destroy(gameObject);
        //    }
        }
        #region // Set Animations
        public void SetMove(float speed)
        {

            anim.SetFloat("speed", speed);
        }
        public void SetAttack()
        {
            anim.SetTrigger("attack");
        }
        public void SetShooting()
        {
            anim.SetTrigger("arrow");
        }
        public void SetDeath()
        {

            // player character death animation 
            anim.SetBool("dead", death);
        }
        public void SetDeath(bool death)
        {

            // player character death animation 
            anim.SetBool("dead", death);
        }
        public void SetAttackStep()
        {
            anim.SetInteger("attacks", comboNum);
        }

        public bool IsAttacking(string meleeAttack)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(meleeAttack))
            {

                return isAttack = true;
            }
            else
            {
                return isAttack = false;
            }
        }
        public bool IsAttacking(string[] meleeAttack)
        {
            isAttack = false;
            for (int i = 0; i < meleeAttack.Length; i++)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(meleeAttack[i]))
                {
                   
                    return isAttack = true;
                }

            }
           
            return isAttack = false;
        }
        public bool IsShooting(string[] shoot)
        {
            isShoot = false;
            for (int i = 0; i < shoot.Length; i++)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(shoot[i]))
                {
                    return isShoot = true;
                }

            }


            return isShoot = false;

        }
        public void  Busy()
        {
            Debug.Log("True");
            isBusy = true;
        }
        public void IsBusy()
        {
            Debug.Log("False");

            isBusy = false;
        }
        public void AttackCount()
        {
            if (comboNum < 2)
            {
                comboNum++;
            }
            else
            {
                comboNum = 0;
            }
        }


        #endregion
        private void Start()
        {
         
        }

        private void Update()
        {
         // SetAttackStep();

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






//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using Character.Player;

//namespace Character
//{

//    public class CharacterAnimationHandler : MonoBehaviour
//    {
//        #region // Propertities
//        public static CharacterAnimationHandler instance;
//        protected Animator anim;

//        private float speed; // character speed property
//        private bool attack;
//        private bool dead;

//        private bool isAttack;
//        public string[] meleeAttack = { "A_PlayerMeleeAttack01", "A_PlayerMeleeAttack02", "A_PlayerMeleeAttack03" };
//        private int comboNum;

//        public float Speed { get { return speed; } set { speed = value; } }
//        public bool Attack { get { return attack; } set { attack = value; } }
//        public bool Dead { get { return dead; } set { dead = value; } }
//        public int ComboNum { get { return comboNum; } set { comboNum = value; } }
//        #endregion

//        private void Awake()
//        {
//            if (instance == null)
//            {
//                instance = this;
//            }
//            else if (instance != null)
//            {
//                Destroy(gameObject);
//            }
//        }
//        #region // Set Animations
//        public void SetMove(float speed)
//        {

//            anim.SetFloat("speed", speed);
//        }
//        public void SetAttack()
//        {
//            anim.SetTrigger("attack");
//        }
//        public void SetDeath()
//        {
//            //To Do 
//            // player character death animation 
//        }
//        public void SetAttackStep()
//        {
//            anim.SetInteger("attacks", comboNum);
//        }
//        public bool IsAttacking(string meleeAttack)
//        {
//            isAttack = false;
//            if (anim.GetCurrentAnimatorStateInfo(0).IsName(meleeAttack))
//            {
//                return isAttack = true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        public bool IsAttacking(string[] meleeAttack)
//        {
//            isAttack = false;
//            for (int i = 0; i < meleeAttack.Length; i++)
//            {
//                if (anim.GetCurrentAnimatorStateInfo(0).IsName(meleeAttack[i]))
//                {
//                    return isAttack = true;
//                }

//            }
//            return false;
//        }
//        public void AttackCount()
//        {
//            if (comboNum < 2)
//            {
//                comboNum++;
//            }
//            else
//            {
//                comboNum = 0;
//            }
//        }

//        #endregion
//        private void Start()
//        {
//            anim = GetComponent<Animator>();
//        }

//        private void Update()
//        {
//            SetAttackStep();
//        }
//    }
//}

//public void IsBusy()
//{
//    busy = true;

//}
//public void NotBusy()
//{
//    busy = false;

//}