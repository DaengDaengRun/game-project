using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHomePrefabSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // home의 프리팹
    // 랜덤 좌표 설정
    public float minX = -5f, maxX = 5f;
    public float minY = -3f, maxY = 3f;

    private static bool hasSpawned = false; // 최초 1회만 실행되도록 설정


    void Awake()
    {
        if (hasSpawned)  // 이미 생성되었으면 삭제하여 중복 실행 방지
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);  // 이 오브젝트를 유지하여 한 번만 실행되도록 설정
        hasSpawned = true;  // 최초 실행 여부를 기록
    }

    void Start(){
        if (itemPrefab == null)
        {
            Debug.LogError("⚠️ itemPrefab이 설정되지 않았습니다! " +
                           "Inspector에서 itemPrefab 슬롯에 HomePrefab을 할당하세요.");
            return; 
        }

        Debug.Log("✅ RandomHomePrefabSpawner Start 실행됨");

        SpawnRandomSprite();

    }

    void SpawnRandomSprite()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 randomPosition = new Vector3(randomX, randomY, -5);
        Debug.Log($"📌 집 생성! 좌표: {randomX}, {randomY}");

        // Instantiate(itemPrefab, randomPosition, Quaternion.identity);
        GameObject newItem = Instantiate(itemPrefab, randomPosition, Quaternion.identity);

        newItem.transform.position = randomPosition;  // 위치 재설정
        newItem.SetActive(true);  // 활성화

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player와 충돌했을 때의 처리
            Debug.Log("찾았다!"); 
        }
    }
}
