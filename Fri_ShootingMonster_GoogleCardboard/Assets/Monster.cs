using System.Collections;
using System.Collections.Generic;
//import unity 程式庫
using UnityEngine;
//import unity AI程式庫
using UnityEngine.AI;
using UnityEngine.UI;

//類別名稱與程式檔名要一致，否則無法將腳本拖曳給物件
public class Monster : MonoBehaviour
{
    [Header("擺放怪物")]
    public NavMeshAgent monster;
    
    [Header("擺放攝影機")]
    public GameObject Camera;
    
    [Header("輸入攝影機的名字")]
    public string CameraName;

    [Header("怪物和玩家剩下多少距離值時，怪物停止走動")]
    public float StopDis;


    [Header("怪物的總血量")]
    public float TotalHP;
    //程式計算怪物的血量
    float HP;
    [Header("怪物被打到後扣除的血量值")]
    public float HurtHP;
    [Header("怪物的血條")]
    public Image HPBar;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find(CameraName);
        //程式中計算的血樣值 = 屬性面板中計算的血量數值
        HP = TotalHP;
    }

    // Update is called once per frame
    void Update()
    {
        //如果怪物和玩家之間的距離值>自設定的設定值
        if(Vector3.Distance(monster.transform.position, Camera.transform.position) > StopDis)
        {
            //怪物最終的座標位置 = 攝影機的位置
            monster.destination = Camera.transform.position;
        }
        else
        {
            //停止巡路
            monster.Stop();
            //讓怪物的動切換成攻擊動畫
            GetComponent<Animator>().SetBool("Attack", true);
            //怪物面向著玩家攝影機
            transform.LookAt(Camera.transform.position);
        }
    }

    public void HurtMonster()
    {
        //當東西打到怪物時，怪物進行扣血
        HP -= HurtHP;
        //將HP數值轉換成0-1的小數值，目前怪物的血量/在屬性面板中的血量
        HPBar.fillAmount = HP/TotalHP;
        //當HP<=0時，怪物死亡
        if(HP <= 0)
        {
            //觸發死亡動畫
            GetComponent<Animator>().SetTrigger("Die");
            // Collider 進行關閉
            GetComponent<Collider>().enabled = false;
            //怪物死亡數量增加
            GameObject.Find("GM").GetComponent<GM>().MonsterDeadNum++;
        }
    }

    public void HurtPlayer()
    {
        //當怪物做到攻擊玩家的動作時，呼叫 GM 物件上的 GM 腳本裡的 HurtPlayer function
        GameObject.Find("GM").GetComponent<GM>().HurtPlayer();
    }
}
