using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    public float targetAspect = 16.0f / 9.0f; // 원하는 비율 (예: 16:9)

    void Start()
    {
        SetCameraAspect();
    }

    void SetCameraAspect()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f; // 위아래 검은 여백 추가
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f; // 좌우 검은 여백 추가
            rect.y = 0;
            cam.rect = rect;
        }
    }
}
