using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // 플레이어 프리팹
    public BoxCollider2D mapBounds; // 맵 경계 (BoxCollider2D)
    public float safeMargin = 1.0f; // 벽과의 최소 거리

    void Start()
    {
        if (mapBounds == null)
        {
            // Debug.LogError("⚠ MapBounds가 설정되지 않았습니다!");
            return;
        }

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Vector2 spawnPosition = GetRandomPositionInsideMap();
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        // Debug.Log($"🎮 플레이어 스폰 위치: {spawnPosition}");
    }

    // 맵 경계 내부에서 랜덤 위치 반환
    Vector2 GetRandomPositionInsideMap()
    {
        Bounds bounds = mapBounds.bounds;

        float randomX = Random.Range(bounds.min.x + safeMargin, bounds.max.x - safeMargin);
        float randomY = Random.Range(bounds.min.y + safeMargin, bounds.max.y - safeMargin);

        return new Vector2(randomX, randomY);
    }
}
