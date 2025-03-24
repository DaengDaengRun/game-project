using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        AudioManager.instance.PlaySFX("StartClick"); // "Click" 효과음 재생
    }
}
