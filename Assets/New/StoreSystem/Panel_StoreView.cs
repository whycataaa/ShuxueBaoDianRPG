using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_StoreView : MonoBehaviour
{
    public GameObject panel_Store,panel_showStore;
    public GameObject panel_Buy,panel_Sold;

    public Button Bt_SwitchBuy,Bt_SwitchSold;
    public Button Bt_BuyItem;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Bt_SwitchBuy.onClick.AddListener(()=>Panel_BagView.SetObjectToActive(panel_Buy,panel_Sold));
        Bt_SwitchSold.onClick.AddListener(()=>Panel_BagView.SetObjectToActive(panel_Sold,panel_Buy));
        panel_Store.SetActive(false);
        panel_showStore.SetActive(false);

        panel_showStore.GetComponent<Button>().onClick.AddListener(()=>panel_Store.SetActive(true));
    }

}
