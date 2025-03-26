using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public GameObject settingsPanel; // 설정 패널 UI

    // 설정 패널을 토글하는 함수 (설정 버튼 클릭 시 사용)
    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    // 설정 패널을 닫는 함수 (X 버튼 클릭 시 사용)
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}
