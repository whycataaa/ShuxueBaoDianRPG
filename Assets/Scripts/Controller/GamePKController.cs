using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// PK场景逻辑
/// </summary>
public class GamePKController : MonoBehaviour
{
    public GameObject panel_Skills, panel_SkillName, panel_Hurt, panel_Exit, panel_Result;//面板界面
    [SerializeField] private GameObject obj_Player, obj_NPC;//主角和NPC
    [SerializeField] private Text t_SkillName, t_Result;//释放技能显示的名称，游戏结束对话
    [SerializeField] private Button bt_Fight0, bt_Fight1, bt_Fight2, bt_Exit, bt_Cancel;//按钮
    [SerializeField] private string s_Scene;//要跳转的场景
    private bool b_PlayerTime = true, b_GameOver;//是否轮到玩家攻击，游戏是否结束

    void Start()
    {
        //监听按钮点击事件
        bt_Fight0.onClick.AddListener(delegate () { OnButtonClick(bt_Fight0); });
        bt_Fight1.onClick.AddListener(delegate () { OnButtonClick(bt_Fight1); });
        bt_Fight2.onClick.AddListener(delegate () { OnButtonClick(bt_Fight2); });
        bt_Exit.onClick.AddListener(delegate () { OnButtonClick(bt_Exit); });
        bt_Cancel.onClick.AddListener(delegate () { OnButtonClick(bt_Cancel); });
    }

    void Update()
    {
        //NPC恶意剑客死亡，弹出结束对话，3秒后返回主场景
        if (obj_NPC.GetComponent<NPCPKController>().f_hp <= 0)
        {
            SetPanelActive(panel_Result, null);
            t_Result.text = "主角：就这，就这，就这水平？\n恶意剑客：无耻小人，用此等手段胜我......你等着！\n主角：随时恭候";
            b_GameOver = true;
            Invoke("ChangeScene", 5);
            return;
        }
        //主角死亡，弹出结束对话，3秒后返回主场景
        if (obj_Player.GetComponent<AvatarPKController>().f_hp <= 0)
        {
            SetPanelActive(panel_Result, null);
            t_Result.text = "恶意剑客：就这，就这，就这水平？小娘子归我了！";
            b_GameOver = true;
            Invoke("ChangeScene", 5);
            return;
        }
        //按下esc键，游戏暂停，弹出退出界面
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPanelActive(panel_Exit, null);
        }
    }

    //点击技能按钮
    void OnButtonClick(Button btn)
    {
        if (!b_PlayerTime) { return; }//如果不是玩家进攻时间，返回

        if (btn == bt_Fight0|| btn == bt_Fight1|| btn == bt_Fight2)//技能0\1\2
        {
            obj_Player.GetComponent<AvatarPKController>().Attack();
            SetPanelActive(panel_SkillName, null);
            t_SkillName.text = btn.gameObject.GetComponentInChildren<Text>().text;
            b_PlayerTime = false;
            Invoke("NPCAttack", 3);
        }
        else if (btn == bt_Exit)//退出面板确定退出
        {
            Invoke("ChangeScene", 0);
        }
        else if (btn == bt_Cancel)//退出面板取消，轮到NPC攻击
        {
            SetPanelActive(null, null);
            NPCAttack();
        }
    }

    //轮到NPC攻击
    void NPCAttack()
    {
        if (b_GameOver) { return; }//游戏结束，返回
        obj_NPC.GetComponent<NPCPKController>().Attack();
        SetPanelActive(panel_SkillName, null);
        t_SkillName.text = "抓奶咸猪手";
        Invoke("PlayerAttackTime", 3);
    }

    //恢复到玩家进攻时间
    void PlayerAttackTime()
    {
        if (b_GameOver) { return; }//游戏结束，返回
        b_PlayerTime = true;
        SetPanelActive(panel_Skills, null);
    }

    //切换场景
    void ChangeScene()
    {
        SceneManager.LoadScene(s_Scene);
    }

    //设置显示某个面板
    public void SetPanelActive(GameObject panel1,GameObject panel2)
    {
        panel_Exit.SetActive(false);
        panel_Hurt.SetActive(false);
        panel_Skills.SetActive(false);
        panel_SkillName.SetActive(false);
        panel_Result.SetActive(false);

        if (panel1 != null) { panel1.SetActive(true); }
        if (panel2 != null) { panel2.SetActive(true); }
    }
}