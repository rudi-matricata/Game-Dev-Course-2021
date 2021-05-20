using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private float bulletSpeed = 10;

    void Start() {
        rigidBody.velocity = transform.right * bulletSpeed;
        StartCoroutine(BulletCleanup(10));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.GetHit();
            Destroy(gameObject);
        }
    }

    IEnumerator BulletCleanup(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
