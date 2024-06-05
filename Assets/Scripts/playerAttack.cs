using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    public GameObject weapon; //weapon object
    private Animator animator; //weapon animator
    private Rigidbody rb; //player rigidbody

    [Header("Block")]
    private bool isBlock;
    private bool isBlockAvailable = true;
    private float blockCooldown = 5f;
    public float knockbackForce = 4000f;

    [Header("Attack")]
    private bool isAttack;
    private katanaController weaponController;

    private void Start()
    {
        animator = weapon.GetComponent<Animator>();
        weaponController = weapon.GetComponent<katanaController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isBlock = Input.GetMouseButton(1) && isBlockAvailable;
        PlayBlockAnimation();

        isAttack = Input.GetMouseButtonDown(0);
        PlayAttackAnimation();
    }

    public void Block(GameObject enemy)
    {
        isBlockAvailable = false;
        Invoke(nameof(ResetBlock), blockCooldown);
        Knockback(enemy);
    }

    private void Knockback(GameObject entity)
    {
        Vector3 direction = transform.position - entity.transform.position;
        direction.y = 0.1f;

        rb.velocity = Vector3.zero;
        rb.AddForce(direction.normalized * knockbackForce, ForceMode.Impulse);
    }

    public bool IsBlock()
    {
        return isBlock;
    }

    private void ResetBlock()
    {
        isBlockAvailable = true;
    }

    private void PlayBlockAnimation()
    {
        animator.SetBool("isBlock", isBlock);
    }

    private void PlayAttackAnimation()
    {
        animator.SetBool("isAttack", isAttack);
    }
}
