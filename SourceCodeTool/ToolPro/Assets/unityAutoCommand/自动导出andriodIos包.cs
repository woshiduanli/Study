using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO; 


public class NewBehaviourScript 
{
    [MenuItem("Star Force/自动导出andriod  ios包", false, 21)]
    public static void ShowWindow()
    {
        //string[] s =   EditorBuildSettingsScene.GetActiveSceneList();

        string d333 = Application.dataPath + "/../";
        Debug.LogError(d333 + System.DateTime.Now.ToString());

        //Path.GetFileNameWithoutExtension(Application.dataPath + "");

        //return;
        EditorBuildSettingsScene[] d = new EditorBuildSettingsScene[1];
        d[0] = new EditorBuildSettingsScene( /*Application.dataPath */ "Assets/a.unity", true);

        string dicstr = "C:/TestPro/TestScreenChange/testAnd/test7";
        if (!Directory.Exists(dicstr))
        {
            Directory.CreateDirectory(dicstr);
        }
        else
        {
            Directory.Delete(dicstr);
            Directory.CreateDirectory(dicstr);
        }

        string dd = BuildPipeline.BuildPlayer(d, dicstr, BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer);
        Debug.LogError(dd + System.DateTime.Now.ToString());

        System.IO.DirectoryInfo ddddddd = new System.IO.DirectoryInfo 
           (dicstr);


        DirectoryInfo[]  dddd2 = ddddddd.GetDirectories();

        dddd2 = dddd2[0].GetDirectories(); 
        foreach (DirectoryInfo item in dddd2)
        {
            Debug.LogError(item.FullName); 
        }








       //System.di
       //第四个参数设置为BuildOptions.AcceptExternalModificationsToPlayer就可以了
    }

}
