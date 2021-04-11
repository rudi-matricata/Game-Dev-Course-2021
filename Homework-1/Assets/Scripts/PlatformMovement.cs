using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour, IHorizontallyMovable {

    private const int moveAmplitude = 2;

    [SerializeField]
    private int platformMoveSpeed = 3;
    
    private float initialX;
    private bool moveRight = true;

    private void Start() {
        initialX = transform.position.x;
    }

    private void Update() {
        moveRight = MovementUtils.Move(ShouldMove(), moveRight,transform, initialX, moveAmplitude, platformMoveSpeed);
    }

    public bool ShouldMove() {
        return true;
    }
}
