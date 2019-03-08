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
        #region ˽�б���

        string mConnString = string.Empty;
        string mTableName = string.Empty;
        string mDbName = string.Empty;//���ݿ�����
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

        #region ICreator�ӿ�ʵ��

        /// <summary>
        /// �������ݿ����Ӵ���
        /// </summary>
        /// <param name="pServerName">����������</param>
        /// <param name="pPort">�˿ںţ���Ϊ�����ʾʹ��Ĭ�϶˿�</param>
        /// <param name="pDbName">���ݿ�����</param>
        /// <param name="pConnMode">��¼���ͣ�Windows�����֤��Sql��֤</param>
        /// <param name="pUser">�û���</param>
        /// <param name="pPwd">����</param>
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
        /// �������ݿ����Ӵ���
        /// </summary>
        /// <param name="pConStr">���Ӵ�</param>
        public void InitConn(string pConStr)
        {
            mConnString = pConStr;
        }

        /// <summary>
        /// �������ݿ�
        /// </summary>
        public void ConnDB()
        {
            con = new MySqlConnection(mConnString);
            if (con.State == ConnectionState.Closed)
                con.Open();
        }



		/// <summary>
		/// ����Ҫ����ʵ���table
		/// </summary>
		/// <param name="tableName">����</param>
		public void InitTableName(string tableName, string dbName)
		{
			mTableName = tableName;
			mDbName = dbName;
		}





        /// <summary>
        /// ��ȡ���б�
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
        /// ���ݱ�ǰ׺ģ����ѯ�����ѯ�����
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
        /// ����ʵ���ࣨ���ַ�������
        /// </summary>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="strClassName">������</param>
        /// <param name="strFilePath">������ĵ�ַ</param>
        /// <param name="isSimple">=true��ʾ���ɼ򻯰�Modelʵ�壬Ĭ��Ϊfalse</param>
        /// <returns>��������ַ���</returns>
        public string CreateEntity(string strNameSpace, string strFilePath, string delField, bool isSimple=false)
        {
            //select column_name,data_type,column_comment from information_schema.columns where table_name='RMBCode';
            string strSQL = string.Format("select  column_name,data_type,column_comment,column_key,EXTRA,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE,COLUMN_KEY,COLUMN_TYPE   from information_schema.columns where table_name='{0}' and TABLE_SCHEMA='{1}'", mTableName, mDbName);
            MySqlCommand com = new MySqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string identityName = "";//��������
			string isdel = delField; // "isdel";//���ɾ�����ֶ�����

            MySqlDataReader reader = com.ExecuteReader();
            List<string> columNames = new List<string>();
            List<string> columDesc = new List<string>();
            List<string> typeList = new List<string>();//��Ӧ��c#����

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
                //û������
                return mTableName + "û������,";
            }
            if (!columNames.Contains(isdel))
            {
                //isdel�ֶ�
				return mTableName + "û��" + isdel + "�ֶ�,";  //û��isdel�ֶ�
            }
			//mTableName = mTableName.Replace("_", "");
			//string csName = mTableName + "Info";//�����ƣ�ȥ�������Ƶ��»��ߣ��������Info��׺��Ϊ�����ַ������class����
			string csName = mTableName;

            csName = csName.Substring(0, 1).ToUpper() + csName.Substring(1, csName.Length - 1);//����ĸ��д

            string strEntity = "";
            if (isSimple)
            {
                //���ɼ򻯰�Modelʵ��
                strEntity = EntityCreater.CreateEntitySimplify(mDbName, mTableName, strNameSpace,  csName, tableSheme, isdel);
            }
            else
            {

                strEntity = EntityCreater.CreateEntity(mDbName, mTableName, strNameSpace, identityName, csName, columNames.ToArray(),
                    columDesc.ToArray(), typeList.ToArray(), isdel);
            }

            //����
            EntityCreater.SaveStrToFile(strEntity, strFilePath + csName + ".cs");

            return "";
        }

        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="strClassName">������</param>
        /// <param name="strFilePath">������ĵ�ַ</param>
        /// <returns>��������ַ���</returns>
		public string CreateControl(string strNameSpace, string strFilePath, string delField,string txtusing)
        {
            //select column_name,data_type,column_comment from information_schema.columns where table_name='RMBCode';
            string strSQL = string.Format("select  column_name,data_type,column_comment,column_key,EXTRA     from information_schema.columns where table_name='{0}' and TABLE_SCHEMA='{1}'", mTableName, mDbName);
            MySqlCommand com = new MySqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string identityName = "";//��������
			string isdel = delField; // "isdel";//���ɾ�����ֶ�����

            MySqlDataReader reader = com.ExecuteReader();
            List<string> columNames = new List<string>();
            List<string> columDesc = new List<string>();
            List<string> typeList = new List<string>();
            List<string> priKey = new List<string>();//������ʶ pri
            List<string> auto_incr = new List<string>();//������ʶ auto_increment

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
                //û������
                return mTableName + "û������,";
            }
            if (!columNames.Contains(isdel))
            {
                //isdel�ֶ�
				return mTableName + "û��" + isdel + "�ֶ�,";
            }

            //mTableName = mTableName.Replace("_", "");//ȥ�������Ƶ��»���
            string csName = mTableName.Substring(0, 1).ToUpper() + mTableName.Substring(1, mTableName.Length - 1) + "";//Data���ݴ���������

			string exten = "DAL";
			string strEntity = ControlCreate.CreateControl(mTableName, strNameSpace, txtusing, exten);
            //����
			EntityCreater.SaveStrToFile(strEntity, strFilePath + csName + exten + ".cs");

            return "";


        }

        /// <summary>
        /// ���ɱ�˵���ĵ�
        /// </summary>
        /// <returns></returns>
        public string CreateTablesHtml(string strFilePath)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder tablesname = new StringBuilder();
            StringBuilder columns = new StringBuilder();

            #region html�ı�����ģ��
            string tmpl = @"<!DOCTYPE html>
