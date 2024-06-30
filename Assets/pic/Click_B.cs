using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Click_B : MonoBehaviour
{
    public void ClickBackButton()
    {
        SceneManager.LoadScene("start");
    }
}