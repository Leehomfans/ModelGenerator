using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace CSharpEntityBuilder
{
	public partial class Form1 : Form
	{
		#region 变量
		const string DefaultEntityName = "Model";
		const string DefaultNameSpace = "DefaultNameSpace";
		const string DefaultControlName = "Control";
		const string DefaultUiLogicName = "UiLogic";


        static string DelField = "state";//isdel state   //用于标记删除的字段。 值为1表示删除



		//进行数据库交互的类
		ICreator Creator = null;
		#endregion

		#region 事件

		public Form1()
		{
			InitializeComponent();


		}

		/// <summary>
		/// 窗体加载事件
		/// </summary>
		private void Form1_Load(object sender, EventArgs e)
		{


			//txtFilePath.Text = Application.StartupPath + "\\" + DefaultEntityName + ".cs";
			//txtCFilePath.Text = Application.StartupPath+"\\";// +"\\" + DefaultControlName + ".cs";
			txtCFilePath.Text = "C:\\Documents and Settings\\Administrator\\桌面\\代码生成\\";

			Creator = new MysqlDbCreater();
		}

		/// <summary>
		/// 连接数据库
		/// </summary>
		private void btnConnect_Click(object sender, EventArgs e)
		{
			lbl_db_msg.Text = "";
			//检查
			if (!ConnCheck())
			{
				return;
			}

			//初始化连接串。
			if (tabControl1.SelectedTab.Name == "tp_UseConnStr")
			{
				Creator.InitConn(txtConStr.Text.Trim());
			}
			else
			{
				Creator.InitConn(txtServer.Text.Trim(), GetPort(), txtDBName.Text.Trim(), txtUserName.Text.Trim(), txtPwd.Text.Trim());
			}

			//连接数据库。
			try
			{
				Creator.ConnDB();

				//获取表信息
				List<string> lsTables = Creator.GetTables();
				cmbTables.Items.Clear();
				cmbTables.Items.AddRange(lsTables.ToArray());

				cmbTables.Items.AddRange(new string[] { "全部" });



				//显示提示信息
				//lbl_db_msg.ForeColor = Color.Green;
				//lbl_db_msg.Text = "数据库连接成功！请选择一个表进行实体类生成！";
				Msg("数据库连接成功！");
			}
			catch (Exception ex)
			{
				//显示提示信息
				lbl_db_msg.ForeColor = Color.Red;
				lbl_db_msg.Text = "数据库连接失败！";

				Msg("连接失败！" + ex.Message);
			}
		}

		/// <summary>
		/// 生成Data处理类 [已无效]
		/// </summary>
		private void btnCreateEntityFile_Click(object sender, EventArgs e)
        {
        //    if (txt_create_tables.Text.Length == 0)
        //    {
        //        Msg("请选择需要生成的表！");
        //        return;
        //    }
        //    else if (txtCNameSpace.Text.Length == 0)
        //    {
        //        Msg("请输入命名空间！");
        //        return;
        //    }

        //    try
        //    {

        //        List<string> tables = new List<string> { };
        //        string[] tableName = txt_create_tables.Text.Replace(" ", ",").Split(','); ;
        //        if (txt_create_tables.Text == "全部")
        //        {
        //            tables = Creator.GetTables();
        //        }
        //        else
        //        {
        //            List<string> t = new List<string>();
        //            foreach (var item in tableName)
        //            {
        //                Creator.InitTableName(item, txtDBName.Text);//初始化表
        //                t = Creator.Tables();
        //                tables.AddRange(t);
        //            }

        //        }
        //        if (tables.Count == 0)
        //        {
        //            Msg("选择的表不存在！");
        //            return;
        //        }

        //        string path = txtCFilePath.Text + "\\Data\\";
        //        foreach (string item in tables)
        //        {
        //            if (item.Length > 0)
        //            {
        //                Creator.InitTableName(item, txtDBName.Text);//初始化表
        //                //生成数据处理方法[Data]并保存到文件
        //                Creator.CreateControl(txtCNameSpace.Text.Trim(), path,DelField,txtusing.Text);
        //            }
        //        }


        //        //提示
        //        Msg("生成成功！");
        //    }
        //    catch (Exception ex)
        //    {
        //        //提示
        //        Msg("生成失败！" + ex.Message);
        //    }
		}


        private void CheckInput()
        {


        }

		//生成Model
		private void button1_Click(object sender, EventArgs e)
		{
			if (txt_create_tables.Text.Length == 0)
            {
                MessageBox.Show("请选择需要生成的表！");
                return;
            }
            if (txtNameSpace.Text.Length == 0)
            {
                MessageBox.Show("请输入命名空间！");
                return;
            }
			try
			{
				StringBuilder msg = new StringBuilder();

				List<string> tables = new List<string> { };
				string[] tableName = txt_create_tables.Text.Replace(" ", ",").Split(',');
				if (txt_create_tables.Text == "全部")
				{
                    tables = Creator.GetTables();
				}
				else
				{
					List<string> t = new List<string>();
					foreach (var item in tableName)
					{
						Creator.InitTableName(item, txtDBName.Text);//初始化表
                        t = Creator.Tables();
						tables.AddRange(t);
					}
				}
				if (tables.Count == 0)
				{
					Msg("选择的表不存在！");
					return;
				}

                DelField =rbtn_state.Checked?"state": "isdel";

				string path = txtCFilePath.Text + "\\Model\\";
				foreach (string item in tables)
				{
					if (item.Length > 0)
					{
						Creator.InitTableName(item, txtDBName.Text);//初始化表
						//生成实体类[Model]并保存到文件

                        bool isSimply = chkSimplyModel.Checked;
                        string strEntity = Creator.CreateEntity(txtNameSpace.Text.Trim(), path, DelField, isSimply);
						msg.Append(strEntity);
					}
				}
				if (msg.Length > 0)
				{
					Msg("生成失败！" + msg);
				}
				else
				{
					//提示
					Msg("生成成功！");
				}
			}
			catch (Exception ex)
			{
				//提示
				Msg("生成失败！" + ex.Message);
			}
		}

        //生成简化Model文件。
        private void button5_Click(object sender, EventArgs e)
        {

            /*
             
             * 格式如下：
             * 参考：User1.cs.bak
             * 
             */





        }



		//生成表说明文档
		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				Creator.InitTableName(cmbTables.Text, txtDBName.Text);//初始化表
				//生成表说明文档
				string strEntity = Creator.CreateTablesHtml(txtCFilePath.Text + "");

				//提示
				Msg("生成成功！");

			}
			catch (Exception ex)
			{
				//提示
				Msg("生成失败！" + ex.Message);
			}
		}














		/// <summary>
		/// 单击实体名称文本框
		/// </summary>
		private void txtNameSpace_Click(object sender, EventArgs e)
		{
			if (txtNameSpace.Text == DefaultNameSpace)
			{
				txtNameSpace.Text = "";
			}
		}

		/// <summary>
		/// 单击实体名称文本框
		/// </summary>
		private void txtEntityName_Click(object sender, EventArgs e)
		{


		}

		/// <summary>
		/// 默认端口复选框。
		/// </summary>
		private void cbUseDefaultPort_CheckedChanged(object sender, EventArgs e)
		{
			if (cbUseDefaultPort.Checked)
			{
				txtPort.Enabled = false;
			}
			else
			{
				txtPort.Enabled = true;
			}
		}

		/// <summary>
		/// 数据库类型单选框。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbSQLType_CheckedChanged(object sender, EventArgs e)
		{
			if (rbMySQL.Checked)
			{
				Creator = new MysqlDbCreater();
			}

			//TODO
			if (rbMSSQL.Checked)
			{

			}

			if (rbOracle.Checked)
			{

			}
		}

		//blog
		//private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		//{
		//    Process.Start("iexplore.exe", "http://");
		//}

		#endregion

		#region 私有方法
		/// <summary>
		/// 点击连接按钮时检查。
		/// </summary>
		/// <returns></returns>
		private bool ConnCheck()
		{
			//填写了自己的连接串，则直接返回true
			if (tabControl1.SelectedTab.Name == "tp_UseConnStr" && string.IsNullOrEmpty(txtConStr.Text.Trim()))
			{
				Msg("请输入连接串！");
				txtConStr.Focus();
				return false;
			}

			//如果是用输入参数的形式
			if (tabControl1.SelectedTab.Name == "tp_UseParams")
			{
				if (string.IsNullOrEmpty(txtServer.Text.Trim()))
				{
					Msg("请填写服务器名称或IP！");
					txtServer.Focus();
					return false;
				}

				if (string.IsNullOrEmpty(txtPort.Text.Trim()) && !cbUseDefaultPort.Checked)
				{
					Msg("请填写端口号！");
					txtPort.Focus();
					return false;
				}

				if (string.IsNullOrEmpty(txtDBName.Text.Trim()))
				{
					Msg("请填写数据库名称！");
					txtDBName.Focus();
					return false;
				}

				if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
				{
					Msg("请填写登录用户名！");
					txtUserName.Focus();
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 点击生成时的检查
		/// </summary>
		/// <returns></returns>
		private bool CreateCheck()
		{
			if (cmbTables.SelectedIndex == -1)
			{
				Msg("请选择一个要转换成实体类的表！");
				cmbTables.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(txtNameSpace.Text.Trim()))
			{
				Msg("请填写命名空间名称！");
				txtNameSpace.Focus();
				return false;
			}



			if (string.IsNullOrEmpty(txtCFilePath.Text.Trim()))
			{
				Msg("请输入或选择保存实体类文件的路径！");
				txtCFilePath.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// 获取端口
		/// </summary>
		/// <returns></returns>
		private string GetPort()
		{
			if (cbUseDefaultPort.Checked)
			{
				return string.Empty;
			}
			else
			{
				return txtPort.Text;
			}
		}

		/// <summary>
		/// 提示
		/// </summary>
		/// <param name="message"></param>
		private void Msg(string message)
		{
			MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		#endregion

		private void btnSelectCFilePath_Click(object sender, EventArgs e)
		{
			//saveFileDialog1.Filter = "*.cs|*.cs|所有文件|*.*";
			//if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			//{
			//    txtCFilePath.Text = saveFileDialog1.FileName;
			//}


			FolderBrowserDialog OpenDirectory = new FolderBrowserDialog();
			OpenDirectory.SelectedPath = "C:\\";
			// 允许在对话框中包括一个新建目录的按钮
			OpenDirectory.ShowNewFolderButton = true;
			// 设置对话框的说明信息
			OpenDirectory.Description = "打开文件夹";
			if (OpenDirectory.ShowDialog() == DialogResult.OK)
			{
				txtCFilePath.Text = OpenDirectory.SelectedPath + "\\";

			}


		}
		//选中
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}
		//选中后触发事件
		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{



		}
		//生成aspx页面
		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			string cmbval = cmbTables.SelectedItem.ToString();
			txt_create_tables.Text = cmbval;
			//Msg("选择表" + cmbval);
		}

        private void button4_Click(object sender, EventArgs e)
        {
            //string path = @"E:\快盘\系统-设计\008.公共类\trunk\代码生成器\CSharpEntityBuilder\CSharpEntityBuilder\bin\Release";
            string path = txtCFilePath.Text;
			FileInfo info = new FileInfo(path);
			if (!info.Directory.Exists)
			{
				Msg("目录不存在，生成后，会自动创建该目录");
				return;
			}
            Process.Start(path);
        }







	}
}