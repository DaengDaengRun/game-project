using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        if (isTimeOver == false){
            times += Time.deltaTime;
            if (isCountDown){
                // 카운트다운
                displayTime = gameTime - times;
                if (displayTime <= 0.0f){
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }else{
                // 카운트업
                displayTime = times;
                if (displayTime >= gameTime){
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
            Debug.Log("TIMES: " + displayTime.ToString("F1"));
            gameTimeText.text = "Time left: " + displayTime.ToString("F1")  + "s";
        }

        if (isTimeOver) {
            finalTime = displayTime; // 종료된 순간의 시간 저장

            // 성공한 경우에만 ShowSuccessTime() 호출
            if (isCountDown && finalTime > 0.0f) {
                ShowSuccessTime();
            } else if (!isCountDown && finalTime >= gameTime) {
                ShowSuccessTime();
            }

            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        
    }
    void ShowSuccessTime(){
        successTimeText.text = "Success Time: " + finalTime.ToString("F1") + "s";
    }
}