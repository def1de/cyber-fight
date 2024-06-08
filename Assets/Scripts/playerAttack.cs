using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    public GameObject weapon; //weapon object
    private Animator animator; //weapon animator

    [Header("Block")]
    private CharacterController Controller;
    private bool isBlock;
    private bool isBlockAvailable = true;
    private float blockCooldown = 5f;
    [SerializeField] private float knockbackForce;

    [Header("Attack")]
    private bool isAttack;
    private katanaController weaponController;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        animator = weapon.GetComponent<Animator>();
        weaponController = weapon.GetComponent<katanaController>();
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
        Vector3 direction = this.transform.position - entity.transform.position;
        direction.y = 0.1f;
        direction.Normalize();
        Controller.Move(direction * knockbackForce * 10 * Time.deltaTime);
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
