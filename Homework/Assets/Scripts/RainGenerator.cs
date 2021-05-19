using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGenerator : MonoBehaviour
{

    private ParticleSystem ps;

    private float nextRainEventTime;
    private float timeDuration;

    [SerializeField]
    private float maxTimeInSeconds;
    [SerializeField]
    private float minTimeInSeconds;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.enableEmission= true;

        timeDuration = 0f;
        maxTimeInSeconds = 60f;
        minTimeInSeconds = 45f;
        setNextRainEvent();
    }

    void Update()
    {
        timeDuration += Time.deltaTime;

        if(timeDuration >= nextRainEventTime) {
            timeDuration = 0f;
            setNextRainEvent();

            ps.enableEmission = !ps.enableEmission;
        }
    }

    private void setNextRainEvent() {
        nextRainEventTime = Random.Range(minTimeInSeconds, maxTimeInSeconds);
    }
}
