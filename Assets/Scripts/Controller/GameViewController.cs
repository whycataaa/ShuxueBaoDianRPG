using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 视图管理器，用来全局管理所要显示的面板
/// </summary>
public class GameViewController : MonoBehaviour
{
    //所要显示的面板
    public GameObject panel_GameMain;
    public GameObject panel_Hurt, panel_Exit, panel_Die;
    public GameObject panel_Bag,panel_TaskMINI,panel_Task;
    [Header("对话系统")]
    [HideInInspector]public GameObject panel_ShowNPCName;
    [HideInInspector]public GameObject panel_Dialogue;
    [HideInInspector]public Text Text_Dialogue;
    [HideInInspector]public Text Text_NPCName;
    [HideInInspector]public Text Text_Name_Top;//显示在对话框顶的角色名
 

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        GameInfo.SetPlayerName("向南");
        //
        //
        panel_ShowNPCName=GameObject.Find("Image_ShowNPCBG");
        panel_Dialogue=GameObject.Find("Image_DialogueBG");
        Text_NPCName=GameObject.Find("Text_NPCName").GetComponent<Text>();
        Text_Name_Top=GameObject.Find("Text_Name_Top").GetComponent<Text>();
        Text_Dialogue=GameObject.Find("Text_Dialogue").GetComponent<Text>();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if(panel_Dialogue){panel_Dialogue.SetActive(false);}
        if(panel_ShowNPCName){panel_ShowNPCName.SetActive(false);}
    }

    //设置了两个可以同时显示的面板，如果只需要显示一个，则第二个填"null"
    public void SetPanelActive(GameObject panel1, GameObject panel2)
    {
        //将全部面板隐藏
        if (panel_GameMain) { panel_GameMain.SetActive(false); }
        if (panel_Hurt) { panel_Hurt.SetActive(false); }
        if (panel_Exit) { panel_Exit.SetActive(false); }
        if (panel_Die) { panel_Die.SetActive(false); }
        if (panel_Bag) { panel_Bag.SetActive(false); }
        if (panel_TaskMINI) { panel_TaskMINI.SetActive(false);}
        if (panel_Task) { panel_Task.SetActive(false);}

        //激活面板
        if (panel1 != null) { panel1.SetActive(true); }
        if (panel2 != null) { panel2.SetActive(true); }



    }
}