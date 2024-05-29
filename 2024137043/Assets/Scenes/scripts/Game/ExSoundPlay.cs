using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExSoundPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))         //1번을 누르면
        {
            SoundManager.instance.PlaySound("BackGround");     //백그라운드 재생
        }    
       if (Input.GetKeyDown(KeyCode.Alpha2))       //2번을 누르면
        {
            SoundManager.instance.PlaySound("Cannon");         //플레이그라운드 재생
        }
    }
}
