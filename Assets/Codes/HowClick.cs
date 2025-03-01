using UnityEngine;
using UnityEngine.SceneManagement;

public class HowClick : MonoBehaviour
{
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene("HowToPlay"); // 스테이지 선택 씬 로드
    }
}