<html>
	<head>
		<meta charset='utf-8'>
		<title>��ṹ˵���ĵ�</title>
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
			<h1>��ṹ˵���ĵ�  <a href='#' title='����'>��</a></h1>
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
<div class='gotop'><a href='#' title='���ض���' alt='���ض���'>��</a></div>
	</body>
</html>
";
            #endregion

            //��ȡ�����ͱ�ע��
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


                //��ȡ����ֶκ����͡�����

                columns.Append(TablesCols(tname, reader[1].ToString()));



            }
            reader.Close();

            string html = tmpl.Replace("{{tablesname}}", tablesname.ToString()).Replace("{{columns}}", columns.ToString());
            //����
            EntityCreater.SaveStrToFile(html, strFilePath + "���ݿ��˵���ĵ�.html", Encoding.UTF8);

            return sb.ToString();
        }

        //�����ֶ���������
        private string TablesCols(string tabname, string tab_comment)
        {
            //��ȡ����ֶκ����͡�����
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
						<td>���</td>
						<td>�ֶ�����</td>
						<td>�ֶ����ͳ���</td>
						<td>�ֶ�����</td>
						<td>ȱʡֵ</td>
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
                    // desc += " [" + (reader_col["column_key"].ToString().Replace("PRI", "����")) + " - " + (reader_col["EXTRA"]) + "]";
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
                ret += column_key.ToString().Replace("PRI", "����").Replace("MUL", "����");
            }
            if (!Convert.IsDBNull(reader_col))
            {
                ret += reader_col.ToString().Replace("auto_increment", "����"); ;
            }
            if (ret.Length > 0)
                return " [" + ret + "]";
            return ret;
        }

        //ȡ����ѡ���ֵ
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
            List<string> priKey = new List<string>();//������ʶ pri
            List<string> auto_incr = new List<string>();//������ʶ auto_increment

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

        #region ˽�з���
        /// <summary>
        /// �����ݿ��е�����ת��Ϊc#������
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private string GetCSharpTypeFromDbType(string dbType)
        {
            //���ݿ��ֶ����ͺ�c#���Ͷ�Ӧ��ϵ�� https://www.cnblogs.com/vjine/p/3462167.html
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
                    return "DateTime";//ʱ���ʽĬ��תΪstring����
                case "time":
                    return "string";
                default:
                    return dbType;
            }
        }

        /// <summary>
        /// ���ݿ����c#���Ͷ�Ӧ
        /// </summary>
        /// <param name="dbType">���ݿ�����</param>
        /// <param name="unsigned">�Ƿ����޷���</param>
        /// <returns></returns>
        private string GetCSharpTypeFromDbType2(string dbType, bool unsigned)
        {
            //tinyint �з��Ŷ�Ӧ sbyte, �޷���byte ,

            //�Ȱ����޷��ŵ��ֶ������ж�һ��,�����ض�Ӧ��c#����
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
                    return "DateTime";//ʱ���ʽĬ��תΪstring����
                case "time":
                    return "string";
                default:
                    return dbType;//mysql�� doubule,decimal,float���ͺ�c#һ��
            }
        }
        
        #endregion
    }
}
