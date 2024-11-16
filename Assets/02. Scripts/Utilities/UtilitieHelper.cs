using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


// 전역으로 접근가능한 각종 계산 static 클래스
public static class UtilitieHelper
{
    // 벡터로부터 각도 구하기  ===========================================================
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x); // Atan2(y,x) 라디안 구하기
        float degrees = radians * Mathf.Rad2Deg;         // 라디안 디그리로 변환

        return degrees;
    }

    // 각도로부터 방향벡터 구하기  ===========================================================
    public static Vector3 GetDirectionVectorFromAngle(float angle)
    {
        Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
        return direction;
    }

    // 확률 계산하기  ===========================================================
    public static bool isSuccess(int percent)
    {
        int chance = Random.Range(1, 101);
        return percent >= chance; // 성공하면 true
    }
    public static bool isSuccess(float percent)
    {
        int chance = Random.Range(1, 101);
        return percent >= chance; // 성공하면 true
    }

    // 퍼센트 % 계산하기  ===========================================================
    public static int IncreaseByPercent(int value, float percent)
    {
        float increase = value * (percent / 100);
        return value + (int)increase;
    }
    public static float IncreaseByPercent(float value, float percent)
    {
        float increase = value * (percent / 100);
        return value + increase;
    }
    public static int DecreaseByPercent(int value, float percent)
    {
        float increase = value * (percent / 100);
        return Mathf.RoundToInt(value - increase);
    }
    public static float DecreaseByPercent(float value, float percent)
    {
        float increase = value * (percent / 100);
        return value - increase;
    }

    // 등급별 색상 리턴하기 ===========================================================
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

    // 선형 볼륨 스케일을 데시벨로 변환 ====================================================================
    public static float LinearToDecibels(int linear)
    {
        float linearScaleRange = 20f;
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }
}
