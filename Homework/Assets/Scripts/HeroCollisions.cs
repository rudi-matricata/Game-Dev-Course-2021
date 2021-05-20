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

    [SerializeField]
    private int trampolineForceFactor;

    private Rigidbody2D rigidBody;

    private void Start() {
        trampolineForceFactor = 7;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(GameConstants.PLATFORM_TAG)) {
            transform.parent = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag(GameConstants.PLATFORM_TAG)) {
            transform.parent = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag(GameConstants.TRAMPOLINE_TAG)) {
            foreach (ContactPoint2D hitPos in collision.contacts) {
                if (hitPos.normal.y > 0) {
                    rigidBody.AddForce(Vector2.up * trampolineForceFactor, ForceMode2D.Impulse);
                }
            }
        }
        if (collisionObject.CompareTag(GameConstants.KEY_TAG)) {
            OnKey(collision);
        }
        if (collisionObject.CompareTag(GameConstants.ENEMY_TAG)) {
            OnEnemy();
        }
        if (collision.gameObject.CompareTag(GameConstants.PLATFORM_TAG)) {
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
        if (collision.gameObject.CompareTag(GameConstants.PLATFORM_TAG)) {
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
        if (collision.gameObject.CompareTag(GameConstants.PLATFORM_TAG)) {
            SetGrounded(collision, false);
        }
    }

    private void SetGrounded(Collision2D collision, bool grounded) {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag(GameConstants.PLATFORM_TAG) || collisionObject.CompareTag(GameConstants.TRAMPOLINE_TAG)) {
            OnGroundHit?.Invoke(grounded);
        }
    }
}
