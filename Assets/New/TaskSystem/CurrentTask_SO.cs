using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskData_SO", menuName = "Data/Test4/CurrentTask")]
public class CurrentTask_SO : ScriptableObject
{
    [Header("任务数据")]
    public string taskID;
    public string taskName;
    public TaskType taskType;
    [TextArea]
    public string taskDescription;//任务简介

    public string taskTarget;

    [Header("任务报酬")]
    public string remuneration;

    [Header("任务状态")]
    public bool finishedTask;//是否启动

    [Header("具体的任务")]
    public GameObject npc;//要找的人
    public List<Item_SO> items;//要收集的物品
    public List<GameObject> enemyPrefabs;//要击杀的敌人
    /// <summary>
    /// 清空列表内容
    /// </summary>
    public void ResetTaskData()
    {
        taskID = string.Empty;
        taskName = string.Empty;
        taskType = TaskType.Default; // 你需要根据实际情况设置默认值
        taskDescription = string.Empty;
        taskTarget=string.Empty;
        remuneration = "";
        finishedTask = false;
        npc=null;
        items.Clear();
        enemyPrefabs.Clear();
    }

}
