using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///控制玩家发射的子弹
/// </summary>
public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject obj_Explosion;//爆炸预制体
    [SerializeField] private int i_Speed = 60;//移动速度
    public int i_Attack = 10;//伤害值，默认10

    void Start()
    {
        //定时销毁
        Invoke("DestroyObject", 5);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * i_Speed);
    }

    //碰撞到其他物体，销毁产生爆炸效果
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Attack_Enemy")
        {
            if (other.tag == "Enemy"|| other.tag == "NPC") { return; }
            Instantiate(obj_Explosion, transform.position, Quaternion.identity);
            DestroyObject();
        }
        else if (gameObject.tag == "Attack_Avatar")
        {
            if (other.tag == "Player") { return; }
            Instantiate(obj_Explosion, transform.position, Quaternion.identity);
            DestroyObject();
        }
    }

    //销毁
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}