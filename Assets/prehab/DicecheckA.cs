using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サイコロにアタッチ
public class DiceCheckA : MonoBehaviour
{
private Transform diceTransform;
private int result;

    void Start()
{
    // ダイスのTransformを取得
    diceTransform = transform;
}

void Update()
{
    // 定期的に出目を計算
    CalculateResult();
}

private void CalculateResult()
{
    // ダイスの出目を計算
    result = GetNumber(diceTransform);
}

public int GetValue()
{
    // 上面の値を返す
    return result;
}

// 出目チェック
private int GetNumber(Transform diceTransform)
{
    int result = 0;

    // ダイスのローカル軸と世界の上向きベクトル(Vector3.up)の内積を計算
    float innerProductX = Vector3.Dot(diceTransform.right, Vector3.up);
    float innerProductY = Vector3.Dot(diceTransform.up, Vector3.up);
    float innerProductZ = Vector3.Dot(diceTransform.forward, Vector3.up);

    // 最も上向きに近い軸を判断し、それに対応するダイスの数字を決定
    if (Mathf.Abs(innerProductX) > Mathf.Abs(innerProductY) && Mathf.Abs(innerProductX) > Mathf.Abs(innerProductZ))
    {
        // X軸が一番近い
        if (innerProductX > 0f)
        {
            result = 4;
        }
        else
        {
            result = 3;
        }
    }
    else if (Mathf.Abs(innerProductY) > Mathf.Abs(innerProductX) && Mathf.Abs(innerProductY) > Mathf.Abs(innerProductZ))
    {
        // Y軸が一番近い
        if (innerProductY > 0f)
        {
            result = 2;
        }
        else
        {
            result = 5;
        }
    }
    else
    {
        // Z軸が一番近い
        if (innerProductZ > 0f)
        {
            result = 1;
        }
        else
        {
            result = 6;
        }
    }

    return result;
}

}
