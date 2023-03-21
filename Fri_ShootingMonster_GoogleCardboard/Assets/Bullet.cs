using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("多少時間以後要刪除物件")]
    public float DeleteTime;
    [Header("子彈移動速度")]
    public float Speed;
    [Header("把打到特效")]
    public GameObject HitObj;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DeleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.forward = (0, 0, 1); Vector3.up = (0, 1, 0)
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    //OnTriggerEnter：兩個物件相撞就觸發一次程式
    //OnTriggerStay：兩個物件一值相撞，程式就一直觸發，直到物件分開為止
    //OnTriggerExit：兩個物件相撞且分開才會觸發一次程式

    private void OnTriggerEnter(Collider hit) 
    {
        if(hit.GetComponent<Collider>().tag == "Monster")
        {
            //特效
            Instantiate(HitObj, hit.transform.position, transform.rotation);
            //聲音

            //怪物扣血，當子彈碰到怪物就呼叫怪物身上的腳本進行扣血
            hit.GetComponent<Monster>().HurtMonster();
            //子彈消失
            Destroy(gameObject);
        }
    }
}
