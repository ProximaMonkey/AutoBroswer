namespace AutoBroswer
{
    partial class AutoBroswerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.beginBTN = new System.Windows.Forms.Button();
            this.compareCB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.broswerNumTXT = new System.Windows.Forms.TextBox();
            this.visitDeepCB = new System.Windows.Forms.ComboBox();
            this.mainItemMinTimeTXT = new System.Windows.Forms.TextBox();
            this.mainItemMaxTimeTXT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.visitDeepRndCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.otherItemMaxTimeTXT = new System.Windows.Forms.TextBox();
            this.otherItemMinTimeTXT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.keywordRichTB = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.sellerNameTB = new System.Windows.Forms.TextBox();
            this.jobExpireTimer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.isDebugCB = new System.Windows.Forms.CheckBox();
            this.ipComboBox = new System.Windows.Forms.ComboBox();
            this.IP = new System.Windows.Forms.Label();
            this.getIPBtn = new System.Windows.Forms.Button();
            this.stopIPBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.simulateInfoText = new System.Windows.Forms.Label();
            this.simulateStopBtn = new System.Windows.Forms.Button();
            this.stopTimeLabel = new System.Windows.Forms.Label();
            this.currentStep = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.URLTextBox = new System.Windows.Forms.TextBox();
            this.webBrowserPanel = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // beginBTN
            // 
            this.beginBTN.Location = new System.Drawing.Point(331, 351);
            this.beginBTN.Name = "beginBTN";
            this.beginBTN.Size = new System.Drawing.Size(74, 22);
            this.beginBTN.TabIndex = 0;
            this.beginBTN.Text = "开始浏览";
            this.beginBTN.UseVisualStyleBackColor = true;
            this.beginBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // compareCB
            // 
            this.compareCB.AutoSize = true;
            this.compareCB.Checked = true;
            this.compareCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.compareCB.Location = new System.Drawing.Point(256, 47);
            this.compareCB.Name = "compareCB";
            this.compareCB.Size = new System.Drawing.Size(96, 16);
            this.compareCB.TabIndex = 1;
            this.compareCB.Text = "随机货比三家";
            this.compareCB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "访问深度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "主宝贝时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "浏览量:";
            // 
            // broswerNumTXT
            // 
            this.broswerNumTXT.Location = new System.Drawing.Point(323, 234);
            this.broswerNumTXT.Name = "broswerNumTXT";
            this.broswerNumTXT.Size = new System.Drawing.Size(44, 21);
            this.broswerNumTXT.TabIndex = 6;
            this.broswerNumTXT.Text = "30";
            this.broswerNumTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // visitDeepCB
            // 
            this.visitDeepCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.visitDeepCB.FormattingEnabled = true;
            this.visitDeepCB.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.visitDeepCB.Location = new System.Drawing.Point(322, 92);
            this.visitDeepCB.Name = "visitDeepCB";
            this.visitDeepCB.Size = new System.Drawing.Size(70, 20);
            this.visitDeepCB.TabIndex = 7;
            // 
            // mainItemMinTimeTXT
            // 
            this.mainItemMinTimeTXT.Location = new System.Drawing.Point(322, 118);
            this.mainItemMinTimeTXT.MaxLength = 3;
            this.mainItemMinTimeTXT.Name = "mainItemMinTimeTXT";
            this.mainItemMinTimeTXT.Size = new System.Drawing.Size(44, 21);
            this.mainItemMinTimeTXT.TabIndex = 8;
            this.mainItemMinTimeTXT.Text = "100";
            this.mainItemMinTimeTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mainItemMaxTimeTXT
            // 
            this.mainItemMaxTimeTXT.Location = new System.Drawing.Point(398, 118);
            this.mainItemMaxTimeTXT.Name = "mainItemMaxTimeTXT";
            this.mainItemMaxTimeTXT.Size = new System.Drawing.Size(41, 21);
            this.mainItemMaxTimeTXT.TabIndex = 9;
            this.mainItemMaxTimeTXT.Text = "240";
            this.mainItemMaxTimeTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(375, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "--";
            // 
            // visitDeepRndCheckBox
            // 
            this.visitDeepRndCheckBox.AutoSize = true;
            this.visitDeepRndCheckBox.Location = new System.Drawing.Point(408, 96);
            this.visitDeepRndCheckBox.Name = "visitDeepRndCheckBox";
            this.visitDeepRndCheckBox.Size = new System.Drawing.Size(72, 16);
            this.visitDeepRndCheckBox.TabIndex = 11;
            this.visitDeepRndCheckBox.Text = "深度随机";
            this.visitDeepRndCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(375, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "--";
            // 
            // otherItemMaxTimeTXT
            // 
            this.otherItemMaxTimeTXT.Location = new System.Drawing.Point(398, 145);
            this.otherItemMaxTimeTXT.Name = "otherItemMaxTimeTXT";
            this.otherItemMaxTimeTXT.Size = new System.Drawing.Size(41, 21);
            this.otherItemMaxTimeTXT.TabIndex = 14;
            this.otherItemMaxTimeTXT.Text = "60";
            this.otherItemMaxTimeTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // otherItemMinTimeTXT
            // 
            this.otherItemMinTimeTXT.Location = new System.Drawing.Point(322, 145);
            this.otherItemMinTimeTXT.MaxLength = 3;
            this.otherItemMinTimeTXT.Name = "otherItemMinTimeTXT";
            this.otherItemMinTimeTXT.Size = new System.Drawing.Size(44, 21);
            this.otherItemMinTimeTXT.TabIndex = 13;
            this.otherItemMinTimeTXT.Text = "50";
            this.otherItemMinTimeTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "其它宝贝时间";
            // 
            // keywordRichTB
            // 
            this.keywordRichTB.Location = new System.Drawing.Point(6, 6);
            this.keywordRichTB.Name = "keywordRichTB";
            this.keywordRichTB.Size = new System.Drawing.Size(239, 361);
            this.keywordRichTB.TabIndex = 16;
            this.keywordRichTB.Text = "铁观音  茶农直销\n铁观音茶叶正品\n铁观音浓香型";
            this.keywordRichTB.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(251, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "卖家ID";
            // 
            // sellerNameTB
            // 
            this.sellerNameTB.Location = new System.Drawing.Point(322, 177);
            this.sellerNameTB.Name = "sellerNameTB";
            this.sellerNameTB.Size = new System.Drawing.Size(158, 21);
            this.sellerNameTB.TabIndex = 18;
            this.sellerNameTB.Text = "铁状元";
            // 
            // jobExpireTimer
            // 
            this.jobExpireTimer.Location = new System.Drawing.Point(398, 68);
            this.jobExpireTimer.Name = "jobExpireTimer";
            this.jobExpireTimer.Size = new System.Drawing.Size(65, 21);
            this.jobExpireTimer.TabIndex = 19;
            this.jobExpireTimer.Text = "15";
            this.jobExpireTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(251, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "单个任务超时时间(分)";
            // 
            // isDebugCB
            // 
            this.isDebugCB.AutoSize = true;
            this.isDebugCB.Location = new System.Drawing.Point(385, 47);
            this.isDebugCB.Name = "isDebugCB";
            this.isDebugCB.Size = new System.Drawing.Size(84, 16);
            this.isDebugCB.TabIndex = 21;
            this.isDebugCB.Text = "是否刷销量";
            this.isDebugCB.UseVisualStyleBackColor = true;
            // 
            // ipComboBox
            // 
            this.ipComboBox.FormattingEnabled = true;
            this.ipComboBox.Items.AddRange(new object[] {
            "91VPN",
            "http://vipiu.net 免费代理"});
            this.ipComboBox.Location = new System.Drawing.Point(277, 7);
            this.ipComboBox.Name = "ipComboBox";
            this.ipComboBox.Size = new System.Drawing.Size(186, 20);
            this.ipComboBox.TabIndex = 22;
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(254, 10);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(17, 12);
            this.IP.TabIndex = 23;
            this.IP.Text = "IP";
            // 
            // getIPBtn
            // 
            this.getIPBtn.Location = new System.Drawing.Point(469, 5);
            this.getIPBtn.Name = "getIPBtn";
            this.getIPBtn.Size = new System.Drawing.Size(75, 23);
            this.getIPBtn.TabIndex = 24;
            this.getIPBtn.Text = "获取IP";
            this.getIPBtn.UseVisualStyleBackColor = true;
            this.getIPBtn.Click += new System.EventHandler(this.getIPBtn_Click);
            // 
            // stopIPBtn
            // 
            this.stopIPBtn.Location = new System.Drawing.Point(561, 5);
            this.stopIPBtn.Name = "stopIPBtn";
            this.stopIPBtn.Size = new System.Drawing.Size(75, 23);
            this.stopIPBtn.TabIndex = 25;
            this.stopIPBtn.Text = "停止获取IP";
            this.stopIPBtn.UseVisualStyleBackColor = true;
            this.stopIPBtn.Click += new System.EventHandler(this.stopIPBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1062, 577);
            this.tabControl1.TabIndex = 26;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.keywordRichTB);
            this.tabPage1.Controls.Add(this.stopIPBtn);
            this.tabPage1.Controls.Add(this.beginBTN);
            this.tabPage1.Controls.Add(this.getIPBtn);
            this.tabPage1.Controls.Add(this.compareCB);
            this.tabPage1.Controls.Add(this.IP);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.ipComboBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.isDebugCB);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.broswerNumTXT);
            this.tabPage1.Controls.Add(this.jobExpireTimer);
            this.tabPage1.Controls.Add(this.visitDeepCB);
            this.tabPage1.Controls.Add(this.sellerNameTB);
            this.tabPage1.Controls.Add(this.mainItemMinTimeTXT);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.mainItemMaxTimeTXT);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.visitDeepRndCheckBox);
            this.tabPage1.Controls.Add(this.otherItemMaxTimeTXT);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.otherItemMinTimeTXT);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1054, 552);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.simulateInfoText);
            this.tabPage2.Controls.Add(this.simulateStopBtn);
            this.tabPage2.Controls.Add(this.stopTimeLabel);
            this.tabPage2.Controls.Add(this.currentStep);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.URLTextBox);
            this.tabPage2.Controls.Add(this.webBrowserPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1054, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "模拟点击";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // simulateInfoText
            // 
            this.simulateInfoText.AutoSize = true;
            this.simulateInfoText.Location = new System.Drawing.Point(89, 37);
            this.simulateInfoText.Name = "simulateInfoText";
            this.simulateInfoText.Size = new System.Drawing.Size(29, 12);
            this.simulateInfoText.TabIndex = 6;
            this.simulateInfoText.Text = "参数";
            // 
            // simulateStopBtn
            // 
            this.simulateStopBtn.Location = new System.Drawing.Point(6, 30);
            this.simulateStopBtn.Name = "simulateStopBtn";
            this.simulateStopBtn.Size = new System.Drawing.Size(75, 23);
            this.simulateStopBtn.TabIndex = 5;
            this.simulateStopBtn.Text = "暂停";
            this.simulateStopBtn.UseVisualStyleBackColor = true;
            this.simulateStopBtn.Click += new System.EventHandler(this.simulateStopBtn_Click);
            // 
            // stopTimeLabel
            // 
            this.stopTimeLabel.AutoSize = true;
            this.stopTimeLabel.Location = new System.Drawing.Point(678, 41);
            this.stopTimeLabel.Name = "stopTimeLabel";
            this.stopTimeLabel.Size = new System.Drawing.Size(47, 12);
            this.stopTimeLabel.TabIndex = 4;
            this.stopTimeLabel.Text = "label10";
            // 
            // currentStep
            // 
            this.currentStep.AutoSize = true;
            this.currentStep.Location = new System.Drawing.Point(748, 15);
            this.currentStep.Name = "currentStep";
            this.currentStep.Size = new System.Drawing.Size(47, 12);
            this.currentStep.TabIndex = 3;
            this.currentStep.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(674, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "当前步骤:";
            // 
            // URLTextBox
            // 
            this.URLTextBox.Location = new System.Drawing.Point(6, 6);
            this.URLTextBox.Name = "URLTextBox";
            this.URLTextBox.Size = new System.Drawing.Size(649, 21);
            this.URLTextBox.TabIndex = 1;
            // 
            // webBrowserPanel
            // 
            this.webBrowserPanel.Location = new System.Drawing.Point(2, 56);
            this.webBrowserPanel.Name = "webBrowserPanel";
            this.webBrowserPanel.Size = new System.Drawing.Size(1049, 485);
            this.webBrowserPanel.TabIndex = 0;
            // 
            // AutoBroswerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 577);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "AutoBroswerForm";
            this.Text = "十年有多少日？";
            this.Load += new System.EventHandler(this.AutoBroswerForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoBroswerForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button beginBTN;
        private System.Windows.Forms.CheckBox compareCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox broswerNumTXT;
        private System.Windows.Forms.ComboBox visitDeepCB;
        private System.Windows.Forms.TextBox mainItemMinTimeTXT;
        private System.Windows.Forms.TextBox mainItemMaxTimeTXT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox visitDeepRndCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox otherItemMaxTimeTXT;
        private System.Windows.Forms.TextBox otherItemMinTimeTXT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox keywordRichTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox sellerNameTB;
        private System.Windows.Forms.TextBox jobExpireTimer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox isDebugCB;
        private System.Windows.Forms.ComboBox ipComboBox;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.Button getIPBtn;
        private System.Windows.Forms.Button stopIPBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.Panel webBrowserPanel;
        public System.Windows.Forms.TextBox URLTextBox;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label currentStep;
        public System.Windows.Forms.Label stopTimeLabel;
        private System.Windows.Forms.Label simulateInfoText;
        private System.Windows.Forms.Button simulateStopBtn;
    }
}

