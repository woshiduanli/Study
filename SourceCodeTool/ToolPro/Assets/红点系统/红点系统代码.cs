using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

// 下面的 callback 返回是bool， 是否显示红点
public delegate bool CEvent.RedDot.CallBack();

// 创建时候的数据， 需要传入的参数
public class RedPointData
{
    public CEvent.RedDot.CallBack cb;
    public RedDotType dot;
}


public class RedPoint : CObject
{
    // 不受子红点影响， 默认会受到
    bool NotBeChildInfluences;
    RedDotType dotType;

    RedPoint parent;
    List<RedPoint> children;

    RedDotUI2 redPointUI;

    private CEventManager.Handler checkHandler;
    bool isShow;
    private CEvent.RedDot.CallBack mCheck;


    public override void Initialize()
    {
        base.Initialize();
        if (this.Args == null) return;
        checkHandler = RegEventHandler<CEvent.RedDot.Refresh>(OnRefreshEvent);

        RedPointData redData = this.Args[0] as RedPointData;
        dotType = redData.dot;
        mCheck = redData.cb;

        ShowDot();
    }

    #region !!!! 不受到子节点影响的时候，特殊情况用
    /// <summary>
    ///  若 NotBeChildinfluences == false , show 字段是无效字段
    /// </summary>
    /// <param name="NotBeChildinfluences"></param>
    /// <param name="show"></param>
    public void SetNotBeChildinfluences(bool NotBeChildinfluences = true, bool show = false)
    {
        this.NotBeChildInfluences = NotBeChildinfluences;

        if (NotBeChildInfluences)
        {
            isShow = show;
            if (redPointUI != null)
                redPointUI.transform.localScale = isShow ? Vector3.one : Vector3.zero;

            SetParentData();
        }
        else
        {
            ShowDot();
        }
    }

    #endregion



    public void SetFather(RedPoint tr)
    {
        parent = tr;
    }

    public void SetChildren(RedPoint red)
    {
        if (children == null)
            children = new List<RedPoint>();
        children.Add(red);
    }

    public void SetUI(GameObject parent, Vector2 pos)
    {
        if (redPointUI == null)
        {
            GameObject gameObject2 = new GameObject(CString.Concat(parent.gameObject.name, "_redPoint"));
            CClientCommon.SetActiveOverload(gameObject2, true);
            gameObject2.transform.parent = parent.transform;
            redPointUI = gameObject2.AddComponent<RedDotUI2>();
            redPointUI.Arg = new object[] { pos };
        }
        ShowDot();
    }

    private void OnRefreshEvent(CObject sender, CEvent.RedDot.Refresh e)
    {
        if ((RedDotType)e.redDotType != dotType)
            return;
        ShowDot();
    }

    public bool GetIsShow()
    {
        if (mCheck != null)
        {
            return mCheck();
        }
        else
        {
            // 如果不受到子红点影响
            if (NotBeChildInfluences)
            {
                return isShow;
            }
            else
            {
                if (children != null && children.Count > 0)
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        if (children[i].GetIsShow())
                        {
                            return true;
                        }
                    }
                }
            }


        }
        return false;
    }

    private void SetParentData()
    {
        if (parent == null) return;
        if (parent.NotBeChildInfluences) return;

        if (isShow)
        {
            parent.isShow = true;
            if (parent.redPointUI != null)
                parent.redPointUI.transform.localScale = Vector3.one;
        }
        else
        {
            if (parent.children != null && parent.children.Count > 0)
            {
                bool isShowTemp = false;
                for (int i = 0; i < parent.children.Count; i++)
                {
                    if (parent.children[i].isShow)
                    {
                        isShowTemp = true;
                        break;
                    }
                }
                parent.isShow = isShowTemp;
                if (parent.redPointUI != null)
                    parent.redPointUI.transform.localScale = isShowTemp ? Vector3.one : Vector3.zero;
            }
        }
        // 递归向上设置数据
        parent.SetParentData();
    }

    private void ShowDot()
    {
        // 如果有子红点， 就不需要回调
        if (children != null && children.Count > 0) mCheck = null;

        isShow = GetIsShow();

        // 显示自己的数据
        if (redPointUI != null)
            redPointUI.transform.localScale = isShow ? Vector3.one : Vector3.zero;

        // 通知父红点改变数据
        SetParentData();
    }
}




// 这个类是， 用来显示红点ui的， 就是那个红色图片
public class RedDotUI2 : CUIElement
{

    private RectTransform m_rectTransform;
    CImage image;
    public override void Initialize()
    {
        image = CClientCommon.AddComponent<CImage>(this.gameObject);
        this.CreateSprite(image, "newmainface", "xiaohongdian");
        image.raycastTarget = false;
        m_rectTransform = GetComponent<RectTransform>();
        m_rectTransform.pivot = new Vector2(0.5f, 0.5f);
        m_rectTransform.anchorMin = Vector2.one;
        m_rectTransform.anchorMax = Vector2.one;
        image.SetNativeSize();
    }

    protected override void OnUIEnable()
    {
        if (Arg != null)
        {
            object[] obj = Arg as object[];
            if (obj != null && obj.Length > 0)
                m_rectTransform.anchoredPosition = (Vector2)obj[0];
        }
        else
        {
            m_rectTransform.anchoredPosition = new Vector2(-5, -5);
        }
    }

    protected override void OnUIDestroy()
    {
        base.OnUIDestroy();
    }
}


public class NewBehaviourScript 
{



    [MenuItem("Star Force/持久数据编辑器", false, 21)]
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
