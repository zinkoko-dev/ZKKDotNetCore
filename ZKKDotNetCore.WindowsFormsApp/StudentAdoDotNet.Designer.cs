namespace ZKKDotNetCore.WindowsFormsApp
{
    partial class StudentAdoDotNet
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
            this.title = new System.Windows.Forms.Label();
            this.lableGender = new System.Windows.Forms.Label();
            this.labelCity = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(52, 49);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(276, 25);
            this.title.TabIndex = 15;
            this.title.Text = "Create Student (ADO.NET)";
            // 
            // lableGender
            // 
            this.lableGender.AutoSize = true;
            this.lableGender.Location = new System.Drawing.Point(53, 300);
            this.lableGender.Name = "lableGender";
            this.lableGender.Size = new System.Drawing.Size(75, 20);
            this.lableGender.TabIndex = 14;
            this.lableGender.Text = "Gender : ";
            // 
            // labelCity
            // 
            this.labelCity.AutoSize = true;
            this.labelCity.Location = new System.Drawing.Point(53, 209);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(47, 20);
            this.labelCity.TabIndex = 13;
            this.labelCity.Text = "City : ";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(53, 117);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(63, 20);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "Name : ";
            // 
            // txtGender
            // 
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(57, 328);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(255, 37);
            this.txtGender.TabIndex = 11;
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(57, 241);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(255, 37);
            this.txtCity.TabIndex = 10;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(57, 147);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 37);
            this.txtName.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(57, 403);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 54);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRead
            // 
            this.btnRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRead.Location = new System.Drawing.Point(224, 403);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(130, 54);
            this.btnRead.TabIndex = 16;
            this.btnRead.Text = "Stu List";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // StudentAdoDotNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 621);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.title);
            this.Controls.Add(this.lableGender);
            this.Controls.Add(this.labelCity);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.txtGender);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Name = "StudentAdoDotNet";
            this.Text = "StudentAdoDotNet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label lableGender;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRead;
    }
}