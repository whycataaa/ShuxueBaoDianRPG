using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///管理背包中的物体
/// </summary>
[CreateAssetMenu(fileName ="New BagItem",menuName ="Bag/New BagItem")]
public class BagItem : ScriptableObject
{
    public List<Item> itemsList_a=new List<Item>();
    public List<Item> itemsList_b=new List<Item>();
    public List<Item> itemsList_c=new List<Item>();

}
