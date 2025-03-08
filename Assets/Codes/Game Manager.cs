using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public Player player;
   public int currentStage = 1; // 현재 스테이지 저장

   void Awake()
   {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
   }

   public void NextStage()
    {
        currentStage++; // 다음 스테이지로 증가
        PlayerPrefs.SetInt("CurrentStage", currentStage); // 저장
    }
}
