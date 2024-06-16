using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanaController : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    public void AttackStart()
    {
        boxCollider.enabled = true;
    }

    public void AttackEnd()
    {
        boxCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<enemyControl>().SetDead();
            other.gameObject.tag = "Untagged";
        }
    }
}
