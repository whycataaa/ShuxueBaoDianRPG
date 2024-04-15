using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskData_SO", menuName = "Data/Test4/Task")]
public class TaskData_SO : ScriptableObject
{
    public List<TaskDetails> TaskDetailsList;
}


[System.Serializable]
public class TaskDetails
{
    [Header("任务数据，ID以1开头")]
    [Header("ID与头像")]
    public int taskID;//任务ID（必要）
    public string taskName;//任务名称
    public TaskType taskType;//任务类型
    [TextArea]
    public string taskDescription;//任务简介

    [TextArea]
    public string taskTarget;//任务目标

    [Header("任务报酬")]
    public string remuneration;


    [Header("任务状态")]
    public bool onTask;
    public bool finishedTask;


    [Header("具体的任务")]
    public GameObject npc;//要找的人
    public List<Item_SO> items;//要收集的物品
    public List<GameObject> enemyPrefabs;//要击杀的敌人


}


