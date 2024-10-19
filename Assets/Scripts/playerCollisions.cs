using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void EnemyTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyControl>().SetTarget(transform);
        }
    }

    public void EnemyTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyControl>().SetTarget(null);
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
