using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testscene : MonoBehaviour
{

    public List<GameObject> Obj;


    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player")
        {
        var obj=other.gameObject;
        foreach(var gameObj in Obj)
        {
            DontDestroyOnLoad(gameObj);
        }
        DontDestroyOnLoad(obj);

        obj.transform.position=new Vector3(0,0,0);
        SceneManager.LoadScene("杨辉");
        }

    }
}
