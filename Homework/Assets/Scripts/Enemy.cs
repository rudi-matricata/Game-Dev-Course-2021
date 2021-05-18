using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHorizontallyMovable {

    private const float moveAmplitude = 1f;

    [SerializeField]
    private float platformMoveSpeed = 1f;

    private float initialX;
    private bool moveRight = true;

    [SerializeField]
    private int health = 3;

    private GameObject player;

    private Rigidbody2D rigidBody;

    void Start()
    {
        initialX = transform.position.x;

        player = GameObject.FindGameObjectWithTag("Player");

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
    }

    void Update()
    {
        moveRight = MovementUtils.Move(ShouldMove(), moveRight, transform, initialX, moveAmplitude, platformMoveSpeed);
    }

    public bool ShouldMove() {
        return Vector2.Distance(player.transform.position, gameObject.transform.position) < 8f;
    }

    public void GetHit() {
        if(--health == 0) {
            Destroy(gameObject);
        }
    }
}
