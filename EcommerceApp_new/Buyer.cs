using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Buyer : Form
{
    private Button Commodities_button;
    private IContainer components = null;
    public string filename;
    public Buyer()
    {
        InitializeComponent();
    }

    private void Commodities_button_Click(object sender, EventArgs e)
    {
        Commodities_info info = new Commodities_info();
        info.filename = filename;
        info.Show();
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
        Commodities_button = new Button();
        SuspendLayout();
        // 
        // Commodities_button
        // 
        Commodities_button.BackColor = Color.Azure;
        Commodities_button.Font = new Font("Calibri", 18F, FontStyle.Bold, GraphicsUnit.Point);
        Commodities_button.Location = new Point(188, 121);
        Commodities_button.Margin = new Padding(3, 4, 3, 4);
        Commodities_button.Name = "Commodities_button";
        Commodities_button.Size = new Size(296, 143);
        Commodities_button.TabIndex = 2;
        Commodities_button.Text = "Commodities 1";
        Commodities_button.UseVisualStyleBackColor = false;
        Commodities_button.Click += Commodities_button_Click;
        // 
        // Buyer
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(677, 404);
        Controls.Add(Commodities_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "Buyer";
        Text = "Danh sách hàng hóa";
        ResumeLayout(false);
    }
}
