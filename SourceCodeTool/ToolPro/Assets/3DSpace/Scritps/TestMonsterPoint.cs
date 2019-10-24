using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    /// <summary>
    /// 追击敌人的时候， 从我的位置，指向敌人的位置，敌人前面的随机点
    /// </summary>
public class TestMonsterPoint : MonoBehaviour
{
    private int count = 100;

    public Transform enmey;
    List<Vector3> vList = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            // 主角的位置，  敌人的位置
          Vector3 v = Vector.OnPostRender(transform.position, enmey.position, 2);
//            Vector3 v = Vector.GetRandomPos( enmey.position, 2);
            vList.Add(v);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < vList.Count; i++)
        {
            Gizmos.DrawCube(vList[i], Vector3.one * 0.3f);
        }
    }
}