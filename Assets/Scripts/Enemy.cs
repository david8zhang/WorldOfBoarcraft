using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed = 3.0f;
    float timer;
    float checkTimer;
    bool checking;
    float changeTime;
    float checkTime;
    Rigidbody2D rigidbody2D;
    Animator animator;
    int direction = 1;

    public int currentHealth;
    public int maxHealth = 10;
    bool isHit;

    public bool isDead;
    public bool dying;

    public bool isStopped;

    bool isAttack;

    public GameObject canvasBoard;
    Slider slider;

    void Start() {
        timer = Random.Range(2, 8);
        checkTimer = Random.Range(2, 8);
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        isDead = false;
        isHit = false;

        slider = canvasBoard.GetComponentInChildren<Slider>();
        slider.value = currentHealth / maxHealth;
    }

    public void CheckDead() {
        if (isDead) {
            Destroy(gameObject);
        }
    }

    void Update() {

        CheckDead();
        isHit = animator.GetCurrentAnimatorStateInfo(0).IsName("Hit");

        Vector2 lookVector = new Vector2(direction, 0);
        if (checking) {
            lookVector = new Vector2(-direction, 0);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookVector, 1.5f, LayerMask.GetMask("Player"));
        if (hit.collider != null) {
            Player player = hit.collider.GetComponent<Player>();
            if (player) {
                animator.SetBool("Attack", true);
                isAttack = true;
            }
        } else {
            animator.SetBool("Attack", false);
            isAttack = false;
        }

        // Idle for a moment
        if (!isAttack && !isDead && !isHit) {
            if (checking) {
                animator.SetBool("Run", false);
                checkTimer -= Time.deltaTime;

                // Finished the check
                if (checkTimer < 0) {
                    checking = false;
                    checkTimer = Random.Range(2, 8);
                    Flip();
                }
            } else {
                timer -= Time.deltaTime;
            }
        }

        // Flip on an interval and move the other way
        if (timer < 0 && !isDead && !isHit && !isAttack) {
            direction = -direction;
            timer = Random.Range(2, 8);
            checking = true;
        }

        if (!checking && !isDead && !isHit && !isAttack && !isStopped) {
            animator.SetBool("Run", true);
            Vector2 position = rigidbody2D.position;
            position.x = position.x + Time.deltaTime * speed * direction;
            rigidbody2D.MovePosition(position);
        }
    }
    private void Flip() {
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
        Vector3 canvasScale = canvasBoard.transform.localScale;

		theScale.x *= -1;
        canvasScale.x *= -1;

        canvasBoard.transform.localScale = canvasScale;
		transform.localScale = theScale;
	}

    public void Stop() {
        animator.SetBool("Run", false);
        isStopped = true;
    }

    public void Go() {
        isStopped = false;
    }

    public void Damage(int amount) {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth == 0) {
            animator.SetTrigger("Death");
        } else {
            animator.SetTrigger("Hit");
        }
        slider.value = (float)currentHealth / (float)maxHealth;
    }
}
