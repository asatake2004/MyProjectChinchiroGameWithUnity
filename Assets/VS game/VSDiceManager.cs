using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSDiceManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public DiceCheckA dice1;
    public DiceCheckA dice2;
    public DiceCheckA dice3;
    public SVChinchiroManager chinchiro;
    private int[] rotateX = new int[3];
    private int[] rotateY = new int[3];
    private int[] rotateZ = new int[3];
    private Vector3[] positions = new Vector3[3];
    private int[] diceValues = new int[3];

    private bool canThrowOrDeleteDice = true;
    private List<GameObject> currentDice = new List<GameObject>();

    void Start()
    {
        positions[0] = new Vector3(5, 5, 0);
        positions[1] = new Vector3(4.5f, 5, 0);
        positions[2] = new Vector3(4, 5, 0);

        if (dice1 == null || dice2 == null || dice3 == null)
        {
            Debug.LogError("DiceCheckAオブジェクトがアサインされていません");
            return;
        }

        if (chinchiro == null)
        {
            Debug.LogError("ChinchiroManagerオブジェクトがアサインされていません");
            return;
        }
    }

    void Update()
    {
        if (canThrowOrDeleteDice && Input.GetMouseButtonUp(0))
        {
            ThrowAllDice();
            StartCoroutine(EvaluateDiceAfterDelay());
        }
    }

    public void ThrowAllDice()
    {
        ClearCurrentDice();

        for (int i = 0; i < 3; i++)
        {
            rotateX[i] = Random.Range(0, 360);
            rotateY[i] = Random.Range(0, 360);
            rotateZ[i] = Random.Range(0, 360);

            GameObject dice = Instantiate(prefabs[i], positions[i], Quaternion.identity);
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            rb.AddForce(-transform.right * 250);
            dice.transform.Rotate(rotateX[i], rotateY[i], rotateZ[i]);

            if (i == 0) dice1 = dice.GetComponent<DiceCheckA>();
            if (i == 1) dice2 = dice.GetComponent<DiceCheckA>();
            if (i == 2) dice3 = dice.GetComponent<DiceCheckA>();

            currentDice.Add(dice);
        }

        canThrowOrDeleteDice = false;
        StartCoroutine(ResetThrowOrDeleteCooldown());
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
        yield return new WaitForSeconds(2.5f);
        EvaluateDice();
    }

    private void EvaluateDice()
    {
        diceValues[0] = dice1.GetValue();
        diceValues[1] = dice2.GetValue();
        diceValues[2] = dice3.GetValue();

        chinchiro.EvaluateDice(diceValues);
    }

    private IEnumerator ResetThrowOrDeleteCooldown()
    {
        yield return new WaitForSeconds(3f);
        canThrowOrDeleteDice = true;
    }
}
