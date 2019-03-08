using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEntityBuilder
{
    public interface ICreator
    {
        /// <summary>
         /// 设置数据库连接串。
         /// </summary>
         /// <param name="pServerName">服务器名称</param>
         /// <param name="pPort">端口号，若为空则表示使用默认端口</param>
         /// <param name="pDbName">数据库名称</param>
         /// <param name="pUser">用户名</param>
         /// <param name="pPwd">密码</param>
        void InitConn(string pServerName, string pPort, string pDbName, string pUser, string pPwd);

        /// <summary>
        /// 设置数据库连接串。
        /// </summary>
        /// <param name="pConStr">链接串</param>
        void InitConn(string pConStr);

        /// <summary>
        /// 连接数据库
        /// </summary>
        void ConnDB();

        /// <summary>
        /// 获取表。
        /// </summary>
        /// <returns></returns>
        List<string> GetTables();

		/// <summary>
		/// 获取表。
		/// </summary>
		/// <returns></returns>
        List<string> Tables();


        /// <summary>
        /// 设置要生成实体的table
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableName">数据库名</param>
        void InitTableName(string tableName,string dbName);

        /// <summary>
        /// 构造实体类（的字符串）。
        /// </summary>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strFilePath">生成类的地址</param>
		///  <param name="delField"></param>
        /// <returns>返回类的字符串</returns>
        string CreateEntity(string strNameSpace, string strFilePath, string delField, bool isSimple = false);

        List<string> CheckList(string strNameSpace, string strClassName, string strFilePath);
        /// <summary>
        /// 构造方法（的字符串）。
        /// </summary>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strFilePath">生成类的地址</param>
        /// <returns>返回类的字符串</returns>
		string CreateControl(string strNameSpace, string strFilePath, string delField, string txtusing);

		/// <summary>
		/// 生成表说明文档
		/// </summary>
		/// <param name="strFilePath"></param>
		/// <returns></returns>
		string CreateTablesHtml(string strFilePath);

        /// <summary>
        /// 创建UiLogic
        /// </summary>
        /// <param name="strNameSpace"></param>
        /// <param name="strClassName"></param>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        string CreateUiLogic(string strNameSpace, string strClassName, string strFilePath);

    }
}
