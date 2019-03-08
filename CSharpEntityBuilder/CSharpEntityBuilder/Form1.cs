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
		#region ����
		const string DefaultEntityName = "Model";
		const string DefaultNameSpace = "DefaultNameSpace";
		const string DefaultControlName = "Control";
		const string DefaultUiLogicName = "UiLogic";


        static string DelField = "state";//isdel state   //���ڱ��ɾ�����ֶΡ� ֵΪ1��ʾɾ��



		//�������ݿ⽻������
		ICreator Creator = null;
		#endregion

		#region �¼�

		public Form1()
		{
			InitializeComponent();


		}

		/// <summary>
		/// ��������¼�
		/// </summary>
		private void Form1_Load(object sender, EventArgs e)
		{


			//txtFilePath.Text = Application.StartupPath + "\\" + DefaultEntityName + ".cs";
			//txtCFilePath.Text = Application.StartupPath+"\\";// +"\\" + DefaultControlName + ".cs";
			txtCFilePath.Text = "C:\\Documents and Settings\\Administrator\\����\\��������\\";

			Creator = new MysqlDbCreater();
		}

		/// <summary>
		/// �������ݿ�
		/// </summary>
		private void btnConnect_Click(object sender, EventArgs e)
		{
			lbl_db_msg.Text = "";
			//���
			if (!ConnCheck())
			{
				return;
			}

			//��ʼ�����Ӵ���
			if (tabControl1.SelectedTab.Name == "tp_UseConnStr")
			{
				Creator.InitConn(txtConStr.Text.Trim());
			}
			else
			{
				Creator.InitConn(txtServer.Text.Trim(), GetPort(), txtDBName.Text.Trim(), txtUserName.Text.Trim(), txtPwd.Text.Trim());
			}

			//�������ݿ⡣
			try
			{
				Creator.ConnDB();

				//��ȡ����Ϣ
				List<string> lsTables = Creator.GetTables();
				cmbTables.Items.Clear();
				cmbTables.Items.AddRange(lsTables.ToArray());

				cmbTables.Items.AddRange(new string[] { "ȫ��" });



				//��ʾ��ʾ��Ϣ
				//lbl_db_msg.ForeColor = Color.Green;
				//lbl_db_msg.Text = "���ݿ����ӳɹ�����ѡ��һ�������ʵ�������ɣ�";
				Msg("���ݿ����ӳɹ���");
			}
			catch (Exception ex)
			{
				//��ʾ��ʾ��Ϣ
				lbl_db_msg.ForeColor = Color.Red;
				lbl_db_msg.Text = "���ݿ�����ʧ�ܣ�";

				Msg("����ʧ�ܣ�" + ex.Message);
			}
		}

		/// <summary>
		/// ����Data������ [����Ч]
		/// </summary>
		private void btnCreateEntityFile_Click(object sender, EventArgs e)
        {
        //    if (txt_create_tables.Text.Length == 0)
        //    {
        //        Msg("��ѡ����Ҫ���ɵı�");
        //        return;
        //    }
        //    else if (txtCNameSpace.Text.Length == 0)
        //    {
        //        Msg("�����������ռ䣡");
        //        return;
        //    }

        //    try
        //    {

        //        List<string> tables = new List<string> { };
        //        string[] tableName = txt_create_tables.Text.Replace(" ", ",").Split(','); ;
        //        if (txt_create_tables.Text == "ȫ��")
        //        {
        //            tables = Creator.GetTables();
        //        }
        //        else
        //        {
        //            List<string> t = new List<string>();
        //            foreach (var item in tableName)
        //            {
        //                Creator.InitTableName(item, txtDBName.Text);//��ʼ����
        //                t = Creator.Tables();
        //                tables.AddRange(t);
        //            }

        //        }
        //        if (tables.Count == 0)
        //        {
        //            Msg("ѡ��ı����ڣ�");
        //            return;
        //        }

        //        string path = txtCFilePath.Text + "\\Data\\";
        //        foreach (string item in tables)
        //        {
        //            if (item.Length > 0)
        //            {
        //                Creator.InitTableName(item, txtDBName.Text);//��ʼ����
        //                //�������ݴ�����[Data]�����浽�ļ�
        //                Creator.CreateControl(txtCNameSpace.Text.Trim(), path,DelField,txtusing.Text);
        //            }
        //        }


        //        //��ʾ
        //        Msg("���ɳɹ���");
        //    }
        //    catch (Exception ex)
        //    {
        //        //��ʾ
        //        Msg("����ʧ�ܣ�" + ex.Message);
        //    }
		}


        private void CheckInput()
        {


        }

		//����Model
		private void button1_Click(object sender, EventArgs e)
		{
			if (txt_create_tables.Text.Length == 0)
            {
                MessageBox.Show("��ѡ����Ҫ���ɵı�");
                return;
            }
            if (txtNameSpace.Text.Length == 0)
            {
                MessageBox.Show("�����������ռ䣡");
                return;
            }
			try
			{
				StringBuilder msg = new StringBuilder();

				List<string> tables = new List<string> { };
				string[] tableName = txt_create_tables.Text.Replace(" ", ",").Split(',');
				if (txt_create_tables.Text == "ȫ��")
				{
                    tables = Creator.GetTables();
				}
				else
				{
					List<string> t = new List<string>();
					foreach (var item in tableName)
					{
						Creator.InitTableName(item, txtDBName.Text);//��ʼ����
                        t = Creator.Tables();
						tables.AddRange(t);
					}
				}
				if (tables.Count == 0)
				{
					Msg("ѡ��ı����ڣ�");
					return;
				}

                DelField =rbtn_state.Checked?"state": "isdel";

				string path = txtCFilePath.Text + "\\Model\\";
				foreach (string item in tables)
				{
					if (item.Length > 0)
					{
						Creator.InitTableName(item, txtDBName.Text);//��ʼ����
						//����ʵ����[Model]�����浽�ļ�

                        bool isSimply = chkSimplyModel.Checked;
                        string strEntity = Creator.CreateEntity(txtNameSpace.Text.Trim(), path, DelField, isSimply);
						msg.Append(strEntity);
					}
				}
				if (msg.Length > 0)
				{
					Msg("����ʧ�ܣ�" + msg);
				}
				else
				{
					//��ʾ
					Msg("���ɳɹ���");
				}
			}
			catch (Exception ex)
			{
				//��ʾ
				Msg("����ʧ�ܣ�" + ex.Message);
			}
		}

        //���ɼ�Model�ļ���
        private void button5_Click(object sender, EventArgs e)
        {

            /*
             
             * ��ʽ���£�
             * �ο���User1.cs.bak
             * 
             */





        }



		//���ɱ�˵���ĵ�
		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				Creator.InitTableName(cmbTables.Text, txtDBName.Text);//��ʼ����
				//���ɱ�˵���ĵ�
				string strEntity = Creator.CreateTablesHtml(txtCFilePath.Text + "");

				//��ʾ
				Msg("���ɳɹ���");

			}
			catch (Exception ex)
			{
				//��ʾ
				Msg("����ʧ�ܣ�" + ex.Message);
			}
		}














		/// <summary>
		/// ����ʵ�������ı���
		/// </summary>
		private void txtNameSpace_Click(object sender, EventArgs e)
		{
			if (txtNameSpace.Text == DefaultNameSpace)
			{
				txtNameSpace.Text = "";
			}
		}

		/// <summary>
		/// ����ʵ�������ı���
		/// </summary>
		private void txtEntityName_Click(object sender, EventArgs e)
		{


		}

		/// <summary>
		/// Ĭ�϶˿ڸ�ѡ��
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
		/// ���ݿ����͵�ѡ��
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

		#region ˽�з���
		/// <summary>
		/// ������Ӱ�ťʱ��顣
		/// </summary>
		/// <returns></returns>
		private bool ConnCheck()
		{
			//��д���Լ������Ӵ�����ֱ�ӷ���true
			if (tabControl1.SelectedTab.Name == "tp_UseConnStr" && string.IsNullOrEmpty(txtConStr.Text.Trim()))
			{
				Msg("���������Ӵ���");
				txtConStr.Focus();
				return false;
			}

			//������������������ʽ
			if (tabControl1.SelectedTab.Name == "tp_UseParams")
			{
				if (string.IsNullOrEmpty(txtServer.Text.Trim()))
				{
					Msg("����д���������ƻ�IP��");
					txtServer.Focus();
					return false;
				}

				if (string.IsNullOrEmpty(txtPort.Text.Trim()) && !cbUseDefaultPort.Checked)
				{
					Msg("����д�˿ںţ�");
					txtPort.Focus();
					return false;
				}

				if (string.IsNullOrEmpty(txtDBName.Text.Trim()))
				{
					Msg("����д���ݿ����ƣ�");
					txtDBName.Focus();
					return false;
				}

				if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
				{
					Msg("����д��¼�û�����");
					txtUserName.Focus();
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// �������ʱ�ļ��
		/// </summary>
		/// <returns></returns>
		private bool CreateCheck()
		{
			if (cmbTables.SelectedIndex == -1)
			{
				Msg("��ѡ��һ��Ҫת����ʵ����ı�");
				cmbTables.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(txtNameSpace.Text.Trim()))
			{
				Msg("����д�����ռ����ƣ�");
				txtNameSpace.Focus();
				return false;
			}



			if (string.IsNullOrEmpty(txtCFilePath.Text.Trim()))
			{
				Msg("�������ѡ�񱣴�ʵ�����ļ���·����");
				txtCFilePath.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// ��ȡ�˿�
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
		/// ��ʾ
		/// </summary>
		/// <param name="message"></param>
		private void Msg(string message)
		{
			MessageBox.Show(message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		#endregion

		private void btnSelectCFilePath_Click(object sender, EventArgs e)
		{
			//saveFileDialog1.Filter = "*.cs|*.cs|�����ļ�|*.*";
			//if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			//{
			//    txtCFilePath.Text = saveFileDialog1.FileName;
			//}


			FolderBrowserDialog OpenDirectory = new FolderBrowserDialog();
			OpenDirectory.SelectedPath = "C:\\";
			// �����ڶԻ����а���һ���½�Ŀ¼�İ�ť
			OpenDirectory.ShowNewFolderButton = true;
			// ���öԻ����˵����Ϣ
			OpenDirectory.Description = "���ļ���";
			if (OpenDirectory.ShowDialog() == DialogResult.OK)
			{
				txtCFilePath.Text = OpenDirectory.SelectedPath + "\\";

			}


		}
		//ѡ��
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}
		//ѡ�к󴥷��¼�
		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{



		}
		//����aspxҳ��
		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			string cmbval = cmbTables.SelectedItem.ToString();
			txt_create_tables.Text = cmbval;
			//Msg("ѡ���" + cmbval);
		}

        private void button4_Click(object sender, EventArgs e)
        {
            //string path = @"E:\����\ϵͳ-���\008.������\trunk\����������\CSharpEntityBuilder\CSharpEntityBuilder\bin\Release";
            string path = txtCFilePath.Text;
			FileInfo info = new FileInfo(path);
			if (!info.Directory.Exists)
			{
				Msg("Ŀ¼�����ڣ����ɺ󣬻��Զ�������Ŀ¼");
				return;
			}
            Process.Start(path);
        }







	}
}