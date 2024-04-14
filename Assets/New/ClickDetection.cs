using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic; // 添加此行

public class ClickDetection : MonoBehaviour
{
    void Update()
    {
        // 检查鼠标左键是否被按下
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标点击的位置
            Vector2 mousePosition = Input.mousePosition;

            // 创建一个PointerEventData用于封装鼠标事件数据
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

            // 设置鼠标位置
            pointerEventData.position = mousePosition;

            // 通过EventSystem检查鼠标点击位置上的UI元素
            List<RaycastResult> results = new List<RaycastResult>(); // 将数组改为列表

            EventSystem.current.RaycastAll(pointerEventData, results);

            // 检查点击结果
            if (results.Count > 0) // 将数组长度改为列表的Count属性
            {
                // 打印点击到的UI元素的名称
                Debug.Log("Clicked UI: " + results[0].gameObject.name);
            }
            else
            {
                // 如果没有点击到UI元素，则打印提示信息
                Debug.Log("Clicked on empty space.");
            }
        }
    }
}


