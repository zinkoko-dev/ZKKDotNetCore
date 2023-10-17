namespace ZKKDotNetCore.WindowsFormsApp
{
    partial class StudentDapper
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
            this.btnSave = new System.Windows.Forms.Button();
            this.lableGender = new System.Windows.Forms.Label();
            this.labelCity = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(63, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(119, 49);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lableGender
            // 
            this.lableGender.AutoSize = true;
            this.lableGender.Location = new System.Drawing.Point(59, 236);
            this.lableGender.Name = "lableGender";
            this.lableGender.Size = new System.Drawing.Size(75, 20);
            this.lableGender.TabIndex = 12;
            this.lableGender.Text = "Gender : ";
            // 
            // labelCity
            // 
            this.labelCity.AutoSize = true;
            this.labelCity.Location = new System.Drawing.Point(59, 145);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(47, 20);
            this.labelCity.TabIndex = 11;
            this.labelCity.Text = "City : ";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(59, 53);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(63, 20);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "Name : ";
            // 
            // txtGender
            // 
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(63, 264);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(255, 37);
            this.txtGender.TabIndex = 9;
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(63, 177);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(255, 37);
            this.txtCity.TabIndex = 8;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(63, 83);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 37);
            this.txtName.TabIndex = 7;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(58, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(250, 25);
            this.title.TabIndex = 13;
            this.title.Text = "Create Student (Dapper)";
            // 
            // StudentDapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.title);
            this.Controls.Add(this.lableGender);
            this.Controls.Add(this.labelCity);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.txtGender);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Name = "StudentDapper";
            this.Text = "StudentDapper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lableGender;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label title;
    }
}