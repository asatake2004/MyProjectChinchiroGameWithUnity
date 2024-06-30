using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Click_S : MonoBehaviour
{
    public void ClickStartButton()
    {
        SceneManager.LoadScene("game");
    }
}