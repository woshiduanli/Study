using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


//[XmlRootAttribute("MyCity", Namespace="abc.abc", IsNullable=false)]     // 当该类为Xml根节点时，以此为根节点名称。
//[XmlAttribute("AreaName")]    // 表现为Xml节点属性。<... AreaName="..."/>
//[XmlElementAttribute("AreaId", IsNullable = false)]    // 表现为Xml节点。<AreaId>...</AreaId>
//[XmlArrayAttribute("Areas")]    // 表现为Xml层次结构，根为Areas，其所属的每个该集合节点元素名为类名。<Areas><Area ... /><Area ... /></Areas>
//[XmlElementAttribute("Area", IsNullable = false)]    // 表现为水平结构的Xml节点。<Area ... /><Area ... />...
//[XmlIgnoreAttribute]    // 忽略该元素的序列化。
using XLua;

[Hotfix]
public static class MyXML
{
    /// <summary>
    /// Loads the xml.
    /// </summary>
    /// <returns>
    /// The xml.
    /// </returns>
    /// <param name='filepath'>
    /// Filepath.
    /// </param>
    /// <typeparam name='T'>
    /// The 1st type parameter.
    /// </typeparam>
    /// 

    public static T LoadXml2<T>(string path)
    {

   TextAsset xml =    Resources.Load<TextAsset>("teachconf");

   return LoadXml<T>(xml.bytes);

        return default(T);
    }

   public static T LoadXml<T>(string path)
    { 
        FileStream stream = null;
        try
        {
            if (path.Contains("://"))
            {
                WWW www = new WWW(path);
                while (!www.isDone) { }//等待加载完成
                return LoadXml<T>(www.bytes);
            }
            if (!File.Exists(path))
            {
                Helper.LogError("File does not exist : " + path);
                return default(T);
            }
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            if (stream == null)
            {
                Helper.LogError("stream error.");
                return default(T);
            }
            XmlSerializer xs = new XmlSerializer(typeof(T));
            T data = (T)xs.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch (FormatException e)
        {
            if (stream != null)
                stream.Close();
            Helper.LogError("load " + path + " error.");
            Helper.LogError(e.GetType());
            Helper.LogError(e.Message);
            return default(T);
        }
    }
    /// <summary>
    /// Loads the xml.
    /// </summary>
    /// <returns>
    /// The xml.
    /// </returns>
    /// <param name='bytes'>
    /// Bytes.
    /// </param>
    /// <typeparam name='T'>
    /// The 1st type parameter.
    /// </typeparam>
    public static T LoadXml<T>(byte[] bytes)
    {
        try
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream(bytes);
            //XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return (T)xs.Deserialize(memoryStream);
        }
        catch (Exception e)
        {
            Helper.LogError("load data error." + e.ToString());
            return default(T);
        }
    }
    public static T LoadXml<T>(TextAsset ts)
    {
        //Helper.LogError(ts.name.ToString());
        return LoadXml<T>(ts.bytes);
    }

    /// <summary>
    /// Saves the xml.
    /// </summary>
    /// <param name='data'>
    /// Data.
    /// </param>
    /// <param name='filepath'>
    /// Filepath.
    /// </param>
    /// <typeparam name='T'>
    /// The 1st type parameter.
    /// </typeparam>
    /// <exception cref='Exception'>
    /// Is thrown when the exception.
    /// </exception>
    public static void SaveXml<T>(T data, string filepath)
    {
        if (data == null)
            throw new Exception("Parameter humanResource is null!");
        if (filepath == "")
            throw new Exception("The filepath is null!");
        UTF8Encoding utf8 = new UTF8Encoding(false);
        StreamWriter writer = new StreamWriter(filepath, false, utf8);
        try
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(writer, data);
            if (writer != null)
                writer.Close();
        }
        catch (Exception e)
        {
            if (writer != null)
                writer.Close();
            Helper.LogError(e.StackTrace.ToString());
            throw new Exception("Xml serialization failed!");
        }
    }

    public static T LoadData<T>(string data)
    {
        try
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringReader memoryStream = new StringReader(data);
            //XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return (T)xs.Deserialize(memoryStream);
        }
        catch (Exception e)
        {
            Helper.LogError("load data error." + e.ToString());
            return default(T);
        }
    }

    public static string GetData<T>(T data)
    {
        string datas = string.Empty;
        XmlSerializer xs = new XmlSerializer(typeof(T));
        StringWriter memoryStream = new StringWriter();
        xs.Serialize(memoryStream, data);
        datas = memoryStream.ToString();
        return datas;
    }
}