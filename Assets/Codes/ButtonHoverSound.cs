using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX("Hover"); // "Hover" 사운드 재생
    }
}
