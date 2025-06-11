namespace Methane2._0
{
    partial class Form1
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
            this.confInfo = new System.Windows.Forms.Button();
            this.methaneInfo = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.loadInfo = new System.Windows.Forms.Button();
            this.loadvsconfig = new System.Windows.Forms.Button();
            this.vsInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // confInfo
            // 
            this.confInfo.Location = new System.Drawing.Point(12, 12);
            this.confInfo.Name = "confInfo";
            this.confInfo.Size = new System.Drawing.Size(200, 40);
            this.confInfo.TabIndex = 0;
            this.confInfo.Text = "Load Config:";
            this.confInfo.UseVisualStyleBackColor = true;
            this.confInfo.Click += new System.EventHandler(this.confInfo_Click);
            // 
            // methaneInfo
            // 
            this.methaneInfo.Location = new System.Drawing.Point(360, 12);
            this.methaneInfo.Name = "methaneInfo";
            this.methaneInfo.Size = new System.Drawing.Size(200, 40);
            this.methaneInfo.TabIndex = 2;
            this.methaneInfo.Text = "METHANE Refactored 2.0";
            this.methaneInfo.UseVisualStyleBackColor = true;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(360, 71);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(200, 40);
            this.startBtn.TabIndex = 3;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // loadInfo
            // 
            this.loadInfo.Location = new System.Drawing.Point(12, 66);
            this.loadInfo.Name = "loadInfo";
            this.loadInfo.Size = new System.Drawing.Size(200, 51);
            this.loadInfo.TabIndex = 5;
            this.loadInfo.Text = "Loaded Config:";
            this.loadInfo.UseVisualStyleBackColor = true;
            // 
            // loadvsconfig
            // 
            this.loadvsconfig.Location = new System.Drawing.Point(12, 172);
            this.loadvsconfig.Name = "loadvsconfig";
            this.loadvsconfig.Size = new System.Drawing.Size(200, 40);
            this.loadvsconfig.TabIndex = 7;
            this.loadvsconfig.Text = "Load VS Config:";
            this.loadvsconfig.UseVisualStyleBackColor = true;
            this.loadvsconfig.Click += new System.EventHandler(this.loadvsconfig_Click);
            // 
            // vsInfo
            // 
            this.vsInfo.Location = new System.Drawing.Point(12, 218);
            this.vsInfo.Name = "vsInfo";
            this.vsInfo.Size = new System.Drawing.Size(200, 51);
            this.vsInfo.TabIndex = 9;
            this.vsInfo.Text = "Loaded VS Config:";
            this.vsInfo.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(595, 321);
            this.Controls.Add(this.vsInfo);
            this.Controls.Add(this.loadvsconfig);
            this.Controls.Add(this.loadInfo);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.methaneInfo);
            this.Controls.Add(this.confInfo);
            this.Name = "Form1";
            this.Text = "Methane 2.0";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button confInfo;
        private System.Windows.Forms.Button methaneInfo;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button loadInfo;
        private System.Windows.Forms.Button loadvsconfig;
        private System.Windows.Forms.Button vsInfo;
    }
}

