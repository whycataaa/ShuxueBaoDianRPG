using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 答题界面，单项选择题
/// </summary>
public class Panel_QuestionView : MonoBehaviour
{
    private GameViewController viewController;
    [SerializeField] private Toggle[] tog_Answer;//题目候选答案列表
    [SerializeField] private Button bt_Answer;//提交问题按钮
    [SerializeField] private int i_Answer;//正确答案索引
    [SerializeField] private string s_0, s_1, s_2;//共两个答案：答案1、答案2、不做选择，不同选择结果的弹窗
    private string s_Talk;
    private bool b_Right;//相应选择是否正确标识

    void Start()
    {
        viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
        //注册单选按钮监听事件
        for (int i = 0; i < tog_Answer.Length; i++)
        {
            int answer = i;
            tog_Answer[i].onValueChanged.AddListener((bool isOn) => { OnClickAnswerToggle(isOn, answer); });
            //初始化单选按钮
            OnClickAnswerToggle(false, -1);
            tog_Answer[i].isOn = false;
        }
        //注册按钮监听事件
        bt_Answer.onClick.AddListener(delegate { OnClickButtonAnswer(); });
    }

    //点击单选按钮：是否选择，选择的选项索引
    void OnClickAnswerToggle(bool isOn, int answerIndex)
    {
        //如果答案的数值大于选项的长度，说明是不能选择，选择则错，不选则对
        if (i_Answer >= tog_Answer.Length)
        {
            if (!isOn)//没有做选择，则正确，更新选择后对话内容，本任务完成，任务计数+1
            {
                b_Right = true;
                s_Talk = s_2;
            }
            else//做选择，则错误，更新选择后对话内容
            {
                b_Right = false;
                if (answerIndex == 0) { s_Talk = s_0; }
                else if (answerIndex == 1) { s_Talk = s_1; }
            }
        }
        //如果答案的数值小于等于选项的长度，说明正确答案在某个选项中，选中则对，选不中则错
        else
        {
            if (!isOn)//没有做选择，则错误，更新选择后对话内容
            {
                b_Right = false;
                s_Talk = s_2;
            }
            else//做选择，根据选择项进一步判断对错
            {
                //选中则对，选不中则错，本任务完成，任务计数+1
                if (answerIndex == i_Answer)
                {
                    b_Right = true;
                }
                else { b_Right = false; }
                //更新选择后对话内容
                if (answerIndex == 0) { s_Talk = s_0; }
                else if (answerIndex == 1) { s_Talk = s_1; }
            }
        }
    }

    //点击提交答案按钮，在这里回答正确完成任务，任务升级，任务级别为2则视为修炼成武功
    void OnClickButtonAnswer()
    {
        if (b_Right)
        {
            //任务级别为2则视为修炼成武功
            if (GameInfo.GetTask() == 1) { GameInfo.SetKongFu(1); }
            //如果是任务3，那么答题正确之后进入PK模式
            if (GameInfo.GetTask() == 3)
            {
                //GameObject.Find("GameManager").GetComponent<GameController>().GoPKScene();
                return;
            }
            //回答正确完成任务，任务升级
            GameInfo.SetTask(GameInfo.GetTask() + 1);
            //显示“详情界面”对话框，结束当前任务
            viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Detail);
            viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail(s_Talk);
        }
        else//如果回答错误，则返回出生点
        {
            Invoke("ReturnBornPos", 3);
            GameObject.Find("Player").GetComponent<AvatarController>().CanMove(false);
            //显示“详情界面”对话框，结束当前任务
            viewController.SetPanelActive(viewController.panel_Hurt, viewController.panel_Detail);
            viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail(s_Talk);
        }
    }

    //返回出生点
    void ReturnBornPos()
    {
        GameObject.Find("GameManager").GetComponent<GameController>().ReturnBornPos();
    }
}