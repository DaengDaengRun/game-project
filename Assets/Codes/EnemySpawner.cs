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
    public BoxCollider2D mapBounds;          
    public float safeMargin = 1.0f;

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
        if (player == null || mapBounds == null)
        {
            Debug.LogWarning("플레이어나 맵 경계가 없습니다.");
            return;
        }

        Vector2 randomPosition;
        float distanceToPlayer;
        int attempts = 0;
        int maxAttempts = 50; // 무한 루프 방지

        do
        {
            randomPosition = GetRandomPositionInsideMap();
            distanceToPlayer = Vector2.Distance(randomPosition, player.position);

            attempts++;
            if (attempts >= maxAttempts)
            {
                Debug.LogError("⚠️ 적절한 적 생성 위치를 찾지 못했습니다! 설정을 확인하세요.");
                return;
            }

        } while (distanceToPlayer < minDistance || distanceToPlayer > maxDistance);

        // 적 생성
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        Debug.Log($"적 생성! 위치: {randomPosition}, 플레이어와 거리: {distanceToPlayer}");
    }

    // 맵 경계 안에서 랜덤 위치 반환
    Vector2 GetRandomPositionInsideMap()
    {
        Bounds bounds = mapBounds.bounds;

        float randomX = Random.Range(bounds.min.x + safeMargin, bounds.max.x - safeMargin);
        float randomY = Random.Range(bounds.min.y + safeMargin, bounds.max.y - safeMargin);

        return new Vector2(randomX, randomY);
    }
}
