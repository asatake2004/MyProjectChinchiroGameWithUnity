using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SVDiceManager_com : MonoBehaviour
{
    public GameObject[] prefabs; // サイコロのPrefabを格納する配列
    public DiceCheckA dice1; // DiceCheckAをアタッチしたサイコロ1
    public DiceCheckA dice2; // DiceCheckAをアタッチしたサイコロ2
    public DiceCheckA dice3; // DiceCheckAをアタッチしたサイコロ3
    public SVChinchiroManager_com chinchiro; // SVChinchiroManager_comに変更
    private int[] rotateX = new int[3];
    private int[] rotateY = new int[3];
    private int[] rotateZ = new int[3];
    private Vector3[] positions = new Vector3[3]; // サイコロの初期位置を格納する配列
    private int[] diceValues = new int[3];
    

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
            Debug.LogError("ChinchiroManager_comオブジェクトがアサインされていません");
            return;
        }

        // サイコロを振るコルーチンを開始
        StartCoroutine(ThrowDicePeriodically());
    }

    IEnumerator ThrowDicePeriodically()
    {
        while (true)
        {
            ThrowAllDiceCom();
            StartCoroutine(EvaluateDiceAfterDelay());

            // 4秒待つ
            yield return new WaitForSeconds(4f);
        }
    }

    public void ThrowAllDiceCom()
    {
        for (int i = 0; i < 3; i++)
        {
            // ランダムな回転値を生成
            rotateX[i] = Random.Range(0, 360);
            rotateY[i] = Random.Range(0, 360);
            rotateZ[i] = Random.Range(0, 360);

            // サイコロを生成して投げる
            GameObject dice = Instantiate(prefabs[i], positions[i], Quaternion.identity);
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * 250); // 左方向に力を加えるように修正
            dice.transform.Rotate(rotateX[i], rotateY[i], rotateZ[i]);

            // サイコロにDiceCheckAをアタッチ
            if (i == 0) dice1 = dice.GetComponent<DiceCheckA>();
            if (i == 1) dice2 = dice.GetComponent<DiceCheckA>();
            if (i == 2) dice3 = dice.GetComponent<DiceCheckA>();

            // DiceDeleteAutoの機能を組み込む
            Destroy(dice, 4.0f); // 4秒後にサイコロを削除
        }
    }

    private IEnumerator EvaluateDiceAfterDelay()
    {
        // 少し待つ（例えば2.5秒）
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
}
