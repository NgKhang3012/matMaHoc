using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Choose_SU_SI : Form
{
    private IContainer components = null;
    private Button SignUp_button;
    private Button SignIn_button;

    public Choose_SU_SI()
    {
        InitializeComponent();
    }

    private void SignUp_button_Click(object sender, EventArgs e)
    {
        SignUp signupform = new SignUp();
        signupform.ShowDialog();
    }
    private void SignIn_button_Click(object sender, EventArgs e)
    {
        SignIn signinform = new SignIn();
        signinform.ShowDialog();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        SignUp_button = new Button();
        SignIn_button = new Button();
        SuspendLayout();
        // 
        // SignUp_button
        // 
        SignUp_button.BackColor = Color.Azure;
        SignUp_button.Font = new Font("Calibri", 14F, FontStyle.Bold, GraphicsUnit.Point);
        SignUp_button.Location = new Point(50, 99);
        SignUp_button.Margin = new Padding(3, 4, 3, 4);
        SignUp_button.Name = "SignUp_button";
        SignUp_button.Size = new Size(250, 100);
        SignUp_button.TabIndex = 2;
        SignUp_button.Text = "Sign Up";
        SignUp_button.UseVisualStyleBackColor = false;
        SignUp_button.Click += SignUp_button_Click;
        // 
        // SignIn_button
        // 
        SignIn_button.BackColor = Color.Azure;
        SignIn_button.Font = new Font("Calibri", 14F, FontStyle.Bold, GraphicsUnit.Point);
        SignIn_button.Location = new Point(350, 99);
        SignIn_button.Margin = new Padding(3, 4, 3, 4);
        SignIn_button.Name = "SignIn_button";
        SignIn_button.Size = new Size(250, 100);
        SignIn_button.TabIndex = 3;
        SignIn_button.Text = "Sign In";
        SignIn_button.UseVisualStyleBackColor = false;
        SignIn_button.Click += SignIn_button_Click;
        // 
        // Choose_SU_SI
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(677, 404);
        Controls.Add(SignUp_button);
        Controls.Add(SignIn_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "Choose_SU_SI";
        Text = "ChooseOption";
        Load += Choose_SU_SI_Load;
        ResumeLayout(false);
    }

    private void Message_Click(object sender, EventArgs e)
    {

    }

    private void Choose_SU_SI_Load(object sender, EventArgs e)
    {

    }
}

