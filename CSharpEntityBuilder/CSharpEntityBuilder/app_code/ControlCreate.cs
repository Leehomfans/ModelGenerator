using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEntityBuilder
{
    public static class ControlCreate
    {
		/// <summary>
		/// 生成数据处理类。
		/// </summary>
		/// <param name="strTableName"></param>
		/// <param name="strNameSpace"></param>
		/// <param name="txtusing"></param>
		/// <param name="exten">data类名称的后缀，一般以DAL结尾</param>
		/// <returns></returns>
        public static string CreateControl(string strTableName, string strNameSpace,string txtusing,string exten)
        {
            //Model: TableInfo
            //Data: Table
            //strTableName = strTableName.Replace("_", "");//去掉表名称的下划线
			//数据访问类命名规则： demoDAL.cs


            string ModelName = strTableName.Substring(0, 1).ToUpper() + strTableName.Substring(1, strTableName.Length - 1) + "";//Model类名称：和表名一致

			string DataClassName = strTableName.Substring(0, 1).ToUpper() + strTableName.Substring(1, strTableName.Length - 1) + exten;  //数据访问类名称：跟上DAL后缀

            StringBuilder sb = new StringBuilder();
            //文件头注释
            sb.AppendLine("/*");
            sb.AppendLine("* " + DataClassName + ".cs");
            sb.AppendLine("* 表[" + strTableName + "]的数据处理类");
            sb.AppendLine("* 自动生成 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("*/");
            sb.AppendLine();
            sb.AppendLine();

            //Using
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();
            //sb.AppendLine("using Ordering.Model;");
            //sb.AppendLine("using Ordering.DataProvider;");
			txtusing = txtusing.Replace(";",";\n");
			sb.AppendLine(txtusing);



            //;
            sb.AppendLine();

            //namespace
            sb.AppendLine("namespace " + strNameSpace);
            sb.AppendLine("{");

            //class desc
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 表[" + strTableName + "]的数据处理方法");
            sb.AppendLine("    /// </summary>");

            //class
            sb.AppendLine("    public partial class " + DataClassName);
            sb.AppendLine("    {");
            sb.AppendLine();
            sb.AppendLine();

            //Save方法
            sb.AppendLine(CreateSave(strTableName, ModelName));

            //update方法
            sb.AppendLine(CreateUpdate(strTableName, ModelName));


            //delete方法
            sb.AppendLine(CreateDelete(strTableName, ModelName));
            
            //query方法
            sb.AppendLine(QueryCount(ModelName));
            sb.AppendLine(Query(ModelName));
            sb.AppendLine(Query2(ModelName));

            sb.AppendLine(QueryModel(strTableName, ModelName));
            sb.AppendLine(QueryModel2( ModelName));


            sb.AppendLine("    }");
            sb.AppendLine("}");



            return sb.ToString();
        }

        #region //createControl

        /// <summary>
		/// 生成Save方法
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="columNames"></param>
        /// <param name="columDesc"></param>
        /// <param name="typeList"></param>
        /// <returns></returns>
        public static string CreateSave(string strTableName, string ModelName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
			result.AppendLine("        /// 保存数据，根据主键ID自动判断insert或update");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"model\">" + strTableName + "的Model</param>");
            result.AppendLine("        /// <returns>id>0正常</returns>");
            result.AppendLine("        public static int Save(" + ModelName + " model)");
            result.AppendLine("        {");
            result.AppendLine("            string tableName = " + ModelName + ".TableName;");
            result.AppendLine("            string identityName = " + ModelName + ".IdentityField;");
			result.AppendLine("");
            result.AppendLine("            return Provider<" + ModelName + ">.Save(model.keys, tableName, identityName, model);");
            result.AppendLine("        }");
            return result.ToString();
        }

        //生成update方法
        public static string CreateUpdate(string strTableName, string ModelName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 保存数据，根据主键ID自动判断insert或update");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"model\">" + strTableName + "的Model</param>");
            result.AppendLine("        /// <returns>id>0正常</returns>");
            result.AppendLine("        public static int Update(string[] field, " + ModelName + " model, List<Condition> Select)");
            result.AppendLine("        {");
            result.AppendLine("            List<string> keys = new List<string>(field);");
            result.AppendLine("            string tableName = " + ModelName + ".TableName;");
            result.AppendLine("            string identityName = " + ModelName + ".IdentityField;");
            result.AppendLine("");
            result.AppendLine("            return Provider<" + ModelName + ">.Update(keys, tableName, identityName, model, Select);");
            result.AppendLine("        }");
            return result.ToString();
        }

        //生成delete方法
        #region
        public static string CreateDelete(string strTableName, string ModelName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 根据主键ID删除数据");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"id\">主键ID</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static int Del(int id)");
            result.AppendLine("        {");
            result.AppendLine("            string tableName = " + ModelName + ".TableName;");
            result.AppendLine("            string identityName = " + ModelName + ".IdentityField;");
            result.AppendLine("            string delFiled = " + ModelName + ".DelField;");
            result.AppendLine("            List<Condition> Select = new List<Condition> { ");
            result.AppendLine("                new Condition(identityName,id)");
            result.AppendLine("            };");

            result.AppendLine("");
            result.AppendLine("            return Provider<" + ModelName + ">.Del(tableName, delFiled, Select);");
            result.AppendLine("        }");
            return result.ToString();
        }
        #endregion


        //生成查询方法 QueryCount按条件返回记录数量
        public static string QueryCount(string ModelName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 查询数量");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"Select\">条件</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static int QueryCount(List<Condition> Select)");
            result.AppendLine("        {");
            result.AppendLine("            string tableName = " + ModelName + ".TableName;");
            result.AppendLine("            return Provider<" + ModelName + ">.QueryCount(tableName, Select);");
            result.AppendLine("        }");
            result.AppendLine("");

            return result.ToString();

        }

        //生成查询方法 查询(指定数量)
        public static string Query(string ModelName)
        {

            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 查询数据(指定数量)");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"field\">字段</param>");
            result.AppendLine("        /// <param name=\"Select\">条件</param>");
            result.AppendLine("        /// <param name=\"sort\">排序</param>");
            result.AppendLine("        /// <param name=\"Count\">查询的数量</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static List<" + ModelName + "> Query(string[] field, List<Condition> Select, string sort, int Count)");
            result.AppendLine("        {");
			result.AppendLine("            int totalrows=0;");
			result.AppendLine("            if (Count == 0)");
			result.AppendLine("                return Query(field, Select, sort, null, out totalrows);");
            result.AppendLine("");
            result.AppendLine("            Paging page = new Paging(0, Count);");
			result.AppendLine("            return Query(field, Select, sort, page, out totalrows);");
            result.AppendLine("        }");
            result.AppendLine("            ");
            return result.ToString();

        }
        //生成查询方法 查询 - 分页:参数：Model对象的名称
        
        public static string Query2( string ModelName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 查询数据");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"field\">字段</param>");
            result.AppendLine("        /// <param name=\"Select\">条件</param>");
            result.AppendLine("        /// <param name=\"sort\">排序</param>");
            result.AppendLine("        /// <param name=\"page\">分页对象</param>");
            result.AppendLine("        /// <returns></returns>");
			result.AppendLine("        public static List<" + ModelName + "> Query(string[] field, List<Condition> Select, string sort, Paging page, out int totalrows)");
            result.AppendLine("        {");
            result.AppendLine("            totalrows = 0;");
            result.AppendLine("            List<string> keys = new List<string>(field);");
            result.AppendLine("            string tableName = " + ModelName + ".TableName;");
            result.AppendLine("            List<" + ModelName + "> list = Provider<" + ModelName + ">.Query(keys, tableName, Select, sort, page, out totalrows);");
            result.AppendLine("            return list;");
            result.AppendLine("        }");
            result.AppendLine("            ");

            return result.ToString();

        }
        
        //生成查询方法 查询详细信息QueryModel
        public static string QueryModel(string strTableName, string ModelName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 根据主键ID查询一条数据");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"field\">要查询的字段</param>");
            result.AppendLine("        /// <param name=\"id\"></param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static " + ModelName + " QueryModel(string[] field, int id)");
            result.AppendLine("        {");
            result.AppendLine("            string identityName = " + ModelName + ".IdentityField;");
            result.AppendLine("            List<Condition> Select = new List<Condition> { ");
            result.AppendLine("                new Condition(identityName,id)");
            result.AppendLine("            };");
            result.AppendLine("            return QueryModel(field, Select, \"\");");

            result.AppendLine("        }");

            return result.ToString();

        }
        //生成查询方法 查询详细信息QueryModel
        public static string QueryModel2(string ModelName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 根据指定条件查询一条数据");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"field\">要查询的字段</param>");
            result.AppendLine("        /// <param name=\"Select\">条件</param>");
            result.AppendLine("        /// <param name=\"sort\">排序字段</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static " + ModelName + " QueryModel(string[] field, List<Condition> Select, string sort)");
            result.AppendLine("        {");
            result.AppendLine("            List<string> keys = new List<string>(field);");
            result.AppendLine("            " + ModelName + " model = new " + ModelName + "();");
            result.AppendLine("            string tableName = " + ModelName + ".TableName;");
            result.AppendLine("            string identityName = " + ModelName + ".IdentityField;");

            result.AppendLine("            " + ModelName + " obj = Provider<" + ModelName + ">.QueryModel(keys, tableName, Select, sort);");
            result.AppendLine("            if (obj != null)");
            result.AppendLine("                model = obj;");
            result.AppendLine("            return model;");

            result.AppendLine("        }");

            return result.ToString();

        }

        #endregion

        



        


        //uiLogic
        #region //createUiLogic

        public static string CreateUiLogic(string strTableName, string strNameSpace, string strClassName, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {


            StringBuilder sb = new StringBuilder();
            //文件头注释
            sb.AppendLine("// ================================================================================");
            sb.AppendLine("// File: " + strClassName + ".cs");
            sb.AppendLine("// Author: 自动生成");
            sb.AppendLine("// Date: " + DateTime.Now.ToString("yyyy年MM月dd日"));
            sb.AppendLine("// ================================================================================");
            sb.AppendLine("// Change History");
            sb.AppendLine("// ================================================================================");
            sb.AppendLine("// 		Date:		Author:				Description:");
            sb.AppendLine("// 		--------	--------			-------------------");
            sb.AppendLine("//    ");
            sb.AppendLine("// ================================================================================");

            //Using
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine();
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using MySql.Data;");
            sb.AppendLine("using MySql.Data.MySqlClient;");
            sb.AppendLine("using Common;");
            //
            //


            //;
            sb.AppendLine();

            //namespace
            sb.AppendLine("namespace " + strNameSpace);
            sb.AppendLine("{");

            //class desc
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 表[" + strTableName + "]");
            sb.AppendLine("    /// </summary>");

            //class
            sb.AppendLine("    public partial class " + strClassName);
            sb.AppendLine("    {");
            sb.AppendLine();
            sb.AppendLine("        private static DBHelp.DBMySql DB = new DBHelp.DBMySql();//数据操作对象");
            sb.AppendLine("        Control _control = new Control();");
            sb.AppendLine();
            sb.AppendLine("        private  int totalRow;");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 总记录");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        public int TotalRow");
            sb.AppendLine("        {");
            sb.AppendLine("            get { return totalRow; }");
            sb.AppendLine("            set { totalRow = value; }");
            sb.AppendLine("        }");
            sb.AppendLine("");

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 按条件返回记录数量 - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"whereKeyVal\">对应的条件</param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("         public static int QueryCount(List<Conditions.Condition> whereKeyVal)");
            sb.AppendLine("         {");
            sb.AppendLine("            return Control.QueryCount(whereKeyVal);");
            sb.AppendLine("         }");
            sb.AppendLine("");




            sb.AppendLine("");
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 查询指定数量 - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"showKeys\">字段</param>");
            sb.AppendLine("        /// <param name=\"whereKeyVal\">对应的条件</param>");
            sb.AppendLine("        /// <param name=\"OrderView\">排序字段(可为空)</param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("         public List<Model> Query(string showKeys, List<Conditions.Condition> whereList, string orderView, int Count)");
            sb.AppendLine("         {");
            sb.AppendLine("            List<Model> list = _control.Query(showKeys, whereList, orderView, Count);");
            sb.AppendLine("            totalRow = _control.TotalRow;");
            sb.AppendLine("            return list;");
            sb.AppendLine("         }");
            sb.AppendLine();
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 查询列表（分页） - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"showKeys\">字段</param>");
            sb.AppendLine("        /// <param name=\"whereKeyVal\">对应的条件</param>");
            sb.AppendLine("        /// <param name=\"OrderView\">排序字段(可为空)</param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("         public List<Model> Query(string showKeys, List<Conditions.Condition> whereList, string orderView, Common.PageControl page)");
            sb.AppendLine("         {");
            sb.AppendLine("            List<Model> list = _control.Query(showKeys, whereList, orderView, page);");
            sb.AppendLine("            totalRow = _control.TotalRow;");
            sb.AppendLine("            return list;");
            sb.AppendLine("         }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 查询主键ID查询详细信息 - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"showKeys\">字段</param>");
            sb.AppendLine("        /// <param name=\"Id\">主键ID</param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("         public static Model QueryModel(string showKeys, int Id)");
            sb.AppendLine("         {");
            sb.AppendLine("            List<Conditions.Condition> whereKeyVal = new List<Conditions.Condition>() {");
            sb.AppendLine("                new Conditions.Condition(\"" + columNames[0] + "\",Id.ToString())");
            sb.AppendLine("           };");
            sb.AppendLine("           return Control.QueryModel(showKeys, whereKeyVal,\"\") ;");
            sb.AppendLine("         }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 根据主键ID修改指定的字段值 - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"showKeys\">字段</param>");
            sb.AppendLine("        /// <param name=\"Id\">主键ID</param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("         public static int Update(List<Conditions.Condition> updateKeyVal, int Id)");
            sb.AppendLine("         {");
            sb.AppendLine("            List<Conditions.Condition> whereKeyVal = new List<Conditions.Condition>() {");
            sb.AppendLine("                new Conditions.Condition(\"" + columNames[0] + "\",Id.ToString())");
            sb.AppendLine("           };");
            sb.AppendLine("           return Control.Update(updateKeyVal, whereKeyVal);");
            sb.AppendLine("         }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 根据指定条件修改字段值 - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"updateKeyVal\">字段和值</param>");
            sb.AppendLine("        /// <param name=\"whereKeyVal\">条件</param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("         public static int Update(List<Conditions.Condition> updateKeyVal, List<Conditions.Condition> whereKeyVal)");
            sb.AppendLine("         {");
            sb.AppendLine("            return Control.Update(updateKeyVal, whereKeyVal);");
            sb.AppendLine("         }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 插入数据");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"model\">" + strTableName + "的Model</param>");
            sb.AppendLine("        /// <returns>大于0表示成功.返回添加后的自增ID</returns>");
            sb.AppendLine("         public static int Insert(Model model)");
            sb.AppendLine("         {");
            sb.AppendLine("            return Control.Insert(model);");
            sb.AppendLine("         }");
            sb.AppendLine();

            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 根据主键删除，多个主键，用逗号隔开 - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"id\">主键ID</param>");
            sb.AppendLine("        /// <returns>返回删除成功的数量</returns>");
            sb.AppendLine("         public static int Delete(string id)");
            sb.AppendLine("         {");
            sb.AppendLine("            return Control.Delete(id);");
            sb.AppendLine("         }");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("        //代码自动生成 END");
            sb.AppendLine("        //----------------------------------");

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        #endregion

        // 将数据库中的类型转换为c#的类型
        private static string GetCSharpTypeFromDbType(string dbType,string columNames)
        {
            // Convert.ToDateTime
            switch (dbType.ToLower())
            {
                case "tinyint":
                case "smallint":
                case "mediumint":
                case "int":
                case "numeric":
                    return "Convert.IsDBNull(dr[\"" + columNames + "\"])?0:Convert.ToInt32(dr[\"" + columNames + "\"])";
                case "bigint":
                    return "Convert.IsDBNull(dr[\"" + columNames + "\"])?0:Convert.ToInt64(dr[\"" + columNames + "\"])";
                case "decimal":
                    return "Convert.IsDBNull(dr[\"" + columNames + "\"])?0:Convert.ToDecimal(dr[\"" + columNames + "\"])";
                case "float":
                    return "float.Parse((dr[\"" + columNames + "\"].ToString()))";
                case "bit":
                    return "Convert.ToBoolean(dr[\"" + columNames + "\"])";
                case "enum":
                case "set":
                case "char":
                case "varchar":
				case "text":
				case "longtext":
					return "Convert.IsDBNull(dr[\"" + columNames + "\"])?\"\":dr[\"" + columNames + "\"].ToString()";
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblob":
                case "binary":
                case "varbinary":
                    return "Convert.ToByte(dr[\"" + columNames + "\"])";
                case "datetime":
                case "date":
                    return "Convert.IsDBNull(dr[\"" + columNames + "\"])?DateTime.Now:Convert.ToDateTime(dr[\"" + columNames + "\"])";
                default:
                    return "dr[\"" + columNames + "\"].ToString()";
            }
        }
    }
}
