using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class SVChinchiroManager_com : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    private SVGameManager svGameManager;

    void Start()
    {
        svGameManager = FindObjectOfType<SVGameManager>();
        if (svGameManager == null)
        {
            Debug.LogError("SVGameManagerが見つかりませんでした。SVChinchiroManager_comが正しく動作しない可能性があります。");
        }
    }

    public void EvaluateDice(int[] diceValues)
    {
        string diceString = string.Join(", ", diceValues);
        resultText.text = "サイコロの出目: " + diceString + "\n";

        string yaku = GetHand(diceValues);
        resultText.text += "役: " + yaku;

        svGameManager.AddComScore(yaku);
    }

    public string GetHand(int[] values)
    {
        if (values.Distinct().Count() == 1)
        {
            if (values[0] == 1) return "ピンゾロ";
            else return "アラシ";
        }
        else if (values.Contains(4) && values.Contains(5) && values.Contains(6))
        {
            return "シゴロ";
        }
        else if (values.Distinct().Count() == 2)
        {
            int remainingValue = values.GroupBy(v => v).Where(g => g.Count() == 1).First().Key;
            return remainingValue.ToString();
        }
        else if (values.OrderBy(v => v).SequenceEqual(new int[] { 1, 2, 3 }))
        {
            return "ヒフミ";
        }
        else if (values.Any(v => v == 0))
        {
            return "ションベン";
        }
        else
        {
            return "役なし";
        }
    }
}
