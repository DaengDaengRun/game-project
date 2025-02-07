using UnityEngine;

public class ChestnutEnemySpawner : MonoBehaviour
{
    public GameObject wanderingEnemyPrefab; // 새로운 적 프리팹
    public Transform player; // 플레이어 위치
    public float spawnRadius = 5f; // 플레이어 주변에서 생성될 거리
    public float spawnInterval = 3f; // 적 생성 간격

    void Start()
    {
        InvokeRepeating(nameof(SpawnWanderingEnemy), 1f, spawnInterval);
    }

    void SpawnWanderingEnemy()
    {
        if (player == null) return;

        // 🔥 플레이어 주변 랜덤 위치 계산
        Vector2 randomPosition = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
        
        // 🔥 새로운 적 생성
        Instantiate(wanderingEnemyPrefab, randomPosition, Quaternion.identity);
        Debug.Log($"👹 새로운 랜덤 적 생성! 위치: {randomPosition}");
    }
}
