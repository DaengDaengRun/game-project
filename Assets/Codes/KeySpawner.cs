using System.Collections;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject itemPrefab; // 열쇠 프리팹
    public float minX = -10f, maxX = 10f;
    public float minY = -8f, maxY = 8f;
    public float minDistance = 8f;  // 🔥 플레이어와 최소 거리 (이 값을 반드시 유지)
    public float maxDistance = 17f;  // 🔥 플레이어와 최대 거리
    private static bool hasSpawned = false;
    private GameObject spawnedKey;
    public BoxCollider2D mapBounds;

    private Transform player; // 🔥 플레이어 위치

    void Awake()
    {
        if (hasSpawned)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        hasSpawned = true;
    }

    void Start()
    {
        // 🔥 플레이어 찾기
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("⚠️ Player를 찾을 수 없습니다! Player 오브젝트의 태그를 'Player'로 설정하세요.");
            return;
        }

        if (itemPrefab == null)
        {
            Debug.LogError("⚠️ itemPrefab이 설정되지 않았습니다!");
            return;
        }

        SpawnRandomKey();
    }

    void SpawnRandomKey()
    {
        Vector2 randomPosition;
        int attempts = 0;
        int maxAttempts = 100; // 🔥 무한 루프 방지

        do
        {
            // 🔥 플레이어의 위치를 기준으로 일정 거리 이상 떨어진 위치를 찾음
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // 🔥 360도 방향 중 랜덤 선택
            float distance = Random.Range(minDistance, maxDistance); // 🔥 거리도 랜덤 (minDistance 이상)

            // 🔥 방향과 거리 기반으로 위치 계산
            float randomX = player.position.x + Mathf.Cos(angle) * distance;
            float randomY = player.position.y + Mathf.Sin(angle) * distance;
            randomPosition = new Vector2(randomX, randomY);

            attempts++;

            if (attempts >= maxAttempts)
            {
                Debug.LogError("⚠️ 적절한 열쇠 위치를 찾지 못했습니다. min/max 설정을 확인하세요.");
                return;
            }

        } while (!IsInsideMap(randomPosition));

        // 🔥 열쇠 생성
        spawnedKey = Instantiate(itemPrefab, randomPosition, Quaternion.identity);
        spawnedKey.transform.position = randomPosition;
        spawnedKey.SetActive(true);

        Debug.Log($"📌 열쇠 생성! 위치: {randomPosition}, 플레이어와 거리: {Vector2.Distance(player.position, randomPosition)}");

         ArrowIndicator arrowIndicator = FindFirstObjectByType<ArrowIndicator>();
         arrowIndicator.SetBoneTarget(spawnedKey.transform);
    }
    
    // ✅ 특정 위치가 맵 범위 안에 있는지 확인하는 함수
    bool IsInsideMap(Vector2 position)
    {
        Bounds bounds = mapBounds.bounds;
        return position.x >= bounds.min.x && position.x <= bounds.max.x &&
               position.y >= bounds.min.y && position.y <= bounds.max.y;
    }
}
