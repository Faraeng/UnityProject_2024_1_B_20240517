using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;                    //과일 프리팹 오브젝트
    public Transform GetTransform;                      //과일이 생성될 위치 오브젝트
    public float Timecheck;                             //시간을 체크하기 위한 (float) 값
    public bool isGen;

    // Start is called before the first frame update
    void Start()
    {
        GenObject();                                            //게임이 시작되었을 때 함수를 호출해서 초기화시킨다.
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGen) // if(isGen == false)
        {
            Timecheck -= Time.deltaTime;                           //매 프레임마다 프레임 시간을 빼준다.
            if(Timecheck <= 0 )                                    //해당 초 시간이 지났을 경우 (1초 -> 0초가 되었을 경우)
            {
                int RandNumber = Random.Range(0, 3);                 // 0 ~ 2까지의 랜덤 숫자를 생성
                GameObject Temp = Instantiate(CircleObject[RandNumber]); //과일 프리팹 오브젝트를 생성시켜준다.   (Instantiate)
                Temp.transform.position = GetTransform.position;        //설정한 위치로 이동시킨다.
                isGen = true;                                          //Gen이 되었다고 true로 bool 값을 변경한다.
            }
        }
    }

    public void GenObject()
    {
        isGen = false;                           //초기화 : isGen을 false (생성되지 않았다)
        Timecheck = 1.0f;                        //1초 후 과일 프리팹을 생성시키기 위한 초기화
    }

    public void MergeObject(int index, Vector3 position)    //Merge 함수는 과일번호(int)과 생성 위치값(Vector3)을 전달 받는다.
    {
        GameObject Temp = Instantiate(CircleObject[index]);    //index를 그대로 쓴다. (0 부터 배열이 시작되지만 index 값이 1 더 있어서)
        Temp.transform.position = position;
        Temp.GetComponent<CircleObject>().used();              //t\선언한 Used 함수 사용
    }
}
