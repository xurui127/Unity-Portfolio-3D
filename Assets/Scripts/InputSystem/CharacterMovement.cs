using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Character.Movement {
    public class CharacterMovement  :MonoBehaviour
    {
 
        private void Start()
        {
                
        }
        private void Update()
        {
            
        }
        public void SetDestination(Vector3 destination, Vector3 hitDestination)
        {
            destination =hitDestination;
            Debug.Log("2222");
        }

        //public void MoveAnimtion(float speed, NavMeshAgent navMeshAgent, Animator anim, string animName)
        //{
        //    speed = Mathf.Clamp(navMeshAgent.velocity.magnitude, 0, 1);
        //    anim.SetFloat(animName, speed);
        //    Debug.Log("333");
        //}
        //public void MoveAnimtion(float speed, NavMeshAgent navMeshAgent, Animator anim, string animName)
        //{
        //    anim.Speed = Mathf.Clamp(navMeshAgent.velocity.magnitude, 0, 1);
        //    anim.SetFloat(animName, speed);
        //    Debug.Log("333");
        //}
    }
}
//void CharacterMove(Ray mouseRay , LayerMask layerMask)
//{

//        //mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//        //Physics.Raycast(mouseRay, out hit, 1000f, layerMask);

//}