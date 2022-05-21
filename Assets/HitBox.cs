using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private CharacterStatus characterStatus;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    StartCoroutine(TakeDamager(other));
        //}
    }
    //IEnumerator TakeDamager(Collider other)
    //{
    //    other.gameObject.GetComponent<CharacterStatus>().TakeDamage(characterStatus.GetDamage());
    //    yield return new WaitForSeconds(1);
    //}
}
