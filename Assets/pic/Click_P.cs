using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Click_P : MonoBehaviour
{
    public void ClickpauseButton()
    {
        SceneManager.LoadScene("pause");
    }
}