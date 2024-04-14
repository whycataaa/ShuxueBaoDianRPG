using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NPC管理器，主要处理和玩家之间的对话
/// </summary>
public class NPCController : MonoBehaviour
{
//    [SerializeField] private bool b_HaveTalk;//是否有对话
    [SerializeField] private int i_Task;//NPC任务序号
    [SerializeField] private TextAsset textFile;//对话的文本
    [SerializeField]private GameViewController viewController;
    [SerializeField]private List<string> textList=new List<string>();//对话列表
    [SerializeField]private int index;//每按一下F数值+1，i_Count == s_Talk.Length时对话结束
    [SerializeField]private bool textFinished=true;//对话是否结束
    [SerializeField]private string NPC_Name;//Npc名字
    [SerializeField]private float textSpeed;//文本速度
    [SerializeField]private TaskManager taskManager;
    [SerializeField]private BagGridControl bagGridControl;
    bool haveTask;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        haveTask=true;
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        bagGridControl=FindObjectOfType(typeof(BagGridControl)) as BagGridControl;
        viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
        taskManager=GameObject.Find("GameManager").GetComponent<TaskManager>();
        textFinished=true;
        NPC_Name=this.gameObject.name;
        GetTextFromFile(textFile);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // TaskDetect();

    }
    /// <summary>
    /// 任务检测
    /// </summary>
    // private void TaskDetect()
    // {
    //    bagGridControl.FindItemFromBag(a);
    // }

    IEnumerator SetTextUI()
    {
        textFinished=false;
        viewController.Text_Dialogue.text="";
        if(textList[index].Trim()==NPC_Name)
        {
            viewController.Text_Name_Top.text=NPC_Name;
            index++;
        }

        if(textList[index].Trim()==GameInfo.GetPlayerName())
        {
            viewController.Text_Name_Top.text=GameInfo.GetPlayerName();
            index++;
        }

        for(int i=0;i<textList[index].Length;i++)
        {
            viewController.Text_Dialogue.text+=textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }

        textFinished=true;
        index++;

    }

    /// <summary>
    ///将文本文件转为字符串数组
    /// </summary>
    /// <param name="textFile"></param>
    private void GetTextFromFile(TextAsset textFile)
    {
        textList.Clear();
        index=0;

        var lineData=textFile.text.Split('\n');//按每行切分

        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    // void Start()
    // {
        
    //     // if(b_HaveTalk)
    //     // {
    //     //     taskView = viewController.panel_Task.GetComponent<Panel_TaskView>();
    //     //     // bt_Task.onClick.AddListener(delegate () { OnButtonTalk(); });
    //     // }
    // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }
        //靠近NPC显示NPC名字信息
        ShowNPCName();
    }
    //碰撞检测，只有当前任务序号和i_Task相等才会触发任务对话模式，否则就是一句敷衍了事的对话
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") { return; }
        //玩家按下F交互
        if(Input.GetKeyDown(KeyCode.F)&&index==textList.Count)
        {
            if(haveTask)
            {
                taskManager.CopyTaskButton(i_Task);
                taskManager.TaskDataDisplay();
            }

            viewController.panel_Dialogue.SetActive(false);
            GameObject.Find("Player").GetComponent<AvatarController>().CanMove(true);//取消停止玩家移动
            index=0;
            return;
        }
        if(Input.GetKeyDown(KeyCode.F)&&textFinished)
        {
            GameObject.Find("Player").GetComponent<AvatarController>().CanMove(false);//停止玩家移动
            DisShowNPCName();
            //显示对话框
            viewController.panel_Dialogue.SetActive(true);
            StartCoroutine(SetTextUI());
        }


        // b_Active = true;
        // //如果该NPC屌毛事多，非得整上几句屁话
        // if (b_HaveTalk)
        // {
        //     GameObject.Find("Player").GetComponent<AvatarController>().CanMove(false);
        //     //任务级别是从1开始：1，2，3，4，如果NPC任务序号不等于当前任务级别，抛出s_Talk[0]的对话
        //     if (i_Task != GameInfo.GetTask())
        //     {
        //         viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Task);
        //         taskView.UpdateTask(s_Talk[0]);
        //         i_Count = s_Talk.Length;
        //     }
        //     //任务级别是从1开始：1，2，3，4，如果NPC任务序号等于当前任务级别，则使用_Talk[1、2、3]对话
        //     else
        //     {
        //         viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Task);
        //         taskView.UpdateTask(s_Talk[1]);
        //         i_Count = 1;
        //     }
        //     return;
        // }
        // //如果该NPC屌毛屁都不放，上来就让你做选择
        // if (b_HaveDaTi && i_Task == GameInfo.GetTask())
        // {
        //     DaTi();
        //     GameObject.Find("Player").GetComponent<AvatarController>().CanMove(false);
        // }
    }

    private void ShowNPCName()
    {
        viewController.panel_ShowNPCName.SetActive(true);
        viewController.Text_NPCName.text=NPC_Name;
    }
    private void DisShowNPCName()
    {
        viewController.panel_ShowNPCName.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        DisShowNPCName();
        //b_Active = false;
    }

    // //点击对话框按钮
    // void OnButtonTalk()
    // {
    //     //如果不是对话状态，返回
    //     if (!b_Active) { return; }
    //     //如果NPC任务序号不等于当前任务级别，关闭对话框
    //     if (i_Task != GameInfo.GetTask())
    //     {
    //         viewController.SetPanelActive(viewController.panel_GameMain, null);
    //         GameObject.Find("Player").GetComponent<AvatarController>().CanMove(true);
    //         //如果该NPC是药师，随机获得药物
    //         if(gameObject.name== "YaoShi")
    //         {
    //             //随机获得奖励
    //             int i = Random.Range(0, 4);
    //             if (i == 0)
    //             {
    //                 GameObject.Find("Player").GetComponent<AvatarController>().GetItem(ConstString.item_HP);
    //                 viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail("获得伟哥金枪不倒丸，可以补充血量");
    //             }
    //             else if (i == 1)
    //             {
    //                 GameObject.Find("Player").GetComponent<AvatarController>().GetItem(ConstString.item_MP);
    //                 viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail("获得印度神油，可以补充体力");
    //             }
    //             else if (i == 2)
    //             {
    //                 GameObject.Find("Player").GetComponent<AvatarController>().GetItem(ConstString.item_Attack);
    //                 viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail("获得电动狼牙棒，使体力输出更为猛烈");
    //             }
    //             else if (i == 3)
    //             {
    //                 GameObject.Find("Player").GetComponent<AvatarController>().GetItem(ConstString.item_Magic);
    //                 viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail("获得充气娃娃，使魔法输出更猛烈");
    //             }
    //             viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Detail);
    //         }
    //     }
    //     //如果NPC任务序号等于当前任务级别，继续对话，对话结束弹出问题选项
    //     else
    //     {
    //         i_Count++;
    //         if (i_Count < s_Talk.Length)
    //         {
    //             taskView.UpdateTask(s_Talk[i_Count]);
    //         }
    //         //对话结束，如果有答题功能进入答题模式
    //         else if (i_Count >= s_Talk.Length)
    //         {
    //             i_Count = 0;
    //             if (b_HaveDaTi) { DaTi(); }
    //         }
    //     }
    // }

    // //进入答题环节，根据任务级别，显示答题面板，有的NPC有答题互动，有的则没有
    // void DaTi()
    // {
    //     //把答题面板显示出来
    //     Panel_DaTiView daTiView;
    //     daTiView = viewController.panel_DaTi.GetComponent<Panel_DaTiView>();
    //     viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_DaTi);

    //     //根据当前任务进展情况，显示对应的选择题目，答对就算了，答错要你狗命
    //     if (GameInfo.GetTask() == 0) { daTiView.SetPanelActive(daTiView.panel_Question[0]); }
    //     else if (GameInfo.GetTask() == 1) { daTiView.SetPanelActive(daTiView.panel_Question[1]); }
    //     else if (GameInfo.GetTask() == 2) { daTiView.SetPanelActive(daTiView.panel_Question[2]); }
    //     else if (GameInfo.GetTask() == 3) { daTiView.SetPanelActive(daTiView.panel_Question[3]); }
    // }


}