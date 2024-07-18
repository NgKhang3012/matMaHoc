using MongoDB.Driver;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Konscious.Security.Cryptography;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Security.Cryptography;
using NSec.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using MongoDataAccess.Models;
using Person;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
public class SignUp : Form
{
    private IContainer components = null;
    private System.Windows.Forms.Label Message;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private TextBox fname;
    private TextBox lname;
    private TextBox uname;
    private TextBox password;
    private System.Windows.Forms.Label label4;
    private TextBox textBox1;
    private Button button1;
    private Button SignUp_button;
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
    static string RemoveBeginEndHeaders(string publicKey)
    {
        // Define the pattern to match the headers and footer
        string pattern = @"-----BEGIN PUBLIC KEY-----|-----END PUBLIC KEY-----";

        // Use Regex to replace the headers and footer with an empty string
        string cleanedKey = Regex.Replace(publicKey, pattern, "");
        cleanedKey=Regex.Replace(cleanedKey, @"\r\n?|\n", "");
        return cleanedKey;
    }

    public SignUp()
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
        SignUp_button = new Button();
        Message = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        fname = new TextBox();
        lname = new TextBox();
        uname = new TextBox();
        password = new TextBox();
        label4 = new System.Windows.Forms.Label();
        textBox1 = new TextBox();
        button1 = new Button();
        SuspendLayout();
        // 
        // SignUp_button
        // 
        SignUp_button.BackColor = Color.Azure;
        SignUp_button.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        SignUp_button.ForeColor = Color.Black;
        SignUp_button.Location = new Point(425, 341);
        SignUp_button.Margin = new Padding(3, 4, 3, 4);
        SignUp_button.Name = "SignUp_button";
        SignUp_button.Size = new Size(240, 50);
        SignUp_button.TabIndex = 3;
        SignUp_button.Text = "Sign Up";
        SignUp_button.UseVisualStyleBackColor = false;
        SignUp_button.Click += SignUp_button_Click;
        // 
        // Message
        // 
        Message.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        Message.ForeColor = Color.Blue;
        Message.Location = new Point(49, 19);
        Message.Name = "Message";
        Message.Size = new Size(154, 33);
        Message.TabIndex = 1;
        Message.Text = "First Name";
        Message.Click += Message_Click;
        // 
        // label1
        // 
        label1.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        label1.ForeColor = Color.Blue;
        label1.Location = new Point(49, 90);
        label1.Name = "label1";
        label1.Size = new Size(154, 33);
        label1.TabIndex = 4;
        label1.Text = "Last Name";
        // 
        // label2
        // 
        label2.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        label2.Location = new Point(49, 152);
        label2.Name = "label2";
        label2.Size = new Size(154, 33);
        label2.TabIndex = 5;
        label2.Text = "User Name";
        // 
        // label3
        // 
        label3.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        label3.Location = new Point(49, 216);
        label3.Name = "label3";
        label3.Size = new Size(240, 39);
        label3.TabIndex = 6;
        label3.Text = "Password";
        // 
        // fname
        // 
        fname.Location = new Point(222, 22);
        fname.Multiline = true;
        fname.Name = "fname";
        fname.Size = new Size(240, 30);
        fname.TabIndex = 7;
        fname.TextChanged += fname_TextChanged;
        // 
        // lname
        // 
        lname.Location = new Point(222, 90);
        lname.Multiline = true;
        lname.Name = "lname";
        lname.Size = new Size(240, 30);
        lname.TabIndex = 8;
        // 
        // uname
        // 
        uname.Location = new Point(222, 152);
        uname.Multiline = true;
        uname.Name = "uname";
        uname.Size = new Size(240, 30);
        uname.TabIndex = 9;
        // 
        // password
        // 
        password.Location = new Point(222, 216);
        password.Multiline = true;
        password.Name = "password";
        password.PasswordChar = '*';
        password.Size = new Size(240, 30);
        password.TabIndex = 10;
        password.TextChanged += password_TextChanged;
        // 
        // label4
        // 
        label4.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        label4.Location = new Point(49, 273);
        label4.Name = "label4";
        label4.Size = new Size(240, 39);
        label4.TabIndex = 11;
        label4.Text = "Public Key Path";
        label4.Click += label4_Click;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(222, 273);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(240, 30);
        textBox1.TabIndex = 12;
        textBox1.TextChanged += textBox1_TextChanged;
        // 
        // button1
        // 
        button1.Location = new Point(507, 275);
        button1.Name = "button1";
        button1.Size = new Size(90, 29);
        button1.TabIndex = 13;
        button1.Text = "Browse";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // SignUp
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(677, 404);
        Controls.Add(button1);
        Controls.Add(textBox1);
        Controls.Add(label4);
        Controls.Add(password);
        Controls.Add(uname);
        Controls.Add(lname);
        Controls.Add(fname);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(Message);
        Controls.Add(SignUp_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "SignUp";
        Text = "SignUp";
        Load += SignUp_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    private void SignUp_Load(object sender, EventArgs e)
    {

    }

    private async void SignUp_button_Click(object sender, EventArgs e)
    {
        string Fname = fname.Text;
        string Lname = lname.Text;
        string Uname = uname.Text;
        string Password = password.Text;
        string connectionString = "mongodb+srv://Khang3012:FBHgzHfehN3okF7T@database-key.nwritex.mongodb.net/";
        var client = new MongoClient(connectionString);
        string databaseName = "User_inFormation";
        string collectionName = "infor";
        var db = client.GetDatabase(databaseName);
        var collection = db.GetCollection<PersonModel>(collectionName);

        var filter = Builders<PersonModel>.Filter.Eq("UserName", SHA3(Uname));
        var user = collection.Find(filter).FirstOrDefault();
        if (user != null)
        {
            MessageBox.Show("User name is exist");
        }
        else
        {
            string filepub = textBox1.Text;
            string pk = File.ReadAllText(filepub);
            var person = new PersonModel { Id = SHA3(RemoveBeginEndHeaders(pk)), FirstName = Fname, LastName = Lname, UserName = SHA3(uname.Text), password = SHA3(password.Text),pubkeyname= SHA3(uname.Text)+"pub.pem", pubkeycontent=pk };

            await collection.InsertOneAsync(person);

            var results = await collection.FindAsync(_ => true);

            Option option = new Option();
            MessageBox.Show("Sign Up Sucessfully");
            option.filename = SHA3(uname.Text);
            option.ShowDialog();
        }
    }

    private void Message_Click(object sender, EventArgs e)
    {

    }

    private void password_TextChanged(object sender, EventArgs e)
    {

    }

    private void fname_TextChanged(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Select a file";
        openFileDialog.Filter = "Key Files (.)|*.pem";
        DialogResult result = openFileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            textBox1.Text = openFileDialog.FileName;
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }
}
