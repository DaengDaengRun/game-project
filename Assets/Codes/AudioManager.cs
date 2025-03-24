using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfxSource;
    public List<AudioClip> sfxClips; // 효과음 리스트
    private Dictionary<string, AudioClip> sfxDictionary; // 효과음 딕셔너리

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        sfxDictionary = new Dictionary<string, AudioClip>();

        foreach (var clip in sfxClips)
        {
            sfxDictionary[clip.name] = clip;
            Debug.Log($"등록된 효과음: {clip.name}"); // 🔥 효과음 등록 확인
        }
    }

    public void PlaySFX(string clipName)
    {
        if (sfxDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log($"재생 중: {clipName}"); // 🔥 효과음 재생 확인
        }
        else
        {
            Debug.LogWarning($"효과음 '{clipName}'을(를) 찾을 수 없음!");
        }
    }
}
