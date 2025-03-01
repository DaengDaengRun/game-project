using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 생성할 적 프리팹
    public Transform player; // 플레이어 참조
    public float spawnInterval = 3f; // 적 생성 간격
    public float spawnRadius = 10f; // 플레이어 기준 적 생성 범위

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        // 랜덤 위치 계산 (플레이어 기준 일정 거리 밖)
        Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
        Vector2 spawnPosition = (Vector2)player.position + randomDirection;

        // 적 생성
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
