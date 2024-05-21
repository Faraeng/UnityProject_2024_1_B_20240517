using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text pointText;            //���� ǥ���� UI
    public Text bestscoreText;         //�ְ� ���� ǥ���� UI

    void OnEnable()
    {
        GameManager.OnPointChanged += UpdatePointText;                      //�̺�Ʈ ���
        GameManager.OnBestScoreChanged += UpdateBestScoreText;            //�̺�Ʈ ���
    }

    void OnDisable()
    {
        GameManager.OnPointChanged -= UpdatePointText;                          //�̺�Ʈ ����
        GameManager.OnBestScoreChanged -= UpdateBestScoreText;                 //�̺�Ʈ ����
    }

    void UpdatePointText(int newPoint)
    {
        pointText.text = "Points : " + newPoint;            //�Լ� �̺�Ʈ�� ���� �μ��� ����
    }

    void UpdateBestScoreText(int newBestScore)              //�Լ� �̺�Ʈ�� ���� �μ� ����
    {
        bestscoreText.text = "Best Score : " + newBestScore;
    }


}

