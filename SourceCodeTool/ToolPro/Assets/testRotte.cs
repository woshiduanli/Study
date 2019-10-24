using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class testRotte : MonoBehaviour
{
    public Transform enm;

    public int count = 20;

    List<Vector3> _list = new List<Vector3>();

// Use this for initialization
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 v3 = Vector.OnPostRender(transform.position, enm.position, 2);
            _list.Add(v3);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
//       DrawGizmo.GetCustomAttribute()

    }


}