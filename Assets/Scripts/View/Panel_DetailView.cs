using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 答题、领取任务结果
/// </summary>
public class Panel_DetailView : MonoBehaviour
{
    [SerializeField] private Text t_Detail;

    private void OnEnable()
    {
        GameObject.Find("Player").GetComponent<AvatarController>().CanMove(true);
        Invoke("ChangePanel", 5);
    }

    //改变显示的界面
    void ChangePanel()
    {
        //显示剧情名称
        GameViewController viewController;
        viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
        viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateTaskName();
        gameObject.SetActive(false);
    }

    //设置显示结果文字
    public void SetDetail(string detail)
    {
        t_Detail.text = detail;
    }
}
