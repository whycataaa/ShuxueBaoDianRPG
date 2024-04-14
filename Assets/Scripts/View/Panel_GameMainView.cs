using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏主场景界面
/// </summary>
public class Panel_GameMainView : MonoBehaviour
{
    //药水小物品按钮：血量，体力，普通攻击力，魔法攻击力，普通防御力，魔法防御力
    [SerializeField] private Button bt_HP, bt_MP, bt_Attack, bt_Magic, bt_ATKDefense, bt_MGDefense, bt_Exit, bt_MapTask;
    //药水小物品图标：血量，体力，普通攻击力，魔法攻击力，普通防御力，魔法防御力
    [SerializeField] private Text item_HP, item_MP, item_Attack, item_Magic, item_ATKDefense, item_MGDefense, coin, taskName;
    //武器图标锁1，2，3
    [SerializeField] private GameObject weaponLock1, weaponLock2, weaponLock3;
    //血量条、体力条
    [SerializeField] private GameObject img_HP, img_MP;
    //任务名称
    [SerializeField] private string[] s_TaskName;
    //血量条宽度，血量条高度，当前血量值，当前体力值
    private float f_Panel_HPWidth, f_Panel_HPHight, f_Panel_MPWidth, f_Panel_MPHight;   

    void Start()
    {
        //获取血量条长度、高度
        f_Panel_HPHight = img_HP.GetComponent<RectTransform>().rect.height;
        f_Panel_HPWidth = img_HP.GetComponent<RectTransform>().rect.width;
        f_Panel_MPHight = img_MP.GetComponent<RectTransform>().rect.height;
        f_Panel_MPWidth = img_MP.GetComponent<RectTransform>().rect.width;
        //注册按钮监听事件
        bt_HP.onClick.AddListener(delegate () { OnButtonClick(bt_HP); });
        bt_MP.onClick.AddListener(delegate () { OnButtonClick(bt_MP); });
        bt_Attack.onClick.AddListener(delegate () { OnButtonClick(bt_Attack); });
        bt_Magic.onClick.AddListener(delegate () { OnButtonClick(bt_Magic); });
        bt_ATKDefense.onClick.AddListener(delegate () { OnButtonClick(bt_ATKDefense); });
        bt_MGDefense.onClick.AddListener(delegate () { OnButtonClick(bt_MGDefense); });
        bt_Exit.onClick.AddListener(delegate () { OnButtonClick(bt_Exit); });
        bt_MapTask.onClick.AddListener(delegate () { OnButtonClick(bt_MapTask); });

        //更新一下面板。血量条、体力条由玩家主动请求更新
        UpdateWeapons();
        UpdateItems();
        UpdateCoin();
        UpdateTaskName();
    }

    //点击按钮事件
    void OnButtonClick(Button btn)
    {
        if(btn == bt_HP)//增加血量
        {
            GameObject.Find("Player").GetComponent<AvatarController>().UseItem(ConstString.item_HP);
        }
        else if(btn == bt_MP)//增加体力
        {
            GameObject.Find("Player").GetComponent<AvatarController>().UseItem(ConstString.item_MP);
        }
        else if (btn == bt_Attack)//增加攻击力
        {
            GameObject.Find("Player").GetComponent<AvatarController>().UseItem(ConstString.item_Attack);
        }
        else if(btn == bt_Magic)//增加魔法值
        {
            GameObject.Find("Player").GetComponent<AvatarController>().UseItem(ConstString.item_Magic);
        }
        else if (btn == bt_ATKDefense)//增加普通防御力
        {
            GameObject.Find("Player").GetComponent<AvatarController>().UseItem(ConstString.item_ATKDefense);
        }
        else if (btn == bt_MGDefense)//增加魔仿力
        {
            GameObject.Find("Player").GetComponent<AvatarController>().UseItem(ConstString.item_MGDefense);
        }
        else if(btn == bt_Exit)//退出按钮
        {
            GameViewController viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
            viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Exit);
            Time.timeScale = 0;
        }
        else if (btn == bt_MapTask)//点击任务地图按钮
        {
            GameViewController viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
            viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_MapTask);
            Time.timeScale = 0;
        }
    }

    //更新血量条
    public void UpdateHP(float hp, float hpMax)
    {
        img_HP.GetComponent<RectTransform>().sizeDelta = new Vector2(hp / hpMax * f_Panel_HPWidth, f_Panel_HPHight);
    }

    //更新体力条
    public void UpdateMP(float mp, float mpMax)
    {
        img_MP.GetComponent<RectTransform>().sizeDelta = new Vector2(mp / mpMax * f_Panel_MPWidth, f_Panel_MPHight);
    }

    //更新武器列表
    public void UpdateWeapons()
    {
        weaponLock1.SetActive(true);
        weaponLock2.SetActive(true);
        weaponLock3.SetActive(true);
        if (GameInfo.GetLevel() >= 1) { weaponLock1.SetActive(false); }
        if (GameInfo.GetLevel() >= 2) { weaponLock2.SetActive(false); }
        if (GameInfo.GetLevel() >= 3) { weaponLock3.SetActive(false); }
    }

    //更新小物品列表
    public void UpdateItems()
    {
        item_HP.text = GameInfo.Get_Item_HP().ToString();
        item_MP.text = GameInfo.Get_Item_MP().ToString();
        item_Attack.text = GameInfo.Get_Item_Attack().ToString();
        item_Magic.text = GameInfo.Get_Item_Magic().ToString();
        item_ATKDefense.text = GameInfo.Get_Item_ATKDefense().ToString();
        item_MGDefense.text = GameInfo.Get_Item_MGDefense().ToString();
    }

    //更新金币
    public void UpdateCoin()
    {
        coin.text = GameInfo.GetCoin().ToString();
    }

    //更新任务名称
    public void UpdateTaskName()
    {
        taskName.text = s_TaskName[GameInfo.GetTask()];//显示剧情任务名称
    }
}