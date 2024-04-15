using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskViewManager : MonoBehaviour
{
    [Header("SO获取")]
    public TaskData_SO taskListData;
    public CurrentTask_SO currentTaskData_Main;
    public CurrentTask_SO currentTaskData_Branch;
    [Header("Test获取")]
    [Header("小任务面板")]
    public Text taskName;
    public Text taskDescription;
    public Text taskTarget;
    public Text taskRemuneration;
    [Header("大任务面板")]
    //一些其他的变量
    public GameObject taskMINI;
    private string taskInfo;
    private bool isClear;
    private bool missID;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //运行任务显示
        TaskDataDisplay();
    }
    private void Update()
    {
        if(isClear)
        {
            taskMINI.SetActive(false);
        }else
        {
            taskMINI.SetActive(true);
        }
        
        if(Input.GetKeyDown(KeyCode.P))
        {
            ClearCurrentTask(currentTaskData_Main);
        }
        if(currentTaskData_Main.finishedTask)
        {
            ClearCurrentTask(currentTaskData_Main);
        }
    }

    /// <summary>
    /// 获取任务列表ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public TaskDetails GetTaskDetails(int ID)
    {
        //返回任务列表中ID为i（传入参数）的东西
        return taskListData.TaskDetailsList.Find(i => i.taskID == ID);
    }

    /// <summary>
    /// 从任务总列表中拷贝任务至当前任务（按钮模式）
    /// </summary>
    /// <param name="ID"></param>
    public void CopyTaskButton(int ID,CurrentTask_SO currentTask)//根据ID进行拷贝，可以改成根据列表里的任务来传入ID
    {
        CopyTaskDataToCurrentTask(taskListData, currentTask, ID);
    }

    /// <summary>
    /// 复制任务从任务总表至当前任务SO
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="ID"></param>
    public void CopyTaskDataToCurrentTask(TaskData_SO source, CurrentTask_SO destination, int ID)
    {
        //关闭清除任务状态
        isClear = false;
        missID = false;
        // 寻找TaskData_SO中taskID为传入的ID的项
        TaskDetails taskDetailsToCopy = source.TaskDetailsList.Find(task => task.taskID == ID);

        // 如果找到了符合条件的项，则复制数据到CurrentTask_SO
        if (taskDetailsToCopy != null)
        {
            //开始复制
            destination.taskID = taskDetailsToCopy.taskID;
            destination.taskName = taskDetailsToCopy.taskName;
            destination.taskType = taskDetailsToCopy.taskType;
            destination.taskTarget=taskDetailsToCopy.taskTarget;
            destination.taskDescription = taskDetailsToCopy.taskDescription;
            destination.remuneration = taskDetailsToCopy.remuneration;
            destination.finishedTask=taskDetailsToCopy.finishedTask;

            destination.npc = taskDetailsToCopy.npc;
            //将列表中的每个值都复制
            taskDetailsToCopy.items.ForEach(i=>destination.items.Add(i));

            taskDetailsToCopy.enemyPrefabs.ForEach(i=>destination.enemyPrefabs.Add(i));
        }
        else
        {
            missID = true;
            Debug.LogWarning("未找到相应ID的任务");
        }
    }

    /// <summary>
    /// 当前任务内容显示
    /// </summary>
    public void TaskDataDisplay()
    {
        taskName.text = currentTaskData_Main.taskName;//任务名称
        taskDescription.text =currentTaskData_Main.taskDescription;//任务内容
        taskTarget.text=currentTaskData_Main.taskTarget;//任务目标

        taskRemuneration.text = "" + currentTaskData_Main.remuneration;//任务报酬
    }

    /// <summary>
    /// 清空当前任务内容
    /// </summary>
     public void ClearCurrentTask(CurrentTask_SO currentTask_)
     {
        isClear = true;
        currentTask_.ResetTaskData();
        taskName.text = "";
        taskDescription.text = "内容：" + "";
        taskTarget.text = "";
        taskRemuneration.text = "";
    }
}
