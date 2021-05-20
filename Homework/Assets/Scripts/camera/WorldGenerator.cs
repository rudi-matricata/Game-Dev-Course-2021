using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject platformPrefab;
    [SerializeField]
    private GameObject movingPlatformPrefab;
    [SerializeField]
    private GameObject platformWithKeyPrefab;
    [SerializeField]
    private GameObject platformWithTrampolinePrefab;
    [SerializeField]
    private GameObject platformWithEnemy;

    public List<GameObject> prefabs = new List<GameObject>();

    private bool generate = false;

    void Start()
    {
        prefabs.Add(platformPrefab);
        prefabs.Add(movingPlatformPrefab);
        prefabs.Add(platformWithKeyPrefab);
        prefabs.Add(platformWithTrampolinePrefab);
        prefabs.Add(platformWithEnemy);

        StartCoroutine(DoGeneration());
    }

    void Update() {
        Debug.Log(generate);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 5);
        foreach (var hitCollider in hitColliders) {
            if(!hitCollider.gameObject.CompareTag(GameConstants.PLAYER_TAG)) {
                if(hitCollider.gameObject.transform.position.y > transform.position.y) {
                    generate = false;
                    return;
                }
            }
        }
        generate = true;
    }

    IEnumerator DoGeneration() {
        while(true) {
            if(generate) {
                float randomX = Random.Range(transform.position.x - 6, transform.position.x + 6);
                float randomY = Random.Range(transform.position.y + 2 , transform.position.y + 3);
                Vector2 spawnPosition = new Vector3(randomX, randomY, 0);
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(spawnPosition, 1f);
                foreach (var hitCollider in hitColliders) {
                    if (!hitCollider.gameObject.CompareTag(GameConstants.PLAYER_TAG)) {
                       goto afterInstantiation; 
                    }
                }
                Instantiate(prefabs[Random.Range(0, prefabs.Count)], spawnPosition, Quaternion.identity);


                afterInstantiation: { }
            }

            yield return new WaitForSeconds(2);
        }
    }
}
