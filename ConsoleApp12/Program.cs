using Microsoft.JScript;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convert = System.Convert;

namespace ConsoleApp12
{
    internal class Program
    {
        public static void DeletScreenShot(int weeks, string path, StreamWriter logfile)
        {
            var directoryInfo = new DirectoryInfo(path);
            List<FileInfo> fileInfos = directoryInfo.GetFiles().OrderBy(f => f.Name).ToList();
            foreach(var fileinfo in fileInfos)
            {
                string imagePath = fileinfo.Name.Replace(".png", "");
                try
                {
                    DateTime datePath = DateTime.ParseExact(imagePath, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
                    if ((DateTime.Now - datePath).Days >= weeks * 7)
                    {
                        File.Delete(path + "\\" + fileinfo.Name);
                        Console.WriteLine(path + "\\" + fileinfo.Name + " deleted");
                        logfile.WriteLine(path + "\\" + fileinfo.Name + " deleted");

                    }
                }
                catch(Exception e) 
                { 
                    logfile.WriteLine(e.ToString());

                    break;
                }
            }
           

        }
      
        public static void GetScreenShot(int seconds, string destDir, int weeks)
        {
            var path = destDir + "\\images";
            Directory.CreateDirectory(path);
            using (var logFile = new StreamWriter(destDir + "\\log.txt", true))
            {
                
                while (true)
                {
                    try
                    {
                        DeletScreenShot(weeks, path, logFile);
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
                       

                        Thread.Sleep(seconds * 1000);
                    }
                    catch (Exception e)
                    {
                        logFile.WriteLine(e.ToString());
                        if (e.GetType() == new ExternalException().GetType() ||
                            e.GetType() == new IOException().GetType() ||
                            e.GetType() == new EncoderFallbackException().GetType())
                        {
                            break;
                        }
                    }

                }

            }

          
        }
        static void Main(string[] args)
        {
            if (args.Length < 3) 
            {
                Console.WriteLine("not enought inpit arguments");
                return;
            }
            try
            {
                GetScreenShot(Convert.ToInt32(args[0]), args[1], Convert.ToInt32(args[2]));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Thread.Sleep(12 * 1000);
            }
                
            
        }
    }
}
