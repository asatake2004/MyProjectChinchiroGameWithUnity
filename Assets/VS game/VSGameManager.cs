using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VSGameManager : MonoBehaviour
{
    private int playerScore = 0;
    private int comScore = 0;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI comScoreText;
    public GameObject resultPanel; // 結果パネル
    public TextMeshProUGUI victorReview; // 結果表示用のTextMeshPro
    public DiceManagerVS diceManager; // DiceManagerVSの参照
    public TextMeshProUGUI turnIndicator; // ターンインジケーター
    public TextMeshProUGUI playerHandText; // プレイヤーの役表示
    public TextMeshProUGUI comHandText; // COMの役表示

    private bool isPlayerTurn = true; // プレイヤーのターンかどうか
    private int consecutiveNoRole = 0; // 連続で役なしの回数
    private bool playerTurnFinished = false; // プレイヤーのターンが終了したか
    private bool comTurnFinished = false; // COMのターンが終了したか

    void Start()
    {
        resultPanel.SetActive(false);
        UpdateTurnIndicator();

        if (diceManager == null)
        {
            Debug.LogError("DiceManagerVSオブジェクトがアサインされていません");
            return;
        }
    }

    void Update()
    {
        if (isPlayerTurn && Input.GetMouseButtonUp(0) && diceManager.CanThrowOrDeleteDice())
        {
            diceManager.ThrowAllDice();
            StartCoroutine(WaitForPlayerEvaluation());
        }
    }

    private IEnumerator WaitForPlayerEvaluation()
    {
        yield return new WaitForSeconds(2.5f);
        EvaluateTurn("player");
    }

    private IEnumerator ComTurnCoroutine()
    {
        while (!isPlayerTurn)
        {
            if (diceManager.CanThrowOrDeleteDice())
            {
                diceManager.ThrowAllDice();
                yield return new WaitForSeconds(2.5f);
                EvaluateTurn("com");
            }

            yield return null;
        }
    }

    private void EvaluateTurn(string turn)
    {
        string yaku = diceManager.chinchiro.GetHand(diceManager.diceValues);
        int score = CalculateScore(yaku);
        bool hasRole = yaku != "役なし";

        if (turn == "player")
        {
            playerHandText.text = "Playerの役: " + yaku;
            if (!hasRole)
            {
                consecutiveNoRole++;
                if (consecutiveNoRole >= 3)
                {
                    playerHandText.text = "Playerの役: 役なし";
                    playerTurnFinished = true;
                    ChangeTurn();
                }
            }
            else
            {
                playerScore = score;
                playerTurnFinished = true;
                ChangeTurn();
            }
        }
        else if (turn == "com")
        {
            comHandText.text = "Comの役: " + yaku;
            if (!hasRole)
            {
                consecutiveNoRole++;
                if (consecutiveNoRole >= 3)
                {
                    comHandText.text = "Comの役: 役なし";
                    comTurnFinished = true;
                    ChangeTurn();
                }
            }
            else
            {
                comScore = score;
                comTurnFinished = true;
                ChangeTurn();
            }
        }

        diceManager.ResetThrowOrDeleteCooldown();
        CheckGameEnd();
    }

    private void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        UpdateTurnIndicator();
        consecutiveNoRole = 0;

        if (isPlayerTurn)
        {
            StopCoroutine(ComTurnCoroutine());
        }
        else
        {
            StartCoroutine(ComTurnCoroutine());
        }
    }

    private void UpdateTurnIndicator()
    {
        turnIndicator.text = isPlayerTurn ? "Playerのターン" : "Comのターン";
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
        // プレイヤーとCOMのターンが両方終了した場合
        if (playerTurnFinished && comTurnFinished)
        {
            if (playerScore > comScore)
            {
                EndGame("あなたのかち!");
            }
            else if (comScore > playerScore)
            {
                EndGame("あなたのまけ!");
            }
            else
            {
                EndGame("ひきわけ!");
            }
        }
    }

    private void EndGame(string result)
    {
        resultPanel.SetActive(true);
        victorReview.text = result;
    }
}
