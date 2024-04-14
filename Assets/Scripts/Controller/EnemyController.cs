using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人控制器
/// </summary>
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animation animation;
    private GameObject player;
    private Quaternion targetRotation;//设定要旋转到的角度：目标相对于物体的世界坐标系角度
    [SerializeField] private GameObject obj_attackPos, pre_Explosion, pre_Bullet;//敌人攻击位置，爆炸预制体，子弹预制体
    [SerializeField] private int i_SearchDistance = 20, i_AttackDistance = 3;//敌人搜索范围、攻击范围
    [SerializeField] private int i_Life = 100, i_Coin = 100, i_Attack = 10;//血量、击杀得分、伤害值
    private bool b_CanFire = true;//是否可以开枪

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponent<Animation>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player == null) { player = GameObject.Find("Player");  return; }
        //如果玩家在搜索范围之外
        if (Vector3.Distance(transform.position, player.transform.position) > i_SearchDistance)
        {
            animation.CrossFade("idle");
            return;
        }
        //如果玩家在搜索范围内
        if (Vector3.Distance(transform.position, player.transform.position) > i_AttackDistance && Vector3.Distance(transform.position, player.transform.position) <= i_SearchDistance)
        {
            FindPlayer();
            return;
        }
        //如果玩家在攻击范围内
        if (Vector3.Distance(transform.position, player.transform.position) <= i_AttackDistance)
        {
            Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //被玩家攻击，减少血量
        if (other.tag == "Attack_Avatar")
        {
            int damage = other.GetComponent<BulletController>().i_Attack;
            OnDemage(damage);
        }
    }

    //伤害值处理
    void OnDemage(int demage)
    {
        i_Life -= demage;
        if (i_Life <= 0)
        {
            //奖励金币
            GameInfo.SetCoin(i_Coin + GameInfo.GetCoin());
            GameViewController viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
            viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateCoin();
            //销毁实体，并生成爆炸效果
            Instantiate(pre_Explosion, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
    }

    //转向玩家
    void RotateToPlayer()
    {
        //获得一向量坐标点与另一坐标点之间夹角
        Vector3 targetDir = player.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        //两坐标点向量与世界坐标Z轴向量之间夹角，即为要旋转到的角度
        float targetAngle = Vector3.Angle(targetDir, Vector3.forward);

        if (transform.position.x > player.transform.position.x)
        {
            targetRotation = Quaternion.Euler(0, -targetAngle, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, targetAngle, 0);
        }
        if (angle > 5)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8 * Time.deltaTime);
        }
    }

    //寻找玩家并走过去
    void FindPlayer()
    {
        //开始寻路
        agent.SetDestination(player.transform.position);
        RotateToPlayer();
        animation.CrossFade("run");
    }

    //攻击玩家
    void Attack()
    {
        agent.ResetPath();//停止寻路
        RotateToPlayer();//面向攻击目标 
        if (b_CanFire)
        {
            animation.CrossFade("attack");
            pre_Bullet.GetComponent<BulletController>().i_Attack = i_Attack;
            Instantiate(pre_Bullet, obj_attackPos.transform.position, transform.rotation);
            b_CanFire = false;
            Invoke("FireSleep", 1);
        }
    }

    //射击休眠恢复
    void FireSleep()
    {
        b_CanFire = true;
    }
}