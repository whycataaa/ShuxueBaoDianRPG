using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCheck : MonoBehaviour
{
    [SerializeField]public BagItem_SO bagItem;//背包数据
    [SerializeField]public int taskID;//任务id
    [SerializeField]public  CurrentTask_SO currentTask_SO;//当前任务数据
    [SerializeField]public TaskViewManager taskViewManager;//任务的面板管理
    [SerializeField]private float serachPersonDistance;
    // [SerializeField]public GameController gameController;//获取人物数据 

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(currentTask_SO.taskType==TaskType.searchPerson)
        {
            var npcPos=currentTask_SO.npc.transform.position;
            if((GameInfo.GetPos()-npcPos).sqrMagnitude<serachPersonDistance)
            {
                currentTask_SO.finishedTask=true;
                taskViewManager.taskListData.TaskDetailsList[taskID].finishedTask=true;
            }
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void Update()
    // {
    //     if(currentTask_SO.taskID!=null)
    //     {
    //         switch(currentTask_SO.taskID)
    //         {
    //             case "1": Task1Check();break;
    //         }
    //     }
        
    // }

    // private void Task1Check()
    // {
    //     Item_SO itemToFind=bagItem.bag_A.Find(item=>item.itemName==taskItemName);
    //     if(itemToFind!=null)
    //     {
    //         if(itemToFind.itemNum>=3)
    //         {
    //             currentTask_SO.finishedTask=true;
    //         }
            
    //         foreach(var item in currentTask_SO.items)
    //         {
    //             sceneItem.AddItem(item);
    //         }

    //     }
    //     else
    //     {
    //         currentTask_SO.finishedTask=false;
    //     }
    // }

}
