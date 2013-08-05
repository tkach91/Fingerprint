using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Drawing;
using DBInteraction;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для ScanWindows.xaml
    /// </summary>
    public partial class ScanWindow : Window, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;
        delegate void Function();

        private DPFP.Processing.Enrollment Enroller;
        public delegate void OnTemplateEventHandler(DPFP.Template template);
        public event OnTemplateEventHandler OnTemplate;

        public int counter;

        private int eid, mid, fn;

        public ScanWindow(int ei, int mi)
        {
            InitializeComponent();
            Init();
            //btnAccept.IsEnabled = false;
            counter = 0;
            Enroller = new DPFP.Processing.Enrollment();
            eid = ei;
            mid = mi;
        }

        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();

                if (null != Capturer)
                    Capturer.EventHandler = this;
                else
                    SetPrompt("Не удалось провести сканирование!");
            }
            catch
            {
                SetPrompt("Не удалось провести сканирование!");
            }
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    SetPrompt("Используя сканер отпечатков пальцев отсканируйте палец.");
                    Capturer.StartCapture();
                }
                catch
                {
                    SetPrompt("Не удалось провести сканирование!");
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                    counter = 0;
                }
                catch
                {
                    SetPrompt("Не удалось остановить сканирование!");
                    counter = 0;
                }
            }
        }

        private string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void insertIntoDB(byte[] arr, int fing_num, int emp_id, int metr_id)
        {
            DBNetBroker DBNB = new DBNetBroker("localhost", "", "", "fingerdata");
            DBHandler DBH = new DBHandler(DBNB);

            string str = ByteArrayToString(arr);

            string query = "INSERT INTO finger (FNumber, MetricsId, EmployeeID, FPrint) VALUES(" + "'" + fing_num + "'," + "'" + metr_id + "'," + "'" + emp_id + "'," + "'" + str + "')";

            DBH.executeNQnoData(query);
        }

        protected void Process(DPFP.Sample Sample)
        {
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if (features != null) 
                try
                {
                    SetPrompt("Отпечаток получен. Повторите сканирование (до 4 раз).");
                    Enroller.AddFeatures(features);
                    counter++;
                    PB1.Image = new Bitmap(ConvertSampleToBitmap(Sample), PB1.Size);
                }
                finally
                {
                    switch (Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:
                            SetPrompt("Процесс сканирования завершен.");
                            Stop();
                            byte[] btarr = null;
                            Enroller.Template.Serialize(ref btarr);

                            insertIntoDB(btarr, fn, eid, mid);

                            //Application.Current.Properties.Add("template", Enroller.Template);

                            break;

                        case DPFP.Processing.Enrollment.Status.Failed:
                            Enroller.Clear();
                            Stop();
                            OnTemplate(null);
                            Start();
                            break;
                    }
                }
        }

        #region EventHandler Members
        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            SetPrompt("Образец отпечатка создан.");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            SetPrompt("Палец снят со сканера.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            SetPrompt("Палец установлен на сканер.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            SetPrompt("Сканер отпечатков подключен.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            SetPrompt("Сканер отпечатков отключен.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                SetPrompt("Получен образец в хорошем качестве.");
            else
                SetPrompt("Получен образец в плохом качестве.");
        }
        #endregion

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();	// Create a sample convertor.
            Bitmap bitmap = null;												            // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);									// TODO: return bitmap as a result
            return bitmap;
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton1.IsChecked == true)
                fn = 1;
            if (radioButton2.IsChecked == true)
                fn = 2;
            if (radioButton3.IsChecked == true)
                fn = 3;
            if (radioButton4.IsChecked == true)
                fn = 4;
            if (radioButton5.IsChecked == true)
                fn = 5;
            if (radioButton6.IsChecked == true)
                fn = 6;
            if (radioButton7.IsChecked == true)
                fn = 7;
            if (radioButton8.IsChecked == true)
                fn = 8;
            if (radioButton9.IsChecked == true)
                fn = 9;
            if (radioButton10.IsChecked == true)
                fn = 10;
            Start();
        }

        private void SetPrompt(string Text)
        {
            this.Dispatcher.Invoke(new Function(delegate()
            {
                textBox1.Text = Text;
            }));
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            PB1.Image.Save("tmp.bmp");
            this.Close();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            this.Close();
        }
    }
}
