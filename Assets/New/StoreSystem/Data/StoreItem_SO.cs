using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StoreItem",menuName ="Store/StoreItem")]
public class StoreItem : ScriptableObject
{
    public List<Item_SO> items;
}
