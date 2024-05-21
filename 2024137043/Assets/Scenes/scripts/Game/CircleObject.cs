using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;      //드래그 중인지 판단하는 bool
    public bool isUsed;      //사용 완료 판단하는 bool
    Rigidbody2D rigidbody2D;    //2D 강체를 불러온다.

    public int index;          //과일 번호를 만든다.

    public float EndTime = 0.0f;
    public SpriteRenderer spriteRenderer;         //종료 전 시간 체크 변수

    public GameManager gameMananger;      

    // Start is called before the first frame update

    private void Awake()                                 //시작 전 소스 단계서 셋팅
    {
        isUsed = false;                                  //사용 완료가 되지 않음
        rigidbody2D = GetComponent<Rigidbody2D>();      //강체를 가져온다.
        rigidbody2D.simulated = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        gameMananger = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsed) return;                              //사용 완료된 물체를 어디상 업데이트 하지 않기 위해서 return 로 돌려준다.

        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4f + transform.localScale.x / 2f;                                 
            float RightBorder = 4f - transform.localScale.x / 2f;                          //최대 오른쪽으로 갈 수 있는 범위

            if (mousePos.x < leftBorder) mousePos.x = leftBorder;                             //최대 왼쪽으로 갈 수 있는 범위를 넘어갈 경우 최대 범위 위치를 대입해서 넘어가지 못하게 한다.
            if (mousePos.x > RightBorder) mousePos.x = RightBorder;

            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);             //이 오브젝트의 위치는 마우스 위치로 이동된다. 0.2f 속도로 이동된다.

        }

        if (Input.GetMouseButtonDown(0)) Drag();         //마우스 버튼이 눌렸을 때 Drag 함수 호출
        if (Input.GetMouseButtonUp(0)) Drop();           //마우스 버튼이 올라갈 때 Drop 함수 호출
    }

    void Drag()
    {
        isDrag = true;                            //드래그 시작
        rigidbody2D.simulated = false;            //드래그 중에는 물리 현상이 일어나는 것을 막기 위해서 (false)
    }

    void Drop()
    {
        isDrag = false;                       //드래그 완료
        isUsed = true;                      //사용 완료
        rigidbody2D.simulated = true;      //물리 현상 시작

        GameObject Temp = GameObject.FindWithTag("GameManager");                         //Tag : GameManager를 Scene 찾아서 오브젝트를 가져온다
        if(Temp != null)                                                                 //해당 오브젝트가 존재하면
        {
            Temp.gameObject.GetComponent<GameManager>().GenObject();                    //GenObject 함수를 호출한다. (GetComponent 통해서 클래스에 접근한다)
        }
    }

    public void used()
    {
        isDrag = false;
        isUsed = true;
        rigidbody2D.simulated = true;     //물리 현상 시작
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "EndLine")                       //c\충돌 중인 물체가의 TAG 가 EndLine일 경우
        {
            EndTime += Time.deltaTime;

            if(EndTime > 1)
            {
                spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);
            }
            if(EndTime > 3)
            {
                gameMananger.EndGame();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "EndLine")                      //충돌 물체가 빠져 나갔을 때
        {
            EndTime = 0.0f;
            spriteRenderer.color = Color.white;             //기존 색상으로 변경
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (index >= 7)         //준비된 과일이 최대 7개
            return;

        if (collision.gameObject.tag == "Fruit")           //충돌 물체의 TAG가 fruit 일 경우
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();       //임시로 class temp를 선언하고 충돌체의 class를 받아온다.

            if(temp.index == index) //과일 번호가 동일한 경우
            {
                if(gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())     //유니티에서 지원하는 고유의 ID룰 받아와서 ID가 큰쪽에서 다음 과일 생성
                {
                    //gamemanger에서 생성 함수 호출
                    GameObject Temp = GameObject.FindWithTag("GameManager");
                    if (Temp != null)
                    {
                        Temp.gameObject.GetComponent<GameManager>().MergeObject(index, gameObject.transform.position); //생성된 MergeObject 함수에 인수와 함께 전달
                    }
                }

                Destroy(temp.gameObject);           //충돌 물체 파괴
                Destroy(gameObject);                    //자기 자신 파괴
            }
        }

    }

}
