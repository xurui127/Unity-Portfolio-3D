using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTimer;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    private const float randomPos = 1.5f;
    private float randomNum;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTimer);
        transform.localPosition += offset;

        randomNum = Random.Range(-randomPos, randomPos);
        //float clampNum = 
        transform.localPosition += new Vector3(randomNum,
                                           0,
                                           0);


    }
    private void Update()
    {

        transform.localPosition += new Vector3(0, 0.02f, 0);
    }

}
