using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //2つのPanelを格納する変数
    //インスペクターウィンドウからゲームオブジェクトを設定する
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject PausePanel;
 
 
    // Start is called before the first frame update
    void Start()
    {
        //BackToGameメソッドを呼び出す
        BackToGame();
    }
 
 
    //GamePanelでPauseButtonが押されたときの処理
    //PausePanelをアクティブにする
    public void SelectPauseDescription()
    {
        GamePanel.SetActive(false);
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
 
 
    //DescriptionPanelでBackButtonが押されたときの処理
    //GamePanelをアクティブにする
    public void BackToGame()
    {
        GamePanel.SetActive(true);
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}