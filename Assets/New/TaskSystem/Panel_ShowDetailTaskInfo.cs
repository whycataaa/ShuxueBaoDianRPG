using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ShowDetailTaskInfo : MonoBehaviour
{
    [SerializeField]CurrentTask_SO currentTask_SO;

    [SerializeField]public Text taskName;
    [SerializeField]public Text taskInfo;
    [SerializeField]public Text taskReward;
    [SerializeField]public Button Bt_Tracking;
    [SerializeField]public TaskViewManager taskViewManager;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Bt_Tracking.onClick.AddListener(()=>taskViewManager.TaskDataDisplay(currentTask_SO));
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(currentTask_SO.taskID!=0)
        {
            taskName.text=currentTask_SO.taskName;
            taskInfo.text=currentTask_SO.taskDescription;
            taskReward.text=currentTask_SO.remuneration;
        }



        //else
        // if(currentTask_SO.taskID==0)
        // {
        //     taskName.text=currentTask_SO.taskName;
        //     taskInfo.text=currentTask_SO.taskDescription;
        //     taskReward.text=currentTask_SO.remuneration;
        // }
    }
}
