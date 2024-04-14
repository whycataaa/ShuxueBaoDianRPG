using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家任务面板(显示一个未完成的任务)
/// </summary>
public class Panel_TaskView : MonoBehaviour
{
    [SerializeField] private Text t_Task;

    //更新对话弹窗文字
    public void UpdateTask(string npcTalk)
    {
        t_Task.text =npcTalk;
    }
}