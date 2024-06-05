using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderFollowObject : MonoBehaviour
{
    public Transform targetObject;                      //���� 3D ������Ʈ
    public Vector3 offset;                              //��ġ�� ����
    public SliderFollowObject slider;                   //����ٴ� SliderUI

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //3D ������Ʈ�� ��ġ�� ȭ�� ��ǥ�� ��ȯ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetObject.position);

        //ȭ�� ��ǥ�� Canvas ��ǥ�� ��ȯ
        RectTransform canvasRect = slider.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 cavasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out cavasPos);

        //slider UI ������Ʈ
        slider.transform.localPosition = cavasPos;
    }
}
