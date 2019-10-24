using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerRotate : MonoBehaviour
{
    public Transform enmey;


    private Quaternion targetPos;

    private Vector3 dir;

    // Use this for initialization
    void Start()
    {
        // 从我的位置 ， 转向敌人的位置， 看着敌人
        dir = enmey.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);
    }
}