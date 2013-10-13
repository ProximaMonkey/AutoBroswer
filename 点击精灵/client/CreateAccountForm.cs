namespace client
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CreateAccountForm : Form
    {
        private ManageForm _parent;
        private Button buttonCheckAccountName;
        private Button buttonModifyAccountSubmit;
        private IContainer components;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label labelTip;
        private TextBox textBoxModifyAccount;
        private TextBox textBoxModifyPassword;
        private TextBox textBoxModifyPassword2;
        private TextBox textBoxModifyPhone;
        private TextBox textBoxReferee;
        private TextBox textBoxUserName;

        public CreateAccountForm(ManageForm parent)
        {
            this._parent = parent;
            this.InitializeComponent();
        }

        private void buttonCheckAccountName_Click(object sender, EventArgs e)
        {
            this.labelTip.Text = "";
            string str = WorldPacket.ResizeUTF8String(this.textBoxUserName.Text.Trim(), 0x20);
            this.textBoxUserName.Text = str;
            if (string.IsNullOrEmpty(str))
            {
                this.labelTip.Text = "请输入您的用户名";
            }
            else if (str.IndexOf('@') != -1)
            {
                this.labelTip.Text = "非法字符:@";
            }
            else
            {
                this._parent.SendCheckAccountName(str);
            }
        }

        private void buttonModifyAccountSubmit_Click(object sender, EventArgs e)
        {
            this.labelTip.Text = "";
            string str = WorldPacket.ResizeUTF8String(this.textBoxUserName.Text.Trim(), 0x20);
            this.textBoxUserName.Text = str;
            string account = WorldPacket.ResizeUTF8String(this.textBoxModifyAccount.Text.Trim(), 0x40);
            this.textBoxModifyAccount.Text = account;
            string str3 = WorldPacket.ResizeUTF8String(this.textBoxModifyPassword.Text.Trim(), 0x20);
            this.textBoxModifyPassword.Text = str3;
            string str4 = WorldPacket.ResizeUTF8String(this.textBoxModifyPassword2.Text.Trim(), 0x20);
            this.textBoxModifyPassword2.Text = str4;
            string phone = WorldPacket.ResizeUTF8String(this.textBoxModifyPhone.Text.Trim(), 0x40);
            this.textBoxModifyPhone.Text = phone;
            string referee = WorldPacket.ResizeUTF8String(this.textBoxReferee.Text.Trim(), 0x40);
            this.textBoxReferee.Text = referee;
            if (string.IsNullOrEmpty(str))
            {
                this.labelTip.Text = "请输入您的用户名";
            }
            else if (!WindowUtil.CheckEmail(account))
            {
                this.labelTip.Text = "请输入您的Email地址";
            }
            else if ((string.IsNullOrEmpty(str3) || (str3.Length < 6)) || (str3.Length > 0x20))
            {
                this.labelTip.Text = "请输入6位-32位密码";
            }
            else if (string.IsNullOrEmpty(str4))
            {
                this.labelTip.Text = "请再次输入您的密码";
            }
            else if (str3 != str4)
            {
                this.labelTip.Text = "两次输入的密码不一致!";
            }
            else
            {
                this._parent.SendModifyAccount(str, account, str3, phone, referee);
                base.Close();
            }
        }

        public void CheckAccount(int ret)
        {
            if (ret == 0)
            {
                this.labelTip.Text = "该帐户名可用";
            }
            else
            {
                this.labelTip.Text = "该帐户名已经被注册!";
            }
        }

        private void CreateAccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._parent != null)
            {
                this._parent.CreateAccountForm = null;
                this._parent = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(CreateAccountForm));
            this.textBoxModifyAccount = new TextBox();
            this.label1 = new Label();
            this.textBoxModifyPassword = new TextBox();
            this.label2 = new Label();
            this.textBoxModifyPassword2 = new TextBox();
            this.label3 = new Label();
            this.textBoxModifyPhone = new TextBox();
            this.label5 = new Label();
            this.buttonCheckAccountName = new Button();
            this.buttonModifyAccountSubmit = new Button();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.labelTip = new Label();
            this.label4 = new Label();
            this.textBoxReferee = new TextBox();
            this.label10 = new Label();
            this.label11 = new Label();
            this.label12 = new Label();
            this.textBoxUserName = new TextBox();
            base.SuspendLayout();
            this.textBoxModifyAccount.Location = new Point(0x80, 0x35);
            this.textBoxModifyAccount.Name = "textBoxModifyAccount";
            this.textBoxModifyAccount.Size = new Size(0xf6, 0x15);
            this.textBoxModifyAccount.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x45, 0x19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "帐户名：";
            this.textBoxModifyPassword.Location = new Point(0x80, 0x54);
            this.textBoxModifyPassword.Name = "textBoxModifyPassword";
            this.textBoxModifyPassword.Size = new Size(0xf6, 0x15);
            this.textBoxModifyPassword.TabIndex = 2;
            this.textBoxModifyPassword.UseSystemPasswordChar = true;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x51, 0x57);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "密码：";
            this.textBoxModifyPassword2.Location = new Point(0x80, 0x73);
            this.textBoxModifyPassword2.Name = "textBoxModifyPassword2";
            this.textBoxModifyPassword2.Size = new Size(0xf6, 0x15);
            this.textBoxModifyPassword2.TabIndex = 3;
            this.textBoxModifyPassword2.UseSystemPasswordChar = true;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 0x76);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x71, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "请再输入一次密码：";
            this.textBoxModifyPhone.Location = new Point(0x80, 0x92);
            this.textBoxModifyPhone.Name = "textBoxModifyPhone";
            this.textBoxModifyPhone.Size = new Size(0xf6, 0x15);
            this.textBoxModifyPhone.TabIndex = 4;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x2d, 180);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x4d, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "推荐人帐号：";
            this.buttonCheckAccountName.Location = new Point(0x199, 20);
            this.buttonCheckAccountName.Name = "buttonCheckAccountName";
            this.buttonCheckAccountName.Size = new Size(0x4b, 0x17);
            this.buttonCheckAccountName.TabIndex = 6;
            this.buttonCheckAccountName.Text = "检测重名";
            this.buttonCheckAccountName.UseVisualStyleBackColor = true;
            this.buttonCheckAccountName.Click += new EventHandler(this.buttonCheckAccountName_Click);
            this.buttonModifyAccountSubmit.Location = new Point(0xf8, 0x112);
            this.buttonModifyAccountSubmit.Name = "buttonModifyAccountSubmit";
            this.buttonModifyAccountSubmit.Size = new Size(0x4b, 0x17);
            this.buttonModifyAccountSubmit.TabIndex = 6;
            this.buttonModifyAccountSubmit.Text = "提交";
            this.buttonModifyAccountSubmit.UseVisualStyleBackColor = true;
            this.buttonModifyAccountSubmit.Click += new EventHandler(this.buttonModifyAccountSubmit_Click);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(380, 0x19);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "(*)";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(380, 0x57);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x17, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "(*)";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(380, 0x76);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x17, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "(*)";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x59, 0xd3);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x14f, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "(*为必须填写栏目，建议填写您的联系电话，以方便找回密码)";
            this.labelTip.AutoSize = true;
            this.labelTip.ForeColor = Color.Red;
            this.labelTip.Location = new Point(0x59, 0xf2);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new Size(0, 12);
            this.labelTip.TabIndex = 15;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x51, 0x95);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "电话：";
            this.textBoxReferee.Location = new Point(0x80, 0xb1);
            this.textBoxReferee.Name = "textBoxReferee";
            this.textBoxReferee.Size = new Size(0xf6, 0x15);
            this.textBoxReferee.TabIndex = 5;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(380, 180);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0xad, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "(推荐人将会获得额外金币哦！)";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x4b, 0x38);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x2f, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "Email：";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(380, 0x38);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x17, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "(*)";
            this.textBoxUserName.Location = new Point(0x80, 0x16);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new Size(0xf6, 0x15);
            this.textBoxUserName.TabIndex = 1;
            base.AcceptButton = this.buttonModifyAccountSubmit;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x236, 0x135);
            base.Controls.Add(this.labelTip);
            base.Controls.Add(this.buttonModifyAccountSubmit);
            base.Controls.Add(this.buttonCheckAccountName);
            base.Controls.Add(this.textBoxReferee);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBoxModifyPhone);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.textBoxModifyPassword2);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBoxModifyPassword);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBoxUserName);
            base.Controls.Add(this.textBoxModifyAccount);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "CreateAccountForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "修改用户信息";
            base.FormClosing += new FormClosingEventHandler(this.CreateAccountForm_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

