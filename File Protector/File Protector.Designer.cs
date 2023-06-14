namespace File_Protector
{
    partial class FileProtector
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileProtector));
            groupBox1 = new GroupBox();
            saveLoginCheckBox = new CheckBox();
            unmaskPass = new CheckBox();
            Reg_Btn = new Button();
            Login_Btn = new Button();
            UserPasswrd_Inpt = new TextBox();
            Pass_Word = new Label();
            Userinpt_Text = new TextBox();
            User_Name = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(saveLoginCheckBox);
            groupBox1.Controls.Add(unmaskPass);
            groupBox1.Controls.Add(Reg_Btn);
            groupBox1.Controls.Add(Login_Btn);
            groupBox1.Controls.Add(UserPasswrd_Inpt);
            groupBox1.Controls.Add(Pass_Word);
            groupBox1.Controls.Add(Userinpt_Text);
            groupBox1.Controls.Add(User_Name);
            groupBox1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.WhiteSmoke;
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(568, 287);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Sign In";
            // 
            // saveLoginCheckBox
            // 
            saveLoginCheckBox.AutoSize = true;
            saveLoginCheckBox.Location = new Point(6, 249);
            saveLoginCheckBox.Name = "saveLoginCheckBox";
            saveLoginCheckBox.Size = new Size(167, 29);
            saveLoginCheckBox.TabIndex = 10;
            saveLoginCheckBox.Text = "Remember Me";
            saveLoginCheckBox.UseVisualStyleBackColor = true;
            // 
            // unmaskPass
            // 
            unmaskPass.AutoSize = true;
            unmaskPass.Location = new Point(389, 249);
            unmaskPass.Name = "unmaskPass";
            unmaskPass.Size = new Size(173, 29);
            unmaskPass.TabIndex = 9;
            unmaskPass.Text = "View Password";
            unmaskPass.UseVisualStyleBackColor = true;
            unmaskPass.CheckedChanged += unmaskPass_CheckedChanged;
            // 
            // Reg_Btn
            // 
            Reg_Btn.FlatAppearance.BorderSize = 3;
            Reg_Btn.FlatStyle = FlatStyle.Flat;
            Reg_Btn.Location = new Point(6, 203);
            Reg_Btn.Name = "Reg_Btn";
            Reg_Btn.Size = new Size(556, 40);
            Reg_Btn.TabIndex = 5;
            Reg_Btn.Text = "Click Here If You Don't Have An Account";
            Reg_Btn.UseVisualStyleBackColor = true;
            Reg_Btn.Click += Reg_Btn_Click;
            // 
            // Login_Btn
            // 
            Login_Btn.FlatAppearance.BorderSize = 3;
            Login_Btn.FlatStyle = FlatStyle.Flat;
            Login_Btn.ForeColor = Color.WhiteSmoke;
            Login_Btn.Location = new Point(6, 157);
            Login_Btn.Name = "Login_Btn";
            Login_Btn.Size = new Size(556, 40);
            Login_Btn.TabIndex = 4;
            Login_Btn.Text = "Sign In";
            Login_Btn.UseVisualStyleBackColor = true;
            Login_Btn.Click += Login_Btn_Click;
            // 
            // UserPasswrd_Inpt
            // 
            UserPasswrd_Inpt.BackColor = Color.Black;
            UserPasswrd_Inpt.ForeColor = Color.Gold;
            UserPasswrd_Inpt.Location = new Point(6, 119);
            UserPasswrd_Inpt.Name = "UserPasswrd_Inpt";
            UserPasswrd_Inpt.Size = new Size(556, 32);
            UserPasswrd_Inpt.TabIndex = 3;
            UserPasswrd_Inpt.UseSystemPasswordChar = true;
            // 
            // Pass_Word
            // 
            Pass_Word.AutoSize = true;
            Pass_Word.Location = new Point(6, 91);
            Pass_Word.Name = "Pass_Word";
            Pass_Word.Size = new Size(98, 25);
            Pass_Word.TabIndex = 2;
            Pass_Word.Text = "Password";
            // 
            // Userinpt_Text
            // 
            Userinpt_Text.BackColor = Color.Black;
            Userinpt_Text.ForeColor = Color.Gold;
            Userinpt_Text.Location = new Point(6, 56);
            Userinpt_Text.Name = "Userinpt_Text";
            Userinpt_Text.Size = new Size(556, 32);
            Userinpt_Text.TabIndex = 1;
            // 
            // User_Name
            // 
            User_Name.AutoSize = true;
            User_Name.Location = new Point(6, 28);
            User_Name.Name = "User_Name";
            User_Name.Size = new Size(100, 25);
            User_Name.TabIndex = 0;
            User_Name.Text = "Username";
            // 
            // FileProtector
            // 
            AcceptButton = Login_Btn;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(591, 311);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FileProtector";
            Text = "File Protector";
            Load += File_Protector_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox UserPasswrd_Inpt;
        private Label Pass_Word;
        private TextBox Userinpt_Text;
        private Label User_Name;
        private Button Reg_Btn;
        private Button Login_Btn;
        private CheckBox unmaskPass;
        private CheckBox saveLoginCheckBox;
    }
}