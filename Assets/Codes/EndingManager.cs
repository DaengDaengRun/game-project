using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public TMP_Text stage1TimeText; // Stage 1 시간 표시용
    public TMP_Text stage2TimeText; // Stage 2 시간 표시용
    public TMP_Text stage3TimeText; // Stage 3 시간 표시용

    private void Start()
    {
        // 각 스테이지의 시간 불러오기
        float stage1Time = PlayerPrefs.GetFloat("Stage1Time", 0f);
        float stage2Time = PlayerPrefs.GetFloat("Stage2Time", 0f);
        float stage3Time = PlayerPrefs.GetFloat("Stage3Time", 0f);

        // 각 스테이지의 시간을 UI에 표시
        stage1TimeText.text = "Stage 1 \nSuccess Time: " + stage1Time.ToString("F1") + "s";
        stage2TimeText.text = "Stage 2 \nSuccess Time: " + stage2Time.ToString("F1") + "s";
        stage3TimeText.text = "Stage 3 \nSuccess Time: " + stage3Time.ToString("F1") + "s";
    }
}
