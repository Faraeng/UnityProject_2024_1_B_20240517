using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicMasterSlider;
    [SerializeField] private Slider musicBGMSlider;
    [SerializeField] private Slider musicSFXSlider;

    //슬라이더 MinValue 0.001

    private void Awake()
    {
        musicMasterSlider.onValueChanged.AddListener(SetMasterVolume);          //Master 슬라이더의 값이 변경됡 때 리스너를 통해서 함수에 값을 전달한다.

        musicBGMSlider.onValueChanged.AddListener(SetBGMVolume);            //SFX 슬라이더의 값이 변경됡 때 리스너를 통해서 함수에 값을 전달한다.

        musicSFXSlider.onValueChanged.AddListener(SetSFXVolume);           //SFX 슬라이더의 값이 변경됡 때 리스너를 통해서 함수에 값을 전달한다.
    }

    public void SetMasterVolume(float volume)                                   //마스터 볼륨 슬라이더가  Mixer에 반영되게
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);                 //볼륨은 Log10 단위에 x20을 해준다.
    }

    public void SetBGMVolume(float volume)                                 //SFX 볼륨 술라이더가 Mixer에 반영되게
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)                               //SFX 볼륨 술라이더가 Mixer에 반영되게
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
