using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SVGameManager : MonoBehaviour
{
    private int playerScore = 0;
    private int comScore = 0;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI comScoreText;
    public GameObject resultPanel; // 結果パネル
    public TextMeshProUGUI victorReview; // 結果表示用のTextMeshPro

    void Start()
    {
        // 初期化時にスコアを表示する
        UpdatePlayerScoreText();
        UpdateComScoreText();

        // 結果パネルを非表示にする
        resultPanel.SetActive(false);
    }

    public void AddPlayerScore(string yaku)
    {
        int scoreToAdd = CalculateScore(yaku);
        playerScore += scoreToAdd;
        Debug.Log("Player Score added: " + scoreToAdd + ", Total: " + playerScore);
        UpdatePlayerScoreText();
        CheckGameEnd();
    }
    
    public void AddComScore(string yaku)
    {
        int scoreToAdd = CalculateScore(yaku);
        comScore += scoreToAdd;
        Debug.Log("Com Score added: " + scoreToAdd + ", Total: " + comScore);
        UpdateComScoreText();
        CheckGameEnd();
    }

    private void UpdatePlayerScoreText()
    {
        playerScoreText.text = "Player Score: " + playerScore.ToString();
        Debug.Log("Player Score Text updated: " + playerScoreText.text);
    }

    private void UpdateComScoreText()
    {
        comScoreText.text = "Com Score: " + comScore.ToString();
        Debug.Log("Com Score Text updated: " + comScoreText.text);
    }

    private int CalculateScore(string yaku)
    {
        switch (yaku)
        {
            case "ピンゾロ":
                return 100;
            case "アラシ":
                return 50;
            case "シゴロ":
                return 20;
            case "ヒフミ":
                return -30;
            case "ションベン":
                return -20;
            default:
                if (int.TryParse(yaku, out int value))
                {
                    return value;
                }
                else
                {
                    return 0;
                }
        }
    }

    private void CheckGameEnd()
    {
        if (playerScore >= 100)
        {
            EndGame("あなたのかち!");
        }
        else if (comScore >= 100)
        {
            EndGame("あなたのまけ!");
        }
    }

    private void EndGame(string result)
    {
        // 結果パネルを表示して、結果を設定する
        resultPanel.SetActive(true);
        victorReview.text = result;

        Time.timeScale = 0;
    }
}
