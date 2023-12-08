using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
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
            var logFile = new StreamWriter(destDir + "\\log.txt", true);
            DateTime currentTime = DateTime.Now;
            while (true)
            {
                try
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

                    Thread.Sleep(seconds * 1000);
                }
                catch(Exception e)
                {
                    logFile.WriteLine(e.ToString());
                    logFile.Flush();
                    if(e.GetType() == new ExternalException().GetType() ||
                        e.GetType() == new IOException().GetType() ||
                        e.GetType() == new EncoderFallbackException().GetType())
                    {
                        break;
                    }
                }
               
                

            }
        }
        static void Main(string[] args)
        {
            if (args.Length < 2) 
            {
                Console.WriteLine("not enought inpit arguments");
            }
            try
            {
                GetScreenShot(Convert.ToInt32(args[0]), args[1]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Thread.Sleep(12 * 1000);
            }
               
            
            
        }
    }
}
