using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrajectory : MonoBehaviour
{
  //  Rigidbody rigid;
    [SerializeField] float speed;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Transform arrowPos;
    CharacterStatus target;
    [SerializeField] float damage;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
       StartCoroutine(Lunch());
    }
    private void OnDisable()
    {
        if (target!=null)
        {
        target.CurrentHealth -= damage;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Lunch()
    {
        while (gameObject.activeSelf)
        {

            transform.Translate(moveDirection * speed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var currentObject = collision.gameObject;
        if (currentObject.CompareTag("Enemy"))
        {
            // Take Damage() in characters
            target = collision.gameObject.GetComponent<CharacterStatus>();
            
            gameObject.SetActive(false);
        }
        else
        {
            target = null;
            gameObject.SetActive(false);

        }

    }
    //IEnumerator Hit()
    //{
       
       
    //    yield return null;
    //}
    //public void LanchArrow()
    //{


    //    // Transform originalPos = GameObject.Find("Character_Player/ArrowPos").transform;
    //    // rigid.velocity = originalPos.forward;
    //    // Vector3 rote = rigid.velocity;
    //    //transform.rotation = Quaternion.LookRotation(rote);
    //}



    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        rigid.velocity = Vector3.zero;
    //        gameObject.SetActive(false);
    //    }
    //}
}
        //if (isOnGround == false)
        //{
        //    // chache projectile drop point 

        //    //Vector3 targetPos = new Vector3(originalPos.x, 0, originalPos.z + distance);
        //    //// set shoot distance 
        //    //float shootDistance = distance;
        //    //// Check projectle current height == projectle position in Y- axis
        //    //float projectileHight = ProjectileHight(targetPos);
        //    //// caculate the angle of the projectile 



        //    //// calculate initial speed  in y axis and z axis 
        //    ////float Vz = Mathf.Sqrt((gravity * shootDistance * shootDistance) / (2.0f * (projectileHight - shootDistance * shootingAngle)));
        //    //float Vz = Mathf.Sqrt((gravity * shootDistance * shootDistance) / (2.0f * (projectileHight +ProjectileHight(targetPos) - shootDistance * shootingAngle)));
        //    //float Vy = shootingAngle * Vz;

        //    ////set projectile initial velocity 

        //    ////Transform loacal Velocity to Global Velocity 

        //    //// rigid.AddForce(globalVelocity);
        //    //// transform the projectie current rotation 
        //    //

        //    ////if (Vector3.Distance(originalPos,transform.position)>= 0f)
        //    ////{
        //    ////    rigid.velocity = Vector3.zero;
        //    ////    Debug.Log("Too Far");
        //    ////}
        //    ///
        //    Vector3 initialObjectPos = new Vector3(transform.position.x, 0, transform.position.z);
        //    Vector3 initialTargetPos = new Vector3(targetTrans.position.x, 0, targetTrans.position.z);
        //    Vector3 initialX = new Vector3(PlayerRot.x, 0, 0);
        //    float distance = Vector3.Distance(initialObjectPos, initialTargetPos);
      
        //    float shootingAngle = Mathf.Tan(angle * Mathf.Deg2Rad);

        //    // VzeroX = sqrt((gravity * distance * distance) / (2.0f * (initialHeight - distance *  tan (shooting Angle))))
        //    float VZZ = Mathf.Sqrt((gravity * distance * distance) / (2.0f * (InitialHeight() - distance * shootingAngle)));
        //    float VZY = shootingAngle * VZZ;
        //    Vector3 localVelocity = new Vector3(0, VZY, VZZ);
        //    Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        //    rigid.velocity = globalVelocity;

        //    transform.rotation = Quaternion.LookRotation(rigid.velocity);

        //    // VzeroY = VzeroX * tan (shooting Angle);