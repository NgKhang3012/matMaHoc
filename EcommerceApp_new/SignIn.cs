using MongoDataAccess.Models;
using MongoDB.Driver;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Person;

public class SignIn : Form
{
    private IContainer components = null;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private TextBox uname;
    private TextBox password;
    private Button SignIn_button;

    private static string SHA3(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        // Choose the desired SHA-3 variant (e.g., SHA3Digest.Sha3_224, Sha3_256, Sha3_384, Sha3_512)
        Sha3Digest sha3 = new Sha3Digest(512);

        sha3.BlockUpdate(inputBytes, 0, inputBytes.Length);

        byte[] hashBytes = new byte[sha3.GetDigestSize()];
        sha3.DoFinal(hashBytes, 0);

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    public SignIn()
    {
        InitializeComponent();
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
        SignIn_button = new Button();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        uname = new TextBox();
        password = new TextBox();
        SuspendLayout();
        // 
        // SignIn_button
        // 
        SignIn_button.BackColor = Color.Azure;
        SignIn_button.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        SignIn_button.Location = new Point(261, 213);
        SignIn_button.Margin = new Padding(3, 4, 3, 4);
        SignIn_button.Name = "SignIn_button";
        SignIn_button.Size = new Size(250, 100);
        SignIn_button.TabIndex = 3;
        SignIn_button.Text = "Sign In";
        SignIn_button.UseVisualStyleBackColor = false;
        SignIn_button.Click += SignIn_button_Click;
        // 
        // label2
        // 
        label2.Font = new Font("Calibri", 18F, FontStyle.Bold, GraphicsUnit.Point);
        label2.ForeColor = Color.Blue; 
        label2.Location = new Point(48, 42);
        label2.Name = "label2";
        label2.Size = new Size(240, 50);
        label2.TabIndex = 5;
        label2.Text = "User Name";
        label2.Click += label2_Click;

        // 
        // label3
        // 
        label3.Font = new Font("Calibri", 18F, FontStyle.Bold, GraphicsUnit.Point);
        label3.Location = new Point(48, 132);
        label3.Name = "label3";
        label3.Size = new Size(170, 37);
        label3.TabIndex = 6;
        label3.Text = "Password";
        label3.Click += label3_Click;
        label3.ForeColor = Color.Blue;
        // 
        // uname
        // 
        uname.Location = new Point(261, 52);
        uname.Name = "uname";
        uname.Size = new Size(250, 27);
        uname.TabIndex = 9;
        uname.TextChanged += uname_TextChanged;
        // 
        // password
        // 
        password.Location = new Point(261, 142);
        password.Name = "password";
        password.PasswordChar = '*';
        password.Size = new Size(250, 27);
        password.TabIndex = 10;
        password.TextChanged += password_TextChanged;
        // 
        // SignIn
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(677, 404);
        Controls.Add(password);
        Controls.Add(uname);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(SignIn_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "SignIn";
        Text = "SignIn";
        Load += SignIn_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    private void SignIn_Load(object sender, EventArgs e)
    {

    }

    private void SignIn_button_Click(object sender, EventArgs e)
    {
        string connectionString = "mongodb+srv://Khang3012:FBHgzHfehN3okF7T@database-key.nwritex.mongodb.net/";
        string databaseName = "User_inFormation";
        string collectionName = "infor";

        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        var collection = db.GetCollection<PersonModel>(collectionName);

        string Uname = uname.Text;
        string Password = password.Text;

        var filter = Builders<PersonModel>.Filter.Eq("UserName", SHA3(Uname)) & Builders<PersonModel>.Filter.Eq("password", SHA3(Password));

        var user = collection.Find(filter).FirstOrDefault();

        if (user != null)
        {
            MessageBox.Show("User found. Authentication successful.");
            Option option = new Option();
            option.filename = SHA3(uname.Text);
            option.ShowDialog();
        }
        else
        {
            MessageBox.Show("User not found or incorrect password. Authentication failed.");
        }
    }

    private void Message_Click(object sender, EventArgs e)
    {

    }

    private void password_TextChanged(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label3_Click(object sender, EventArgs e)
    {

    }

    private void uname_TextChanged(object sender, EventArgs e)
    {

    }
}
