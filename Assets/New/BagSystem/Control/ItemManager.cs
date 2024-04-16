using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///对背包和场景物品进行管理
/// </summary>
public class ItemManager : MonoBehaviour
{
    //背包的物品数据仓库
    public BagItem_SO bagItem;

    public void AddItem(Item_SO item)
    {
        switch(item.itemType)
        {
            case 0:
            if(!bagItem.bag_A.Contains(item))
            {
                bagItem.bag_A.Add(item);
            }
            item.itemNum++;
            break;

            case (Item_SO.ItemType)1:
            if(!bagItem.bag_B.Contains(item))
            {
                bagItem.bag_B.Add(item);
            }
            item.itemNum++;
            break;

            case (Item_SO.ItemType)2:
            if(!bagItem.bag_C.Contains(item))
            {
                bagItem.bag_C.Add(item);
            }
            item.itemNum++;
            break;

        }
        BagGridControl.updateItemToUI();
    }

    public void DeleteItem(Item_SO item)
    {
        switch(item.itemType)
        {
            case 0:
                foreach(var x in bagItem.bag_A)
                {
                    if(item.itemName==x.itemName)
                    {
                        x.itemNum--;
                        if(x.itemNum==0)
                        {
                            bagItem.bag_A.Remove(x);
                        }
                    }
                }
                break;
            
            case (Item_SO.ItemType)1:
                foreach(var x in bagItem.bag_B)
                {
                    if(item.itemName==x.itemName)
                    {
                        x.itemNum--;
                        if(x.itemNum==0)
                        {
                            bagItem.bag_A.Remove(x);
                        }
                    }
                }
                break;
            
            case (Item_SO.ItemType)2:
                foreach(var x in bagItem.bag_C)
                {
                    if(item.itemName==x.itemName)
                    {
                        x.itemNum--;
                        if(x.itemNum==0)
                        {
                            bagItem.bag_A.Remove(x);
                        }
                    }
                }
                break;
        }
        BagGridControl.updateItemToUI();
    }
    /// <summary>
    /// 查询背包中某个物品的个数
    /// </summary>
    /// <param name="item_SO"></param>
    /// <returns></returns>
    public int FindItem(Item_SO item)
    {
        switch(item.itemType)
        {
            case 0:
                foreach(var x in bagItem.bag_A)
                {
                    if(item.itemName==x.itemName)
                    {
                        return x.itemNum;
                    }
                    else
                    {
                        return 0;
                    }
                }
                break;
            
            case (Item_SO.ItemType)1:
                foreach(var x in bagItem.bag_B)
                {
                    if(item.itemName==x.itemName)
                    {
                        return x.itemNum;
                    }
                    else
                    {
                        return 0;
                    }
                }
                break;
            
            case (Item_SO.ItemType)2:
                foreach(var x in bagItem.bag_C)
                {
                    if(item.itemName==x.itemName)
                    {
                        return x.itemNum;
                    }
                    else
                    {
                        return 0;
                    }
                }
                break;

        }
        return 0;
    }
}
