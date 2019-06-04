namespace cvisoftware
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
            this.button_InitAll = new System.Windows.Forms.Button();
            this.button_showObj = new System.Windows.Forms.Button();
            this.curvePanel = new System.Windows.Forms.Panel();
            this.textBoxSysLog = new System.Windows.Forms.TextBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.修改LabCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试信息录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_InitAll
            // 
            this.button_InitAll.Location = new System.Drawing.Point(37, 598);
            this.button_InitAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_InitAll.Name = "button_InitAll";
            this.button_InitAll.Size = new System.Drawing.Size(100, 29);
            this.button_InitAll.TabIndex = 0;
            this.button_InitAll.Text = "全部初始化";
            this.button_InitAll.UseVisualStyleBackColor = true;
            this.button_InitAll.Click += new System.EventHandler(this.button_InitAll_Click);
            // 
            // button_showObj
            // 
            this.button_showObj.Location = new System.Drawing.Point(37, 679);
            this.button_showObj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_showObj.Name = "button_showObj";
            this.button_showObj.Size = new System.Drawing.Size(100, 29);
            this.button_showObj.TabIndex = 1;
            this.button_showObj.Text = "显示组件";
            this.button_showObj.UseVisualStyleBackColor = true;
            this.button_showObj.Click += new System.EventHandler(this.button_showObj_Click);
            // 
            // curvePanel
            // 
            this.curvePanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.curvePanel.Location = new System.Drawing.Point(80, 56);
            this.curvePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.curvePanel.Name = "curvePanel";
            this.curvePanel.Size = new System.Drawing.Size(932, 506);
            this.curvePanel.TabIndex = 2;
            // 
            // textBoxSysLog
            // 
            this.textBoxSysLog.Location = new System.Drawing.Point(213, 598);
            this.textBoxSysLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSysLog.Multiline = true;
            this.textBoxSysLog.Name = "textBoxSysLog";
            this.textBoxSysLog.ReadOnly = true;
            this.textBoxSysLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSysLog.Size = new System.Drawing.Size(601, 189);
            this.textBoxSysLog.TabIndex = 3;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(875, 690);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(100, 29);
            this.buttonExport.TabIndex = 4;
            this.buttonExport.Text = "导出日志";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(875, 759);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(100, 29);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "清空";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改LabCodeToolStripMenuItem,
            this.测试信息录入ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1101, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 修改LabCodeToolStripMenuItem
            // 
            this.修改LabCodeToolStripMenuItem.Name = "修改LabCodeToolStripMenuItem";
            this.修改LabCodeToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.修改LabCodeToolStripMenuItem.Text = "修改LabCode";
            this.修改LabCodeToolStripMenuItem.Click += new System.EventHandler(this.修改LabCodeToolStripMenuItem_Click);
            // 
            // 测试信息录入ToolStripMenuItem
            // 
            this.测试信息录入ToolStripMenuItem.Name = "测试信息录入ToolStripMenuItem";
            this.测试信息录入ToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.测试信息录入ToolStripMenuItem.Text = "测试信息录入";
            this.测试信息录入ToolStripMenuItem.Click += new System.EventHandler(this.测试信息录入ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 802);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.textBoxSysLog);
            this.Controls.Add(this.curvePanel);
            this.Controls.Add(this.button_showObj);
            this.Controls.Add(this.button_InitAll);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "实验1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_InitAll;
        private System.Windows.Forms.Button button_showObj;
        private System.Windows.Forms.Panel curvePanel;
        private System.Windows.Forms.TextBox textBoxSysLog;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 修改LabCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试信息录入ToolStripMenuItem;
    }
}

