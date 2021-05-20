using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCollisions : MonoBehaviour {

    public delegate void GroundHit(bool grounded);
    public static event GroundHit OnGroundHit;

    public delegate void EnemyHit();
    public static event EnemyHit OnEnemyHit;

    public delegate void ObtainKey();
    public static event ObtainKey OnKeyObtained;

    private const string platformTag = "Platform";
    private const string trampolineTag = "Trampoline";
    private const string keyTag = "Key";
    private const string enemyTag = "Enemy";

    [SerializeField]
    private int trampolineForceFactor;

    private Rigidbody2D rigidBody;

    private void Start() {
        trampolineForceFactor = 7;

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
            foreach (ContactPoint2D hitPos in collision.contacts) {
                if (hitPos.normal.y > 0) {
                    rigidBody.AddForce(Vector2.up * trampolineForceFactor, ForceMode2D.Impulse);
                }
            }
        }
        if (collisionObject.CompareTag(keyTag)) {
            OnKey(collision);
        }
        if (collisionObject.CompareTag(enemyTag)) {
            OnEnemy();
        }
        if (collision.gameObject.CompareTag(platformTag)) {
            SetGrounded(collision, true);
        }
    }

    private void OnKey(Collision2D collision) {
        OnKeyObtained?.Invoke();
        Destroy(collision.gameObject);
    }

    public void OnEnemy() {
        OnEnemyHit?.Invoke();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(platformTag)) {
            foreach (ContactPoint2D hitPos in collision.contacts) {
                if (hitPos.normal.x != 0) {
                    SetGrounded(collision, false);
                } else if (hitPos.normal.y > 0) {
                    SetGrounded(collision, true);
                } else {
                    SetGrounded(collision, false);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(platformTag)) {
            SetGrounded(collision, false);
        }
    }

    private void SetGrounded(Collision2D collision, bool grounded) {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag(platformTag) || collisionObject.CompareTag(trampolineTag)) {
            OnGroundHit?.Invoke(grounded);
        }
    }
}
