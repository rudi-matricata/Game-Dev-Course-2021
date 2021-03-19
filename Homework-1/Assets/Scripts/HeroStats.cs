using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private int obtainedKeysCount;

    private void Start() {
        obtainedKeysCount = 0;

        Hero.OnKeyObtained += increaseObtainedKeys;
    }

    private void increaseObtainedKeys() {
        obtainedKeysCount++;
        Debug.Log("Obtained keys: " + obtainedKeysCount);
    }
}
