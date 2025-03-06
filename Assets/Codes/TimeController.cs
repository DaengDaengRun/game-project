using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeController : MonoBehaviour{
    public bool isCountDown = true; // true: 카운트 다운으로 시간 측정
    public float gameTime = 5;      // 게임의 최대 시간
    public bool isTimeOver = false; // true: 타이머 정지
    public float displayTime = 0;   // 표시 시간
    public TMP_Text gameTimeText;
    public TMP_Text successTimeText; // 게임 종료 후 성공 시간 표시 UI
    private float finalTime = 0;     // 게임 종료 후 저장될 최종 시간

    float times = 0;                // 현재 시간
    private bool hasLoggedGameOver = false;

    // Start is called before the first frame update
    void Start(){
        if (isCountDown){
            // 카운트다운
            displayTime = gameTime;
        }
        successTimeText.text = "";
    }

    // Update is called once per frame
    void Update(){
        GameOverManager gameOverManager = FindFirstObjectByType<GameOverManager>();
        if (!isTimeOver){
            times += Time.deltaTime;

            if (isCountDown){
                displayTime = gameTime - times;
                if (displayTime <= 0.0f){
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            } else {
                displayTime = times;
                if (displayTime >= gameTime){
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }

            gameTimeText.text = "Time Left: " + displayTime.ToString("F1"); 
        }

        if (isTimeOver && !hasLoggedGameOver) {
            finalTime = displayTime;

            if ((isCountDown && finalTime > 0.0f) || (!isCountDown && finalTime >= gameTime)) {
                ShowSuccessTime();
            }

            hasLoggedGameOver = true;
            if (gameOverManager != null)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                Debug.LogError("⚠️ GameOverManager를 찾을 수 없습니다! 씬에 추가하세요.");
            }
        }
    }
    void ShowSuccessTime(){
        successTimeText.text = "Success Time: " + finalTime.ToString("F1") + "s";
    }
}