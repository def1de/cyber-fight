using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyControl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public NavMeshAgent agent;
    private bool isDead = false;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isDead)
        {
            ChasePlayer();
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            agent.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            animator.SetBool("isDead", true);
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void ChasePlayer()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    public void SetTarget(Transform player)
    {
        target = player;
    }

    public void SetDead()
    {
        isDead = true;
    }
}
