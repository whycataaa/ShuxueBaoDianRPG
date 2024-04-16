using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///控制每个背包格子
/// </summary>
public class BagGridControl : MonoBehaviour
{
    //单例模式
    static BagGridControl bagGridControl;
    private void Awake()
    {
        if(bagGridControl!=null)
        {
            Destroy(this);
        }
        bagGridControl = this;
    }

    //背包数据仓库、格子中物体预制体、和UI中显示物体元素的父元素
    public BagItem_SO bagItem;
    public BagGrid gridPrefab;
    public GameObject[] myBag;

    //每次打开背包，动态的更新背包UI元素
    private void OnEnable()
    {
        updateItemToUI();
    }

    /// <summary>
    /// 在UI中将一个物体的数据仓库显示出来
    /// </summary>
    /// <param name="item"></param>
    public static void insertItemToUI(Item_SO item)
    {
        switch(item.itemType)
        {
            case 0:
            BagGrid grid_a = Instantiate(bagGridControl.gridPrefab,FindTransToInsert(0));
            grid_a.gridImage.sprite = item.itemImage;
            grid_a.gridNum.text = item.itemNum.ToString();
            break;

            case (Item_SO.ItemType)1:
            BagGrid grid_b = Instantiate(bagGridControl.gridPrefab,FindTransToInsert(1));
            grid_b.gridImage.sprite = item.itemImage;
            grid_b.gridNum.text = item.itemNum.ToString();
            break;

            case (Item_SO.ItemType)2:
            BagGrid grid_c = Instantiate(bagGridControl.gridPrefab,FindTransToInsert(2));
            grid_c.gridImage.sprite = item.itemImage;
            grid_c.gridNum.text = item.itemNum.ToString();
            break;

        }
    }

    private static Transform FindTransToInsert(int bagNum)
    {

        for(int i=0;i<bagGridControl.myBag[bagNum].transform.childCount;i++)
        {
            //Debug.Log(bagGridControl.myBag[bagNum].transform.GetChild(i).name);

            if(bagGridControl.myBag[bagNum].transform.GetChild(i).childCount==0)
            {
                return bagGridControl.myBag[bagNum].transform.GetChild(i).transform;
            }
        }
        Debug.Log("Full Item");
        return null;
    }
    /// <summary>
    /// 将背包数据仓库中所有物体显示在UI上
    /// </summary>
    public static void updateItemToUI()
    {
        for(int j=0;j<bagGridControl.myBag.Length;j++)
        {
            for (int i = 0; i < bagGridControl.myBag[j].transform.childCount; i++)
            {
                if(bagGridControl.myBag[j].transform.GetChild(i).childCount!=0)
                {
                    //Debug.Log(bagGridControl.myBag[i].transform.GetChild(j).name);
                    DestroyImmediate(bagGridControl.myBag[j].transform.GetChild(i).GetChild(0).gameObject);
                }
            }
        }
            foreach(var item in bagGridControl.bagItem.bag_A)
            {
                insertItemToUI(item);
            }
            foreach(var item in bagGridControl.bagItem.bag_B)
            {
                insertItemToUI(item);
            }
            foreach(var item in bagGridControl.bagItem.bag_C)
            {
                insertItemToUI(item);
            }


    }
}
