using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для IMPProcessingWindow.xaml
    /// </summary>
    public partial class IMPProcessingWindow : Window
    {
        Thread gThread;
        Thread bThread;
        Thread sThread;
        Thread aThread;
        Thread tThread;

        public IMPProcessingWindow()
        {
            InitializeComponent();
            bool deleteComplete = false;

            /*
            do
            {
                try
                {
                    File.Delete(ImageProc.IMP_TMP_ALLMIN);
                    File.Delete(ImageProc.IMP_TMP_BINAR);
                    File.Delete(ImageProc.IMP_TMP_GABOR);
                    File.Delete(ImageProc.IMP_TMP_SCELET);
                    File.Delete(ImageProc.IMP_TMP_TRUEMIN);
                    deleteComplete = true;
                }
                catch (Exception exc)
                {
                    deleteComplete = false;
                }
            } while(!deleteComplete);
            */

            /*gThread = new Thread(monitorGabor);
            bThread = new Thread(monitorBinar);
            sThread = new Thread(monitorScelet);
            aThread = new Thread(monitorAllmin);
            tThread = new Thread(monitorTruemin);

            gThread.Start(pB1);
            bThread.Start(pB2);
            sThread.Start(pB3);
            aThread.Start(pB4);
            tThread.Start(pB5);*/
        }

        /*private void monitorGabor(object o)
        {
            DateTime dt = DateTime.Now;
            PictureBox pbx = (PictureBox)o;
            while (!(File.Exists(ImageProc.IMP_TMP_GABOR) && (File.GetLastWriteTime(ImageProc.IMP_TMP_GABOR) > dt)))
                Thread.Sleep(500);
            Thread.Sleep(100);

            try
            {
                FileStream fs = new FileStream(ImageProc.IMP_TMP_GABOR, FileMode.Open, FileAccess.Read);
                pbx.Image = new Bitmap(fs);
                fs.Close();
                //File.Delete(ImageProc.IMP_TMP_GABOR);
            }
            catch (Exception exc) { }
        }

        private void monitorBinar(object o)
        {
            DateTime dt = DateTime.Now;
            PictureBox pbx = (PictureBox)o;
            while (!(File.Exists(ImageProc.IMP_TMP_BINAR) && (File.GetLastWriteTime(ImageProc.IMP_TMP_BINAR) > dt)))
                Thread.Sleep(500);
            Thread.Sleep(100);
            try
            {
                FileStream fs = new FileStream(ImageProc.IMP_TMP_BINAR, FileMode.Open, FileAccess.Read);
                pbx.Image = new Bitmap(fs);
                fs.Close();
                //pbx.Image = new Bitmap(ImageProc.IMP_TMP_BINAR);
                //File.Delete(ImageProc.IMP_TMP_BINAR);
            }
            catch (Exception exc) { }
        }

        private void monitorScelet(object o)
        {
            DateTime dt = DateTime.Now;
            PictureBox pbx = (PictureBox)o;
            while (!(File.Exists(ImageProc.IMP_TMP_SCELET) && (File.GetLastWriteTime(ImageProc.IMP_TMP_SCELET) > dt)))
                Thread.Sleep(500);
            Thread.Sleep(100);
            try
            {
                FileStream fs = new FileStream(ImageProc.IMP_TMP_SCELET, FileMode.Open, FileAccess.Read);
                pbx.Image = new Bitmap(fs);
                fs.Close();
                //pbx.Image = new Bitmap(ImageProc.IMP_TMP_SCELET);
                //File.Delete(ImageProc.IMP_TMP_SCELET);
            }
            catch (Exception exc) { }
        }

        private void monitorAllmin(object o)
        {
            DateTime dt = DateTime.Now;
            PictureBox pbx = (PictureBox)o;
            while (!(File.Exists(ImageProc.IMP_TMP_ALLMIN) && (File.GetLastWriteTime(ImageProc.IMP_TMP_ALLMIN) > dt)))
                Thread.Sleep(500);
            Thread.Sleep(100);
            try
            {
                FileStream fs = new FileStream(ImageProc.IMP_TMP_ALLMIN, FileMode.Open, FileAccess.Read);
                pbx.Image = new Bitmap(fs);
                fs.Close();
                //pbx.Image = new Bitmap(ImageProc.IMP_TMP_ALLMIN);
                //File.Delete(ImageProc.IMP_TMP_ALLMIN);
            }
            catch (Exception exc) { }
        }

        private void monitorTruemin(object o)
        {
            DateTime dt = DateTime.Now;
            PictureBox pbx = (PictureBox)o;
            while (!(File.Exists(ImageProc.IMP_TMP_TRUEMIN) && (File.GetLastWriteTime(ImageProc.IMP_TMP_TRUEMIN) > dt)))
                Thread.Sleep(500);
            Thread.Sleep(100);
            try
            {
                FileStream fs = new FileStream(ImageProc.IMP_TMP_TRUEMIN, FileMode.Open, FileAccess.Read);
                pbx.Image = new Bitmap(fs);
                fs.Close();
                //pbx.Image = new Bitmap(ImageProc.IMP_TMP_TRUEMIN);
                //File.Delete(ImageProc.IMP_TMP_TRUEMIN);
            }
            catch (Exception exc) { }
        }*/
    }
}
