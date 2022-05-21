using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactive : MonoBehaviour
{
    [SerializeField] bool destroyGameObject;
    [SerializeField] float lifetime = 3f;
    WaitForSeconds waitLifeTime;

    private void Awake()
    {
        waitLifeTime = new WaitForSeconds(lifetime);
    }
    private void OnEnable()
    {
        StartCoroutine(DeactiveCoroutine());
    }
    IEnumerator DeactiveCoroutine()
    {
        yield return waitLifeTime;
        if (destroyGameObject)
        {
            gameObject.SetActive(false);
        }

    }
}
