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
using FM;
using System.Printing;
using System.Text.RegularExpressions;
public class Seller : Form
{
    private IContainer components = null;
    private Button Order_details_button;
    [DllImport("Verify.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "Verify")]
    private static extern int Verify(string filepath, string filesig, string filepub);
    public string filename;
    static string RemoveBeginEndHeaders(string publicKey)
    {
        // Define the pattern to match the headers and footer
        string pattern = @"-----BEGIN PUBLIC KEY-----|-----END PUBLIC KEY-----";

        // Use Regex to replace the headers and footer with an empty string
        string cleanedKey = Regex.Replace(publicKey, pattern, "");
        cleanedKey = Regex.Replace(cleanedKey, @"\r\n?|\n", "");
        return cleanedKey;
    }
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
    public Seller()
    {
        InitializeComponent();
    }

    private void Order_details_button_Click(object sender, EventArgs e)
    {
        // load file từ database(file order,file sig, file pubkey)
        string connectionString = "mongodb+srv://Khang3012:FBHgzHfehN3okF7T@database-key.nwritex.mongodb.net/";
        var client = new MongoClient(connectionString);
        string databaseName = "User_inFormation";
        string collectionName = "infor";
        var db = client.GetDatabase(databaseName);
        var collection2 = db.GetCollection<PersonModel>(collectionName);
        var filter2 = Builders<PersonModel>.Filter.Eq("UserName",filename);
        var user1 = collection2.Find(filter2).FirstOrDefault();
        string idseller = user1.Id;

        databaseName = "DDH";
        collectionName = "File";
        db = client.GetDatabase(databaseName);
        var collection = db.GetCollection<FModel>(collectionName);

        var filter = Builders<FModel>.Filter.Eq("IdSeller", idseller);
        var filedown = collection.Find(filter).FirstOrDefault();
        
        if (filedown != null)
        {
            string folder = "";
            string filepathdown = filedown.FilePdfName;
            string filesigdown = filedown.FileSigName;
            string filepubdown = filedown.FilePubName;


            string filepath = folder + filepathdown;
            string filesig = folder + filesigdown;
            string filepub = folder + filepubdown;
            File.WriteAllBytes(filepath, filedown.ContentPdf);
            File.WriteAllText(filesig, filedown.ContentSig);
            File.WriteAllText(filepub, filedown.ContentPub);
            if (Verify(filepath, filesig, filepub) == 0)
            {
                //MessageBox.Show("Sucessfully");
                // shake256(pubkey) và trên database rồi out tên người gửi

                string databaseName1 = "User_inFormation";
                string collectionName1 = "infor";
                var db1 = client.GetDatabase(databaseName1);
                var collection1 = db1.GetCollection<PersonModel>(collectionName1);
                var filter1 = Builders<PersonModel>.Filter.Eq("_id", SHA3(RemoveBeginEndHeaders(filedown.ContentPub)));
                var user = collection1.Find(filter1).FirstOrDefault();
                if (user != null)
                {
                    string Userfullname = user.FirstName + " " + user.LastName;
                    MessageBox.Show("Sucessfully Verify the order of " + Userfullname);
                }
                else
                {
                    MessageBox.Show("Fail, no user has this id");
                }
            }
            else
            {
                MessageBox.Show("Fail verify");
            }
        }
        else
        {
            MessageBox.Show("This item is not exits");
        }

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
        Order_details_button = new Button();
        SuspendLayout();
        // 
        // Order_details_button
        // 
        Order_details_button.BackColor = Color.Azure;
        Order_details_button.Font = new Font("Calibri", 20F, FontStyle.Bold, GraphicsUnit.Point);
        Order_details_button.Location = new Point(186, 125);
        Order_details_button.Margin = new Padding(3, 4, 3, 4);
        Order_details_button.Name = "Order_details_button";
        Order_details_button.Size = new Size(308, 171);
        Order_details_button.TabIndex = 2;
        Order_details_button.Text = "Verify Order Detail";
        Order_details_button.UseVisualStyleBackColor = false;
        Order_details_button.Click += Order_details_button_Click;
        // 
        // Seller
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(677, 404);
        Controls.Add(Order_details_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "Seller";
        Text = "Verify";
        Load += Seller_Load;
        ResumeLayout(false);
    }

    private void Seller_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }
}