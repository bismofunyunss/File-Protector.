﻿namespace File_Protector
{
    partial class Register_Form
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
            regBox = new GroupBox();
            unmaskPass = new CheckBox();
            confirmPassTxt = new TextBox();
            confirmPassLbl = new Label();
            userLbl = new Label();
            registerBtn = new Button();
            passTxt = new TextBox();
            passWrd = new Label();
            userTxt = new TextBox();
            regBox.SuspendLayout();
            SuspendLayout();
            // 
            // regBox
            // 
            regBox.BackColor = SystemColors.ControlDarkDark;
            regBox.Controls.Add(unmaskPass);
            regBox.Controls.Add(confirmPassTxt);
            regBox.Controls.Add(confirmPassLbl);
            regBox.Controls.Add(userLbl);
            regBox.Controls.Add(registerBtn);
            regBox.Controls.Add(passTxt);
            regBox.Controls.Add(passWrd);
            regBox.Controls.Add(userTxt);
            regBox.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            regBox.ForeColor = Color.WhiteSmoke;
            regBox.Location = new Point(12, 12);
            regBox.Name = "regBox";
            regBox.Size = new Size(454, 300);
            regBox.TabIndex = 9;
            regBox.TabStop = false;
            regBox.Text = "Register";
            // 
            // unmaskPass
            // 
            unmaskPass.AutoSize = true;
            unmaskPass.Location = new Point(264, 263);
            unmaskPass.Name = "unmaskPass";
            unmaskPass.Size = new Size(173, 29);
            unmaskPass.TabIndex = 8;
            unmaskPass.Text = "View Password";
            unmaskPass.UseVisualStyleBackColor = true;
            unmaskPass.CheckedChanged += unmaskPass_CheckedChanged;
            // 
            // confirmPassTxt
            // 
            confirmPassTxt.BackColor = Color.Black;
            confirmPassTxt.ForeColor = Color.Gold;
            confirmPassTxt.Location = new Point(8, 180);
            confirmPassTxt.Name = "confirmPassTxt";
            confirmPassTxt.Size = new Size(429, 32);
            confirmPassTxt.TabIndex = 6;
            confirmPassTxt.UseSystemPasswordChar = true;
            // 
            // confirmPassLbl
            // 
            confirmPassLbl.AutoSize = true;
            confirmPassLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            confirmPassLbl.ForeColor = Color.WhiteSmoke;
            confirmPassLbl.Location = new Point(8, 152);
            confirmPassLbl.Name = "confirmPassLbl";
            confirmPassLbl.Size = new Size(175, 25);
            confirmPassLbl.TabIndex = 7;
            confirmPassLbl.Text = "Confirm Password";
            // 
            // userLbl
            // 
            userLbl.AutoSize = true;
            userLbl.BackColor = SystemColors.ControlDarkDark;
            userLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            userLbl.ForeColor = Color.WhiteSmoke;
            userLbl.Location = new Point(6, 27);
            userLbl.Name = "userLbl";
            userLbl.Size = new Size(100, 25);
            userLbl.TabIndex = 4;
            userLbl.Text = "Username";
            // 
            // registerBtn
            // 
            registerBtn.FlatAppearance.BorderSize = 3;
            registerBtn.FlatStyle = FlatStyle.Flat;
            registerBtn.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            registerBtn.ForeColor = Color.WhiteSmoke;
            registerBtn.Location = new Point(6, 218);
            registerBtn.Name = "registerBtn";
            registerBtn.Size = new Size(431, 39);
            registerBtn.TabIndex = 3;
            registerBtn.Text = "Register";
            registerBtn.UseVisualStyleBackColor = true;
            registerBtn.Click += registerBtn_Click;
            // 
            // passTxt
            // 
            passTxt.BackColor = Color.Black;
            passTxt.ForeColor = Color.Gold;
            passTxt.Location = new Point(8, 117);
            passTxt.Name = "passTxt";
            passTxt.Size = new Size(429, 32);
            passTxt.TabIndex = 1;
            passTxt.UseSystemPasswordChar = true;
            // 
            // passWrd
            // 
            passWrd.AutoSize = true;
            passWrd.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            passWrd.ForeColor = Color.WhiteSmoke;
            passWrd.Location = new Point(8, 89);
            passWrd.Name = "passWrd";
            passWrd.Size = new Size(98, 25);
            passWrd.TabIndex = 5;
            passWrd.Text = "Password";
            // 
            // userTxt
            // 
            userTxt.BackColor = Color.Black;
            userTxt.ForeColor = Color.Gold;
            userTxt.Location = new Point(8, 55);
            userTxt.Name = "userTxt";
            userTxt.Size = new Size(429, 32);
            userTxt.TabIndex = 0;
            // 
            // Register_Form
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(478, 324);
            Controls.Add(regBox);
            Name = "Register_Form";
            Text = "Register";
            FormClosing += Register_Form_FormClosing;
            regBox.ResumeLayout(false);
            regBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox regBox;
        private Label userLbl;
        private Button registerBtn;
        private TextBox passTxt;
        private Label passWrd;
        private TextBox userTxt;
        private CheckBox unmaskPass;
        private TextBox confirmPassTxt;
        private Label confirmPassLbl;
    }
}