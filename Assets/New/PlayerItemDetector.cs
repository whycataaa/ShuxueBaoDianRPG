using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDetector : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        //创建一条从主摄像机到鼠标触摸点的射线(控制物体被选中时的高亮)
        Ray ray1=Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo=new RaycastHit();
        if(Physics.Raycast(ray1,out hitInfo))
        {
            var itemObj=hitInfo.collider.gameObject;
            var itemScene=itemObj.GetComponent<SceneItem>();

            if(itemObj.tag=="Item")
            {
                //物体高亮
                Debug.Log("Item was selected");

                if(Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(itemObj);
                    itemScene.AddItem(itemScene.item);
                }
            }
        }
    }
}
