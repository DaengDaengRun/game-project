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
    public float hideDistance = 2f; // 뼈다귀와 가까우면 화살표 숨김

    private CanvasGroup arrowCanvasGroup; // 화살표 투명도 조절용

    void Start()
    {
        // 화살표 UI에 CanvasGroup이 없으면 추가
        arrowCanvasGroup = arrowUI.GetComponent<CanvasGroup>();
        if (arrowCanvasGroup == null)
        {
            arrowCanvasGroup = arrowUI.gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        if (boneTarget == null || !boneTarget.gameObject.activeInHierarchy)
        {
            arrowCanvasGroup.alpha = 0; // 뼈다귀 없으면 화살표 숨기기
            return;
        }

        // 플레이어 → 뼈다귀 방향 벡터
        Vector2 direction = (boneTarget.position - player.position).normalized;

        // 화살표 회전 계산 (올바른 방향을 가리키도록 설정)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle);

        // 너무 가까우면 화살표 숨김
        float distance = Vector2.Distance(player.position, boneTarget.position);
        arrowCanvasGroup.alpha = (distance < hideDistance) ? 0 : 1;
    }

    // 뼈다귀가 스폰될 때 자동 갱신하는 함수
    public void SetBoneTarget(Transform newBoneTarget)
    {
        boneTarget = newBoneTarget;
    }
}
