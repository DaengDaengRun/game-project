using System.Collections;
using UnityEngine;


public class KeySpawner : MonoBehaviour
{
    public GameObject itemPrefab; // 열쇠 프리팹
    public float minX = -5f, maxX = 5f;
    public float minY = -3f, maxY = 3f;
    public float minDistance = 3f;  // 🔥 플레이어와 최소 거리
    public float maxDistance = 17f;  // 🔥 플레이어와 최대 거리
    private static bool hasSpawned = false;
    private GameObject spawnedKey;

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
        // 플레이어 찾기
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
    float distanceToPlayer;

    // 🔥 적절한 위치를 찾을 때까지 반복
    do
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        randomPosition = new Vector2(randomX, randomY);
        distanceToPlayer = Vector2.Distance(randomPosition, player.position);

    } while (distanceToPlayer < minDistance || distanceToPlayer > maxDistance);

    // 🔥 열쇠 생성
    spawnedKey = Instantiate(itemPrefab, randomPosition, Quaternion.identity);
    spawnedKey.transform.position = randomPosition;
    spawnedKey.SetActive(true);

    Debug.Log($"📌 열쇠 생성! 위치: {randomPosition}, 플레이어와 거리: {distanceToPlayer}");


}


}
