using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    [Header("生成子彈物件")]
    public GameObject Bullet;
    [Header("生成子彈位置")]
    public GameObject CreatePos;
    [Header("多少時間生成一個子彈")]
    public float SetTime;
    //判斷是否有看到怪物才產生子彈
    bool isLook;

    [Header("自動顯示怪物總數量")]
    public int TotalMonster;
    [Header("怪物死亡數量")]
    public int MonsterDeadNum;
    [Header("顯示怪物數量的文字")]
    public Text MonsterNumText;

    [Header("勝利UI物件")]
    public GameObject WinObj;
    [Header("遊戲首頁的名稱")]
    public string MenuSceneName;

    [Header("玩家的血量")]
    public float TotalPlyerHP;
    [Header("玩家扣血量")]
    public float ReducePlyerHP;
    [Header("玩家的血條")]
    public Image PlayerHPBar;
    [Header("失敗的UI物件")]
    public GameObject LossObj;

    //暫存設定玩家的總血量
    float SaveTotalPlayerHP;

    // Start is called before the first frame update
    void Start()
    {
        //找尋場景上所有Monster標籤物件的數量
        TotalMonster = GameObject.FindGameObjectsWithTag("Monster").Length;
        //InvokeRepeating("要呼叫的function", 遊戲執行後第一次等待多少時間才可以執行, 第二次第三次執行的時間)
        InvokeRepeating("Create_Bullet", SetTime, SetTime);

        //暫存的總血量數值 = 自訂的玩家總血量
        SaveTotalPlayerHP = TotalPlyerHP;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterNumText.text = "剩餘怪物數量：" + MonsterDeadNum + "/" + TotalMonster;
        //如果怪物死亡數量與總數量相等
        if(MonsterDeadNum == TotalMonster)
        {
            //開啟遊戲勝利UI物件
            WinObj.SetActive(true);
        }
    }

    public void BackMenu()
    {
        Application.LoadLevel(MenuSceneName);
    }

    void Create_Bullet()
    {
        //當看到怪物才可以生成子彈
        if(isLook)
        {
            //Instantiate(要動態生成的物件, 要生成的位置, 生成出來的物件角度)
            Instantiate(Bullet, CreatePos.transform.position, CreatePos.transform.rotation);
        }
    }

    public void LookMonster()
    {
        isLook = true;
    }

    public void UnLookMonster()
    {
        isLook = false;
    }


    public void HurtPlayer()
    {
        //玩家扣血
        TotalPlyerHP -= ReducePlyerHP;
        //玩家的血條
        PlayerHPBar.fillAmount = TotalPlyerHP / SaveTotalPlayerHP;
        
        //玩家的血量為零 = 死亡
        if(PlayerHPBar.fillAmount <= 0)
        {
            //開啟遊戲結束的UI物件
            LossObj.SetActive(true);
            //玩家不能有丟武器的動作
            Camera.main.GetComponent<GvrPointerPhysicsRaycaster>().enabled = false;
        }

    }
}

