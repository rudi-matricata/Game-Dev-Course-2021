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
    private bool faceRight;

    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    Vector3 lastPosition;

    private void Start() {
        forceFactor = 10;
        moveSpeed = 3;
        isGrounded = false;
        faceRight = true;

        rigidBody.freezeRotation = true;

        animator = GetComponent<Animator>();

        HeroCollisions.OnGroundHit += SetGrounded;
        HeroCollisions.OnEnemyHit += ShowHurt;
    }

    private void Update() {
        lastPosition = transform.position;

        if (Input.GetKey(KeyCode.A)) {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            if(faceRight) {
                Flip();
            }
            animator.SetBool(xCoordAlteredAnimCondition, true);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            if (!faceRight) {
                Flip();
            }
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

    private void Flip() {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
