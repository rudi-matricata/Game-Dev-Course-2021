using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private const string heartTag = "Heart";

    public Stack<GameObject> Hearts { get; } = new Stack<GameObject>();

    public HealthBar() {
        HeroCollisions.OnEnemyHit += LoseHeart;
    }

    private void Start() {
        foreach (Transform child in transform) {
            if (child.CompareTag(heartTag)) {
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
