using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOn : MonoBehaviour
{

    MeshRenderer meshRenderer;
    Color emissionOn;
    [SerializeField]Material startMatial;
    [SerializeField]Material endMatial;
    [SerializeField] LayerMask hitLayerMask; 
    GameObject player;
    Ray collideRay;
    RaycastHit hit;
     [SerializeField]GameObject hitPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] float misDis= 1000f;
    [SerializeField] Camera cam;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMatial;
        player = GameObject.FindGameObjectWithTag("Player");
        collideRay = new Ray(hitPoint.transform.position, hitPoint.transform.forward);
     
    }

    // Update is called once per frame
    void Update()
    {

       Debug.DrawRay(hitPoint.transform.position, hitPoint.transform.forward,Color.red);
       // Debug.DrawLine(hitPoint.transform.position, hitPoint.transform.forward, Color.blue);
        pos = player.transform.position;
        GetRayCollider();
    }

     public void GetRayCollider()
    {
        
        Collider collider;
        
        if (Physics.Raycast(collideRay, out hit,1000f))
        {

            
            collider = hit.collider;
            if (collider.gameObject.CompareTag("Enemy"))
            {
               
                Debug.Log("hit");
                meshRenderer.material = endMatial;
            }
            else
            {

                Debug.Log("no");

                meshRenderer.material = startMatial;
            }
        }
    }
}
