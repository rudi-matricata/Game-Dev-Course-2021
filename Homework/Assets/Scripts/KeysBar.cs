using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysBar : MonoBehaviour {

    private const string heroKeyTag = "HeroKey";
    private const string fullKeySpriteName = "HudKeyFull";

    private int obtainedKeysCount;

    private RawImage[] keys = new RawImage[3];
    private Texture2D fullKeySprite;

    private void Start() {
        obtainedKeysCount = 0;
        fullKeySprite = Resources.Load<Texture2D>(fullKeySpriteName);

        HeroCollisions.OnKeyObtained += IncreaseObtainedKeys;

        int i = 0;
        foreach (Transform child in transform) {
            if (child.CompareTag(heroKeyTag)) {
                keys[i++] = child.GetComponent<RawImage>();
            }
        }
    }

    private void OnDestroy() {
        HeroCollisions.OnKeyObtained -= IncreaseObtainedKeys;
    }

    private void IncreaseObtainedKeys() {
        if (obtainedKeysCount >= 3) {
            return;
        }
        keys[obtainedKeysCount++].texture = fullKeySprite;

        Debug.Log("Obtained keys: " + obtainedKeysCount);
    }
}
