namespace GoBang
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chessBoardGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.WhiteChessLabel = new System.Windows.Forms.Label();
            this.BlackChessLabel = new System.Windows.Forms.Label();
            this.ExitGameButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.StartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerFirstMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompFirstMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.GameRuleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.电脑等级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LowLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MediumLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HighLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regretButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chessBoardGroupBox
            // 
            this.chessBoardGroupBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chessBoardGroupBox.Location = new System.Drawing.Point(12, 16);
            this.chessBoardGroupBox.Name = "chessBoardGroupBox";
            this.chessBoardGroupBox.Size = new System.Drawing.Size(633, 608);
            this.chessBoardGroupBox.TabIndex = 0;
            this.chessBoardGroupBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WhiteChessLabel);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.BlackChessLabel);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(651, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 166);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "玩家信息";
            // 
            // WhiteChessLabel
            // 
            this.WhiteChessLabel.AutoSize = true;
            this.WhiteChessLabel.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WhiteChessLabel.ForeColor = System.Drawing.Color.Lime;
            this.WhiteChessLabel.Location = new System.Drawing.Point(64, 121);
            this.WhiteChessLabel.Name = "WhiteChessLabel";
            this.WhiteChessLabel.Size = new System.Drawing.Size(54, 28);
            this.WhiteChessLabel.TabIndex = 3;
            this.WhiteChessLabel.Text = "电脑";
            // 
            // BlackChessLabel
            // 
            this.BlackChessLabel.AutoSize = true;
            this.BlackChessLabel.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BlackChessLabel.ForeColor = System.Drawing.Color.Red;
            this.BlackChessLabel.Location = new System.Drawing.Point(64, 52);
            this.BlackChessLabel.Name = "BlackChessLabel";
            this.BlackChessLabel.Size = new System.Drawing.Size(54, 28);
            this.BlackChessLabel.TabIndex = 1;
            this.BlackChessLabel.Text = "玩家";
            // 
            // ExitGameButton
            // 
            this.ExitGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitGameButton.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExitGameButton.Location = new System.Drawing.Point(669, 478);
            this.ExitGameButton.Name = "ExitGameButton";
            this.ExitGameButton.Size = new System.Drawing.Size(100, 37);
            this.ExitGameButton.TabIndex = 2;
            this.ExitGameButton.Text = "退出棋局";
            this.ExitGameButton.UseVisualStyleBackColor = true;
            this.ExitGameButton.Click += new System.EventHandler(this.ExitGameButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.电脑等级ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(795, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartMenuItem,
            this.PlayerFirstMenuItem,
            this.CompFirstMenuItem,
            this.ExitMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem1.Text = "游戏选项";
            // 
            // StartMenuItem
            // 
            this.StartMenuItem.Name = "StartMenuItem";
            this.StartMenuItem.Size = new System.Drawing.Size(112, 22);
            this.StartMenuItem.Text = "开始";
            this.StartMenuItem.Click += new System.EventHandler(this.StartMenuItem_Click);
            // 
            // PlayerFirstMenuItem
            // 
            this.PlayerFirstMenuItem.Name = "PlayerFirstMenuItem";
            this.PlayerFirstMenuItem.Size = new System.Drawing.Size(112, 22);
            this.PlayerFirstMenuItem.Text = "玩家先";
            this.PlayerFirstMenuItem.Click += new System.EventHandler(this.PlayerFirstMenuItem_Click);
            // 
            // CompFirstMenuItem
            // 
            this.CompFirstMenuItem.Name = "CompFirstMenuItem";
            this.CompFirstMenuItem.Size = new System.Drawing.Size(112, 22);
            this.CompFirstMenuItem.Text = "电脑先";
            this.CompFirstMenuItem.Click += new System.EventHandler(this.CompFirstMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(112, 22);
            this.ExitMenuItem.Text = "退出";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameRuleMenuItem,
            this.AboutMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem2.Text = "帮助";
            // 
            // GameRuleMenuItem
            // 
            this.GameRuleMenuItem.Name = "GameRuleMenuItem";
            this.GameRuleMenuItem.Size = new System.Drawing.Size(124, 22);
            this.GameRuleMenuItem.Text = "游戏规则";
            this.GameRuleMenuItem.Click += new System.EventHandler(this.GameRuleMenuItem_Click);
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(124, 22);
            this.AboutMenuItem.Text = "关于";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // 电脑等级ToolStripMenuItem
            // 
            this.电脑等级ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LowLevelMenuItem,
            this.MediumLevelMenuItem,
            this.HighLevelMenuItem});
            this.电脑等级ToolStripMenuItem.Name = "电脑等级ToolStripMenuItem";
            this.电脑等级ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.电脑等级ToolStripMenuItem.Text = "电脑等级";
            // 
            // LowLevelMenuItem
            // 
            this.LowLevelMenuItem.Name = "LowLevelMenuItem";
            this.LowLevelMenuItem.Size = new System.Drawing.Size(88, 22);
            this.LowLevelMenuItem.Text = "低";
            this.LowLevelMenuItem.Click += new System.EventHandler(this.LowLevelMenuItem_Click);
            // 
            // MediumLevelMenuItem
            // 
            this.MediumLevelMenuItem.Name = "MediumLevelMenuItem";
            this.MediumLevelMenuItem.Size = new System.Drawing.Size(88, 22);
            this.MediumLevelMenuItem.Text = "中";
            this.MediumLevelMenuItem.Click += new System.EventHandler(this.MediumLevelMenuItem_Click);
            // 
            // HighLevelMenuItem
            // 
            this.HighLevelMenuItem.Name = "HighLevelMenuItem";
            this.HighLevelMenuItem.Size = new System.Drawing.Size(88, 22);
            this.HighLevelMenuItem.Text = "高";
            this.HighLevelMenuItem.Click += new System.EventHandler(this.HighLevelMenuItem_Click);
            // 
            // regretButton
            // 
            this.regretButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.regretButton.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.regretButton.Location = new System.Drawing.Point(669, 404);
            this.regretButton.Name = "regretButton";
            this.regretButton.Size = new System.Drawing.Size(100, 37);
            this.regretButton.TabIndex = 4;
            this.regretButton.Text = "悔棋";
            this.regretButton.UseVisualStyleBackColor = true;
            this.regretButton.Click += new System.EventHandler(this.regrentButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GoBang.Properties.Resources.white;
            this.pictureBox2.Location = new System.Drawing.Point(18, 121);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(27, 28);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GoBang.Properties.Resources.black;
            this.pictureBox1.Location = new System.Drawing.Point(18, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 30);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 636);
            this.Controls.Add(this.regretButton);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ExitGameButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chessBoardGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox chessBoardGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label WhiteChessLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label BlackChessLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ExitGameButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button regretButton;
        private System.Windows.Forms.ToolStripMenuItem StartMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayerFirstMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CompFirstMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameRuleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 电脑等级ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LowLevelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MediumLevelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HighLevelMenuItem;
    }
}

