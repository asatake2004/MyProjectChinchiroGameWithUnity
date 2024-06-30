using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SVDiceDelete : MonoBehaviour
{
    private bool canDelete = true; // サイコロを削除できるかどうかのフラグ

    void Update()
    {
        if (canDelete && Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            canDelete = false; // サイコロを削除したのでフラグをfalseにする
            StartCoroutine(ResetDeleteCooldown()); // クールダウンを開始する
        }
    }

    private IEnumerator ResetDeleteCooldown()
    {
        yield return new WaitForSeconds(3f); // 3秒待つ
        canDelete = true; // サイコロを再び削除できるようにする
    }
}
