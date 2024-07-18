using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using MongoDataAccess.DataAccess;
using MongoDB.Driver;
using MongoDataAccess.Models;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.IO;
using FM;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using System.Text;
public class Commodities_info : Form
{

    // =======================================================================================================

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
    private System.ComponentModel.IContainer components = null;

    private Button button1;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Label label3;

    public string filename;

    private OpenFileDialog openFileDialog1;

    private OpenFileDialog openFileDialog2;
    private TextBox textBox1;
    private System.Windows.Forms.Label label4;
    private Button button2;
    private Button button3;
    private System.Windows.Forms.Label label5;
    private TextBox textBox2;
    private SaveFileDialog savesignatureDialog;

    [DllImport("Sign.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "Sign")]
    private static extern int Sign(string filepath, string filepri, string filesig);

    public Commodities_info()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        string filenamepdf = filename + ".pdf";
        string filenamepri = textBox1.Text;
        string filenamepub = textBox2.Text;
        string filenamesig = filename + ".sig";
        string idSeller = SHA3("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");
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(17));

                page.Header()
                    .Text("Order Detail")
                    .SemiBold().FontSize(27).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(10);
                        x.Item().Text("Name Commodities: Commodities 1");
                        x.Item().Text("Price: 300USD");
                        x.Item().Text("Name buyer: User1");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                    });
            });
        })
        //.ShowInPreviewer();
        .GeneratePdf(filenamepdf);
        string filepath = filenamepdf;
        string filepri = filenamepri;
        string filesig = filenamesig;
        string filepub = filenamepub;
        if (Sign(filepath, filepri, filesig) != 0)
        {
            MessageBox.Show("Error, try again");
        }
        else
        {
            // gửi tới người bán
            string connectionString = "mongodb+srv://Khang3012:FBHgzHfehN3okF7T@database-key.nwritex.mongodb.net/";
            string databaseName = "DDH";
            string collectionName = "File";
            // code upfile
            MessageBox.Show("Sucessfully");
            // Read the PDF file into a byte array
            byte[] pdfBytes = File.ReadAllBytes(filenamepdf);
            string sigText = File.ReadAllText(filesig);
            string pubText = File.ReadAllText(filepub);
            // Create MongoClient and connect to MongoDB
            var client = new MongoClient(connectionString);
            // Access a specific database
            IMongoDatabase database = client.GetDatabase(databaseName);
            // Access a specific collection (create if not exists)
            IMongoCollection<FModel> collection = database.GetCollection<FModel>(collectionName);
            // Create a document with the PDF data
            var file = new FModel
            {
                // Set the desired file name
                FilePdfName = filenamepdf,
                ContentPdf = pdfBytes,
                FileSigName = filesig,
                ContentSig = sigText,
                FilePubName = filename+"pub"+".pem",
                ContentPub = pubText,
                IdSeller = idSeller
            };
            collection.InsertOne(file);                                           // Insert the document into the collection
            Console.WriteLine("PDF file inserted successfully.");
        }
    }

    private void Commodities_Load(object sender, EventArgs e)
    {
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
        button1 = new Button();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        openFileDialog1 = new OpenFileDialog();
        openFileDialog2 = new OpenFileDialog();
        savesignatureDialog = new SaveFileDialog();
        textBox1 = new TextBox();
        label4 = new System.Windows.Forms.Label();
        button2 = new Button();
        button3 = new Button();
        label5 = new System.Windows.Forms.Label();
        textBox2 = new TextBox();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Font = new Font("Calibri", 20F, FontStyle.Bold, GraphicsUnit.Point);
        button1.ForeColor = Color.Blue;
        button1.Location = new Point(604, 55);
        button1.Margin = new Padding(3, 4, 3, 4);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(160, 111);
        button1.TabIndex = 3;
        button1.Text = "Purchase";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // label1
        // 
        label1.Font = new Font("Calibri", 20F, FontStyle.Bold, GraphicsUnit.Point);
        label1.ImageAlign = ContentAlignment.BottomLeft;
        label1.Location = new Point(35, 47);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(409, 60);
        label1.TabIndex = 4;
        label1.Text = "Name commodities:  Card";
        label1.TextAlign = ContentAlignment.BottomCenter;
        label1.Click += label1_Click;
        // 
        // label2
        // 
        label2.Font = new Font("Calibri", 20F, FontStyle.Bold, GraphicsUnit.Point);
        label2.Location = new Point(51, 112);
        label2.Name = "label2";
        label2.RightToLeft = RightToLeft.No;
        label2.Size = new System.Drawing.Size(445, 69);
        label2.TabIndex = 5;
        label2.Text = "Price                     : 300USD";
        label2.Click += label2_Click;
        // 
        // label3
        // 
        label3.Location = new Point(0, 0);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(100, 29);
        label3.TabIndex = 0;
        // 
        // textBox1
        // 
        textBox1.Location = new Point(260, 222);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(287, 33);
        textBox1.TabIndex = 6;
        textBox1.TextChanged += textBox1_TextChanged;
        // 
        // label4
        // 
        label4.Font = new Font("Calibri", 15F, FontStyle.Bold, GraphicsUnit.Point);
        label4.Location = new Point(51, 222);
        label4.Name = "label4";
        label4.RightToLeft = RightToLeft.No;
        label4.Size = new System.Drawing.Size(203, 33);
        label4.TabIndex = 7;
        label4.Text = "Private Key Path";
        label4.Click += label4_Click;
        // 
        // button2
        // 
        button2.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        button2.ForeColor = Color.Black;
        button2.Location = new Point(604, 222);
        button2.Margin = new Padding(3, 4, 3, 4);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(116, 33);
        button2.TabIndex = 8;
        button2.Text = "Browse";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // button3
        // 
        button3.Font = new Font("Calibri", 12F, FontStyle.Bold, GraphicsUnit.Point);
        button3.ForeColor = Color.Black;
        button3.Location = new Point(604, 301);
        button3.Margin = new Padding(3, 4, 3, 4);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(116, 33);
        button3.TabIndex = 11;
        button3.Text = "Browse";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // label5
        // 
        label5.Font = new Font("Calibri", 15F, FontStyle.Bold, GraphicsUnit.Point);
        label5.Location = new Point(51, 301);
        label5.Name = "label5";
        label5.RightToLeft = RightToLeft.No;
        label5.Size = new System.Drawing.Size(203, 33);
        label5.TabIndex = 10;
        label5.Text = "Public Key Path";
        label5.Click += label5_Click;
        // 
        // textBox2
        // 
        textBox2.Location = new Point(260, 301);
        textBox2.Multiline = true;
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(287, 33);
        textBox2.TabIndex = 9;
        textBox2.TextChanged += textBox2_TextChanged;
        // 
        // Commodities_info
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(916, 646);
        Controls.Add(button3);
        Controls.Add(label5);
        Controls.Add(textBox2);
        Controls.Add(button2);
        Controls.Add(label4);
        Controls.Add(textBox1);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(button1);
        Margin = new Padding(3, 4, 3, 4);
        Name = "Commodities_info";
        Text = "SignPDF";
        Load += Commodities_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label4_Click(object sender, EventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Select a file";
        openFileDialog.Filter = "All Files (.)|*.pem";
        DialogResult result = openFileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            textBox1.Text = openFileDialog.FileName;
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Select a file";
        openFileDialog.Filter = "All Files (.)|*.pem";
        DialogResult result = openFileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            textBox2.Text = openFileDialog.FileName;
        }
    }
}
