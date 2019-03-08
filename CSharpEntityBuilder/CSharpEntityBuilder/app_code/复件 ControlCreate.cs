using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEntityBuilder
{
    public static class ControlCreate
    {
        /// <summary>
        /// 生成实体类。
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strClassName">类名称</param>
        /// <param name="columNames">实体字段名列表</param>
        /// <param name="columDesc">实体字段描述列表</param>
        /// <param name="typeList">字段类型列表</param>
        /// <returns>实体类的代码</returns>
        public static string CreateControl(string strTableName, string strNameSpace, string strClassName, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
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
            sb.AppendLine("    public class " + strClassName);
            sb.AppendLine("    {");
            sb.AppendLine();
            sb.AppendLine("        private static DBHelp.DBMySql DB = new DBHelp.DBMySql();//数据操作对象");
            sb.AppendLine();
            sb.AppendLine("        private int totalRow;");
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

            sb.AppendLine();
            //insert方法
            sb.AppendLine(CreateInsert(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            //update方法
            sb.AppendLine(CreateUpdate(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            //delete方法
            sb.AppendLine(CreateDelete(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            
            //query方法
            sb.AppendLine(CreateQuery0(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            sb.AppendLine(CreateQuery1(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            sb.AppendLine(CreateQuery2(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            sb.AppendLine(CreateQuery3(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            sb.AppendLine(CreateQuery4(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));

            
            sb.AppendLine(CreateSetModel(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));

            //Common里公共的方法
            //sb.AppendLine(CreateGetWhereSql(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            //sb.AppendLine(CreateGetWhereSql_Str(strTableName, strNameSpace, columNames, columDesc, typeList, priKey, auto_incr));
            //sb.AppendLine(CreatePara());

            sb.AppendLine("        //代码自动生成 END");
            sb.AppendLine("        //----------------------------------");

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        #region //createControl

        /// <summary>
        /// 生成insert方法
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="columNames"></param>
        /// <param name="columDesc"></param>
        /// <param name="typeList"></param>
        /// <returns></returns>
        public static string CreateInsert(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 添加数据 - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"model\">" + strTableName + "的Model</param>");
            result.AppendLine("        /// <returns>id>0正常</returns>");
            result.AppendLine("        public static int Insert(Model model)");
            result.AppendLine("        {");
            result.AppendLine("            int id = 0;");

            string sql = "            string sql = \"insert into " + strTableName + " (";         //执行的sql语句
            string sqlLog = "            string sqlLog = \"insert into " + strTableName + " (";      //写入日志的sql语句
            string fieldName = string.Empty;
            string fieldPar = string.Empty;
            string fieldValue = string.Empty;
            for (int i = 0; i < columNames.Length; i++)
            {
                //排除主键
                if (!(priKey[i].Equals("PRI") && auto_incr[i].Equals("auto_increment")))
                {

                    if (i == columNames.Length - 1)
                    {
                        fieldName = fieldName + "`"+columNames[i]+"`";
                        fieldPar = fieldPar + "@" + columNames[i];
                        fieldValue = fieldValue + "'\" + " + "model." + columNames[i] + " + \"'";
                    }
                    else
                    {
                        fieldName = fieldName + "`" + columNames[i] + "`,";
                        fieldPar = fieldPar + "@" + columNames[i] + ",";
                        fieldValue = fieldValue + "'\" + " + "model." + columNames[i] + " + \"',";
                    }
                }

            }
            sql = sql + fieldName + ") values (" + fieldPar + "); SELECT LAST_INSERT_ID();\";  //执行的sql语句";
            sqlLog = sqlLog + fieldName + ") values (" + fieldValue + "); SELECT LAST_INSERT_ID();\";  //写入日志的sql语句";
            result.AppendLine(sql);
            result.AppendLine(sqlLog);
            result.AppendLine("            MySqlParameter[] para = new MySqlParameter[]{");
            // new MySqlParameter("@name",model.Name),
            string strParameter = string.Empty;
            for (int i = 0; i < columNames.Length; i++)
            {
                //排除主键
                if (!(priKey[i].Equals("PRI") && auto_incr[i].Equals("auto_increment")))
                {
                    if (i == columNames.Length - 1)
                    {
                        strParameter = strParameter + "new MySqlParameter(\"@" + columNames[i] + "\",model." + columNames[i] + ")";
                    }
                    else
                    {
                        strParameter = strParameter + "new MySqlParameter(\"@" + columNames[i] + "\",model." + columNames[i] + "),";
                    }
                }

            }
            result.AppendLine("                " + strParameter);
            result.AppendLine("            };");
            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                id = Convert.ToInt32(DB.GetScalar(CommandType.Text, sql, para));");
            result.AppendLine("            }");
            result.AppendLine("            catch");
            result.AppendLine("            {");
            result.AppendLine("            }");
            result.AppendLine("            finally");
            result.AppendLine("            {");
            result.AppendLine("            }");
            result.AppendLine("            return id;");
            result.AppendLine("        }");
            return result.ToString();
        }

        //生成update方法
        public static string CreateUpdate(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 根据条件修改指定字段的值 - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"updateKeyVal\">要修改的字段及对应的值</param>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">条件</param>");
            result.AppendLine("        /// <returns>大于0 表示修改成功</returns>");
            result.AppendLine("        public static int Update(List<Conditions.Condition> updateKeyVal, List<Conditions.Condition> whereKeyVal)");
            result.AppendLine("        {");
            result.AppendLine("            if (updateKeyVal == null || updateKeyVal.Count == 0) return -1;");
            result.AppendLine("            StringBuilder sb_sql = new StringBuilder();");
            result.AppendLine("            sb_sql.Append(\"update " + strTableName + " set \");");
            result.AppendLine("            ");

            result.AppendLine("            foreach (Conditions.Condition con in updateKeyVal)");
            result.AppendLine("            {");
            result.AppendLine("                con.Key = con.Key.Replace(\"'\", \"’\");//sql特殊字符替换");
            result.AppendLine("                con.Value = con.Value.Replace(\"'\", \"’\");//sql特殊字符替换");
            result.AppendLine("                sb_sql.Append(con.Key + \" = '\" + con.Value + \"' , \");");
            result.AppendLine("            }");
            result.AppendLine("            string sql = sb_sql.ToString();");
            result.AppendLine("            sql = sql.Substring(0, sql.Length - 2);//去掉最后一个, ");

            result.AppendLine("            string sql_where_str = ControlWhere.GetWhereSql(whereKeyVal);//where后的条件");
            result.AppendLine("            sql += sql_where_str;");
            result.AppendLine("            int count = 0;");

            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                count = DB.ExecuteSql(CommandType.Text, sql,Common.ControlWhere.Par(whereKeyVal));");
            result.AppendLine("            }");
            result.AppendLine("            catch");
            result.AppendLine("            {");
            result.AppendLine("            }");
            result.AppendLine("            finally");
            result.AppendLine("            {");
            result.AppendLine("            }");
            result.AppendLine("            return count;");
            result.AppendLine("        }");
            return result.ToString();
        }

        //生成查询方法 QueryCount按条件返回记录数量
        public static string CreateQuery0(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 按条件返回记录数量 - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">条件</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static int QueryCount(List<Conditions.Condition> whereKeyVal)");
            result.AppendLine("        {");

            result.AppendLine("            int count = 0;");
            result.AppendLine("            string sql = \"select count(0) from "+strTableName+" \";");
            result.AppendLine("            string sql_where_str = ControlWhere.GetWhereSql(whereKeyVal);");
            result.AppendLine("            sql += sql_where_str;");
            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                using (MySqlDataReader dr = DB.ExecuteReader(CommandType.Text, sql,ControlWhere.Par(whereKeyVal)))");
            result.AppendLine("                {");
            result.AppendLine("                    if (dr.Read())");
            result.AppendLine("                    {");
            result.AppendLine("                        count = Convert.ToInt32(dr[0]);");
            result.AppendLine("                    }");
            result.AppendLine("                }");

            result.AppendLine("            }");
            result.AppendLine("            catch");
            result.AppendLine("            {");

            result.AppendLine("            }");
            result.AppendLine("            return count;");
            result.AppendLine("        }");
            result.AppendLine("            ");

            return result.ToString();

        }

        //生成查询方法 QueryScalar按条件查询首行首列的数据
        public static string CreateQuery1(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {

            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 按条件查询首行首列的数据 - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"key\">字段</param>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">条件</param>");
            result.AppendLine("        /// <param name=\"OrderView\">排序字段(可为空)</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static string QueryScalar(string key, List<Conditions.Condition> whereKeyVal, string OrderView)");
            result.AppendLine("        {");

            result.AppendLine("            string obj = \"\";");
            result.AppendLine("            string sql = \"select \" + key + \" from "+strTableName+" \";");
            result.AppendLine();
            result.AppendLine("            string sql_where_str = ControlWhere.GetWhereSql(whereKeyVal);");
            result.AppendLine("            sql += sql_where_str;");
            result.AppendLine("            if (OrderView != \"\")");
            result.AppendLine("            {");
            result.AppendLine("                sql += \" ORDER BY \" + OrderView;");
            result.AppendLine("            }");
            result.AppendLine("            sql += \" limit 1\";");

            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                using (MySqlDataReader dr = DB.ExecuteReader(CommandType.Text, sql,ControlWhere.Par(whereKeyVal)))");
            result.AppendLine("                {");
            result.AppendLine("                    if (dr.Read())");
            result.AppendLine("                    {");
            result.AppendLine("                        obj = dr[0].ToString();");
            result.AppendLine("                    }");
            result.AppendLine("                }");

            result.AppendLine("            }");
            result.AppendLine("            catch");
            result.AppendLine("            {");
            result.AppendLine("                obj = \"\";");
            result.AppendLine("            }");
            result.AppendLine("            return obj;");
            result.AppendLine("        }");
            result.AppendLine("            ");

            return result.ToString();

        }

        //生成查询方法 查询(指定数量)
        public static string CreateQuery2(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 查询(指定数量) - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"key\">字段</param>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">条件</param>");
            result.AppendLine("        /// <param name=\"OrderView\">排序字段(可为空).格式- lector_id desc ,course_id desc（多个用,号隔开）</param>");
            result.AppendLine("        /// <param name=\"Count\">查询的数量</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public List<Model> Query(string key, List<Conditions.Condition> whereKeyVal, string OrderView, int Count)");
            result.AppendLine("        {");
            result.AppendLine("            List<Model> list = new List<Model>();");
            
            result.AppendLine("            string sql = \"select \" + key + \" from " + strTableName + " \";");
            result.AppendLine("            string sql_where_str = ControlWhere.GetWhereSql(whereKeyVal);");
            result.AppendLine("            sql += sql_where_str;");

            result.AppendLine("            if (OrderView != \"\")");
            result.AppendLine("            {");
            result.AppendLine("                sql += \" ORDER BY \" + OrderView;");
            result.AppendLine("            }");
            result.AppendLine("            if (Count > 0)");
            result.AppendLine("            {");
            result.AppendLine("                sql += \" limit \" + Count;");
            result.AppendLine("            }");

            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                using (MySqlDataReader dr = DB.ExecuteReader(CommandType.Text, sql,ControlWhere.Par(whereKeyVal)))");
            result.AppendLine("                {");
            result.AppendLine("                    while (dr.Read())");
            result.AppendLine("                    {");
            result.AppendLine("                        //读取要显示的字段。");
            result.AppendLine("                        list.Add(SetModel(key, dr));");
            result.AppendLine("                    }");
            result.AppendLine("                }");

            result.AppendLine("            }");
            result.AppendLine("            catch");
            result.AppendLine("            {");

            result.AppendLine("            }");
            result.AppendLine("            return list;");
            result.AppendLine("        }");
            result.AppendLine("            ");

            return result.ToString();

        }
        //生成查询方法 查询 - 分页
        public static string CreateQuery3(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 查询 - 分页 - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"key\">字段</param>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">条件</param>");
            result.AppendLine("        /// <param name=\"OrderView\">排序字段(可为空).格式- lector_id desc ,course_id desc（多个用,号隔开）</param>");
            result.AppendLine("        /// <param name=\"page\">查询的数量</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public List<Model> Query(string key, List<Conditions.Condition> whereKeyVal, string OrderView, Common.PageControl page)");
            result.AppendLine("        {");
            result.AppendLine("            List<Model> list = new List<Model>();");

            result.AppendLine("            string sql = \"select \" + key + \" from " + strTableName + " \";");
            result.AppendLine("            string sqlCount = \"select count(0) from " + strTableName + " \";");
            result.AppendLine("            string sql_where_str = ControlWhere.GetWhereSql(whereKeyVal);");
            result.AppendLine("            sql += sql_where_str;");

            result.AppendLine("            if (OrderView != \"\")");
            result.AppendLine("            {");
            result.AppendLine("                sql += \" ORDER BY \" + OrderView;");
            result.AppendLine("            }");
            result.AppendLine("            sql += \" limit \" + (page.CurrentPage - 1) * page.PageSize + \" ,\" + page.PageSize;");
            result.AppendLine("            sqlCount += sql_where_str;");
            result.AppendLine("            totalRow = page.GetTotalCount(sqlCount, ControlWhere.Par(whereKeyVal));//总记录数");

            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                using (MySqlDataReader dr = DB.ExecuteReader(CommandType.Text, sql,ControlWhere.Par(whereKeyVal)))");
            result.AppendLine("                {");
            result.AppendLine("                    while (dr.Read())");
            result.AppendLine("                    {");
            result.AppendLine("                        //读取要显示的字段。");
            result.AppendLine("                        list.Add(SetModel(key, dr));");
            result.AppendLine("                    }");
            result.AppendLine("                }");

            result.AppendLine("            }");
            result.AppendLine("            catch");
            result.AppendLine("            {");

            result.AppendLine("            }");
            result.AppendLine("            return list;");
            result.AppendLine("        }");
            result.AppendLine("            ");

            return result.ToString();

        }
        //生成查询方法 查询详细信息QueryModel
        public static string CreateQuery4(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 查询详细信息 - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"key\">字段</param>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">条件</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        public static Model QueryModel(string key, List<Conditions.Condition> whereKeyVal)");
            result.AppendLine("        {");

            result.AppendLine("            Model m = new Model();");
            result.AppendLine("            string sql = \"select \" + key + \" from "+strTableName+" \";");
            result.AppendLine("            string sql_where_str = ControlWhere.GetWhereSql(whereKeyVal);");
            result.AppendLine("            sql += sql_where_str + \" limit 1\";");
            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                using (MySqlDataReader dr = DB.ExecuteReader(CommandType.Text, sql,ControlWhere.Par(whereKeyVal)))");
            result.AppendLine("                {");
            result.AppendLine("                    if (dr.Read())");
            result.AppendLine("                    {");
            result.AppendLine("                        //读取要显示的字段。");
            result.AppendLine("                        m = SetModel(key, dr);");
            result.AppendLine("                    }");
            result.AppendLine("                }");

            result.AppendLine("            }");
            result.AppendLine("            catch (Exception ex)");
            result.AppendLine("            {");
            result.AppendLine("            }");
            result.AppendLine("            return m;");
            result.AppendLine("        }");
            result.AppendLine("            ");

            return result.ToString();

        }

        #endregion

        //生成delete方法
        #region
        public static string CreateDelete(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            //不支持delete
            return "";

            StringBuilder result = new StringBuilder();

            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 根据主键删除数据 - " + strTableName);
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"id\">多个主键，用逗号隔开</param>");
            result.AppendLine("        /// <returns>返回删除成功的数量</returns>");
            result.AppendLine("        public static int Delete(string id)");
            result.AppendLine("        {");
            result.AppendLine("            LogOperEntity log = new LogOperEntity();");
            result.AppendLine("            log.Name = \"" + strNameSpace + ".Delete\";");
            result.AppendLine("            log.Remark = \"传入参数:(id);\";");
            result.AppendLine("            int count = 0;");

            string sql = "            string sql = \"DELETE   FROM " + strTableName + " where " + columNames[0] + " IN(\" + id.Trim() + \");\";";//执行的sql语句
            result.AppendLine(sql);

            result.AppendLine("            try");
            result.AppendLine("            {");
            result.AppendLine("                count = DB.ExecuteSql(CommandType.Text, sql,null);");
            result.AppendLine("            }");
            result.AppendLine("            catch (Exception ex)");
            result.AppendLine("            {");
            result.AppendLine("                string error = \"错误号：\" + ex.Source.ToString() + \"；错误信息：\" + ex.Message;");
            result.AppendLine("                log.Remark += error.Replace(\"'\",\"#\");");
            result.AppendLine("                log.Style = 1;");
            result.AppendLine("            }");
            result.AppendLine("            finally");
            result.AppendLine("            {");

            result.AppendLine("            }");
            result.AppendLine("            return count;");
            result.AppendLine("        }");
            return result.ToString();
        }
        #endregion



        //私有方法 //已提取为Common公共方法
        #region

        public static string CreateSetModel(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 赋值(读取指定字段)的公共部分。以后改动(比如数据表的字段或Model变动)时，只需修改这里");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"ShowKeys\">ShowKeys</param>");
            result.AppendLine("        /// /<param name=\"dr\">MySqlDataReader</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        private static Model SetModel(string ShowKeys, MySqlDataReader dr)");
            result.AppendLine("        {");
            result.AppendLine("            Model m = new Model();");
            result.AppendLine("            foreach (string ShowKey in ShowKeys.Split(','))");
            result.AppendLine("            {");

            string fieldName = string.Empty;
            string fieldPar = string.Empty;
            string fieldValue = string.Empty;

            StringBuilder sb_1 = new StringBuilder();
            for (int i = 0; i < columNames.Length; i++)
            {
                string strDr = GetCSharpTypeFromDbType(typeList[i], columNames[i].ToLower());
                if (i == 0)
                {

                    sb_1.AppendLine("                if (ShowKey.ToLower() == \"" + columNames[i].ToLower() + "\") m." + columNames[i] + " = " + strDr + ";");
                }
                else
                {
                    sb_1.AppendLine("                else if (ShowKey.ToLower() == \"" + columNames[i].ToLower() + "\") m." + columNames[i] + " = " + strDr + ";");
                }
            }
            result.Append(sb_1);
            result.AppendLine("            }");
            result.AppendLine("            return m;");
            result.AppendLine("        }");

            return result.ToString();
        }

        public static string CreateGetWhereSql(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 拼接 where 后的条件语句 MySqlParameter方式");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">whereKeyVal[条件集合]</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        private static string GetWhereSql(List<Conditions.Condition> whereKeyVal)");
            result.AppendLine("        {");


            result.AppendLine("            StringBuilder sb_where = new StringBuilder();");
            result.AppendLine("            if (whereKeyVal != null && whereKeyVal.Count > 0)");
            result.AppendLine("            {");
            result.AppendLine("                sb_where.Append(\" where \");");
            result.AppendLine("                foreach (Conditions.Condition con in whereKeyVal)");
            result.AppendLine("                {");
            result.AppendLine("                    con.Key = con.Key.Replace(\"'\", \"’\");//sql特殊字符替换");
            result.AppendLine("                    con.Value = con.Value.Replace(\"'\", \"’\");//sql特殊字符替换");
            result.AppendLine("                    //使用mysqlparameter");
            result.AppendLine("                    if (con.Mark.ToLower() == \"in\")");
            result.AppendLine("                    {");
            result.AppendLine("                        //如果操作符为in。//value为 1,3,4,只能是数字。");
            result.AppendLine("                        sb_where.Append(con.Key + \" \" + con.Mark + \" (\" + con.Value.Trim() + \") and \");");
            result.AppendLine("                    }");
            result.AppendLine("                    else");
            result.AppendLine("                    {");
            result.AppendLine("                        sb_where.Append(con.Key + \" \" + con.Mark + \" @\" + con.Key + \" and \");");

            result.AppendLine("                    }");
            result.AppendLine("                }");
            result.AppendLine("                string sql_where_str = sb_where.ToString();");
            result.AppendLine("                sql_where_str = sql_where_str.Substring(0, sql_where_str.Length - 4);//去掉最后1个and 字符");
            result.AppendLine("                return sql_where_str;");
            result.AppendLine("            }");
            result.AppendLine("            return \"\";");
            result.AppendLine("        }");

            result.AppendLine("        ");
            return result.ToString();
        }
        public static string CreateGetWhereSql_Str(string strTableName, string strNameSpace, string[] columNames, string[] columDesc, string[] typeList, string[] priKey, string[] auto_incr)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 拼接 where 后的条件语句 字符串拼接方式，返回拼接后的sql");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">whereKeyVal[条件集合]</param>");
            result.AppendLine("        /// <returns></returns>");
            result.AppendLine("        private static string GetWhereSql_Str(List<Conditions.Condition> whereKeyVal)");
            result.AppendLine("        {");


            result.AppendLine("            StringBuilder sb_where = new StringBuilder();");
            result.AppendLine("            if (whereKeyVal != null && whereKeyVal.Count > 0)");
            result.AppendLine("            {");
            result.AppendLine("                sb_where.Append(\" where \");");
            result.AppendLine("                foreach (Conditions.Condition con in whereKeyVal)");
            result.AppendLine("                {");
            result.AppendLine("                    con.Key = con.Key.Replace(\"'\", \"’\");//sql特殊字符替换");
            result.AppendLine("                    con.Value = con.Value.Replace(\"'\", \"’\");//sql特殊字符替换");
            result.AppendLine("                    //如果value为纯数字");
            result.AppendLine("                    if (Common.Utils.IsNumeric(con.Value))");
            result.AppendLine("                    {");
            result.AppendLine("                        if (con.Mark == \"like\")");
            result.AppendLine("                        {");
            result.AppendLine("                            sb_where.Append(con.Key + \" \" + con.Mark + \" '%\" + con.Value + \"%' and \");");
            result.AppendLine("                        }");
            result.AppendLine("                        else if (con.Mark.ToLower() == \"in\")");
            result.AppendLine("                        {");
            result.AppendLine("                            //如果操作符为in. value为 1,3,4,只能是数字");
            result.AppendLine("                            sb_where.Append(con.Key + \" \" + con.Mark + \" (\" + con.Value.Trim() + \") and \");");
            result.AppendLine("                        }");
            result.AppendLine("                        else");
            result.AppendLine("                        {");
            result.AppendLine("                            sb_where.Append(con.Key + \" \" + con.Mark + \" \" + con.Value + \" and \");");
            result.AppendLine("                        }");
            result.AppendLine("                    }");

            result.AppendLine("                    else");
            result.AppendLine("                    {");
            result.AppendLine("                        if (con.Mark.ToLower() == \"like\")");
            result.AppendLine("                        {");
            result.AppendLine("                            sb_where.Append(con.Key + \" \" + con.Mark + \" '%\" + con.Value + \"%' and \");");
            result.AppendLine("                        }");
            result.AppendLine("                        else if (con.Mark.ToLower() == \"in\")");
            result.AppendLine("                        {");
            result.AppendLine("                            //如果操作符为in. value为 1,3,4,只能是数字");
            result.AppendLine("                            sb_where.Append(con.Key + \" \" + con.Mark + \" (\" + con.Value + \") and \");");
            result.AppendLine("                        }");
            result.AppendLine("                        else");
            result.AppendLine("                        {");
            result.AppendLine("                            sb_where.Append(con.Key + \" \" + con.Mark + \" '\" + con.Value + \"' and \");");
            result.AppendLine("                        }");
            result.AppendLine("                   }");
            result.AppendLine("                }");
            result.AppendLine("                string sql_where_str = sb_where.ToString();");
            result.AppendLine("                sql_where_str = sql_where_str.Substring(0, sql_where_str.Length - 4);//去掉最后1个and 字符");
            result.AppendLine("                return sql_where_str;");
            result.AppendLine("            }");
            result.AppendLine("            return \"\";");
            result.AppendLine("        }");

            result.AppendLine("        ");
            return result.ToString();
        }


        
        public static string CreatePara()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("        /// <summary>");
            result.AppendLine("        /// 参数化 构造。格式 new MySqlParameter(\"@Key\",Value);");
            result.AppendLine("        /// </summary>");
            result.AppendLine("        /// <param name=\"whereKeyVal\">whereKeyVal</param>");
            result.AppendLine("        /// <returns>返回MySqlParameter[]</returns>");
            result.AppendLine("        private static MySqlParameter[] Par(List<Conditions.Condition> whereKeyVal)");
            result.AppendLine("        {");
            result.AppendLine("            int arr_count = 0;");
            result.AppendLine("            if (whereKeyVal == null || whereKeyVal.Count == 0)");
            result.AppendLine("            {");
            result.AppendLine("                return null;");
            result.AppendLine("            }");
            result.AppendLine("            foreach (Conditions.Condition c in whereKeyVal)");
            result.AppendLine("            {");
            result.AppendLine("                if (c.Mark != \"in\")");
            result.AppendLine("                {");
            result.AppendLine("                    arr_count++;");
            result.AppendLine("                }");
            result.AppendLine("            }");


            result.AppendLine("            MySqlParameter[] pa = new MySqlParameter[arr_count];");
            result.AppendLine("            int index = 0;");
            result.AppendLine("            foreach (Conditions.Condition c in whereKeyVal)");
            result.AppendLine("            {");
            result.AppendLine("                if (c.Mark != \"in\")");
            result.AppendLine("                {");
            result.AppendLine("                    pa[index] = new MySqlParameter(\"@\" + c.Key, c.Value);");
            result.AppendLine("                    index++;");
            result.AppendLine("                }");
            result.AppendLine("            }");
            result.AppendLine("            return pa;");
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
            sb.AppendLine("    public class " + strClassName);
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
            sb.AppendLine("        /// <summary>");
            sb.AppendLine("        /// 按条件查询首行首列的数据 - " + strTableName + "");
            sb.AppendLine("        /// </summary>");
            sb.AppendLine("        /// <param name=\"key\">字段</param>");
            sb.AppendLine("        /// <param name=\"whereKeyVal\">对应的条件</param>");
            sb.AppendLine("        /// <param name=\"OrderView\">排序字段(可为空)</param>");
            sb.AppendLine("        /// <returns></returns>");
            sb.AppendLine("         public static string QueryScalar(string key, List<Conditions.Condition> whereKeyVal, string OrderView)");
            sb.AppendLine("         {");
            sb.AppendLine("            return Control.QueryScalar(key, whereKeyVal,OrderView);");
            sb.AppendLine("         }");
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
            sb.AppendLine("           return Control.QueryModel(showKeys, whereKeyVal);;");
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
