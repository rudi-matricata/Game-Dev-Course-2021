using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private const string heroKeysTag = "HeroKeys";
    private const string heroHeartsTag = "HeroHearts";
    private const string fullKeySpriteName = "HudKeyFull";

    private int obtainedKeysCount;

    private readonly SpriteRenderer[] keys = new SpriteRenderer[3];
    private readonly Stack<GameObject> hearts = new Stack<GameObject>();
    private Sprite fullKeySprite;

    private void Start() {
        obtainedKeysCount = 0;
        fullKeySprite = Resources.Load<Sprite>(fullKeySpriteName);

        HeroCollisions.OnKeyObtained += IncreaseObtainedKeys;
        HeroCollisions.OnEnemyHit += LoseHeart;

        foreach (Transform child in transform) {
            if (child.CompareTag(heroKeysTag)) {
                int i = 0;
                foreach(Transform keyTransform in child.transform) {
                    keys[i++] = keyTransform.GetComponent<SpriteRenderer>();
                }
            } else if(child.CompareTag(heroHeartsTag)) {
                foreach (Transform heartTransform in child.transform) {
                    hearts.Push(heartTransform.gameObject);
                }
            }
        }
    }

    private void IncreaseObtainedKeys() {
        if(obtainedKeysCount >= 3) {
            return;
        }
        keys[obtainedKeysCount++].sprite = fullKeySprite;

        Debug.Log("Obtained keys: " + obtainedKeysCount);
    }

    private void LoseHeart() {
        if (hearts.Count > 0) {
            Destroy(hearts.Pop());
        } else {
            Debug.Log("No more hearts");
        }
    }
}
