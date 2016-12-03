using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatSpawner : MonoBehaviour {
    public float sweatSpawnTime = 1.0f;
    public SweatDrop sweatPrefab;

    BoxCollider2D sweatSpawnArea = null;
    float sweatSpawnTimer = 0.0f;

	void Start () {
        sweatSpawnArea = GetComponent<BoxCollider2D>();
	}

	void Update () {
        sweatSpawnTimer += Time.deltaTime;
        if (sweatSpawnTimer >= sweatSpawnTime) {
            SpawnDrop();
            sweatSpawnTimer = 0.0f;
        }
	}

    void SpawnDrop() {
        Vector2 spawnPosition;
        spawnPosition.x = Random.Range(sweatSpawnArea.transform.localPosition.x - sweatSpawnArea.size.x / 2.0f,
            sweatSpawnArea.transform.localPosition.x + sweatSpawnArea.size.x / 2.0f);
        spawnPosition.y = Random.Range(sweatSpawnArea.transform.localPosition.y - sweatSpawnArea.size.y / 2.0f,
            sweatSpawnArea.transform.localPosition.y + sweatSpawnArea.size.y / 2.0f);

        Instantiate(sweatPrefab, spawnPosition, Quaternion.identity, this.transform.parent);
    }
}
