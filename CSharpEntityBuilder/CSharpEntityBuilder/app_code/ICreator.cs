using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEntityBuilder
{
    public interface ICreator
    {
        /// <summary>
         /// �������ݿ����Ӵ���
         /// </summary>
         /// <param name="pServerName">����������</param>
         /// <param name="pPort">�˿ںţ���Ϊ�����ʾʹ��Ĭ�϶˿�</param>
         /// <param name="pDbName">���ݿ�����</param>
         /// <param name="pUser">�û���</param>
         /// <param name="pPwd">����</param>
        void InitConn(string pServerName, string pPort, string pDbName, string pUser, string pPwd);

        /// <summary>
        /// �������ݿ����Ӵ���
        /// </summary>
        /// <param name="pConStr">���Ӵ�</param>
        void InitConn(string pConStr);

        /// <summary>
        /// �������ݿ�
        /// </summary>
        void ConnDB();

        /// <summary>
        /// ��ȡ��
        /// </summary>
        /// <returns></returns>
        List<string> GetTables();

		/// <summary>
		/// ��ȡ��
		/// </summary>
		/// <returns></returns>
        List<string> Tables();


        /// <summary>
        /// ����Ҫ����ʵ���table
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="tableName">���ݿ���</param>
        void InitTableName(string tableName,string dbName);

        /// <summary>
        /// ����ʵ���ࣨ���ַ�������
        /// </summary>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="strFilePath">������ĵ�ַ</param>
		///  <param name="delField"></param>
        /// <returns>��������ַ���</returns>
        string CreateEntity(string strNameSpace, string strFilePath, string delField, bool isSimple = false);

        List<string> CheckList(string strNameSpace, string strClassName, string strFilePath);
        /// <summary>
        /// ���췽�������ַ�������
        /// </summary>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="strFilePath">������ĵ�ַ</param>
        /// <returns>��������ַ���</returns>
		string CreateControl(string strNameSpace, string strFilePath, string delField, string txtusing);

		/// <summary>
		/// ���ɱ�˵���ĵ�
		/// </summary>
		/// <param name="strFilePath"></param>
		/// <returns></returns>
		string CreateTablesHtml(string strFilePath);

        /// <summary>
        /// ����UiLogic
        /// </summary>
        /// <param name="strNameSpace"></param>
        /// <param name="strClassName"></param>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        string CreateUiLogic(string strNameSpace, string strClassName, string strFilePath);

    }
}
