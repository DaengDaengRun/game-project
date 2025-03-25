using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeySpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float minDistance = 10f;  // 플레이어와 최소 거리
    public float maxDistance = 30f;  // 플레이어와 최대 거리
    public float minDistanceFromHome = 15f; // 홈으로부터 최소 거리
    public BoxCollider2D mapBounds;
    public float safeMargin = 1.0f;

    private Transform player;
    private Transform home;
    private GameObject spawnedKey;
    private static bool hasSpawned = false;
    public int currentStage = 3;  // 현재 스테이지


    void Awake()
    {
        currentStage = PlayerPrefs.GetInt("CurrentStage", 1); // 여기서 안전하게 불러옴
        Debug.Log($"🎮 현재 스테이지: {currentStage}");

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
        hasSpawned = false; // 씬 전환 시 다시 스폰 가능하게 초기화
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            // Debug.LogError("Player를 찾을 수 없습니다");
            return;
        }
        GameObject homeObj = GameObject.FindGameObjectWithTag("Home");
        if (homeObj != null)
        {
            home = homeObj.transform;
        }
        else
        {
            // Debug.LogError("Home을 찾을 수 없습니다");
            return;
        }
        if (itemPrefab == null)
        {
            // Debug.LogError("itemPrefab이 설정되지 않았습니다");
            return;
        }

        // 맵 크기에 따라 minDistanceFromHome 자동 조정
        Bounds bounds = mapBounds.bounds;
        float maxMapDistance = Vector2.Distance(
            new Vector2(bounds.min.x, bounds.min.y),
            new Vector2(bounds.max.x, bounds.max.y)
        );

        minDistanceFromHome = Mathf.Clamp(minDistanceFromHome, 0, maxMapDistance / 2f);
        // Debug.Log($"minDistanceFromHome이 자동 조정되었습니다: {minDistanceFromHome}");

        // maxDistance 자동 재조정
        float playerToHomeDistance = Vector2.Distance(player.position, home.position);
        float minMaxDistance = minDistanceFromHome + playerToHomeDistance + 1f;
        if (maxDistance < minMaxDistance)
        {
            // Debug.LogWarning($"maxDistance가 너무 작습니다! 자동으로 {minMaxDistance}로 조정됩니다.");
            maxDistance = minMaxDistance;
        }

        Vector3 spawnPosition = Vector3.zero;

        if (currentStage == 3)
        {
            Debug.Log("🦴 Stage 3: 집을 고정 위치에 생성합니다.");
            spawnPosition = new Vector3(-33f, -20f, 0f); // 고정 좌표
            Debug.Log($"🦴 Stage 3: 뼈다구 위치: {spawnPosition}");
            spawnedKey = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            SpawnRandomKey();
        }

    }

    void SpawnRandomKey()
    {
        Vector2 randomPosition;
        int attempts = 0;
        int maxAttempts = 500; // 무한 루프 방지용 최대 시도 횟수

        if (player == null || home == null || itemPrefab == null)
        {
            // Debug.LogError("필수 객체가 없습니다");
            return;
        }

        do
        {
            // 플레이어 기준으로 일정 거리 떨어진 랜덤 위치 계산
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(minDistance, maxDistance);

            float randomX = player.position.x + Mathf.Cos(angle) * distance;
            float randomY = player.position.y + Mathf.Sin(angle) * distance;
            randomPosition = new Vector2(randomX, randomY);

            attempts++;

            float distanceToHome = Vector2.Distance(randomPosition, home.position);
            bool insideMap = IsInsideMap(randomPosition);
            bool farEnoughFromHome = distanceToHome >= minDistanceFromHome;

            // Debug.Log($"▶시도 {attempts}: 위치 {randomPosition}, home까지 거리 {distanceToHome:F2}, 맵 내부: {insideMap}, home에서 충분히 멀리: {farEnoughFromHome}");

            if (attempts >= maxAttempts)
            {
                // Debug.LogError($"⚠️ {maxAttempts}번 시도 후 실패! minDistanceFromHome: {minDistanceFromHome}, 플레이어 기준 거리: {minDistance}/{maxDistance}");
                return;
            }

        } while (!(IsInsideMap(randomPosition) && IsFarEnoughFromHome(randomPosition)));

        // 열쇠 생성 완료
        spawnedKey = Instantiate(itemPrefab, randomPosition, Quaternion.identity);
        spawnedKey.transform.position = randomPosition;
        spawnedKey.SetActive(true);

        // Debug.Log($"📌 열쇠 생성 완료! 위치: {randomPosition}, 플레이어 거리: {Vector2.Distance(player.position, randomPosition):F2}, home 거리: {Vector2.Distance(home.position, randomPosition):F2}");

        // 화살표 표시기 세팅
        ArrowIndicator arrowIndicator = FindFirstObjectByType<ArrowIndicator>();
        if (arrowIndicator != null)
        {
            arrowIndicator.SetBoneTarget(spawnedKey.transform);
        }
        else
        {
            // Debug.LogWarning("⚠️ ArrowIndicator가 없습니다.");
        }
    }

    bool IsInsideMap(Vector2 position)
    {
        Bounds bounds = mapBounds.bounds;
        return position.x >= bounds.min.x + safeMargin && position.x <= bounds.max.x - safeMargin &&
               position.y >= bounds.min.y + safeMargin && position.y <= bounds.max.y - safeMargin;
    }

    bool IsFarEnoughFromHome(Vector2 position)
    {
        if (home == null) return true;

        float distanceToHome = Vector2.Distance(position, home.position);
        return distanceToHome >= minDistanceFromHome;
    }
}
