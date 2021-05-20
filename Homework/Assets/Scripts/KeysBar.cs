using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysBar : MonoBehaviour {

    private int obtainedKeysCount;

    private RawImage[] keys = new RawImage[3];
    private Texture2D fullKeySprite;

    private void Start() {
        obtainedKeysCount = 0;
        fullKeySprite = Resources.Load<Texture2D>(GameConstants.FULL_KEY_SPRITE_NAME);

        HeroCollisions.OnKeyObtained += IncreaseObtainedKeys;

        int i = 0;
        foreach (Transform child in transform) {
            if (child.CompareTag(GameConstants.HERO_KEY_TAG)) {
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
