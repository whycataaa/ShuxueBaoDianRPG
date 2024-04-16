using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCheck : MonoBehaviour
{
    [SerializeField]public ItemManager itemManager;
    [SerializeField]public  CurrentTask_SO currentTask_SO_Main;//当前主线任务数据
    [SerializeField]public  CurrentTask_SO currentTask_SO_Branch;//当前支线任务数据
    [SerializeField]public TaskViewManager taskViewManager;//任务的面板管理
    [SerializeField]private float searchPersonDistance;
    // [SerializeField]public GameController gameController;//获取人物数据 

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        if(currentTask_SO_Main.taskID!= 0)
        {
            //找人任务
            if(currentTask_SO_Main.taskType==TaskType.searchPerson)
            {
                var npcPos=currentTask_SO_Main.npc.transform.position;
                var playerPos=GameInfo.GetPos();
                // Debug.Log(npcPos);
                // Debug.Log(playerPos);
                // Debug.Log((npcPos-playerPos).magnitude);
                if((npcPos-playerPos).magnitude<searchPersonDistance)
                {
                    Debug.Log("havefindperson");
                    currentTask_SO_Main.finishedTask=true;
                    taskViewManager.taskListData.TaskDetailsList[currentTask_SO_Main.taskID-1].finishedTask=true;
                }
            }
            //收集任务
            if(currentTask_SO_Main.taskType==TaskType.collect)
            {
                var items=currentTask_SO_Main.items;
                foreach(var item in items)
                {
                    if(itemManager.FindItem(item)>0)
                    {
                        Debug.Log("havefinditem");
                        currentTask_SO_Main.finishedTask=true;
                        taskViewManager.taskListData.TaskDetailsList[currentTask_SO_Main.taskID-1].finishedTask=true;
                    }
                }
            }
            //击杀任务
            if(currentTask_SO_Main.taskType==TaskType.exterminate)
            {
                
            }
        }

        if(currentTask_SO_Branch.taskID!= 0)
        {
            if(currentTask_SO_Branch.taskType==TaskType.searchPerson)
            {
                var npcPos=currentTask_SO_Branch.npc.transform.position;
                if((GameInfo.GetPos()-npcPos).sqrMagnitude<searchPersonDistance)
                {
                    currentTask_SO_Branch.finishedTask=true;
                 //   taskViewManager.taskListData.TaskDetailsList[taskID].finishedTask=true;
                }
            }
            if(currentTask_SO_Branch.taskType==TaskType.collect)
            {
                var items=currentTask_SO_Branch.items;
                foreach(var item in items)
                {
                    if(itemManager.FindItem(item)>0)
                    {
                        currentTask_SO_Branch.finishedTask=true;
                    }
                }
            }
        }

    }


}
