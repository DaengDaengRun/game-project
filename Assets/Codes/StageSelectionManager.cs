using UnityEngine;
using UnityEngine.UI;

public class StageSelectionManager : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;

    public GameObject stage1LockIcon;
    public GameObject stage2LockIcon;
    public GameObject stage3LockIcon;

    void Start()
    {
        // Debug.Log("StageSelectionManager 초기화 시작");
        CheckFirstLaunch();              // 1. 초기 실행 여부 확인
        LoadStageProgress();             // 2. 저장된 진행도 불러오기
        UpdateStageUI();                 // 3. 버튼 및 잠금 UI 갱신
    }

    // 1. 첫 실행 시 진행 정보 초기화
    private void CheckFirstLaunch()
    {
        if (PlayerPrefs.GetInt("GameStarted", 0) == 0)
        {
            // Debug.Log("게임이 처음 실행됨! 진행 정보 초기화");
            GameStateManager.Instance.ResetGameProgress();

            PlayerPrefs.SetInt("GameStarted", 1); // 다시 0으로 두고 있었음 → 1로 변경 필요
            PlayerPrefs.Save();
        }
    }

    // 2. 스테이지 클리어 정보 불러오기
    private void LoadStageProgress()
    {
        int lastClearedStage = GameStateManager.Instance.GetLastClearedStage();
        // Debug.Log("마지막 클리어한 스테이지: " + lastClearedStage);

        int currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
        // Debug.Log("현재 플레이 중인 스테이지: " + currentStage);
    }

    // 3. 버튼 상태 및 잠금 아이콘 업데이트
    public void UpdateStageUI()
    {
        int lastClearedStage = GameStateManager.Instance.GetLastClearedStage();

        // 기본 상태: 모두 잠금
        stage1LockIcon.SetActive(false);
        stage1Button.interactable = true;

        stage2LockIcon.SetActive(true);
        stage2Button.interactable = false;

        stage3LockIcon.SetActive(true);
        stage3Button.interactable = false;

        // 클리어한 스테이지 기반으로 잠금 해제
        if (lastClearedStage >= 1)
        {
            stage2LockIcon.SetActive(false);
            stage2Button.interactable = true;
            // Debug.Log("Stage2 오픈");
        }
        if (lastClearedStage >= 2)
        {
            stage3LockIcon.SetActive(false);
            stage3Button.interactable = true;
            // Debug.Log("Stage3 오픈");
        }
    }
}