using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    private static readonly int IsBlockAnim = Animator.StringToHash("isBlock");
    private static readonly int IsAttackAnim = Animator.StringToHash("isAttack");
    public GameObject weapon; //weapon object
    private Animator animator; //weapon animator

    [Header("Block")]
    private bool isBlock;
    private bool isBlockAvailable = true;
    private float blockCooldown = 5f;

    [Header("Attack")]
    private bool isAttack;
    private KatanaController weaponController;

    private void Start()
    {
        animator = weapon.GetComponent<Animator>();
        weaponController = weapon.GetComponent<KatanaController>();
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
        GetComponent<PlayerController>().Knockback(enemy);
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
        animator.SetBool(IsBlockAnim, isBlock);
    }

    private void PlayAttackAnimation()
    {
        animator.SetBool(IsAttackAnim, isAttack);
    }
}
