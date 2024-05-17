using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;                    //���� ������ ������Ʈ
    public Transform GetTransform;                      //������ ������ ��ġ ������Ʈ
    public float Timecheck;                             //�ð��� üũ�ϱ� ���� (float) ��
    public bool isGen;

    // Start is called before the first frame update
    void Start()
    {
        GenObject();                                            //������ ���۵Ǿ��� �� �Լ��� ȣ���ؼ� �ʱ�ȭ��Ų��.
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGen) // if(isGen == false)
        {
            Timecheck -= Time.deltaTime;                           //�� �����Ӹ��� ������ �ð��� ���ش�.
            if(Timecheck <= 0 )                                    //�ش� �� �ð��� ������ ��� (1�� -> 0�ʰ� �Ǿ��� ���)
            {
                int RandNumber = Random.Range(0, 3);                 // 0 ~ 2������ ���� ���ڸ� ����
                GameObject Temp = Instantiate(CircleObject[RandNumber]); //���� ������ ������Ʈ�� ���������ش�.   (Instantiate)
                Temp.transform.position = GetTransform.position;        //������ ��ġ�� �̵���Ų��.
                isGen = true;                                          //Gen�� �Ǿ��ٰ� true�� bool ���� �����Ѵ�.
            }
        }
    }

    public void GenObject()
    {
        isGen = false;                           //�ʱ�ȭ : isGen�� false (�������� �ʾҴ�)
        Timecheck = 1.0f;                        //1�� �� ���� �������� ������Ű�� ���� �ʱ�ȭ
    }

    public void MergeObject(int index, Vector3 position)    //Merge �Լ��� ���Ϲ�ȣ(int)�� ���� ��ġ��(Vector3)�� ���� �޴´�.
    {
        GameObject Temp = Instantiate(CircleObject[index]);    //index�� �״�� ����. (0 ���� �迭�� ���۵����� index ���� 1 �� �־)
        Temp.transform.position = position;
        Temp.GetComponent<CircleObject>().used();              //t\������ Used �Լ� ���
    }
}
