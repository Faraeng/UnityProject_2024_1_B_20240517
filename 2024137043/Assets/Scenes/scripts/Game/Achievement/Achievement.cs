using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Achievement    //���� Ŭ���� ���� ( MonoBehaviour X)
{
    public string name;                  //���� �̸�
    public string description;          //���� ����
    public bool isUnlocked;            //��� ����
    public int currentProgress;      //���� ����
    public int goal;                 //�Ϸ� ����

    public Achievement(string name, string description, int goal)    //������ �Լ�
    {
        this.name = name;                            //Achievement Ŭ������ ������ �� �̸��� �μ��� �޾Ƽ� �޾Ƽ� ����
        this.description = description;              //Achievement Ŭ������ ������ �� ������ �μ��� �޾Ƽ� ����
        this.isUnlocked = false;                     //Achievement Ŭ������ ������ �� false
        this.currentProgress = 0;                   //Achievement Ŭ������ ������ �� 0
        this.goal = goal;                            //Achievement Ŭ������ ������ �� �Ϸ� ����

    }
    public void AddProgress(int amount)          // ���� ���൵ �Լ�
    {
        if(!isUnlocked)
        {
            currentProgress += amount;
            if(currentProgress >= goal)
            {
                isUnlocked = true;
                OnAchievementUnlocked();
            }
        }
    }

    protected virtual void OnAchievementUnlocked()
    {
        Debug.Log($"���� �޼� : {name}");       //$ ǥ�ð� ����ִ� string ���� {} ���� ���� ����� �� �ִ�.
    }

}
