using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class IOUtil
{
    /// <summary>
    ///获取 路径中的文件名字
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFileName(string path)
    {
        // 这句代码也可以啊
        //  System.IO.Path.GetDirectoryName(path);
        //    System.IO.Path.GetFileName(path);
        string fileName = path;
        int lastIndex = path.LastIndexOf('/');
        if (lastIndex > -1)
        {
            fileName = fileName.Substring(lastIndex + 1);
        }

        lastIndex = fileName.LastIndexOf('.');
        if (lastIndex > -1)
        {
            fileName = fileName.Substring(0, lastIndex);
        }

        return fileName;
    }

    /// <summary>
    /// 传入文本文件的地址， 返回这个文本文本的文本内容
    /// </summary>
    /// <param name="filePath">  </param>
    /// <returns></returns>
    public static string GetFileText(string filePath)
    {
        string content = string.Empty;

        if (!File.Exists(filePath))
        {
            return content;
        }

        using (StreamReader sr = File.OpenText(filePath))
        {
            content = sr.ReadToEnd();
        }

        return content;
    }

    #region CreateTextFile 创建文本文件

    /// <summary>
    /// 创建文本文件的同时传入   自定义的文本内容
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="content"></param>
    public static void CreateTextFile(string filePath, string content)
    {
        DeleteFile(filePath);

        using (FileStream fs = File.Create(filePath))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(content.ToString());
            }
        }
    }

    #endregion

    #region DeleteFile 删除文件

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath"></param>
    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    #endregion

    #region CopyDirectory 拷贝文件夹

    /// <summary>
    /// 拷贝文件夹
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDirName"></param>
    public static void CopyDirectory(string sourceDirName, string destDirName)
    {
        try
        {
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
            }

            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                if (File.Exists(destDirName + Path.GetFileName(file)))
                    continue;
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
                    continue;

                File.Copy(file, destDirName + Path.GetFileName(file), true);
                File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
            }

            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}