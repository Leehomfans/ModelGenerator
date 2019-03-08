namespace CSharpEntityBuilder
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.rbMSSQL = new System.Windows.Forms.RadioButton();
            this.rbMySQL = new System.Windows.Forms.RadioButton();
            this.rbOracle = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.cbUseDefaultPort = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_UseParams = new System.Windows.Forms.TabPage();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tp_UseConnStr = new System.Windows.Forms.TabPage();
            this.txtConStr = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.btnCreateEntityFile = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lb_show = new System.Windows.Forms.ToolStripStatusLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCNameSpace = new System.Windows.Forms.TextBox();
            this.txtCFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectCFilePath = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lbl_db_msg = new System.Windows.Forms.Label();
            this.txt_create_tables = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtusing = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtn_state = new System.Windows.Forms.RadioButton();
            this.rbtn_isdel = new System.Windows.Forms.RadioButton();
            this.gbox_delField = new System.Windows.Forms.GroupBox();
            this.chkSimplyModel = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tp_UseParams.SuspendLayout();
            this.tp_UseConnStr.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gbox_delField.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库类型：";
            // 
            // rbMSSQL
            // 
            this.rbMSSQL.AutoSize = true;
            this.rbMSSQL.Enabled = false;
            this.rbMSSQL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbMSSQL.Location = new System.Drawing.Point(106, 16);
            this.rbMSSQL.Name = "rbMSSQL";
            this.rbMSSQL.Size = new System.Drawing.Size(58, 16);
            this.rbMSSQL.TabIndex = 1;
            this.rbMSSQL.Text = "MS-SQL";
            this.rbMSSQL.UseVisualStyleBackColor = true;
            this.rbMSSQL.CheckedChanged += new System.EventHandler(this.rbSQLType_CheckedChanged);
            // 
            // rbMySQL
            // 
            this.rbMySQL.AutoSize = true;
            this.rbMySQL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbMySQL.Location = new System.Drawing.Point(190, 16);
            this.rbMySQL.Name = "rbMySQL";
            this.rbMySQL.Size = new System.Drawing.Size(52, 16);
            this.rbMySQL.TabIndex = 1;
            this.rbMySQL.Text = "MySQL";
            this.rbMySQL.UseVisualStyleBackColor = true;
            this.rbMySQL.CheckedChanged += new System.EventHandler(this.rbSQLType_CheckedChanged);
            // 
            // rbOracle
            // 
            this.rbOracle.AutoSize = true;
            this.rbOracle.Enabled = false;
            this.rbOracle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbOracle.Location = new System.Drawing.Point(265, 16);
            this.rbOracle.Name = "rbOracle";
            this.rbOracle.Size = new System.Drawing.Size(58, 16);
            this.rbOracle.TabIndex = 1;
            this.rbOracle.Text = "Oracle";
            this.rbOracle.UseVisualStyleBackColor = true;
            this.rbOracle.CheckedChanged += new System.EventHandler(this.rbSQLType_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务器名或IP：";
            // 
            // txtServer
            // 
            this.txtServer.BackColor = System.Drawing.SystemColors.Info;
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.ForeColor = System.Drawing.Color.DarkRed;
            this.txtServer.Location = new System.Drawing.Point(91, 13);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(137, 21);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "192.168.0.171";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.SystemColors.Info;
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Enabled = false;
            this.txtPort.ForeColor = System.Drawing.Color.DarkRed;
            this.txtPort.Location = new System.Drawing.Point(311, 13);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(65, 21);
            this.txtPort.TabIndex = 0;
            // 
            // cbUseDefaultPort
            // 
            this.cbUseDefaultPort.AutoSize = true;
            this.cbUseDefaultPort.Checked = true;
            this.cbUseDefaultPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseDefaultPort.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbUseDefaultPort.Location = new System.Drawing.Point(242, 17);
            this.cbUseDefaultPort.Name = "cbUseDefaultPort";
            this.cbUseDefaultPort.Size = new System.Drawing.Size(70, 16);
            this.cbUseDefaultPort.TabIndex = 4;
            this.cbUseDefaultPort.Text = "默认端口";
            this.cbUseDefaultPort.UseVisualStyleBackColor = true;
            this.cbUseDefaultPort.CheckedChanged += new System.EventHandler(this.cbUseDefaultPort_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "用户名：";
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.Info;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.ForeColor = System.Drawing.Color.DarkRed;
            this.txtUserName.Location = new System.Drawing.Point(310, 47);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(66, 21);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Tag = "";
            this.txtUserName.Text = "root";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(384, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "密 码：";
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.SystemColors.Info;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPwd.ForeColor = System.Drawing.Color.DarkRed;
            this.txtPwd.Location = new System.Drawing.Point(426, 47);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(66, 21);
            this.txtPwd.TabIndex = 0;
            this.txtPwd.Text = "jc#318a";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_UseParams);
            this.tabControl1.Controls.Add(this.tp_UseConnStr);
            this.tabControl1.Location = new System.Drawing.Point(5, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(511, 107);
            this.tabControl1.TabIndex = 11;
            // 
            // tp_UseParams
            // 
            this.tp_UseParams.Controls.Add(this.txtPwd);
            this.tp_UseParams.Controls.Add(this.txtDBName);
            this.tp_UseParams.Controls.Add(this.txtServer);
            this.tp_UseParams.Controls.Add(this.label4);
            this.tp_UseParams.Controls.Add(this.label2);
            this.tp_UseParams.Controls.Add(this.txtUserName);
            this.tp_UseParams.Controls.Add(this.txtPort);
            this.tp_UseParams.Controls.Add(this.label9);
            this.tp_UseParams.Controls.Add(this.label3);
            this.tp_UseParams.Controls.Add(this.cbUseDefaultPort);
            this.tp_UseParams.Location = new System.Drawing.Point(4, 22);
            this.tp_UseParams.Name = "tp_UseParams";
            this.tp_UseParams.Padding = new System.Windows.Forms.Padding(3);
            this.tp_UseParams.Size = new System.Drawing.Size(503, 81);
            this.tp_UseParams.TabIndex = 0;
            this.tp_UseParams.Text = "连接数据库";
            this.tp_UseParams.UseVisualStyleBackColor = true;
            // 
            // txtDBName
            // 
            this.txtDBName.BackColor = System.Drawing.SystemColors.Info;
            this.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDBName.Location = new System.Drawing.Point(89, 47);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(139, 21);
            this.txtDBName.TabIndex = 0;
            this.txtDBName.Text = "jcpeixun";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "数据库名称：";
            // 
            // tp_UseConnStr
            // 
            this.tp_UseConnStr.Controls.Add(this.txtConStr);
            this.tp_UseConnStr.Location = new System.Drawing.Point(4, 22);
            this.tp_UseConnStr.Name = "tp_UseConnStr";
            this.tp_UseConnStr.Padding = new System.Windows.Forms.Padding(3);
            this.tp_UseConnStr.Size = new System.Drawing.Size(503, 81);
            this.tp_UseConnStr.TabIndex = 1;
            this.tp_UseConnStr.Text = "使用数据库连接串";
            this.tp_UseConnStr.UseVisualStyleBackColor = true;
            // 
            // txtConStr
            // 
            this.txtConStr.BackColor = System.Drawing.SystemColors.Window;
            this.txtConStr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConStr.Location = new System.Drawing.Point(0, 3);
            this.txtConStr.Multiline = true;
            this.txtConStr.Name = "txtConStr";
            this.txtConStr.Size = new System.Drawing.Size(503, 79);
            this.txtConStr.TabIndex = 0;
            this.txtConStr.Text = "Server=localhost;Database=mydb;Uid=root;Pwd=admin;charset=gbk;";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(521, 68);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(54, 86);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连 接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "选择表：";
            // 
            // cmbTables
            // 
            this.cmbTables.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cmbTables.ForeColor = System.Drawing.Color.Black;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.Location = new System.Drawing.Point(99, 175);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(179, 20);
            this.cmbTables.TabIndex = 15;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
            // 
            // btnCreateEntityFile
            // 
            this.btnCreateEntityFile.Location = new System.Drawing.Point(314, 442);
            this.btnCreateEntityFile.Name = "btnCreateEntityFile";
            this.btnCreateEntityFile.Size = new System.Drawing.Size(147, 33);
            this.btnCreateEntityFile.TabIndex = 0;
            this.btnCreateEntityFile.Text = "生成Data数据处理文件";
            this.btnCreateEntityFile.UseVisualStyleBackColor = true;
            this.btnCreateEntityFile.Visible = false;
            this.btnCreateEntityFile.Click += new System.EventHandler(this.btnCreateEntityFile_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 384);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "Model命名空间：";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.txtNameSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNameSpace.Location = new System.Drawing.Point(102, 381);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(187, 21);
            this.txtNameSpace.TabIndex = 0;
            this.txtNameSpace.Click += new System.EventHandler(this.txtNameSpace_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_show});
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(778, 22);
            this.statusStrip1.TabIndex = 52;
            // 
            // lb_show
            // 
            this.lb_show.Font = new System.Drawing.Font("宋体", 11F);
            this.lb_show.ForeColor = System.Drawing.Color.OliveDrab;
            this.lb_show.Name = "lb_show";
            this.lb_show.Size = new System.Drawing.Size(0, 17);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 451);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 55;
            this.label11.Text = "Data命名空间：";
            this.label11.Visible = false;
            // 
            // txtCNameSpace
            // 
            this.txtCNameSpace.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.txtCNameSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCNameSpace.Location = new System.Drawing.Point(101, 447);
            this.txtCNameSpace.Name = "txtCNameSpace";
            this.txtCNameSpace.Size = new System.Drawing.Size(187, 21);
            this.txtCNameSpace.TabIndex = 0;
            this.txtCNameSpace.Visible = false;
            // 
            // txtCFilePath
            // 
            this.txtCFilePath.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.txtCFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCFilePath.Location = new System.Drawing.Point(98, 250);
            this.txtCFilePath.Name = "txtCFilePath";
            this.txtCFilePath.Size = new System.Drawing.Size(389, 21);
            this.txtCFilePath.TabIndex = 0;
            // 
            // btnSelectCFilePath
            // 
            this.btnSelectCFilePath.BackColor = System.Drawing.Color.SandyBrown;
            this.btnSelectCFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectCFilePath.Location = new System.Drawing.Point(482, 250);
            this.btnSelectCFilePath.Name = "btnSelectCFilePath";
            this.btnSelectCFilePath.Size = new System.Drawing.Size(49, 21);
            this.btnSelectCFilePath.TabIndex = 59;
            this.btnSelectCFilePath.Text = "…";
            this.btnSelectCFilePath.UseVisualStyleBackColor = false;
            this.btnSelectCFilePath.Click += new System.EventHandler(this.btnSelectCFilePath_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 256);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 64;
            this.label15.Text = "文件存放路径：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 423);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(521, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "---------------------------------------------------------------------------------" +
                "-----";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成Model文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(9, 310);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(132, 16);
            this.checkBox2.TabIndex = 67;
            this.checkBox2.Text = "生成handle处理页面";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(183, 310);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 66;
            this.checkBox1.Text = "生成查询页面";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(314, 301);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 33);
            this.button2.TabIndex = 69;
            this.button2.Text = "生成页面";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(600, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 86);
            this.button3.TabIndex = 0;
            this.button3.Text = "生成数据库表说明文档[html格式]";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.SandyBrown;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(561, 247);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 26);
            this.button4.TabIndex = 73;
            this.button4.Text = "打开文件夹";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lbl_db_msg
            // 
            this.lbl_db_msg.AutoSize = true;
            this.lbl_db_msg.Location = new System.Drawing.Point(393, 48);
            this.lbl_db_msg.Name = "lbl_db_msg";
            this.lbl_db_msg.Size = new System.Drawing.Size(17, 12);
            this.lbl_db_msg.TabIndex = 74;
            this.lbl_db_msg.Text = "..";
            // 
            // txt_create_tables
            // 
            this.txt_create_tables.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.txt_create_tables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_create_tables.Location = new System.Drawing.Point(98, 208);
            this.txt_create_tables.Name = "txt_create_tables";
            this.txt_create_tables.Size = new System.Drawing.Size(187, 21);
            this.txt_create_tables.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 213);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 76;
            this.label10.Text = "需要生成的表：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(291, 213);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(287, 12);
            this.label13.TabIndex = 77;
            this.label13.Text = "可输入表前缀*查询;可输入多个表,用逗号或空格隔开";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(401, 12);
            this.label6.TabIndex = 78;
            this.label6.Text = "生成的表必须有主键和标记删除的字段（isdel或state） ，值为1表示删除";
            // 
            // txtusing
            // 
            this.txtusing.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.txtusing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusing.Location = new System.Drawing.Point(100, 484);
            this.txtusing.Name = "txtusing";
            this.txtusing.Size = new System.Drawing.Size(187, 21);
            this.txtusing.TabIndex = 0;
            this.txtusing.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 486);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 80;
            this.label8.Text = "自定义using：";
            this.label8.Visible = false;
            // 
            // rbtn_state
            // 
            this.rbtn_state.AutoSize = true;
            this.rbtn_state.Checked = true;
            this.rbtn_state.Location = new System.Drawing.Point(6, 20);
            this.rbtn_state.Name = "rbtn_state";
            this.rbtn_state.Size = new System.Drawing.Size(53, 16);
            this.rbtn_state.TabIndex = 81;
            this.rbtn_state.TabStop = true;
            this.rbtn_state.Text = "state";
            this.rbtn_state.UseVisualStyleBackColor = true;
            // 
            // rbtn_isdel
            // 
            this.rbtn_isdel.AutoSize = true;
            this.rbtn_isdel.Location = new System.Drawing.Point(104, 20);
            this.rbtn_isdel.Name = "rbtn_isdel";
            this.rbtn_isdel.Size = new System.Drawing.Size(53, 16);
            this.rbtn_isdel.TabIndex = 82;
            this.rbtn_isdel.Text = "isdel";
            this.rbtn_isdel.UseVisualStyleBackColor = true;
            // 
            // gbox_delField
            // 
            this.gbox_delField.Controls.Add(this.rbtn_isdel);
            this.gbox_delField.Controls.Add(this.rbtn_state);
            this.gbox_delField.Location = new System.Drawing.Point(521, 345);
            this.gbox_delField.Name = "gbox_delField";
            this.gbox_delField.Size = new System.Drawing.Size(200, 62);
            this.gbox_delField.TabIndex = 83;
            this.gbox_delField.TabStop = false;
            this.gbox_delField.Text = "标记删除字段";
            // 
            // chkSimplyModel
            // 
            this.chkSimplyModel.AutoSize = true;
            this.chkSimplyModel.Location = new System.Drawing.Point(423, 383);
            this.chkSimplyModel.Name = "chkSimplyModel";
            this.chkSimplyModel.Size = new System.Drawing.Size(84, 16);
            this.chkSimplyModel.TabIndex = 84;
            this.chkSimplyModel.Text = "生成EF实体";
            this.chkSimplyModel.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 555);
            this.Controls.Add(this.chkSimplyModel);
            this.Controls.Add(this.gbox_delField);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtusing);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_create_tables);
            this.Controls.Add(this.lbl_db_msg);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnSelectCFilePath);
            this.Controls.Add(this.txtCFilePath);
            this.Controls.Add(this.txtCNameSpace);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.btnCreateEntityFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.rbOracle);
            this.Controls.Add(this.rbMySQL);
            this.Controls.Add(this.rbMSSQL);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "C#实体类生成器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tp_UseParams.ResumeLayout(false);
            this.tp_UseParams.PerformLayout();
            this.tp_UseConnStr.ResumeLayout(false);
            this.tp_UseConnStr.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbox_delField.ResumeLayout(false);
            this.gbox_delField.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbMSSQL;
        private System.Windows.Forms.RadioButton rbMySQL;
        private System.Windows.Forms.RadioButton rbOracle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox cbUseDefaultPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_UseParams;
        private System.Windows.Forms.TabPage tp_UseConnStr;
        private System.Windows.Forms.TextBox txtConStr;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTables;
        private System.Windows.Forms.Button btnCreateEntityFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lb_show;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCNameSpace;
        private System.Windows.Forms.TextBox txtCFilePath;
		private System.Windows.Forms.Button btnSelectCFilePath;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lbl_db_msg;
		private System.Windows.Forms.TextBox txt_create_tables;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtusing;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rbtn_state;
        private System.Windows.Forms.RadioButton rbtn_isdel;
        private System.Windows.Forms.GroupBox gbox_delField;
        private System.Windows.Forms.CheckBox chkSimplyModel;

    }
}

