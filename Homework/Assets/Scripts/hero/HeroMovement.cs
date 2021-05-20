using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMovement : MonoBehaviour
{

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

    float xCoordDelta = 0;

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
        if ((xCoordDelta < 0 && faceRight) || (xCoordDelta > 0 && !faceRight)) {
            Flip();
        } 
        rigidBody.velocity = new Vector2(xCoordDelta, rigidBody.velocity.y);
        ConfigureAnimation();
    }

    public void OnJump() {
        if (isGrounded) {
            rigidBody.AddForce(Vector2.up * forceFactor, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public void OnMove(InputValue input) {
        Vector2 inputVec = input.Get<Vector2>();
        xCoordDelta = inputVec.x * moveSpeed;
    }

    private void OnDestroy() {
        HeroCollisions.OnEnemyHit -= ShowHurt;
        HeroCollisions.OnGroundHit -= SetGrounded;
    }

    private void ConfigureAnimation() {
        animator.SetBool(GameConstants.GROUNDED_ANIMATION_CONDITION, isGrounded);

        if(xCoordDelta == 0) {
            animator.SetBool(GameConstants.MOVE_HORIZONTALLY_ANIMATION_CONDITION, false);
        } else {
            animator.SetBool(GameConstants.MOVE_HORIZONTALLY_ANIMATION_CONDITION, true);
        }
    }

    private void ShowHurt() {
        animator.SetTrigger(GameConstants.HURT_ANIMATION_CONDITION_TRIGGER);
    }

    private void SetGrounded(bool grounded) {
        isGrounded = grounded;
    }

    private void Flip() {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
