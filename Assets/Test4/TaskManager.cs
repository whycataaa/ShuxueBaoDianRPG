using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("SO获取")]
    public TaskData_SO taskListData;
    public CurrentTask_SO currentTaskData;
    [Header("Test获取")]
    public Text taskName;
    public Text taskDescription;
    public Text taskTarget;
    public Text taskRemuneration;
    // [Header("放弃键")]
    // public Button myButton;

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
            ClearCurrentTask();
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
    public void CopyTaskButton(int ID)//根据ID进行拷贝，可以改成根据列表里的任务来传入ID
    {
        CopyTaskDataToCurrentTask(taskListData, currentTaskData, ID);
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
            destination.taskID = taskDetailsToCopy.taskID.ToString();
            destination.taskName = taskDetailsToCopy.taskName;
            destination.taskType = taskDetailsToCopy.taskType;
            destination.taskTarget=taskDetailsToCopy.taskTarget;
            destination.taskDescription = taskDetailsToCopy.taskDescription;

            // destination.waves = new List<CurrentTask_SO.Wave>();

            // // 复制每个Wave，由于是列表所以要额外操作，通过foreach来获取到列表中所有的内容（类似于扫描）
            // foreach (TaskDetails.Wave sourceWave in taskDetailsToCopy.waves)
            // {
            //     CurrentTask_SO.Wave destinationWave = new CurrentTask_SO.Wave
            //     {
            //         waveName = sourceWave.waveName,
            //         enemyGroups = new List<CurrentTask_SO.EnemyGroup>()
            //     };

            //     // 复制每个EnemyGroups
            //     foreach (TaskDetails.EnemyGroup sourceEnemyGroup in sourceWave.enemyGroups)
            //     {
            //         CurrentTask_SO.EnemyGroup destinationEnemyGroup = new CurrentTask_SO.EnemyGroup
            //         {
            //             enemyID = sourceEnemyGroup.enemyID,
            //             enemyName = sourceEnemyGroup.enemyName,
            //             enemyCount = sourceEnemyGroup.enemyCount,
            //             spawnCount = sourceEnemyGroup.spawnCount,
            //             enemyPrefab = sourceEnemyGroup.enemyPrefab
            //         };
            //         //添加复制的列表
            //         destinationWave.enemyGroups.Add(destinationEnemyGroup);
            //     }

            //     //扫描完后将内容复制到新列表中
            //     destination.waves.Add(destinationWave);
            // }

            // 其他参数的复制...
            // destination.waveInterval = taskDetailsToCopy.waveInterval;
            // destination.maxEnemiesAllowed = taskDetailsToCopy.maxEnemiesAllowed;
            destination.remuneration = taskDetailsToCopy.remuneration;
            destination.taskCompleted = taskDetailsToCopy.taskCompleted;
            destination.isMandatoryTask = taskDetailsToCopy.isMandatoryTask;
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
        taskName.text = currentTaskData.taskName;//任务名称
        taskDescription.text =currentTaskData.taskDescription;//任务内容

        // //任务目标
        // if (!isClear && !missID)
        // {
        //     taskInfo = null;//先清空后再复制
        //     if (taskInfo == null && currentTaskData != null)
        //     {
        //         // foreach (var wave in currentTaskData.waves)//将列表中的内容以名称与数量进行显示
        //         // {
        //         //     foreach (var enemyGroup in wave.enemyGroups)
        //         //     {
        //         //         taskInfo += $"{enemyGroup.enemyName} 数量: {enemyGroup.enemyCount}\n";
        //         //     }
        //         // }
        //     }
        //     taskTarget.text = taskInfo;
        // }
        // else
        // {
        //     taskInfo = null;//如果没有找到对应ID的话内容显示为空
        // }
        taskTarget.text=currentTaskData.taskTarget;

        taskRemuneration.text = "" + currentTaskData.remuneration;//任务报酬

        // if (!currentTaskData.isMandatoryTask)//根据任务类型（是否是强制任务）来修改放弃键的状态
        // {
        //     myButton.interactable = true;
        // }
        // else
        // {
        //     myButton.interactable = false;//将其变为无法操作
        // }
    }

    /// <summary>
    /// 清空当前任务内容,按钮控制
    /// </summary>
     public void ClearCurrentTask()
     {
         if (!currentTaskData.isMandatoryTask)
         {
            // myButton.interactable = true;
            isClear = true;
            currentTaskData.ResetTaskData();
            taskName.text = "";
            taskDescription.text = "内容：" + "";
            taskTarget.text = "";
            taskRemuneration.text = "";
        }        
    }
}
