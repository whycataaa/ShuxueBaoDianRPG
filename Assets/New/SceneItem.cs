using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///管理场景中的可拾取物品
/// </summary>
public class SceneItem : MonoBehaviour
{
    //物体的数据仓库
    public Item item;
    //背包的物品数据仓库
    public BagItem bagItem;
    public void AddItem(Item item)
    {
        switch(item.itemType.ToString())
        {
            case "a":
            if(!bagItem.itemsList_a.Contains(item))
            {
                bagItem.itemsList_a.Add(item);
            }
            item.itemNum++;
            break;

            case "b":
            if(!bagItem.itemsList_b.Contains(item))
            {
                bagItem.itemsList_b.Add(item);
            }
            item.itemNum++;
            break;

            case "c":
            if(!bagItem.itemsList_c.Contains(item))
            {
                bagItem.itemsList_c.Add(item);
            }
            item.itemNum++;
            break;

        }
        BagGridControl.updateItemToUI();
    }
}
