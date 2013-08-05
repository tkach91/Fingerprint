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

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для CompareWindow.xaml
    /// </summary>
    public partial class CompareWindow : Window, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;
        delegate void Function();

        private DPFP.Verification.Verification Verificator;

        public CompareWindow()
        {
            InitializeComponent();

            Init();

            Verificator = new DPFP.Verification.Verification();		// Create a fingerprint template verificator
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

        protected void Process(DPFP.Sample Sample)
        {
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            if (features != null)
            {
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                Verificator.Verify(features, (DPFP.Template)Application.Current.Properties["template"], ref result);
                if (result.Verified)
                    SetPrompt("Отпечатки совпадают");
                else
                    SetPrompt("Отпечатки не совпадают");
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
                }
                catch
                {
                    SetPrompt("Не удалось остановить сканирование!");
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

        private void SetPrompt(string Text)
        {
            this.Dispatcher.Invoke(new Function(delegate()
            {
                textBox1.Text = Text;
            }));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }
    }
}
