using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // List 사용을 위한 네임스페이스 추가

public class HomeSpawner : MonoBehaviour
{
    public GameObject homePrefab; // 집 프리팹
    public float minX = -10f, maxX = 10f;
    public float minY = -8f, maxY = 8f;
    public float minDistance = 5f;  // 최소 거리 조정 (너무 크지 않도록)
    public float maxDistance = 15f;  // 최대 거리 조정
    private static bool hasSpawned = false;
    private GameObject spawnedHome;
    public BoxCollider2D mapBounds;

    private Transform player; // 플레이어 위치
    public float safeMargin = 1.0f;
    public int currentStage = 3;  // 현재 스테이지


    // 스테이지 2에서 고정 위치로 생성할 뼈다구 위치
    private List<Vector3> stage2Positions = new List<Vector3>
    {
        new Vector3(7f, -11f, 0f),
        new Vector3(25f, -11f, 0f),
        new Vector3(-25f, -8f, 0f),
    };

    // 스테이지 3에서 고정 위치로 생성할 뼈다구 위치
    private List<Vector3> stage3Positions = new List<Vector3>
    {
        new Vector3(50f, 25f, 0f),
        new Vector3(50f, -30f, 0f),
        new Vector3(-32f, 24f, 0f)
    };

    void Awake()
    {
        currentStage = PlayerPrefs.GetInt("CurrentStage", 1); // 여기서 안전하게 불러옴
        // Debug.Log($"🎮 현재 스테이지: {currentStage}");

        if (hasSpawned)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        hasSpawned = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasSpawned = false; // 새로운 씬에서 다시 집 생성 가능하도록 설정
    }

    void Start()
    {
        // 플레이어 찾기
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            // Debug.LogError("⚠️ Player를 찾을 수 없습니다! Player 오브젝트의 태그를 'Player'로 설정하세요.");
            return;
        }

        if (homePrefab == null)
        {
            // Debug.LogError("⚠️ homePrefab이 설정되지 않았습니다!");
            return;
        }

        Vector3 spawnPosition = Vector3.zero;

        if (currentStage == 3)
        {
            Debug.Log("🏠 Stage 3: 집을 고정 위치에 생성합니다.");
            int randomIndex = Random.Range(0, stage3Positions.Count);
            spawnPosition = stage3Positions[randomIndex];
            Debug.Log($"🏠 Stage 3: 집 위치: {spawnPosition}");
            spawnedHome = Instantiate(homePrefab, spawnPosition, Quaternion.identity);
        }
        else if (currentStage == 2)
        {
            Debug.Log("🏠 Stage 2: 집을 고정 위치에 생성합니다.");
            int randomIndex = Random.Range(0, stage3Positions.Count);
            spawnPosition = stage2Positions[randomIndex];
            Debug.Log($"🏠 Stage 2: 집 위치: {spawnPosition}");
            spawnedHome = Instantiate(homePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            SpawnRandomHome();
        }

    }

    void SpawnRandomHome()
    {
        Vector2 randomPosition;
        int attempts = 0;
        int maxAttempts = 100; // 무한 루프 방지

        do
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // 360도 방향 중 랜덤 선택
            float distance = Random.Range(minDistance, maxDistance); // 거리 랜덤 설정

            // 방향과 거리 기반으로 위치 계산
            float randomX = player.position.x + Mathf.Cos(angle) * distance;
            float randomY = player.position.y + Mathf.Sin(angle) * distance;
            randomPosition = new Vector2(randomX, randomY);

            attempts++;

            if (attempts >= maxAttempts)
            {
                // Debug.LogError("⚠️ 적절한 집 위치를 찾지 못했습니다. min/max 설정을 확인하세요.");
                return;
            }

        } while (!IsInsideMap(randomPosition));

        // 집 생성
        spawnedHome = Instantiate(homePrefab, randomPosition, Quaternion.identity);
        spawnedHome.transform.position = randomPosition;
        spawnedHome.SetActive(true);

        // Debug.Log($"🏠 집 생성! 위치: {randomPosition}, 플레이어와 거리: {Vector2.Distance(player.position, randomPosition)}");
    }

    //  특정 위치가 맵 범위 안에 있는지 확인하는 함수
    bool IsInsideMap(Vector2 position)
    {
        Bounds bounds = mapBounds.bounds;

        return position.x >= bounds.min.x + safeMargin && position.x <= bounds.max.x - safeMargin &&
               position.y >= bounds.min.y + safeMargin && position.y <= bounds.max.y - safeMargin;
    }
}
