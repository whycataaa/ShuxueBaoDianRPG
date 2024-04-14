using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPKController : MonoBehaviour
{
    [SerializeField] private GameObject img_HP, obj_AttackPos, pre_Bullet;
    private Animator anim;//主角动作
    private GamePKController pkController;
    //血量条宽度，血量条高度，当前血量值，当前体力值
    private float f_Panel_HPWidth, f_Panel_HPHight, f_hpMax = 100;
    public float f_hp;

    void Start()
    {
        anim = GetComponent<Animator>();
        pkController = GameObject.Find("GameManager").GetComponent<GamePKController>();
        f_Panel_HPHight = img_HP.GetComponent<RectTransform>().rect.height;
        f_Panel_HPWidth = img_HP.GetComponent<RectTransform>().rect.width;
        f_hp = f_hpMax;
        UpdateHP(f_hp);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack_Enemy")
        {
            f_hp -= 60;
            UpdateHP(f_hp);
            pkController.SetPanelActive(pkController.panel_SkillName, pkController.panel_Hurt);
        }
    }

    //更新血量条
    void UpdateHP(float hp)
    {
        img_HP.GetComponent<RectTransform>().sizeDelta = new Vector2(hp / f_hpMax * f_Panel_HPWidth, f_Panel_HPHight);
        //如果玩家死亡，播放死亡动画
        if (f_hp <= 0)
        {
            anim.Play("Death", -1, 0F);
        }
    }

    //攻击动画
    public void Attack()
    {
        GetComponent<AudioSource>().Play();
        anim.Play("Attack", -1, 0F);
        Instantiate(pre_Bullet, obj_AttackPos.transform.position, obj_AttackPos.transform.rotation);
    }
}