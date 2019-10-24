using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerRotate3 : MonoBehaviour
{
    public Transform enmey;


    private Quaternion targetPos;

    private Vector3 dir;

    // Use this for initialization
    void Start()
    {
        // 让主角和敌人， 看向同一方向


        float angle = Vector3.Angle(transform.forward, enmey.position - transform.position);


        Debug.LogError("打印出主角正前面和敌人之间的夹角 ：：：：  " + angle);
    }


    /// <summary>
    ///   判断对象2 是否在对象1的追击范围内  ，
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monster"></param>
    /// <param name="viewAngle1"></param>
    /// <param name="viewAngel2"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public bool GetPos(Transform obj1, Transform obj2, float viewAngle1, float viewAngel2, float distance)
    {
        float angle = Vector3.Angle(obj1.forward, obj2.position - obj1.position);
        if (angle >= viewAngle1 && angle <= viewAngel2)
        {
            float dis = Vector3.Distance(obj1.position, obj2.position);
            return dis <= distance;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // 让主角和敌人， 看向同一方向
//        transform.rotation = Quaternion.Lerp(transform.rotation, enmey.rotation, 0.2f);
    }
}