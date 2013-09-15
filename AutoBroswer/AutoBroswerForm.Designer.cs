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
            this.SuspendLayout();
            // 
            // beginBTN
            // 
            this.beginBTN.Location = new System.Drawing.Point(326, 356);
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
            this.compareCB.Location = new System.Drawing.Point(251, 13);
            this.compareCB.Name = "compareCB";
            this.compareCB.Size = new System.Drawing.Size(96, 16);
            this.compareCB.TabIndex = 1;
            this.compareCB.Text = "随机货比三家";
            this.compareCB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "访问深度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "主宝贝时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "浏览量:";
            // 
            // broswerNumTXT
            // 
            this.broswerNumTXT.Location = new System.Drawing.Point(318, 200);
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
            this.visitDeepCB.Location = new System.Drawing.Point(317, 58);
            this.visitDeepCB.Name = "visitDeepCB";
            this.visitDeepCB.Size = new System.Drawing.Size(70, 20);
            this.visitDeepCB.TabIndex = 7;
            // 
            // mainItemMinTimeTXT
            // 
            this.mainItemMinTimeTXT.Location = new System.Drawing.Point(317, 84);
            this.mainItemMinTimeTXT.MaxLength = 3;
            this.mainItemMinTimeTXT.Name = "mainItemMinTimeTXT";
            this.mainItemMinTimeTXT.Size = new System.Drawing.Size(44, 21);
            this.mainItemMinTimeTXT.TabIndex = 8;
            this.mainItemMinTimeTXT.Text = "100";
            this.mainItemMinTimeTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // mainItemMaxTimeTXT
            // 
            this.mainItemMaxTimeTXT.Location = new System.Drawing.Point(393, 84);
            this.mainItemMaxTimeTXT.Name = "mainItemMaxTimeTXT";
            this.mainItemMaxTimeTXT.Size = new System.Drawing.Size(41, 21);
            this.mainItemMaxTimeTXT.TabIndex = 9;
            this.mainItemMaxTimeTXT.Text = "240";
            this.mainItemMaxTimeTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(370, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "--";
            // 
            // visitDeepRndCheckBox
            // 
            this.visitDeepRndCheckBox.AutoSize = true;
            this.visitDeepRndCheckBox.Checked = true;
            this.visitDeepRndCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visitDeepRndCheckBox.Location = new System.Drawing.Point(403, 62);
            this.visitDeepRndCheckBox.Name = "visitDeepRndCheckBox";
            this.visitDeepRndCheckBox.Size = new System.Drawing.Size(72, 16);
            this.visitDeepRndCheckBox.TabIndex = 11;
            this.visitDeepRndCheckBox.Text = "深度随机";
            this.visitDeepRndCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(370, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "--";
            // 
            // otherItemMaxTimeTXT
            // 
            this.otherItemMaxTimeTXT.Location = new System.Drawing.Point(393, 111);
            this.otherItemMaxTimeTXT.Name = "otherItemMaxTimeTXT";
            this.otherItemMaxTimeTXT.Size = new System.Drawing.Size(41, 21);
            this.otherItemMaxTimeTXT.TabIndex = 14;
            this.otherItemMaxTimeTXT.Text = "60";
            this.otherItemMaxTimeTXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // otherItemMinTimeTXT
            // 
            this.otherItemMinTimeTXT.Location = new System.Drawing.Point(317, 111);
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
            this.label6.Location = new System.Drawing.Point(246, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "其它宝贝时间";
            // 
            // keywordRichTB
            // 
            this.keywordRichTB.Location = new System.Drawing.Point(1, 11);
            this.keywordRichTB.Name = "keywordRichTB";
            this.keywordRichTB.Size = new System.Drawing.Size(239, 361);
            this.keywordRichTB.TabIndex = 16;
            this.keywordRichTB.Text = "铁观音  茶农直销\n铁观音茶叶正品\n铁观音浓香型";
            this.keywordRichTB.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "卖家ID";
            // 
            // sellerNameTB
            // 
            this.sellerNameTB.Location = new System.Drawing.Point(317, 143);
            this.sellerNameTB.Name = "sellerNameTB";
            this.sellerNameTB.Size = new System.Drawing.Size(158, 21);
            this.sellerNameTB.TabIndex = 18;
            this.sellerNameTB.Text = "铁状元23";
            // 
            // jobExpireTimer
            // 
            this.jobExpireTimer.Location = new System.Drawing.Point(393, 34);
            this.jobExpireTimer.Name = "jobExpireTimer";
            this.jobExpireTimer.Size = new System.Drawing.Size(65, 21);
            this.jobExpireTimer.TabIndex = 19;
            this.jobExpireTimer.Text = "15";
            this.jobExpireTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(246, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "单个任务超时时间(分)";
            // 
            // AutoBroswerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 390);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.jobExpireTimer);
            this.Controls.Add(this.sellerNameTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.keywordRichTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.otherItemMaxTimeTXT);
            this.Controls.Add(this.otherItemMinTimeTXT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.visitDeepRndCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mainItemMaxTimeTXT);
            this.Controls.Add(this.mainItemMinTimeTXT);
            this.Controls.Add(this.visitDeepCB);
            this.Controls.Add(this.broswerNumTXT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.compareCB);
            this.Controls.Add(this.beginBTN);
            this.Name = "AutoBroswerForm";
            this.Text = "十年有多少日？";
            this.Load += new System.EventHandler(this.AutoBroswerForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoBroswerForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

