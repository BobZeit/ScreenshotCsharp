using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp12
{
    internal class Program
    {
        public static void GetScreenShot(int seconds, string destDir)
        {
            var path = destDir + "\\images";
            Directory.CreateDirectory(path);
            StreamWriter logFile = new StreamWriter(destDir + "\\log.txt", true);
            DateTime currentTime = DateTime.Now;
            while (true)
            {
                int screenLeft = SystemInformation.VirtualScreen.Left;
                int screenTop = SystemInformation.VirtualScreen.Top;
                int screenWidth = SystemInformation.VirtualScreen.Width;
                int screenHeight = SystemInformation.VirtualScreen.Height;
                var bitmap = new Bitmap(screenWidth, screenHeight);
                var graphic = Graphics.FromImage(bitmap);
                graphic.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap.Size);
                var docuName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                bitmap.Save(path + "\\" + docuName + ".png", ImageFormat.Png);
                Console.WriteLine(path + "\\" + docuName + ".png saved");
                logFile.WriteLine(path + "\\" + docuName + ".png saved");
                logFile.Flush();

                Thread.Sleep(seconds*1000);
                

            }
        }
        static void Main(string[] args)
        {
            
            
                GetScreenShot(Convert.ToInt32(args[0]), args[1]);
            
            
        }
    }
}
