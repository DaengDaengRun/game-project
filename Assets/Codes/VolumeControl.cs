using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer; // Audio Mixer를 연결합니다.
    public Slider musicSlider; // 배경음악 슬라이더
    public Slider sfxSlider; // 효과음 슬라이더

    void Start()
    {
        // 시작 시 현재 슬라이더 값 설정
        float currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f); // 기본값은 0.75
        float currentSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f); // 기본값은 0.75

        musicSlider.value = currentMusicVolume;
        sfxSlider.value = currentSFXVolume;

        // 슬라이더 값에 맞게 믹서 볼륨 설정
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(currentMusicVolume) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(currentSFXVolume) * 20);
        
        // 슬라이더 값 변경시 음량 조절
        musicSlider.onValueChanged.AddListener((value) => {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("MusicVolume", value);
        });

        sfxSlider.onValueChanged.AddListener((value) => {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("SFXVolume", value);
        });
    }
}
