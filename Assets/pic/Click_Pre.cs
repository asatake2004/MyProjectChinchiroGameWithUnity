using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class Click_Pre : MonoBehaviour
{
    public void ClickpreButton()
    {
        SceneManager.LoadScene("preparation");
    }
}