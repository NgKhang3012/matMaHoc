using System;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(defaultValue: false);
        System.Windows.Forms.Application.Run(new Choose_SU_SI());
    }
}
