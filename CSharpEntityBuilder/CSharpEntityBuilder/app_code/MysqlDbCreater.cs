using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace CSharpEntityBuilder
{
    public class MysqlDbCreater : ICreator
    {
        #region 私有变量

        string mConnString = string.Empty;
        string mTableName = string.Empty;
        string mDbName = string.Empty;//数据库名称
        //string mDbName = string.Empty;
        MySqlConnection con = null;
        #endregion

        public EntityCreater EntityCreater
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        #region ICreator接口实现

        /// <summary>
        /// 设置数据库连接串。
        /// </summary>
        /// <param name="pServerName">服务器名称</param>
        /// <param name="pPort">端口号，若为空则表示使用默认端口</param>
        /// <param name="pDbName">数据库名称</param>
        /// <param name="pConnMode">登录类型：Windows身份验证；Sql验证</param>
        /// <param name="pUser">用户名</param>
        /// <param name="pPwd">密码</param>
        public void InitConn(string pServerName, string pPort, string pDbName, string pUser, string pPwd)
        {
            //Data Source=192.168.1.61;Port=3306;User ID=root;Password=admin;DataBase=yourDbName;Allow Zero Datetime=true;Charset=gbk;
            mConnString = "Data Source=" + pServerName + ";";

            if (!string.IsNullOrEmpty(pPort))
            {
                mConnString += "Port=" + pPort + ";";
            }
            mConnString += "DataBase=" + pDbName + ";";
            mConnString += "User ID=" + pUser + ";";
            mConnString += "Password=" + pPwd + ";";
            mConnString += "Allow Zero Datetime=true;";
            // mConnString += "Charset=gbk;";
        }

        /// <summary>
        /// 设置数据库连接串。
        /// </summary>
        /// <param name="pConStr">链接串</param>
        public void InitConn(string pConStr)
        {
            mConnString = pConStr;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        public void ConnDB()
        {
            con = new MySqlConnection(mConnString);
            if (con.State == ConnectionState.Closed)
                con.Open();
        }



		/// <summary>
		/// 设置要生成实体的table
		/// </summary>
		/// <param name="tableName">表名</param>
		public void InitTableName(string tableName, string dbName)
		{
			mTableName = tableName;
			mDbName = dbName;
		}





        /// <summary>
        /// 获取所有表。
        /// </summary>
        /// <returns></returns>
        public List<string> GetTables()
        {
            string strSQL = string.Format("use {0};show tables;", con.Database);
            MySqlCommand com = new MySqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlDataReader reader = com.ExecuteReader();
            List<string> tableList = new List<string>();
            while (reader.Read())
            {
                tableList.Add(reader[0].ToString());
            }

            reader.Close();

            return tableList;
        }


        /// <summary>
        /// 根据表前缀模糊查询表，或查询多个表
        /// </summary>
        /// <returns></returns>
        public List<string> Tables()
		{
			List<string> list = new List<string>();

            mTableName = mTableName.Replace("*", "%");
            bool isLike = mTableName.Contains("%");

            string sql = string.Format("select table_name from information_schema.tables where table_schema='{0}'   and table_type='base table' and TABLE_NAME " + (isLike?"like":"=") + "  '{1}" + "';", mDbName, mTableName);
			MySqlCommand com = new MySqlCommand(sql, con);
			if (con.State == ConnectionState.Closed)
			{
				con.Open();
			}
			MySqlDataReader reader = com.ExecuteReader();
			List<string> columNames = new List<string>();
			List<string> columDesc = new List<string>();
			List<string> typeList = new List<string>();
			while (reader.Read())
			{
				list.Add(reader[0].ToString());
			}

			reader.Close();
			con.Close();

			return list;

		}





        /// <summary>
        /// 构造实体类（的字符串）。
        /// </summary>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strClassName">类名称</param>
        /// <param name="strFilePath">生成类的地址</param>
        /// <param name="isSimple">=true表示生成简化版Model实体，默认为false</param>
        /// <returns>返回类的字符串</returns>
        public string CreateEntity(string strNameSpace, string strFilePath, string delField, bool isSimple=false)
        {
            //select column_name,data_type,column_comment from information_schema.columns where table_name='RMBCode';
            string strSQL = string.Format("select  column_name,data_type,column_comment,column_key,EXTRA,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE,COLUMN_KEY,COLUMN_TYPE   from information_schema.columns where table_name='{0}' and TABLE_SCHEMA='{1}'", mTableName, mDbName);
            MySqlCommand com = new MySqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string identityName = "";//主键名称
			string isdel = delField; // "isdel";//标记删除的字段名称

            MySqlDataReader reader = com.ExecuteReader();
            List<string> columNames = new List<string>();
            List<string> columDesc = new List<string>();
            List<string> typeList = new List<string>();//对应的c#类型

            List<TableSchema> tableSheme = new List<TableSchema>();

            while (reader.Read())
            {
                TableSchema tSchema = new TableSchema();

                string column_key = reader["column_key"].ToString();
                string extra = reader["extra"].ToString();
                bool unsigned = reader["COLUMN_TYPE"].ToString().Contains("unsigned");
                if ((column_key.Equals("PRI") && extra.Equals("auto_increment")))
                {
                    identityName = reader["column_name"].ToString();
                }

                columNames.Add(reader["column_name"].ToString());
                columDesc.Add(reader["column_comment"].ToString());
                typeList.Add(GetCSharpTypeFromDbType(reader["data_type"].ToString()));
                

                tSchema.isPri = column_key.Equals("PRI");
                tSchema.EXTRA = reader["extra"].ToString();
                tSchema.COLUMN_KEY = reader["column_key"].ToString();
                tSchema.column_name = reader["column_name"].ToString();
                tSchema.column_comment = reader["column_comment"].ToString();
                tSchema.data_type = reader["data_type"].ToString();
                tSchema.csharp_data_type = GetCSharpTypeFromDbType(reader["data_type"].ToString());
                tSchema.csharp_data_type2 = GetCSharpTypeFromDbType2(reader["data_type"].ToString(), unsigned);
                tSchema.IS_NULLABLE = reader["IS_NULLABLE"].ToString()=="YES";
                tSchema.maxLength = reader["CHARACTER_MAXIMUM_LENGTH"].ToString();

                tableSheme.Add(tSchema);

            }
            reader.Close();
            if (identityName.Length == 0)
            {
                //没有主键
                return mTableName + "没有主键,";
            }
            if (!columNames.Contains(isdel))
            {
                //isdel字段
				return mTableName + "没有" + isdel + "字段,";  //没有isdel字段
            }
			//mTableName = mTableName.Replace("_", "");
			//string csName = mTableName + "Info";//类名称：去掉表名称的下划线，后面跟上Info后缀，为了区分方法里的class名称
			string csName = mTableName;

            csName = csName.Substring(0, 1).ToUpper() + csName.Substring(1, csName.Length - 1);//首字母大写

            string strEntity = "";
            if (isSimple)
            {
                //生成简化版Model实体
                strEntity = EntityCreater.CreateEntitySimplify(mDbName, mTableName, strNameSpace,  csName, tableSheme, isdel);
            }
            else
            {

                strEntity = EntityCreater.CreateEntity(mDbName, mTableName, strNameSpace, identityName, csName, columNames.ToArray(),
                    columDesc.ToArray(), typeList.ToArray(), isdel);
            }

            //保存
            EntityCreater.SaveStrToFile(strEntity, strFilePath + csName + ".cs");

            return "";
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strClassName">类名称</param>
        /// <param name="strFilePath">生成类的地址</param>
        /// <returns>返回类的字符串</returns>
		public string CreateControl(string strNameSpace, string strFilePath, string delField,string txtusing)
        {
            //select column_name,data_type,column_comment from information_schema.columns where table_name='RMBCode';
            string strSQL = string.Format("select  column_name,data_type,column_comment,column_key,EXTRA     from information_schema.columns where table_name='{0}' and TABLE_SCHEMA='{1}'", mTableName, mDbName);
            MySqlCommand com = new MySqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string identityName = "";//主键名称
			string isdel = delField; // "isdel";//标记删除的字段名称

            MySqlDataReader reader = com.ExecuteReader();
            List<string> columNames = new List<string>();
            List<string> columDesc = new List<string>();
            List<string> typeList = new List<string>();
            List<string> priKey = new List<string>();//主键标识 pri
            List<string> auto_incr = new List<string>();//自增标识 auto_increment

            while (reader.Read())
            {
                string column_key = reader["column_key"].ToString();
                string extra = reader["extra"].ToString();
                if ((column_key.Equals("PRI") && extra.Equals("auto_increment")))
                {
                    identityName = reader["column_name"].ToString();
                }

                columNames.Add(reader["column_name"].ToString());
                columDesc.Add(reader["column_comment"].ToString());
                typeList.Add(GetCSharpTypeFromDbType(reader["data_type"].ToString()));
                priKey.Add(reader["column_key"].ToString());
                auto_incr.Add(reader["EXTRA"].ToString());

            }
            reader.Close();
            if (identityName.Length == 0)
            {
                //没有主键
                return mTableName + "没有主键,";
            }
            if (!columNames.Contains(isdel))
            {
                //isdel字段
				return mTableName + "没有" + isdel + "字段,";
            }

            //mTableName = mTableName.Replace("_", "");//去掉表名称的下划线
            string csName = mTableName.Substring(0, 1).ToUpper() + mTableName.Substring(1, mTableName.Length - 1) + "";//Data数据处理类名称

			string exten = "DAL";
			string strEntity = ControlCreate.CreateControl(mTableName, strNameSpace, txtusing, exten);
            //保存
			EntityCreater.SaveStrToFile(strEntity, strFilePath + csName + exten + ".cs");

            return "";


        }

        /// <summary>
        /// 生成表说明文档
        /// </summary>
        /// <returns></returns>
        public string CreateTablesHtml(string strFilePath)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder tablesname = new StringBuilder();
            StringBuilder columns = new StringBuilder();

            #region html文本内容模板
            string tmpl = @"<!DOCTYPE html>
<html>
	<head>
		<meta charset='utf-8'>
		<title>表结构说明文档</title>
		<link href='http://libs.baidu.com/bootstrap/3.0.3/css/bootstrap.css' rel='stylesheet'>
		<style>
			a,a:hover{text-decoration: none;}
			h4{padding-top: 90px;maring-top: -90px;}
            .container{width: 1300px;}
			.h-title{position: fixed;background: #fff;width: 100%;z-index: 1;box-shadow: 1px 1px 1px rgba(0,0,0,.2); text-align:center;}
			.gotop{width: 40px;height: 38px;line-height: 35px;background: black;color: #fff;font-size: 14px;text-align: center;line-height: 38px;position: fixed;
			left: 0;margin-left: 20px;bottom: 60px;z-index: 10;}
			.gotop a{display: block;color: #fff;height: 38px;}
			.bdr{border-right: 1px solid #ddd;}.bdl{border-left: 1px solid #ddd;}
		</style>
		</style>
	</head>
	<body>
		<div class='h-title'>
			<h1>表结构说明文档  <a href='#' title='顶部'>↑</a></h1>
		</div>
		<div class='container'>
		<div style='height: 90px;'></div>
		<div class='row'>
			<div class='col-md-3'>
				{{tablesname}}
			</div>
			<div class='col-md-9 bdl'>
				{{columns}}
			</div>
		</div>
	</div>
<div class='gotop'><a href='#' title='返回顶部' alt='返回顶部'>↑</a></div>
	</body>
</html>
";
            #endregion

            //读取表名和表注释
            string strSQL_tables = string.Format("Select table_name  ,TABLE_COMMENT   from INFORMATION_SCHEMA.TABLES   Where table_schema = '{0}'", mDbName);
            MySqlCommand com = new MySqlCommand(strSQL_tables, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                string tname = reader[0].ToString();
                tablesname.Append("<p><a href='#" + tname + "'>" + tname + " </a>" + reader[1] + "</p>");


                //读取表的字段和类型、描述

                columns.Append(TablesCols(tname, reader[1].ToString()));



            }
            reader.Close();

            string html = tmpl.Replace("{{tablesname}}", tablesname.ToString()).Replace("{{columns}}", columns.ToString());
            //保存
            EntityCreater.SaveStrToFile(html, strFilePath + "数据库表说明文档.html", Encoding.UTF8);

            return sb.ToString();
        }

        //生成字段名称描述
        private string TablesCols(string tabname, string tab_comment)
        {
            //读取表的字段和类型、描述
            StringBuilder columns = new StringBuilder();

            string strSQL = string.Format("select column_name,data_type,column_comment,column_key,EXTRA , column_type,COLUMN_DEFAULT  from information_schema.columns where table_name='{0}' and TABLE_SCHEMA='{1}'", tabname, mDbName);
            MySqlConnection con2 = new MySqlConnection(mConnString);
            MySqlCommand com2 = new MySqlCommand(strSQL, con2);

            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }

            MySqlDataReader reader_col = com2.ExecuteReader();
            columns.AppendLine("");
            columns.Append(@"<h4 id='" + tabname + "'>" + tabname + " " + tab_comment + "</h4>");
            columns.Append(@"
				<table class='table'>
					<thead>
						<tr>
						<td>序号</td>
						<td>字段名称</td>
						<td>字段类型长度</td>
						<td>字段描述</td>
						<td>缺省值</td>
					</tr>
					</thead>
					<tbody>");

            int i = 1;
            while (reader_col.Read())
            {
                object desc = reader_col["column_comment"];
                if (!Convert.IsDBNull(desc))
                {
                    desc += keyTypeString(reader_col["column_key"], reader_col["EXTRA"]);
                    // desc += " [" + (reader_col["column_key"].ToString().Replace("PRI", "主键")) + " - " + (reader_col["EXTRA"]) + "]";
                }

                columns.Append(@"<tr>");
                columns.Append("	<td>" + i + "</td>");
                columns.Append("	<td>" + (reader_col["column_name"]) + "</td>");
                columns.Append("	<td>" + (reader_col["column_type"]) + "</td>");
                columns.Append("	<td>" + desc + "</td>");
                columns.Append("	<td>" + (reader_col["COLUMN_DEFAULT"]) + "</td>");
                columns.Append("</tr>");

                i++;
            }
            columns.Append("</tbody></table><hr/><br><br><br>");
            reader_col.Close();
			con2.Close();
			
            return columns.ToString();
        }

        private static string keyTypeString(object column_key, object reader_col)
        {
            string ret = "";
            if (!Convert.IsDBNull(column_key))
            {
                ret += column_key.ToString().Replace("PRI", "主键").Replace("MUL", "索引");
            }
            if (!Convert.IsDBNull(reader_col))
            {
                ret += reader_col.ToString().Replace("auto_increment", "自增"); ;
            }
            if (ret.Length > 0)
                return " [" + ret + "]";
            return ret;
        }

        //取出复选框的值
        public List<string> CheckList(string strNameSpace, string strClassName, string strFilePath)
        {
            string strSQL = string.Format("select  column_name,data_type,column_comment,column_key,EXTRA    from information_schema.columns where table_name='{0}' and TABLE_SCHEMA='{1}'", mTableName, mDbName);
            MySqlCommand com = new MySqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlDataReader reader = com.ExecuteReader();
            List<string> colum = new List<string>();
            while (reader.Read())
            {
                colum.Add(reader["column_name"].ToString() + "*" + reader["column_comment"].ToString());
            }
            reader.Close();
            return colum;
        }

        public string CreateUiLogic(string strNameSpace, string strClassName, string strFilePath)
        {
            //select column_name,data_type,column_comment from information_schema.columns where table_name='RMBCode';
            string strSQL = string.Format("select  column_name,data_type,column_comment,column_key,EXTRA     from information_schema.columns where table_name='{0}' and TABLE_SCHEMA='{1}'", mTableName, mDbName);
            MySqlCommand com = new MySqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlDataReader reader = com.ExecuteReader();
            List<string> columNames = new List<string>();
            List<string> columDesc = new List<string>();
            List<string> typeList = new List<string>();
            List<string> priKey = new List<string>();//主键标识 pri
            List<string> auto_incr = new List<string>();//自增标识 auto_increment

            while (reader.Read())
            {
                columNames.Add(reader["column_name"].ToString());
                columDesc.Add(reader["column_comment"].ToString());
                typeList.Add(GetCSharpTypeFromDbType(reader["data_type"].ToString()));
                priKey.Add(reader["column_key"].ToString());
                auto_incr.Add(reader["EXTRA"].ToString());

            }
            reader.Close();
            return ControlCreate.CreateUiLogic(mTableName, strNameSpace, strClassName, columNames.ToArray(),
                columDesc.ToArray(), typeList.ToArray(), priKey.ToArray(), auto_incr.ToArray());
        }


        //public DataSet GetDS()
        //{
        //    MySqlCommand com = new MySqlCommand("select * from cqwg", con);
        //    if (con.State == ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    MySqlDataAdapter da = new MySqlDataAdapter(com);
        //    DataSet ds=new DataSet();
        //    da.Fill(ds, "tb1");
        //    return ds;
        //}
        #endregion

        #region 私有方法
        /// <summary>
        /// 将数据库中的类型转换为c#的类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private string GetCSharpTypeFromDbType(string dbType)
        {
            //数据库字段类型和c#类型对应关系： https://www.cnblogs.com/vjine/p/3462167.html
            switch (dbType.ToLower())
            {
                case "tinyint": 
                case "smallint": //short
                case "mediumint": //short
                case "int":
                case "numeric":
                    return "int";
                case "bigint":
                    return "long";
                case "decimal":
                    return "decimal";
                case "bit":
                    return "bool";
                case "enum":
                case "set":
                case "char":
                case "varchar":
                case "text":
                case "longtext":
                    return "string";
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblob":
                case "binary":
                case "varbinary":
                    return "byte[]";
                case "datetime":
                case "date":
                    return "DateTime";//时间格式默认转为string类型
                case "time":
                    return "string";
                default:
                    return dbType;
            }
        }

        /// <summary>
        /// 数据库类和c#类型对应
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="unsigned">是否标记无符号</param>
        /// <returns></returns>
        private string GetCSharpTypeFromDbType2(string dbType, bool unsigned)
        {
            //tinyint 有符号对应 sbyte, 无符号byte ,

            //先把有无符号的字段类型判断一次,并返回对应的c#类型
            if (dbType == "tinyint" && !unsigned) { return "sbyte"; }
            if (dbType == "tinyint" && unsigned) { return "byte"; }

            if (dbType == "smallint" && !unsigned) { return "short"; }
            if (dbType == "smallint" && unsigned) { return "ushort"; }


            if (dbType == "int" && !unsigned) { return "int"; }
            if (dbType == "int" && unsigned) { return "uint"; }
            if (dbType == "bigint" && !unsigned) { return "long"; }
            if (dbType == "bigint" && unsigned) { return "ulong"; }


            switch (dbType.ToLower())
            {
                case "mediumint":
                case "int":
                case "numeric":
                    return "int";
                case "bigint":
                    return "long";
                case "bit":
                    return "bool";
                case "enum":
                case "set":
                case "char":
                case "varchar":
                case "text":
                case "longtext":
                    return "string";
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblob":
                case "binary":
                case "varbinary":
                    return "byte[]";
                case "datetime":
                case "date":
                    return "DateTime";//时间格式默认转为string类型
                case "time":
                    return "string";
                default:
                    return dbType;//mysql的 doubule,decimal,float类型和c#一致
            }
        }
        
        #endregion
    }
}
