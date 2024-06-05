using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderFollowObject : MonoBehaviour
{
    public Transform targetObject;                      //따라갈 3D 오브젝트
    public Vector3 offset;                              //위치값 보정
    public SliderFollowObject slider;                   //따라다닐 SliderUI

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //3D 오브젝트의 위치를 화면 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetObject.position);

        //화면 좌표를 Canvas 좌표로 변환
        RectTransform canvasRect = slider.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 cavasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out cavasPos);

        //slider UI 업데이트
        slider.transform.localPosition = cavasPos;
    }
}
