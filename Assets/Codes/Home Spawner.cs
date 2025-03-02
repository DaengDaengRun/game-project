using System.Collections;
using UnityEngine;

public class HomeSpawner : MonoBehaviour
{
    public GameObject homePrefab; // 🏠 집 프리팹
    public float minX = -10f, maxX = 10f;
    public float minY = -8f, maxY = 8f;
    public float minDistance = 5f;  // 🔥 최소 거리 조정 (너무 크지 않도록)
    public float maxDistance = 15f;  // 🔥 최대 거리 조정
    private static bool hasSpawned = false;
    private GameObject spawnedHome;

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

        if (homePrefab == null)
        {
            Debug.LogError("⚠️ homePrefab이 설정되지 않았습니다!");
            return;
        }

        SpawnRandomHome();
    }

    void SpawnRandomHome()
    {
        Vector2 randomPosition;
        int attempts = 0;
        int maxAttempts = 100; // 🔥 무한 루프 방지

        do
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // 🔥 360도 방향 중 랜덤 선택
            float distance = Random.Range(minDistance, maxDistance); // 🔥 거리 랜덤 설정

            // 🔥 방향과 거리 기반으로 위치 계산
            float randomX = player.position.x + Mathf.Cos(angle) * distance;
            float randomY = player.position.y + Mathf.Sin(angle) * distance;
            randomPosition = new Vector2(randomX, randomY);

            attempts++;

            if (attempts >= maxAttempts)
            {
                Debug.LogError("⚠️ 적절한 집 위치를 찾지 못했습니다. min/max 설정을 확인하세요.");
                return;
            }

        } while (randomPosition.x < minX || randomPosition.x > maxX || randomPosition.y < minY || randomPosition.y > maxY);

        // 🔥 집 생성
        spawnedHome = Instantiate(homePrefab, randomPosition, Quaternion.identity);
        spawnedHome.transform.position = randomPosition;
        spawnedHome.SetActive(true);

        Debug.Log($"🏠 집 생성! 위치: {randomPosition}, 플레이어와 거리: {Vector2.Distance(player.position, randomPosition)}");
    }
}
