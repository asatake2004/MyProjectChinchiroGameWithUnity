using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Click_R : MonoBehaviour
{
    public void ClickRestartButton()
    {
        SceneManager.LoadScene("game");
    }
}