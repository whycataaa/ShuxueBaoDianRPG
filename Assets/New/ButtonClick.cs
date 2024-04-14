using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonClick :MonoBehaviour, IPointerClickHandler
{
    public GameObject info;
    public UnityEvent rightClick;


    protected void Start()
    {
        rightClick.AddListener(new UnityAction(ButtonRightClick));

    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        info.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        rightClick.Invoke();
    }


    protected virtual void ButtonRightClick()
    {
        info.SetActive(!info.activeSelf);
        Debug.Log("Button Right Click");
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        info.SetActive(false);
    }
}

