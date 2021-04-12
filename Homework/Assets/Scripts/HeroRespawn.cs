using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRespawn : MonoBehaviour
{
    [SerializeField]
    private int minYLimitBeforeRespawn;

    private Vector2 respawnVector;
    private Rigidbody2D rigidBody;

    void Start()
    {
        minYLimitBeforeRespawn = -20;
        respawnVector = new Vector2(transform.position.x, transform.position.y);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (transform.position.y < minYLimitBeforeRespawn) {
            transform.position = respawnVector;
            rigidBody.velocity = Vector2.zero;
        }
    }
}
