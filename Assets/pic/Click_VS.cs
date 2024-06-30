using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Click_VS : MonoBehaviour
{
    public void ClickVSButton()
    {
        SceneManager.LoadScene("game_VS");
    }
}