using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace TaskManager.Utils.Common
{
    /// <summary>
    /// IO操作帮助类
    /// </summary>
    public class IOHelper
    {
        /// <summary>
        /// 获取目录大小 递归
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            // 所有文件大小.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            // 遍历出当前目录的所有文件夹.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += DirSize(di);   //这就用到递归了，调用父方法,注意，这里并不是直接返回值，而是调用父返回来的
            }
            return (Size);
        }

        /// <summary>
        /// 根据文件路径，创建文件对应的文件夹，若已存在则跳过
        /// </summary>
        /// <param name="filepath"></param>
        public static void CreateDirectory(string filepath)
        {
            try
            {
                string dir = System.IO.Path.GetDirectoryName(filepath);
                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);
            }
            catch (Exception exp)
            {
                throw new Exception("路径"+filepath,exp);
            }
        }

        /// <summary>
        /// 目录拷贝
        /// 不支持父子目录拷贝，否则出现死循环递归
        /// </summary>
        /// <param name="srcDir"></param>
        /// <param name="tgtDir"></param>
        public static void CopyDirectory(string srcDir, string tgtDir)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(tgtDir);

            if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("父目录不能拷贝到子目录！");
            }

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
            }

            DirectoryInfo[] dirs = source.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, target.FullName + @"\" + dirs[j].Name);
            }
        } 

        /// <summary>
        /// 读取文本类型文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static string ReadTextFile(string path)
        {
            return System.IO.File.ReadAllText(path);

        }

        /// <summary>
        /// 写入文本类型文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static void WriteTextFile(string path,string content)
        {
            System.IO.File.WriteAllText(path, content);

        }

        private static string ToCSVSafeString(string s)
        {
            s = s ?? "";
            string oldstring = s;
            bool has_d = s.Contains(",");
            bool has_y = s.Contains("\"");
            if (has_y)
            {
                s = s.Replace("\"", "\"\"");
            }
            if (has_d || has_y)
            {
                s = "\"" + s + "\"";
            }
            return s;
        }
    }
}
