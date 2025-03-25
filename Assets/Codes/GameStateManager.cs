using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public int CurrentPlayingStage { get; private set; } = 0;  // 현재 플레이 중인 스테이지
    public int LastClearedStage { get; private set; } = 0;  // 마지막으로 클리어한 스테이지

    private bool IsFirstLaunch()
    {
        return PlayerPrefs.GetInt("HasLaunched", 0) == 0;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시 유지
            if (IsFirstLaunch())
            {
                ResetGameProgress();
                PlayerPrefs.SetInt("HasLaunched", 1); // 다음부턴 초기화 안 함
                PlayerPrefs.Save();
            }

            LoadLastClearedStage(); // 저장된 진행 정보 로딩        
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    public void SetCurrentPlayingStage(int stage)
    {
        CurrentPlayingStage = stage;
        // Debug.Log("🟢 현재 플레이 중인 스테이지: " + CurrentPlayingStage);
    }

    public void SetLastClearedStage()
    {
        LastClearedStage = CurrentPlayingStage;
        PlayerPrefs.SetInt("LastClearedStage", LastClearedStage);
        PlayerPrefs.Save();
        // Debug.Log("🏆 클리어한 스테이지 업데이트: " + LastClearedStage);
    }

    public int GetLastClearedStage()
    {
        if (LastClearedStage == 0)
        {
            LoadLastClearedStage();
        }
        return LastClearedStage;
    }

    private void LoadLastClearedStage()
    {
        LastClearedStage = PlayerPrefs.GetInt("LastClearedStage", 0);
    }

    public void ResetGameProgress()
    {
        PlayerPrefs.DeleteAll(); 
        PlayerPrefs.Save();

        LastClearedStage = 0;
        CurrentPlayingStage = 0;

        // Debug.Log("ResetGameProgress 완료: LastClearedStage = 0");
    }

    public void TryUpdateLastClearedStage()
    {
        if (CurrentPlayingStage > LastClearedStage)
        {
            SetLastClearedStage();
        }
    }
}
