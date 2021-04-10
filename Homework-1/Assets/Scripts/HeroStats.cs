using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private const string heroKeysTag = "HeroKeys";
    private const string fullKeySpriteName = "HudKeyFull";

    private int obtainedKeysCount;
    private int heartsCount;

    private readonly SpriteRenderer[] keys = new SpriteRenderer[3];
    private readonly Stack<GameObject> hearts = new Stack<GameObject>();

    private void Start() {
        obtainedKeysCount = 0;

        HeroCollisions.OnKeyObtained += IncreaseObtainedKeys;
        HeroCollisions.OnEnemyHit += LoseHeart;

        foreach (Transform child in transform) {
            if (child.CompareTag(heroKeysTag)) {
                int i = 0;
                foreach(Transform keyTransform in child.transform) {
                    keys[i++] = keyTransform.GetComponent<SpriteRenderer>();
                }
            } else if(child.CompareTag("HeroHearts")) {
                foreach (Transform heartTransform in child.transform) {
                    hearts.Push(heartTransform.gameObject);
                }
                heartsCount = hearts.Count;
            }

        }
    }

    private void IncreaseObtainedKeys() {
        if(obtainedKeysCount >= 3) {
            return;
        }
        keys[obtainedKeysCount++].sprite = Resources.Load<Sprite>(fullKeySpriteName);

        Debug.Log("Obtained keys: " + obtainedKeysCount);
    }

    private void LoseHeart() {
        heartsCount--;
        Destroy(hearts.Pop());
    }
}
