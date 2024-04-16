using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 玩家角色控制器
/// </summary>
public class AvatarController : MonoBehaviour
{
    //最大血量值，最大魔法值，攻击力，魔法值，防御力，魔仿力
    [SerializeField] private float f_HPMax = 100, f_MPMax = 100, f_Attack = 30, f_Magic = 50, f_ATKDefense = 5, f_MGDefense = 5;
    [SerializeField] private AudioClip audio_Attack, audio_Die;//攻击音效，死亡音效
    [SerializeField] private GameObject obj_attackPos, pre_Bullet;//攻击位置，子弹预制体
    private GameViewController viewController;
    private Animator anim;
    private bool b_CanAttack = true, b_CanMove = true;
    private float f_HP, f_MP;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // //鼠标锁定
        // Cursor.visible=true;
        // Cursor.lockState=CursorLockMode.Locked;
    }
    void Start()
    {

        f_HP = f_HPMax;
        f_MP = f_MPMax;
        anim = GetComponent<Animator>();
        viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateHP(f_HP, f_HPMax);
        // viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateMP(f_MP, f_MPMax);
    }

    void Update()
    {
        GameInfo.SetPos(transform.position);
        if (f_HP <= 0)
        {
            if (!viewController.panel_Die.activeSelf)
            {
                viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Die);
            }
            return;
        }
        //按下ESC键，锁定/解锁玩家移动
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            b_CanMove = !b_CanMove;
            if (b_CanMove) { CanMove(true); }
            else { CanMove(false); }

        }


        //按下左键，释放技能
        if (Input.GetMouseButtonDown(0) && b_CanAttack && GetComponent<PlayerInput>().enabled && GameInfo.GetKongFu())
        {
            StartCoroutine(Attack());
        }
    }

    //进入碰撞检测区域
    void OnTriggerEnter(Collider other)
    {
        //被怪物攻击，更新血量
        if (other.tag == "Attack_Enemy")
        {
            //伤害值=怪物攻击力-防御力
            int damage = other.GetComponent<BulletController>().i_Attack - (int)f_ATKDefense;
            //玩家有血量有体力
            if (f_HP > 0 && damage > 0)
            {
                viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Hurt);
                GetComponent<AudioSource>().clip = audio_Die;
                GetComponent<AudioSource>().Play();
                UpdateHP(-damage);
            }
        }
    }


    //攻击休眠
    IEnumerator Attack()
    {
        //攻击：锁定移动，锁定攻击，播放动画，播放声音，生成子弹
        //CanMove(false);
        b_CanAttack = false;
        anim.SetBool("Attack", true);
        GetComponent<AudioSource>().clip = audio_Attack;
        GetComponent<AudioSource>().Play();
        pre_Bullet.GetComponent<BulletController>().i_Attack = (int)f_Attack;
        Instantiate(pre_Bullet,obj_attackPos.transform.position, transform.rotation);
        //休眠1秒，恢复可攻击状态
        yield return new WaitForSeconds(1f);
        b_CanAttack = true;
        anim.SetBool("Attack", false);
        //CanMove(true);
    }

    //更新生命
    void UpdateHP(int hp)
    {
        f_HP += hp;
        if (f_HP < 0) { f_HP = 0; }
        else if (f_HP > f_HPMax) { f_HP = f_HPMax; }
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateHP(f_HP, f_HPMax);
    }

    // //更新体力
    // void UpdateMP(int mp)
    // {
    //     f_MP += mp;
    //     if (f_MP < 0) { f_MP = 0; }
    //     else if (f_MP > f_MPMax) { f_MP = f_MPMax; }
    //     viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateMP(f_MP, f_MPMax);
    // }

    //使玩家停止移动方法
    public void CanMove(bool canMove)
    {
        if (canMove) { GetComponent<PlayerInput>().enabled = true; }
        else { GetComponent<PlayerInput>().enabled = false; }
    }


}
