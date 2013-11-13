namespace EterashopWinFormsDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.loginTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.outputFilenameTBox = new System.Windows.Forms.TextBox();
            this.setFilenameBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.categoriesTBox = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // loginTBox
            // 
            this.loginTBox.Location = new System.Drawing.Point(12, 25);
            this.loginTBox.Name = "loginTBox";
            this.loginTBox.Size = new System.Drawing.Size(421, 20);
            this.loginTBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // passwordTBox
            // 
            this.passwordTBox.Location = new System.Drawing.Point(12, 64);
            this.passwordTBox.Name = "passwordTBox";
            this.passwordTBox.Size = new System.Drawing.Size(421, 20);
            this.passwordTBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output filename";
            // 
            // outputFilenameTBox
            // 
            this.outputFilenameTBox.Location = new System.Drawing.Point(12, 103);
            this.outputFilenameTBox.Name = "outputFilenameTBox";
            this.outputFilenameTBox.Size = new System.Drawing.Size(394, 20);
            this.outputFilenameTBox.TabIndex = 5;
            // 
            // setFilenameBtn
            // 
            this.setFilenameBtn.Location = new System.Drawing.Point(412, 101);
            this.setFilenameBtn.Name = "setFilenameBtn";
            this.setFilenameBtn.Size = new System.Drawing.Size(21, 23);
            this.setFilenameBtn.TabIndex = 6;
            this.setFilenameBtn.Text = "...";
            this.setFilenameBtn.UseVisualStyleBackColor = true;
            this.setFilenameBtn.Click += new System.EventHandler(this.setFilenameBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Categories (one per line)";
            // 
            // categoriesTBox
            // 
            this.categoriesTBox.Location = new System.Drawing.Point(12, 142);
            this.categoriesTBox.Multiline = true;
            this.categoriesTBox.Name = "categoriesTBox";
            this.categoriesTBox.Size = new System.Drawing.Size(421, 86);
            this.categoriesTBox.TabIndex = 8;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(358, 234);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 9;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 261);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.categoriesTBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.setFilenameBtn);
            this.Controls.Add(this.outputFilenameTBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passwordTBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loginTBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loginTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox outputFilenameTBox;
        private System.Windows.Forms.Button setFilenameBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox categoriesTBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

