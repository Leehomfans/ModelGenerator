using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSharpEntityBuilder
{
    public class EntityCreater
    {
        /// <summary>
        /// ����ʵ���ࡣ
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strNameSpace">�����ռ�����</param>
		/// <param name="identityName">��������������</param>
        /// <param name="columNames">ʵ���ֶ����б�</param>
        /// <param name="columDesc">ʵ���ֶ������б�</param>
        /// <param name="typeList">�ֶ������б�</param>
		/// <param name="delField">��ǵ�ɾ���ֶ�</param>
        /// <returns>ʵ����Ĵ���</returns>
        public static string CreateEntity(string mDbName,string strTableName,string strNameSpace,string identityName,string csName, string[] columNames, string[] columDesc, string[] typeList,string delField)
        {
			
			//model�����������򣺰��ձ���������

			//className = className .Substring(0, 1).ToUpper() + className .Substring(1, className .Length - 1);//����ĸ��д

            StringBuilder sb = new StringBuilder();
            //�ļ�ͷע��
            sb.AppendLine("/*");
			sb.AppendLine("* " + csName + ".cs");
			sb.AppendLine("* ��[" + strTableName + "]��ʵ����");
			sb.AppendLine("* �Զ����� " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
            sb.AppendLine("    /// ��[" + mDbName + "." + strTableName + "]��ʵ����");
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

            sb.AppendLine("        public static readonly string TableName = \"" + mDbName + "." + strTableName + "\";//������");
			sb.AppendLine("        public static readonly string IdentityField = \"" + identityName + "\";//������������");
			sb.AppendLine("        public static readonly string DelField = \"" + delField + "\";//���ɾ�����ֶ�����");
            sb.AppendLine("        public static readonly string[] allkeys = { " + allkeys +" };//�����ֶ�");

            sb.AppendLine();
            sb.AppendLine("        public List<string> keys = new List<string>();//������ʱ������ӻ��޸�ʱ���ֶ�");
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("        #region ��Ӧ���ݿ���ֶ�����");
			sb.AppendLine("        //��F_��ǰ׺�������������ͬ��");
			for (int i = 0; i < columNames.Length; i++)
			{
				sb.AppendLine("        public  static readonly string F_" + columNames[i] + " = \"" + columNames[i] + "\";");

			}
			sb.AppendLine("        #endregion");
			sb.AppendLine();

			//�ֶ�����
			sb.AppendLine("        #region �ֶ����ԡ�����");
            sb.AppendLine();
            for (int i = 0; i < columNames.Length; i++)
            {
				sb.AppendLine("        private " + typeList[i] + " _" + columNames[i] + ";//" + columDesc[i] + "");
            }
            

            //�ֶ�����
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
        /// ����ʵ���ࡾ�򻯰�ʵ���࣬����EF��ܡ�
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="identityName">��������������</param>
        /// <param name="columNames">ʵ���ֶ����б�</param>
        /// <param name="columDesc">ʵ���ֶ������б�</param>
        /// <param name="typeList">�ֶ������б�</param>
        /// <param name="delField">��ǵ�ɾ���ֶ�</param>
        /// <returns>ʵ����Ĵ���</returns>
        public static string CreateEntitySimplify(string mDbName, string strTableName, string strNameSpace, string csName, List<TableSchema> tableSheme, string delField)
        {

            //model�����������򣺰��ձ���������

            //className = className .Substring(0, 1).ToUpper() + className .Substring(1, className .Length - 1);//����ĸ��д

            StringBuilder sb = new StringBuilder();
            //�ļ�ͷע��
            sb.AppendLine("/*");
            sb.AppendLine("* " + csName + ".cs");
            sb.AppendLine("* ��[" + strTableName + "]��ʵ����");
            sb.AppendLine("* �Զ����� " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
            sb.AppendLine("    /// ��[" + mDbName + "." + strTableName + "]��ʵ����");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    [Serializable] ");
            sb.AppendLine("    [Table(\"" + strTableName + "\")] ");
            sb.AppendLine("    public class " + csName);
            sb.AppendLine("    {");
            sb.AppendLine();


            //�ֶ�����
            sb.AppendLine("        #region �ֶ����ԡ�����");
            

            //�ֶ�����
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
        /// �����ַ������ļ�
        /// </summary>
        /// <param name="str">Ҫ������ַ���</param>
        /// <param name="filePath">�ļ�·��</param>
        public static void SaveStrToFile(string str, string filePath)
        {
			SaveStrToFile(str, filePath, Encoding.Default);


        }
		/// <summary>
		/// �����ַ������ļ�
		/// </summary>
		/// <param name="str">Ҫ������ַ���</param>
		/// <param name="filePath">�ļ�·��</param>
		public static void SaveStrToFile(string str, string filePath,Encoding en)
		{
			FileInfo info = new FileInfo(filePath);
			if (!info.Directory.Exists)
			{
				Directory.CreateDirectory(info.DirectoryName);
			}
			StreamWriter stream = null;
			//����
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
