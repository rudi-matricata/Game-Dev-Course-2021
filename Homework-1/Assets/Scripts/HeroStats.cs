using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private const string heroKeysTag = "HeroKeys";
    private const string fullKeySpriteName = "HudKeyFull";

    private int obtainedKeysCount;

    private readonly SpriteRenderer[] keys = new SpriteRenderer[3];

    private void Start() {
        obtainedKeysCount = 0;

        HeroCollisions.OnKeyObtained += IncreaseObtainedKeys;

        foreach (Transform child in transform) {
            if (child.CompareTag(heroKeysTag)) {
                int i = 0;
                foreach(Transform keyTransform in child.transform) {
                    keys[i++] = keyTransform.GetComponent<SpriteRenderer>();
                }
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
}
