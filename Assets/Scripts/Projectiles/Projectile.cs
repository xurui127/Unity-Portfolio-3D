using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float speed;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Transform arrowPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(Lunch());
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
}
