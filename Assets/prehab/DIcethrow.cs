using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceThrow : MonoBehaviour
{
    public GameObject[] prefabs; // サイコロのPrefabを格納する配列
    private int[] rotateX = new int[3];
    private int[] rotateY = new int[3];
    private int[] rotateZ = new int[3];
    private Vector3[] positions = new Vector3[3]; // サイコロの初期位置を格納する配列

    void Start()
    {
        // サイコロの初期位置を設定
        positions[0] = new Vector3(5, 5, 0);
        positions[1] = new Vector3(4.5f, 5, 0);
        positions[2] = new Vector3(4, 5, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ThrowAllDice();
        }
    }

    void ThrowAllDice()
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
            rb.AddForce(-transform.right * 250);
            dice.transform.Rotate(rotateX[i], rotateY[i], rotateZ[i]);
        }
    }
}
