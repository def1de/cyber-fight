using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollisions : MonoBehaviour
{
    private playerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponent<playerAttack>();
    }

    public void EnemyTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<enemyControl>().SetTarget(transform);
        }
    }

    public void EnemyTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<enemyControl>().SetTarget(null);
        }
    }

    public void EnemyTriggerTouch(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (playerAttack.IsBlock())
            {
                playerAttack.Block(other.gameObject);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
