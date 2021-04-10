using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollisions : MonoBehaviour
{
    public delegate void GroundHit(bool grounded);
    public static event GroundHit OnGroundHit;

    public delegate void ObtainKey();
    public static event ObtainKey OnKeyObtained;

    private const string platformTag = "Platform";
    private const string trampolineTag = "Trampoline";
    private const string keyTag = "Key";

    [SerializeField]
    private int trampolineForceFactor;

    private Rigidbody2D rigidBody;

    private void Start() {
        trampolineForceFactor = 13;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
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
            OnGroundHit?.Invoke(grounded);
        }
    }
}
