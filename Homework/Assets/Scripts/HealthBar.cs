using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Stack<GameObject> Hearts { get; } = new Stack<GameObject>();

    public HealthBar() {
        HeroCollisions.OnEnemyHit += LoseHeart;
    }

    private void Start() {
        foreach (Transform child in transform) {
            if (child.CompareTag(GameConstants.HEART_TAG)) {
                Hearts.Push(child.gameObject);
            }
        }
    }

    private void OnDestroy() {
        HeroCollisions.OnEnemyHit -= LoseHeart;
    }

    private void LoseHeart() {
        if (Hearts.Count > 0) {
            Destroy(Hearts.Pop());
        } else {
            Debug.Log("No more hearts");
        }
    }
}
