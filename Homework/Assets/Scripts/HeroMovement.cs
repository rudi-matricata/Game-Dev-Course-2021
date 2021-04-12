using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private const string groundedAnimCondition = "isGrounded";
    private const string xCoordAlteredAnimCondition = "isXMoving";
    private const string hurtConditionTrigger = "hurt";

    [SerializeField]
    private int forceFactor;
    [SerializeField]
    private int moveSpeed;

    private bool isGrounded;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    Vector3 lastPosition;

    private void Start() {
        forceFactor = 10;
        moveSpeed = 3;
        isGrounded = false;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        HeroCollisions.OnGroundHit += SetGrounded;
        HeroCollisions.OnEnemyHit += ShowHurt;
    }

    private void Update() {
        lastPosition = transform.position;

        if (Input.GetKey(KeyCode.A)) {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            spriteRenderer.flipX = true;
            animator.SetBool(xCoordAlteredAnimCondition, true);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            spriteRenderer.flipX = false;
            animator.SetBool(xCoordAlteredAnimCondition, true);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded) {
                rigidBody.AddForce(Vector2.up * forceFactor, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
        ConfigureAnimation();
    }

    private void OnDestroy() {
        HeroCollisions.OnEnemyHit -= ShowHurt;
        HeroCollisions.OnGroundHit -= SetGrounded;
    }

    private void ConfigureAnimation() {
        animator.SetBool(groundedAnimCondition, isGrounded);
        if (lastPosition == transform.position) {
            animator.SetBool(xCoordAlteredAnimCondition, false);
        }
    }

    private void ShowHurt() {
        animator.SetTrigger(hurtConditionTrigger);
    }

    private void SetGrounded(bool grounded) {
        isGrounded = grounded;
    }
}
