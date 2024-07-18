using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Option : Form
{
    private IContainer components = null;
    private Button Buyer_button;
    private Button Seller_button;
    public string filename;

    public Option()
    {
        InitializeComponent();
    }

    private void Buyer_button_Click(object sender, EventArgs e)
    {
        Buyer Buyerform = new Buyer();
        Buyerform.filename = filename;
        Buyerform.Show();
    }
    private void Seller_button_Click(object sender, EventArgs e)
    {
        Seller Sellerform = new Seller();
        Sellerform.filename = filename;
        Sellerform.Show();
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
        Buyer_button = new Button();
        Seller_button = new Button();
        SuspendLayout();
        // 
        // Buyer_button
        // 
        Buyer_button.BackColor = Color.Azure;
        Buyer_button.Font = new Font("Calibri", 18F, FontStyle.Bold, GraphicsUnit.Point);
        Buyer_button.Location = new Point(49, 95);
        Buyer_button.Margin = new Padding(3, 4, 3, 4);
        Buyer_button.Name = "Buyer_button";
        Buyer_button.Size = new Size(250, 100);
        Buyer_button.TabIndex = 2;
        Buyer_button.Text = "Buyer";
        Buyer_button.UseVisualStyleBackColor = false;
        Buyer_button.Click += Buyer_button_Click;

        // 
        // Seller_button
        // 
        Seller_button.BackColor = Color.Azure;
        Seller_button.Font = new Font("Calibri", 18F, FontStyle.Bold, GraphicsUnit.Point);
        Seller_button.Location = new Point(367, 95);
        Seller_button.Margin = new Padding(3, 4, 3, 4);
        Seller_button.Name = "Seller_button";
        Seller_button.Size = new Size(250, 100);
        Seller_button.TabIndex = 3;
        Seller_button.Text = "Seller";
        Seller_button.UseVisualStyleBackColor = false;
        Seller_button.Click += Seller_button_Click;

        // 
        // Option
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(677, 404);
        Controls.Add(Buyer_button);
        Controls.Add(Seller_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "Option";
        Text = "Option";
        ResumeLayout(false);
    }

    private void Message_Click(object sender, EventArgs e)
    {

    }
}

