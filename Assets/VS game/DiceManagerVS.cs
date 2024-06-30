using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManagerVS : MonoBehaviour
{
    public GameObject[] prefabs; // サイコロのPrefabを格納する配列
    public DiceCheckA dice1; // DiceCheckAをアタッチしたサイコロ1
    public DiceCheckA dice2; // DiceCheckAをアタッチしたサイコロ2
    public DiceCheckA dice3; // DiceCheckAをアタッチしたサイコロ3
    public VSChinchiroManager chinchiro; // VSChinchiroManagerに変更
    private int[] rotateX = new int[3];
    private int[] rotateY = new int[3];
    private int[] rotateZ = new int[3];
    private Vector3[] positions = new Vector3[3]; // サイコロの初期位置を格納する配列
    public int[] diceValues = new int[3];
    private List<GameObject> currentDice = new List<GameObject>(); // 現在のサイコロを管理するリスト
    private bool canThrowOrDeleteDice = true; // サイコロを振るか削除できるかどうかのフラグ

    void Start()
    {
        // サイコロの初期位置を設定
        positions[0] = new Vector3(0, 5, 5);
        positions[1] = new Vector3(0, 5, 4.5f);
        positions[2] = new Vector3(0, 5, 4);

        if (dice1 == null || dice2 == null || dice3 == null)
        {
            Debug.LogError("DiceCheckAオブジェクトがアサインされていません");
            return;
        }

        if (chinchiro == null)
        {
            Debug.LogError("VSChinchiroManagerオブジェクトがアサインされていません");
            return;
        }
    }

    public void ThrowAllDice()
    {
        ClearCurrentDice(); // 現在のサイコロを削除

        for (int i = 0; i < 3; i++)
        {
            // ランダムな回転値を生成
            rotateX[i] = Random.Range(0, 360);
            rotateY[i] = Random.Range(0, 360);
            rotateZ[i] = Random.Range(0, 360);

            // サイコロを生成して投げる
            GameObject dice = Instantiate(prefabs[i], positions[i], Quaternion.identity);
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * 250); // 力を加える方向を適宜変更
            dice.transform.Rotate(rotateX[i], rotateY[i], rotateZ[i]);

            // サイコロにDiceCheckAをアタッチ
            if (i == 0) dice1 = dice.GetComponent<DiceCheckA>();
            if (i == 1) dice2 = dice.GetComponent<DiceCheckA>();
            if (i == 2) dice3 = dice.GetComponent<DiceCheckA>();

            currentDice.Add(dice); // リストにサイコロを追加
        }

        canThrowOrDeleteDice = false; // サイコロを振ったのでフラグをfalseにする
        StartCoroutine(EvaluateDiceAfterDelay()); // 一定時間後に評価を行う
    }

    private void ClearCurrentDice()
    {
        foreach (GameObject dice in currentDice)
        {
            Destroy(dice);
        }
        currentDice.Clear();
    }

    private IEnumerator EvaluateDiceAfterDelay()
    {
        // 少し待つ（例えば2秒）
        yield return new WaitForSeconds(2.5f);

        // サイコロの結果を評価
        EvaluateDice();
    }

    private void EvaluateDice()
    {
        diceValues[0] = dice1.GetValue();
        diceValues[1] = dice2.GetValue();
        diceValues[2] = dice3.GetValue();

        chinchiro.EvaluateDice(diceValues);
    }

    public void ResetThrowOrDeleteCooldown()
    {
        canThrowOrDeleteDice = true; // サイコロを再び振ったり削除できるようにする
    }

    public bool CanThrowOrDeleteDice()
    {
        return canThrowOrDeleteDice;
    }
}
