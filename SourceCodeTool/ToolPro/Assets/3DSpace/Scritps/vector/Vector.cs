using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector
{
    /// <summary>
    ///  获取 targerPos 为圆心， distance 为半径的随机点
    /// </summary>
    /// <param name="targerPos"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPos(Vector3 targerPos, float distance)
    {
        //1.定义一个向量
        Vector3 v = new Vector3(0, 0, 1); //z轴超前的

        //2.让向量旋转
        v = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * v;

        //3.向量 * 距离(半径) = 坐标点
//        Vector3 pos = v * distance * UnityEngine.Random.Range(0.8f, 1f);

        // 一般用上面那句代码， 产生随机点
        Vector3 pos = v * distance;

        //4.计算出来的 围绕主角的 随机坐标点
        return targerPos + pos;
    }

    /// <summary>
    /// 获取目标点， 指向当前点方向上的 用 radius 为 半径  ， endPos 为圆心 的  半圈上的随机点  测试场景 monsterPos
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="endPos"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 OnPostRender(Vector3 curPos, Vector3 endPos, float radius)
    {
        Vector3 v = (curPos - endPos).normalized;
        v = Quaternion.Euler(0, UnityEngine.Random.Range(-90, 90), 0) * v;
        Vector3 pos = v * radius;
        Vector3 newPos = endPos + pos;
        return newPos;
    }

    /// <summary>
    /// 计算路径的长度
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static float GetPathLen(List<Vector3> path)
    {
        float pathLen = 0f; //路径的总长度 计算出路径

        for (int i = 0; i < path.Count; i++)
        {
            if (i == path.Count - 1) continue;

            float dis = Vector3.Distance(path[i], path[i + 1]);
            pathLen += dis;
        }

        return pathLen;
    }
}