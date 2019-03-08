using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSharpEntityBuilder
{
    public class EntityCreater
    {
        /// <summary>
        /// 生成实体类。
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strNameSpace">命名空间名称</param>
		/// <param name="identityName">自增长主键名称</param>
        /// <param name="columNames">实体字段名列表</param>
        /// <param name="columDesc">实体字段描述列表</param>
        /// <param name="typeList">字段类型列表</param>
		/// <param name="delField">标记的删除字段</param>
        /// <returns>实体类的代码</returns>
        public static string CreateEntity(string mDbName,string strTableName,string strNameSpace,string identityName,string csName, string[] columNames, string[] columDesc, string[] typeList,string delField)
        {
			
			//model对象命名规则：按照表名称名称

			//className = className .Substring(0, 1).ToUpper() + className .Substring(1, className .Length - 1);//首字母大写

            StringBuilder sb = new StringBuilder();
            //文件头注释
            sb.AppendLine("/*");
			sb.AppendLine("* " + csName + ".cs");
			sb.AppendLine("* 表[" + strTableName + "]的实体类");
			sb.AppendLine("* 自动生成 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			sb.AppendLine("*/");
			sb.AppendLine();
			sb.AppendLine();
			//Using
            sb.AppendLine("using System;");
			sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();

            //namespace
            sb.AppendLine("namespace " + strNameSpace);
            sb.AppendLine("{");

            //class desc
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 表[" + mDbName + "." + strTableName + "]的实体类");
            sb.AppendLine("    /// </summary>");

            //class
			sb.AppendLine("    public partial class " + csName);
            sb.AppendLine("    {");
            sb.AppendLine();
            string allkeys = "";
            for (int i = 0; i < columNames.Length; i++)
            {
                if(i==0)
                    allkeys += "\""+columNames[i]+"\"";
                else
                    allkeys += ", \"" + columNames[i] + "\"";
            }

            sb.AppendLine("        public static readonly string TableName = \"" + mDbName + "." + strTableName + "\";//表名称");
			sb.AppendLine("        public static readonly string IdentityField = \"" + identityName + "\";//自增主键名称");
			sb.AppendLine("        public static readonly string DelField = \"" + delField + "\";//标记删除的字段名称");
            sb.AppendLine("        public static readonly string[] allkeys = { " + allkeys +" };//所有字段");

            sb.AppendLine();
            sb.AppendLine("        public List<string> keys = new List<string>();//用于临时保存添加或修改时的字段");
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("        #region 对应数据库的字段名称");
			sb.AppendLine("        //用F_当前缀，避免和属性相同。");
			for (int i = 0; i < columNames.Length; i++)
			{
				sb.AppendLine("        public  static readonly string F_" + columNames[i] + " = \"" + columNames[i] + "\";");

			}
			sb.AppendLine("        #endregion");
			sb.AppendLine();

			//字段属性
			sb.AppendLine("        #region 字段属性、变量");
            sb.AppendLine();
            for (int i = 0; i < columNames.Length; i++)
            {
				sb.AppendLine("        private " + typeList[i] + " _" + columNames[i] + ";//" + columDesc[i] + "");
            }
            

            //字段属性
            sb.AppendLine();
            for (int i = 0; i < columNames.Length; i++)
            {
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// " + columDesc[i]);
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        public " + typeList[i] + " " + columNames[i]);
                sb.AppendLine("        {");
                sb.AppendLine("            get { return" + " _" + columNames[i] + "; }");
                sb.AppendLine("            set { _" + columNames[i] + "=value; if(!keys.Contains(\"" + columNames[i] + "\")) keys.Add(\"" + columNames[i] + "\");}");
                sb.AppendLine("        }");
                sb.AppendLine();
            }
            sb.AppendLine("        #endregion");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }


        /// <summary>
        /// 生成实体类【简化版实体类，用于EF框架】
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="identityName">自增长主键名称</param>
        /// <param name="columNames">实体字段名列表</param>
        /// <param name="columDesc">实体字段描述列表</param>
        /// <param name="typeList">字段类型列表</param>
        /// <param name="delField">标记的删除字段</param>
        /// <returns>实体类的代码</returns>
        public static string CreateEntitySimplify(string mDbName, string strTableName, string strNameSpace, string csName, List<TableSchema> tableSheme, string delField)
        {

            //model对象命名规则：按照表名称名称

            //className = className .Substring(0, 1).ToUpper() + className .Substring(1, className .Length - 1);//首字母大写

            StringBuilder sb = new StringBuilder();
            //文件头注释
            sb.AppendLine("/*");
            sb.AppendLine("* " + csName + ".cs");
            sb.AppendLine("* 表[" + strTableName + "]的实体类");
            sb.AppendLine("* 自动生成 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("*/");
            sb.AppendLine();
            sb.AppendLine();
            //Using
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine();

            //namespace
            sb.AppendLine("namespace " + strNameSpace);
            sb.AppendLine("{");

            //class desc
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 表[" + mDbName + "." + strTableName + "]的实体类");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    [Serializable] ");
            sb.AppendLine("    [Table(\"" + strTableName + "\")] ");
            sb.AppendLine("    public class " + csName);
            sb.AppendLine("    {");
            sb.AppendLine();


            //字段属性
            sb.AppendLine("        #region 字段属性、变量");
            

            //字段属性
            sb.AppendLine();
            for (int i = 0; i < tableSheme.Count; i++)
            {
                sb.AppendLine("        /// <summary>");
                sb.AppendLine("        /// " + tableSheme[i].column_comment);
                sb.AppendLine("        /// </summary>");
                sb.AppendLine("        [Column(\"" + tableSheme[i].column_name + "\", TypeName = \"" + tableSheme[i].data_type + "\")" + (tableSheme[i].isPri ? ", Key" : "") + "" + (tableSheme[i].maxLength.Length > 0 ? ", MaxLength(" + tableSheme[i].maxLength + ")" : "") + "" + (!tableSheme[i].IS_NULLABLE ? ", Required" : "") + "]");
                sb.AppendLine("        public " + tableSheme[i].csharp_data_type2 + (tableSheme[i].csharp_data_type2!="string" && tableSheme[i].IS_NULLABLE?"?":"") + " " + tableSheme[i].column_name + " { get; set; }");
                
                sb.AppendLine();
            }
            sb.AppendLine("        #endregion");
            sb.AppendLine();
            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }


        /// <summary>
        /// 保存字符串到文件
        /// </summary>
        /// <param name="str">要保存的字符串</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveStrToFile(string str, string filePath)
        {
			SaveStrToFile(str, filePath, Encoding.Default);


        }
		/// <summary>
		/// 保存字符串到文件
		/// </summary>
		/// <param name="str">要保存的字符串</param>
		/// <param name="filePath">文件路径</param>
		public static void SaveStrToFile(string str, string filePath,Encoding en)
		{
			FileInfo info = new FileInfo(filePath);
			if (!info.Directory.Exists)
			{
				Directory.CreateDirectory(info.DirectoryName);
			}
			StreamWriter stream = null;
			//保存
			try
			{
				stream = new StreamWriter(filePath, false, en);
				stream.Write(str);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}
	}
}
