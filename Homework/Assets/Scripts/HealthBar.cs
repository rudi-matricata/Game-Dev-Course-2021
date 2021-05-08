using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private const string heartTag = "Heart";

    private readonly Stack<GameObject> hearts = new Stack<GameObject>();

    void Start() {
        foreach (Transform child in transform) {
            if (child.CompareTag(heartTag)) {
                hearts.Push(child.gameObject);
            }
        }
        HeroCollisions.OnEnemyHit += LoseHeart;
    }

    private void OnDestroy() {
        HeroCollisions.OnEnemyHit -= LoseHeart;
    }

    private void LoseHeart() {
        if (hearts.Count > 0) {
            Destroy(hearts.Pop());
        } else {
            Debug.Log("No more hearts");
        }
    }
}
