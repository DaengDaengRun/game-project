using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform arrowUI; // 화살표 UI의 RectTransform

    [Header("Game Object References")]
    public Transform player;      // 플레이어의 Transform
    public Transform boneTarget;  // 뼈다귀(목표 오브젝트)의 Transform

    [Header("Settings")]
    public float hideDistance = 1.5f; // 일정 거리 내에서는 화살표 숨기기

    private CanvasGroup arrowCanvasGroup; // 화살표 투명도 제어용

    void Start()
    {
        // 화살표 UI의 CanvasGroup 가져오기 (없으면 자동 추가)
        arrowCanvasGroup = arrowUI.GetComponent<CanvasGroup>();
        if (arrowCanvasGroup == null)
        {
            arrowCanvasGroup = arrowUI.gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        // 뼈다귀가 있는지 확인
        if (boneTarget != null)
        {
            // 뼈다귀 방향 계산 (플레이어 → 뼈다귀 벡터)
            Vector2 direction = (boneTarget.position - player.position).normalized;

            // 각도 계산 (라디안을 각도로 변환)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle);

            // 화살표가 보이는지 여부 결정 (목표와의 거리 체크)
            float distance = Vector2.Distance(player.position, boneTarget.position);
            arrowCanvasGroup.alpha = distance <= hideDistance ? 0 : 1; // 가까우면 숨김
        }
        else
        {
            // 뼈다귀가 없을 경우 화살표 숨기기
            arrowCanvasGroup.alpha = 0;
        }
    }
}
