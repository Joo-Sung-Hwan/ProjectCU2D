using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;


// �������� ���ٰ����� ���� ��� static Ŭ����
public static class UtilitieHelper
{
    // ���ͷκ��� ���� ���ϱ�  ===========================================================
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x); // Atan2(y,x) ���� ���ϱ�
        float degrees = radians * Mathf.Rad2Deg;         // ���� ��׸��� ��ȯ

        return degrees;
    }

    // �����κ��� ���⺤�� ���ϱ�  ===========================================================
    public static Vector3 GetDirectionVectorFromAngle(float angle)
    {
        Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
        return direction;
    }

    // Ȯ�� ����ϱ�  ===========================================================
    public static bool isSuccess(int percent)
    {
        int chance = Random.Range(1, 101);
        return percent >= chance; // �����ϸ� true
    }
    public static bool isSuccess(float percent)
    {
        int chance = Random.Range(1, 101);
        return percent >= chance; // �����ϸ� true
    }

    // ��޺� ���� �����ϱ� ===========================================================
    public static Color GetGradeColor(EGrade type)
    {
        Color color = Color.white;

        switch (type)
        {
            case EGrade.Normal:
                color = Color.white;
                break;
            case EGrade.Rare:
                color = Settings.rare;
                break;
            case EGrade.Unique:
                color = Settings.unique;
                break;
            case EGrade.Legend:
                color = Settings.legend;
                break;
            default:
                break;
        };

        return color;
    }

    // ���� ���� �������� ���ú��� ��ȯ ====================================================================
    public static float LinearToDecibels(int linear)
    {
        float linearScaleRange = 20f;
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }
}
