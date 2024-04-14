using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务地图面板
/// </summary>
public class Panel_MapTaskView: MonoBehaviour
{
    [SerializeField] private Button bt_Close;
    [SerializeField] private GameObject[] obj_Image;

    void OnEnable()
    {
        bt_Close.onClick.AddListener(delegate () { OnButtonClick(); });
        UpdateMapTask();
    }

    //更新任务地图图标
    void UpdateMapTask()
    {
        int task = GameInfo.GetTask();
        if (task > obj_Image.Length) { task = obj_Image.Length; }

        //先把所有图标隐藏起来
        for (int i = 0; i < obj_Image.Length; i++)
        {
            obj_Image[i].gameObject.SetActive(false);
        }
        if (task > 0)//完成任务才会显示，任务从0开始
        {
            //根据完成任务，把已经完成任务的图标显示出来
            for (int i = 1; i <= task + 1; i++)
            {
                obj_Image[i - 1].gameObject.SetActive(true);
            }
        }
    }

    //点击按钮事件
    void OnButtonClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}