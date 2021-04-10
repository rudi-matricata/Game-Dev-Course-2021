using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField]
    private int forceFactor;
    [SerializeField]
    private int moveSpeed;

    private bool isGrounded;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        forceFactor = 10;
        moveSpeed = 3;
        isGrounded = false;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

        spriteRenderer = GetComponent<SpriteRenderer>();

        HeroCollisions.OnGroundHit += SetGrounded;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.A)) {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded) {
                rigidBody.AddForce(Vector2.up * forceFactor, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }

    private void SetGrounded(bool grounded) {
        isGrounded = grounded;
    }
}
