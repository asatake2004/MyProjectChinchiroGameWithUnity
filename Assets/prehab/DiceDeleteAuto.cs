using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//サイコロにアタッチ
public class DiceDeleteAuto : MonoBehaviour
{
    public float lifetime = 4.0f; // サイコロが削除されるまでの時間

    void Start()
    {
        // 一定時間後にサイコロを削除
        Destroy(gameObject, lifetime);
    }
}
