using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public Text successTimeText;   // 성공 시간 표시 텍스트
    public Button goFasterButton;  // Go Faster 버튼
    private float successTime;     // 성공 시간 기록을 위한 변수

    void Start()
    {
        successTime = 0f;
        goFasterButton.onClick.AddListener(OnGoFasterClicked);
        InvokeRepeating("UpdateSuccessTime", 0f, 1f); // 1초마다 시간을 업데이트
    }

    void UpdateSuccessTime()
    {
        successTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(successTime / 60);
        int seconds = Mathf.FloorToInt(successTime % 60);
        successTimeText.text = $"Success Time: {minutes:00}:{seconds:00}";
    }

    void OnGoFasterClicked()
    {
        // 'Go Faster' 버튼 클릭 시 행동 정의
        // 예를 들어, 타이머를 빨리 가게 하는 코드 추가
        successTime -= 5f; // 5초 앞당기기 (예시)
        if (successTime < 0)
            successTime = 0;
    }
}