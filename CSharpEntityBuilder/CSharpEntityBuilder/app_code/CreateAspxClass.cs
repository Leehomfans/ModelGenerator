using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Caching;
using System.Web;
using System.IO;
using System.Windows.Forms;
namespace CSharpEntityBuilder.app_code
{
    public class CreateAspxClass
    {
      public static void CreateHtml_Information(string[] a, string[] b,string modelname)
        {
            string uploadDir = Application.StartupPath+"/model/"; //HttpContext.Current.Server.MapPath("/model/");//html存放目录
            //if (!System.IO.Directory.Exists(uploadDir))
            //{
            //    //目录不存在--创建
            //    System.IO.Directory.CreateDirectory(uploadDir);
            //}

            string template = "webmodel.aspx";
            string templateUrl = Application.StartupPath +"\\webmodel.aspx";//HttpContext.Current.Server.MapPath("~/app_code/" + template);//获取模板文件路径
            string templateContent = System.IO.File.ReadAllText(templateUrl, System.Text.Encoding.UTF8);//读取模板文件内容
            string str = templateContent;
            //htm命名：6位数ID，不足用0补齐
            string fileName = modelname + ".aspx";
            #region 标签替换
            string head="";//显示的列名
            string body="";//显示列名值
            foreach(string s in a)
            {
                head +="<td>"+s+"</td>";
            }
            foreach(string var in b)
            {
                body +="<td>model."+var+"</td>";
            }
            str = str.Replace("{$head}", head);//替换显示的列名
            str = str.Replace("{$body}", body);//列名值
            str = str.Replace("{$namespace}", modelname);//命名空间
                      #endregion
            StreamWriter sw = new StreamWriter(uploadDir + fileName, false, System.Text.Encoding.UTF8);
            sw.Write(str);
            sw.Flush();
            sw.Dispose();
            sw.Close();

          
    }
}
}
