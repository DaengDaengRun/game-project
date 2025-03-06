using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HPManager : MonoBehaviour
{
    public GameObject hpPrefab;  // HP 아이콘 프리팹 (hp.png 설정)
    public Transform hpContainer; // HP 아이콘을 배치할 부모 오브젝트
    private int currentHP = 3; // 초기 목숨 수
    private List<GameObject> hpIcons = new List<GameObject>(); // HP 아이콘 리스트

    private static HPManager instance; // 싱글턴 패턴 적용

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateHPUI(); // HP 아이콘 생성
    }

    // 🔹 HP 감소 함수 (Player에서 호출)
    public void DecreaseHP()
    {
        if (currentHP > 0)
        {
            currentHP--; // HP 감소
            UpdateHPUI(); // HP 아이콘 업데이트
        }

        if (currentHP <= 0)
        {
            Debug.Log("💀 HP 0 → 게임 종료!");
            GameOverManager gameOverManager = FindFirstObjectByType<GameOverManager>();
            if (gameOverManager != null)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    // 🔹 HP UI 업데이트 함수 (아이콘 조절)
    private void UpdateHPUI()
    {
        // 기존 HP 아이콘 제거
        foreach (GameObject icon in hpIcons)
        {
            Destroy(icon);
        }
        hpIcons.Clear();

        // 현재 HP 수만큼 아이콘 생성
        for (int i = 0; i < currentHP; i++)
        {
            GameObject newHP = Instantiate(hpPrefab, hpContainer);
            newHP.transform.localPosition = new Vector3(i * 40, 0, 0); // 가로로 정렬
            hpIcons.Add(newHP);
        }
    }

    // 🔹 다른 스크립트에서 HP 감소 기능 호출 가능
    public static HPManager GetInstance()
    {
        return instance;
    }
}