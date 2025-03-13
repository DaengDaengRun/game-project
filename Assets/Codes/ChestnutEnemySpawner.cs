using UnityEngine;

public class ChestnutEnemySpawner : MonoBehaviour
{
    public GameObject wanderingEnemyPrefab; // 새로운 적 프리팹
    public Transform player; // 플레이어 위치
    public float spawnRadius = 5f; // 플레이어 주변에서 생성될 거리
    public float spawnInterval = 3f; // 적 생성 간격
    public BoxCollider2D mapBounds;          
    public float safeMargin = 1.0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnWanderingEnemy), 1f, spawnInterval);
    }

    void SpawnWanderingEnemy()
    {
        if (player == null || mapBounds == null)
        {
            Debug.LogWarning("⚠️ 플레이어나 맵 경계가 설정되지 않았습니다!");
            return;
        }

        Vector2 randomPosition;
        int attempts = 0;
        int maxAttempts = 50;  // 무한 루프 방지용

        do
        {
            // 플레이어 주변에서 랜덤 위치 생성
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            randomPosition = (Vector2)player.position + randomOffset;

            attempts++;
            if (attempts >= maxAttempts)
            {
                Debug.LogError("적절한 적 생성 위치를 찾지 못했습니다. 설정을 확인하세요.");
                return;
            }

        } while (!IsInsideMap(randomPosition));

        // 적 생성
        Instantiate(wanderingEnemyPrefab, randomPosition, Quaternion.identity);
        Debug.Log($"새로운 랜덤 적 생성! 위치: {randomPosition}");
    }

    // 울타리 내부에 있는지 체크하는 함수
    bool IsInsideMap(Vector2 position)
    {
        Bounds bounds = mapBounds.bounds;

        return position.x >= bounds.min.x + safeMargin &&
               position.x <= bounds.max.x - safeMargin &&
               position.y >= bounds.min.y + safeMargin &&
               position.y <= bounds.max.y - safeMargin;
    }
}
