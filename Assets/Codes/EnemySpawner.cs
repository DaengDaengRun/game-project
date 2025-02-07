using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 생성할 적 프리팹
    public Transform player; // 플레이어 위치
    public float spawnInterval = 3f; // 적 생성 간격
    public float minDistance = 3f; // 플레이어와 최소 거리
    public float maxDistance = 10f; // 플레이어와 최대 거리
    public float spawnAreaSize = 15f; // 스폰 영역 크기

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

        Vector2 randomPosition;
        float distanceToPlayer;

        // 적절한 위치를 찾을 때까지 반복
        do
        {
            float randomX = Random.Range(-spawnAreaSize, spawnAreaSize);
            float randomY = Random.Range(-spawnAreaSize, spawnAreaSize);
            randomPosition = new Vector2(randomX, randomY);
            distanceToPlayer = Vector2.Distance(randomPosition, player.position);

        } while (distanceToPlayer < minDistance || distanceToPlayer > maxDistance);

        // 적 생성
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        Debug.Log($"👹 적 생성! 위치: {randomPosition}, 플레이어와 거리: {distanceToPlayer}");
    }
}
