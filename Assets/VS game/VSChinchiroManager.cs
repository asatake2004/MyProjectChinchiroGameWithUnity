using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class VSChinchiroManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    public void EvaluateDice(int[] diceValues)
    {
        // サイコロの目を表示
        string diceString = string.Join(", ", diceValues);
        resultText.text = "サイコロの出目: " + diceString + "\n";

        // 役の判定
        string yaku = GetHand(diceValues);

        // 役の表示
        resultText.text += "役: " + yaku;
    }

    public string GetHand(int[] values) // メソッドをpublicに変更
    {
        if (values.Distinct().Count() == 1) // 全ての目が同じ
        {
            if (values[0] == 1) return "ピンゾロ";
            else return "アラシ";
        }
        else if (values.Contains(4) && values.Contains(5) && values.Contains(6))
        {
            return "シゴロ";
        }
        else if (values.Distinct().Count() == 2) // 2つの目が同じ
        {
            int remainingValue = values.GroupBy(v => v).Where(g => g.Count() == 1).First().Key;
            return remainingValue.ToString();
        }
        else if (values.OrderBy(v => v).SequenceEqual(new int[] { 1, 2, 3 }))
        {
            return "ヒフミ";
        }
        else if (values.Any(v => v == 0)) // サイコロがボウルからはみ出ている場合（0がはみ出た目を示す）
        {
            return "ションベン";
        }
        else
        {
            return "役なし";
        }
    }
}
