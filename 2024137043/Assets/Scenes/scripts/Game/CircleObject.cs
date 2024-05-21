using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;      //�巡�� ������ �Ǵ��ϴ� bool
    public bool isUsed;      //��� �Ϸ� �Ǵ��ϴ� bool
    Rigidbody2D rigidbody2D;    //2D ��ü�� �ҷ��´�.

    public int index;          //���� ��ȣ�� �����.

    public float EndTime = 0.0f;
    public SpriteRenderer spriteRenderer;         //���� �� �ð� üũ ����

    public GameManager gameMananger;      

    // Start is called before the first frame update

    private void Awake()                                 //���� �� �ҽ� �ܰ輭 ����
    {
        isUsed = false;                                  //��� �Ϸᰡ ���� ����
        rigidbody2D = GetComponent<Rigidbody2D>();      //��ü�� �����´�.
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
        if (isUsed) return;                              //��� �Ϸ�� ��ü�� ���� ������Ʈ ���� �ʱ� ���ؼ� return �� �����ش�.

        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4f + transform.localScale.x / 2f;                                 
            float RightBorder = 4f - transform.localScale.x / 2f;                          //�ִ� ���������� �� �� �ִ� ����

            if (mousePos.x < leftBorder) mousePos.x = leftBorder;                             //�ִ� �������� �� �� �ִ� ������ �Ѿ ��� �ִ� ���� ��ġ�� �����ؼ� �Ѿ�� ���ϰ� �Ѵ�.
            if (mousePos.x > RightBorder) mousePos.x = RightBorder;

            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);             //�� ������Ʈ�� ��ġ�� ���콺 ��ġ�� �̵��ȴ�. 0.2f �ӵ��� �̵��ȴ�.

        }

        if (Input.GetMouseButtonDown(0)) Drag();         //���콺 ��ư�� ������ �� Drag �Լ� ȣ��
        if (Input.GetMouseButtonUp(0)) Drop();           //���콺 ��ư�� �ö� �� Drop �Լ� ȣ��
    }

    void Drag()
    {
        isDrag = true;                            //�巡�� ����
        rigidbody2D.simulated = false;            //�巡�� �߿��� ���� ������ �Ͼ�� ���� ���� ���ؼ� (false)
    }

    void Drop()
    {
        isDrag = false;                       //�巡�� �Ϸ�
        isUsed = true;                      //��� �Ϸ�
        rigidbody2D.simulated = true;      //���� ���� ����

        GameObject Temp = GameObject.FindWithTag("GameManager");                         //Tag : GameManager�� Scene ã�Ƽ� ������Ʈ�� �����´�
        if(Temp != null)                                                                 //�ش� ������Ʈ�� �����ϸ�
        {
            Temp.gameObject.GetComponent<GameManager>().GenObject();                    //GenObject �Լ��� ȣ���Ѵ�. (GetComponent ���ؼ� Ŭ������ �����Ѵ�)
        }
    }

    public void used()
    {
        isDrag = false;
        isUsed = true;
        rigidbody2D.simulated = true;     //���� ���� ����
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "EndLine")                       //c\�浹 ���� ��ü���� TAG �� EndLine�� ���
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
        if (collision.tag == "EndLine")                      //�浹 ��ü�� ���� ������ ��
        {
            EndTime = 0.0f;
            spriteRenderer.color = Color.white;             //���� �������� ����
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (index >= 7)         //�غ�� ������ �ִ� 7��
            return;

        if (collision.gameObject.tag == "Fruit")           //�浹 ��ü�� TAG�� fruit �� ���
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();       //�ӽ÷� class temp�� �����ϰ� �浹ü�� class�� �޾ƿ´�.

            if(temp.index == index) //���� ��ȣ�� ������ ���
            {
                if(gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())     //����Ƽ���� �����ϴ� ������ ID�� �޾ƿͼ� ID�� ū�ʿ��� ���� ���� ����
                {
                    //gamemanger���� ���� �Լ� ȣ��
                    GameObject Temp = GameObject.FindWithTag("GameManager");
                    if (Temp != null)
                    {
                        Temp.gameObject.GetComponent<GameManager>().MergeObject(index, gameObject.transform.position); //������ MergeObject �Լ��� �μ��� �Բ� ����
                    }
                }

                Destroy(temp.gameObject);           //�浹 ��ü �ı�
                Destroy(gameObject);                    //�ڱ� �ڽ� �ı�
            }
        }

    }

}
