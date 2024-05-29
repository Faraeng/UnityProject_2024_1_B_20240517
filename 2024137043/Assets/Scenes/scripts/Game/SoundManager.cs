using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;         //Audio 관련 기능을 사용하기 위해 추가

[System.Serializable] //searializable 직렬화 (클래스 데이터를 형식을 보여주게 함)
public class Sound
{
    public string name;        //사운드의 이름
    public AudioClip clip;     //사운드 클립

    [Range(0f, 1f)]                //인스펙터에서 범위 설정
    public float volume = 1.0f;    //사운드 볼륨

    [Range(0.1f, 3f)]
    public float pitch = 1.0f;               //사운드 피치
    public bool loop;                       //반복 재생 여부
    public AudioMixerGroup mixerGroup;     //오디오 믹서 그룹

    [HideInInspector]                     //인스펙터 창에서 안 보이게 가린다.
    public AudioSource source;            //오디오 소스
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;                    //싱글톤 인스턴스화 시킴

    public List<Sound> sounds = new List<Sound>();           //사운드 리스트
    public AudioMixer audioMixer;                            //오디오 믹서 참조

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);                //scene 변경되도 오브젝트 파괴 X
        }
        else
        {
            Destroy(gameObject);                         //이미 싱클톤 오브젝트가 있을 경우 파괴한다.
        }

        foreach(Sound sound in sounds)                   //리스트 안에 잇는 사운드들을 초기화한다.
        {
            sound.source = gameObject.AddComponent<AudioSource>();                //소스 하나당 1개씩 컴포넌트를 더해준다.
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;               //오디오 믹서 그룹 설정
        }
    }

    //사운드를 재생하는 메서드
    public void PlaySound(string name)                                          //인수 Name 받아서
    {
        Sound soundToPlay = sounds.Find(sound => sound.name == name);          //List 안에 있는 name 이 같은 것을 검색 후 soundToPlay에 선언

        if(soundToPlay != null)
        {
            soundToPlay.source.Play();
        }
        else
        {
            Debug.LogWarning("사운드 : " + name + "없습니다.");
        }

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
