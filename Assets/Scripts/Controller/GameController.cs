using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏全局控制器
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject obj_BornPos, obj_PKPos, camera_Map;//玩家出生点，小地图摄像机
    [SerializeField] private GameObject obj_Avatar;//玩家不同角色预制体
    [SerializeField] private string s_PkScene;
    private GameObject player;//玩家

    void Start()
    {
        //创建角色
        if (obj_Avatar != null)
        {
            CreateAvatar(obj_Avatar);
        }
    }

    void Update()
    {
        //使地图摄像机与玩家同步
        camera_Map.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15, player.transform.position.z);
    }

    //创建并初始化玩家实体
    void CreateAvatar(GameObject avatar)
    {

         player = Instantiate(avatar, obj_BornPos.transform.position, obj_BornPos.transform.rotation);

         player.name = "Player";
        /*if (GameInfo.GetPos() == ConstString.pos_Born)
        {
           
            
        }
        else if (GameInfo.GetPos() == ConstString.pos_PK)
        {
            player = Instantiate(avatar, obj_PKPos.transform.position, obj_BornPos.transform.rotation);
            player.name = "Player";
        }*/
    }

    //返回出生点
    public void ReturnBornPos()
    {
        player.transform.position = obj_BornPos.transform.position;
        player.transform.rotation = obj_BornPos.transform.rotation;
        player.GetComponent<AvatarController>().CanMove(true);
    }

    //进入PK场景，并记录PK坐标点为下次玩家进入该场景的初始坐标
    public void GoPKScene()
    {
        //SceneManager.LoadScene(s_PkScene);
        GameInfo.SetPos(ConstString.pos_PK);
    }
}