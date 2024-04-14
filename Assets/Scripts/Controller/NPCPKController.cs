using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCPKController : MonoBehaviour
{
    [SerializeField] private GameObject obj_AttackPos, pre_Bullet;
    [SerializeField] private Slider slider;
    private Animator anim;//NPC动作
    //血量条宽度，血量条高度，当前血量值，当前体力值
    public float f_hp, f_hpMax = 100;
    private bool b_Die;

    void Start()
    {
        anim = GetComponent<Animator>();
        f_hp = f_hpMax;
    }

    private void Update()
    {
        //NPC已经是死亡状态，返回
        if (b_Die) { return; }
        //监听slider的数值变化，让血量值等于slider的数值百分比
        f_hp = slider.value * f_hpMax;
        //如果NPC死亡，播放死亡动画
        if (f_hp <= 0)
        {
            anim.Play("Death_01", -1, 0F);
            b_Die = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack_Avatar")
        {
            f_hp -= 10;
            slider.value = f_hp / f_hpMax;
        }
    }

    //攻击动画
    public void Attack()
    {
        GetComponent<AudioSource>().Play();
        int n = Random.Range(1, 6);
        if (n == 1) { anim.Play("Attack_01", -1, 0F); }
        else if (n == 2) { anim.Play("Attack_02", -1, 0F); }
        else if (n == 3) { anim.Play("Attack_03", -1, 0F); }
        else if (n == 4) { anim.Play("Attack_04", -1, 0F); }
        else if (n == 5) { anim.Play("Attack_05", -1, 0F); }
        Instantiate(pre_Bullet, obj_AttackPos.transform.position, obj_AttackPos.transform.rotation);
    }
}