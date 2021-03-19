using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private const int moveAmplitude = 2;

    [SerializeField]
    private int platformMoveSpeed = 3;
    
    private double initialX;
    private bool moveRight = true;

    private void Start() {
        initialX = transform.position.x;
    }

    private void Update() {
        if (transform.position.x - initialX > moveAmplitude) {
            moveRight = false;
        } else if (transform.position.x - initialX < -moveAmplitude) {
            moveRight = true;
        }

        if (moveRight) {
            transform.position = new Vector2(transform.position.x + (platformMoveSpeed * Time.deltaTime), transform.position.y);
        } else {
            transform.position = new Vector2(transform.position.x - (platformMoveSpeed * Time.deltaTime), transform.position.y);
        }
    }
}
