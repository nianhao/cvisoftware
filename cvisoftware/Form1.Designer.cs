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
            this.SuspendLayout();
            // 
            // button_InitAll
            // 
            this.button_InitAll.Location = new System.Drawing.Point(28, 39);
            this.button_InitAll.Name = "button_InitAll";
            this.button_InitAll.Size = new System.Drawing.Size(75, 23);
            this.button_InitAll.TabIndex = 0;
            this.button_InitAll.Text = "全部初始化";
            this.button_InitAll.UseVisualStyleBackColor = true;
            this.button_InitAll.Click += new System.EventHandler(this.button_InitAll_Click);
            // 
            // button_showObj
            // 
            this.button_showObj.Location = new System.Drawing.Point(28, 92);
            this.button_showObj.Name = "button_showObj";
            this.button_showObj.Size = new System.Drawing.Size(75, 23);
            this.button_showObj.TabIndex = 1;
            this.button_showObj.Text = "显示组件";
            this.button_showObj.UseVisualStyleBackColor = true;
            this.button_showObj.Click += new System.EventHandler(this.button_showObj_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_showObj);
            this.Controls.Add(this.button_InitAll);
            this.Name = "Form1";
            this.Text = "实验1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_InitAll;
        private System.Windows.Forms.Button button_showObj;
    }
}

