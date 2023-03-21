using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextGame()
    {
        //跳關卡Application.LoadGameLevel("下一個場景的名稱")
        //Application.loadedLevel("讀取當前場景的ID值")
        Application.LoadLevel("Game");
    }

    public void QuitGame()
    {
        //關閉遊戲exe或APP
        Application.Quit();
    }
}
