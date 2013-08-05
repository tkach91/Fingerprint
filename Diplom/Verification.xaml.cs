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
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using DBInteraction;
using System.IO;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для Verification.xaml
    /// </summary>
    public partial class Verification : Window, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;
        delegate void Function();

        private DPFP.Verification.Verification Verificator;
        private DPFP.Template Template;

        int uid;

        bool isVerified;

        MySqlDataReader reader;

        public Verification()
        {
            InitializeComponent();

            Init();

            isVerified = false;

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

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        protected void Process(DPFP.Sample Sample)
        {
            while (reader.Read())
            {
                string str = (string)reader.GetValue(5);
                byte[] btarr = StringToByteArray(str);
                MemoryStream strm = new MemoryStream(btarr);
                Template = new DPFP.Template(strm);
                Process(Sample);

                DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

                if (features != null)
                {
                    DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                    Verificator.Verify(features, Template, ref result);
                    //UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                    {
                        SetPrompt("Отпечатки совпадают");
                        isVerified = true;
                        break;
                    }
                    else
                    {
                        SetPrompt("Отпечатки не совпадают");
                        isVerified = false;
                    }
                }
            }

            if (isVerified != true)
            {
                SetPrompt("Отпечаток не принадлежит зарегистрированному пользователю");
            }

            Stop();
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

        /*private void SetPrompt2(string Text)
        {
            this.Dispatcher.Invoke(new Function(delegate()
            {
                textBox3.Text = Text;
            }));
        }*/

        /*private void UpdateStatus(int FAR)
        {
            // Показываем значение FAR
            SetPrompt2(String.Format("{0}", FAR));
        }*/

        private MySqlDataReader GetData(int id)
        {
            DBNetBroker DBB = new DBNetBroker("localhost", "", "", "fingerdata");
            DBB.Connection.Open();

            string query = "SELECT * FROM finger WHERE finger.EmployeeID = " + "'" + id + "'";

            MySqlCommand msc = new MySqlCommand(query, DBB.Connection);

            return msc.ExecuteReader();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            uid = Convert.ToInt32(textBox2.Text);
            reader = GetData(uid);

            Start();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            if (isVerified == true)
            {
                PersonPage PP = new PersonPage(uid);
                PP.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Сначала проведите сканирование пальца");
            }
        }
    }
}
