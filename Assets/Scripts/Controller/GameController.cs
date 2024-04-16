using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏全局控制器
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject obj_BornPos, camera_Map;//玩家出生点，小地图摄像机
    [SerializeField] private GameObject obj_Avatar;//玩家不同角色预制体

    private GameObject player;//玩家

    void Start()
    {
        //创建角色
        if (obj_Avatar != null)
        {
            player = Instantiate(obj_Avatar, obj_BornPos.transform.position, obj_BornPos.transform.rotation);

            player.name = "Player";
        }
    }

    void Update()
    {
        //使地图摄像机与玩家同步
        camera_Map.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15, player.transform.position.z);
    }


    //返回出生点
    public void ReturnBornPos()
    {
        player.transform.position = obj_BornPos.transform.position;
        player.transform.rotation = obj_BornPos.transform.rotation;
        player.GetComponent<AvatarController>().CanMove(true);
    }
}