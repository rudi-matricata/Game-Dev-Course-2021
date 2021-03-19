using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour {

    public delegate void ObtainKey();
    public static event ObtainKey OnKeyObtained;

    private const string platformTag = "Platform";
    private const string trampolineTag = "Trampoline";
    private const string keyTag = "Key";

    [SerializeField]
    private int minYLimitBeforeRespawn;
    [SerializeField]
    private int trampolineForceFactor;
    [SerializeField]
    private int forceFactor;
    [SerializeField]
    private int moveSpeed;

    private Vector2 respawnVector;
    private bool isGrounded;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        minYLimitBeforeRespawn = -20;
        trampolineForceFactor = 13;
        forceFactor = 10;
        moveSpeed = 3;
        isGrounded = false;

        respawnVector = new Vector2(3, -8);

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void FixedUpdate() {
        if (transform.position.y < minYLimitBeforeRespawn) {
            transform.position = respawnVector;
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(platformTag)) {
            transform.parent = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag(platformTag)) {
            transform.parent = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag(trampolineTag)) {
            rigidBody.AddForce(Vector2.up * trampolineForceFactor, ForceMode2D.Impulse);
        }
        if (collisionObject.CompareTag(keyTag)) {
            OnKeyObtained?.Invoke();
            Destroy(collision.gameObject);
        }
        SetGrounded(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        SetGrounded(collision, false);
    }

    private void SetGrounded(Collision2D collision, bool grounded) {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag(platformTag) || collisionObject.CompareTag(trampolineTag)) {
            isGrounded = grounded;
        }
    }
}
