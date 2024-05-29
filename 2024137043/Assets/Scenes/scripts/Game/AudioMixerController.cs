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

    //�����̴� MinValue 0.001

    private void Awake()
    {
        musicMasterSlider.onValueChanged.AddListener(SetMasterVolume);          //Master �����̴��� ���� ����� �� �����ʸ� ���ؼ� �Լ��� ���� �����Ѵ�.

        musicBGMSlider.onValueChanged.AddListener(SetBGMVolume);            //SFX �����̴��� ���� ����� �� �����ʸ� ���ؼ� �Լ��� ���� �����Ѵ�.

        musicSFXSlider.onValueChanged.AddListener(SetSFXVolume);           //SFX �����̴��� ���� ����� �� �����ʸ� ���ؼ� �Լ��� ���� �����Ѵ�.
    }

    public void SetMasterVolume(float volume)                                   //������ ���� �����̴���  Mixer�� �ݿ��ǰ�
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);                 //������ Log10 ������ x20�� ���ش�.
    }

    public void SetBGMVolume(float volume)                                 //SFX ���� �����̴��� Mixer�� �ݿ��ǰ�
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)                               //SFX ���� �����̴��� Mixer�� �ݿ��ǰ�
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
