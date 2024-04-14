using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 问答题列表管理器
/// </summary>
public class Panel_DaTiView : MonoBehaviour
{
    public GameObject[] panel_Question;

    public void SetPanelActive(GameObject panel)
    {
        //将全部面板隐藏
        for (int i = 0; i < panel_Question.Length; i++)
        {
            panel_Question[i].SetActive(false);
        }
        //激活面板
        panel.SetActive(true);
    }
}