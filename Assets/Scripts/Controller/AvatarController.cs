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
        //鼠标锁定
        Cursor.visible=true;
        Cursor.lockState=CursorLockMode.Locked;
    }
    void Start()
    {

        f_HP = f_HPMax;
        f_MP = f_MPMax;
        anim = GetComponent<Animator>();
        viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateHP(f_HP, f_HPMax);
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateMP(f_MP, f_MPMax);
    }

    void Update()
    {

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
        //到达传送门，跳转场景
        /*else if (other.tag == "JumpDoor" && GameObject.Find("GameManager").GetComponent<GameController>().Get_Task())
        {
            GameObject.Find("GameManager").GetComponent<GameController>().JumpScene();
        }*/
        //箱子，获得奖励，完成任务并记录，跳转至胜利界面
        else if (other.tag == "Box")
        {
            viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Win);
        }
    }

    // //离开碰撞区域
    // void OnTriggerExit(Collider other)
    // {
    //     //遇到NPC，答题和做任务
    //     if (other.tag == "NPC")
    //     {
    //         viewController.SetPanelActive(viewController.panel_GameMain, null);
    //     }
    // }

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

    //更新体力
    void UpdateMP(int mp)
    {
        f_MP += mp;
        if (f_MP < 0) { f_MP = 0; }
        else if (f_MP > f_MPMax) { f_MP = f_MPMax; }
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateMP(f_MP, f_MPMax);
    }

    //使玩家停止移动方法
    public void CanMove(bool canMove)
    {
        if (canMove) { GetComponent<PlayerInput>().enabled = true; }
        else { GetComponent<PlayerInput>().enabled = false; }
    }

    //获得小物品
    public void GetItem(string itemName)
    {
        //更新小物品数据
        if (itemName == ConstString.item_HP)  
            GameInfo.Set_Item_HP(GameInfo.Get_Item_HP() + 1); 
        else if (itemName == ConstString.item_MP)
            GameInfo.Set_Item_MP(GameInfo.Get_Item_MP() + 1); 
        else if (itemName == ConstString.item_Attack) 
            GameInfo.Set_Item_Attack(GameInfo.Get_Item_Attack() + 1);
        else if (itemName == ConstString.item_Magic) 
            GameInfo.Set_Item_Magic(GameInfo.Get_Item_Magic() + 1); 
        else if (itemName == ConstString.item_ATKDefense) 
            GameInfo.Set_Item_ATKDefense(GameInfo.Get_Item_ATKDefense() + 1); 
        else if (itemName == ConstString.item_MGDefense) 
            GameInfo.Set_Item_MGDefense(GameInfo.Get_Item_MGDefense() + 1); 
        //刷新小物品界面
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateItems();
    }

    //使用小物品
    public void UseItem(string itemName)
    {
        string detail = "";
        //更新小物品数据
        if (itemName == ConstString.item_HP)//增加血量
        {
            if (GameInfo.Get_Item_HP() > 0)
            {
                UpdateHP(50);
                GameInfo.Set_Item_HP(GameInfo.Get_Item_HP() - 1);
                detail = "您使用了血量包补充血量，血量+50";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_MP)//增加体力
        {
            if (GameInfo.Get_Item_MP() > 0)
            {
                UpdateMP(50);
                GameInfo.Set_Item_MP(GameInfo.Get_Item_MP() - 1);
                detail = "您使用了体力包补充体力，体力+50";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_Attack)//增加攻击力
        {
            if (GameInfo.Get_Item_Attack() > 0)
            {
                f_Attack++;
                GameInfo.Set_Item_Attack(GameInfo.Get_Item_Attack() - 1);
                detail = "您服用了伟哥包补充战斗力，战斗力+1";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_Magic)//增加魔法值
        {
            if (GameInfo.Get_Item_Magic() > 0)
            {
                f_Magic++;
                GameInfo.Set_Item_Magic(GameInfo.Get_Item_Magic() - 1);
                detail = "您使用了超级伟哥补充魔法力，魔法力+1";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_ATKDefense)//增加普通防御力
        {
            if (GameInfo.Get_Item_ATKDefense() > 0)
            {
                f_ATKDefense++;
                GameInfo.Set_Item_ATKDefense(GameInfo.Get_Item_ATKDefense() - 1);
                detail = "您使用了避孕套增强防御力，普通防御力+1";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_MGDefense)//增加魔仿力
        {
            if (GameInfo.Get_Item_MGDefense() > 0)
            {
                f_MGDefense++;
                GameInfo.Set_Item_MGDefense(GameInfo.Get_Item_MGDefense() - 1);
                detail = "您服用了避孕药增强魔防力，魔仿力+1";
            }
            else { return; }
        }
        viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Detail);
        viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail(detail);
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateItems();
    }
}
