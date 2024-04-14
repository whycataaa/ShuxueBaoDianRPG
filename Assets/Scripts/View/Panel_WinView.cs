using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 完成任务界面，自动跳转到新手村
/// </summary>
public class Panel_WinView : MonoBehaviour
{
    [SerializeField] private string s_Scene;

    private void OnEnable()
    {
        StartCoroutine(SceneJump());
    }

    IEnumerator SceneJump()
    {
        yield return new WaitForSeconds(3);
        GameInfo.SetTask(GameInfo.GetTask() + 1);
        SceneManager.LoadScene(s_Scene);
    }
}