using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatSpawner : MonoBehaviour {
    public float minSweatSpawnTime = 3.0f;
    public float maxSweatSpawnTime = 0.1f;
    public SweatDrop[] sweatPrefabs;

    public MadLibManager manager;

    BoxCollider2D sweatSpawnArea = null;
    float sweatSpawnTimer = 0.0f;

	void Start () {
        sweatSpawnArea = GetComponent<BoxCollider2D>();
	}

	void Update () {
        float interp = manager.sweatLevel / 100.0f;
        float sweatSpawnTime = minSweatSpawnTime + (interp * (maxSweatSpawnTime - minSweatSpawnTime));

        sweatSpawnTimer += Time.deltaTime;
        if (sweatSpawnTimer >= sweatSpawnTime) {
            SpawnDrop();
            sweatSpawnTimer -= sweatSpawnTime;
        }
	}

    void SpawnDrop() {
        Vector3 spawnPosition;
        spawnPosition.x = Random.Range(sweatSpawnArea.transform.localPosition.x - sweatSpawnArea.size.x / 2.0f,
            sweatSpawnArea.transform.localPosition.x + sweatSpawnArea.size.x / 2.0f);
        spawnPosition.y = Random.Range(sweatSpawnArea.transform.localPosition.y - sweatSpawnArea.size.y / 2.0f,
            sweatSpawnArea.transform.localPosition.y + sweatSpawnArea.size.y / 2.0f);
        spawnPosition.z = 0.0f;

        SweatDrop prefab = sweatPrefabs[Random.Range(0,sweatPrefabs.Length)];
        SweatDrop newInstance = Instantiate(prefab, spawnPosition, Quaternion.identity, this.transform);
        newInstance.transform.localPosition = spawnPosition;
    }
}
