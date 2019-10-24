using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerRotate2 : MonoBehaviour
{
    public Transform enmey;


    private Quaternion targetPos;

    private Vector3 dir;

    // Use this for initialization
    void Start()
    {
        // 让主角和敌人， 看向同一方向


        

    }

    // Update is called once per frame
    void Update()
    {
         // 让主角和敌人， 看向同一方向
        transform.rotation = Quaternion.Lerp(transform.rotation, enmey.rotation, 0.2f);
    }
}