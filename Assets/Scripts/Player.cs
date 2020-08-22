using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Movement variables
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool jumpFlag = false;
    Animator animator;

    //Health varaibles
    public int currentHealth;
    public int maxHealth = 30;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    // Attack variables
    public bool isSlash;
    public bool isBrandish;

    public int slashPressCount = 0;
    public int power = 2;

    void Start() {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        isSlash = false;
        float value = (float)currentHealth / (float)maxHealth;
        HealthBar.instance.SetValue(value);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0) {
                isInvincible = false;
                invincibleTimer = timeInvincible;
            }
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isSlash = stateInfo.IsName("Slash");
        isBrandish = stateInfo.IsName("brandish");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (jumpFlag) {
            animator.SetBool("Jump", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Fire1")) {
            animator.SetTrigger("Slash");
            slashPressCount = 1;
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            animator.SetTrigger("Brandish");
            slashPressCount = 2;
        }
    }

    public void OnLanding() {
        animator.SetBool("Jump", false);
    }

    void FixedUpdate() {
        float moveAmount = horizontalMove * Time.fixedDeltaTime;
        if (isSlash || isBrandish) {
            moveAmount = 0.0f;
        }
        controller.Move(moveAmount, false, jump);

        if (jump) {
            jumpFlag = true;
            jump = false;
        }
    }

    public void ChangeHealth(int newHealth) {
        if (newHealth < 0) {
            if (isInvincible) {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        animator.SetTrigger("Hit");
        currentHealth = Mathf.Clamp(currentHealth + newHealth, 0, maxHealth);
        float value = (float)currentHealth / (float)maxHealth;
        HealthBar.instance.SetValue(value);
    }
}
