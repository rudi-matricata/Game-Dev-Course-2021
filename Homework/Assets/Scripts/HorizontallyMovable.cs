using System;
using UnityEngine;

public interface IHorizontallyMovable {

    bool ShouldMove();
}

class MovementUtils {

    public static bool Move(bool shouldMove, bool moveRight, Transform transform, float initialX, float moveAmplitude, float platformMoveSpeed) {
        if (shouldMove) { 
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
        return moveRight;
    }
}