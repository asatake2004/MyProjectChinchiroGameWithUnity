using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Click_SV : MonoBehaviour
{
    public void ClickStartButton()
    {
        SceneManager.LoadScene("game_VS_Speed");
    }
}