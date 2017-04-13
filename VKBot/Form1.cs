using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

//using xNet;
using System.Text.RegularExpressions;

namespace VKBot
{
    public partial class VKBot : Form
    {
        string client_id; // для адреса аккаунта, с которым мы работаем

        List<string> user_id = new List<string>();

        string access_token; // для ключа доступа к аккаунту

        string url; // для отправки GET-запроса серверу и получения ответа сервера

        string response; // для записи ответа сервера

        WebClient client; // объект, который отправляет и принимает запросы из url

        public int BreakTime;

        public int Rand;

        public int User_index = 0;

        public string Complete_text;

        public bool ReadyToEdit;

        string[] TextBlocks;

        string CurSubstr;

        TextProcessing tp = new TextProcessing();
        public VKBot()
        {
            InitializeComponent();
        }

        public void VKBot_Load(object sender, EventArgs e)
        {
            client = new WebClient();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox6.Text = "Timer";
            if (BreakTime < Rand*10)
            {
                BreakTime++;
                textBox6.Text = BreakTime.ToString() + " index = " + User_index.ToString();
            }
            else
            {
                timer1.Stop();
                User_index++;
                Random_Time();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (client_id_text.Text == "")
            {
                client_id_text.Text = "Enter Client ID";
            }
            else client_id = client_id_text.Text;
            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=5736991&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.52");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            access_token = Regex.Match(webBrowser1.Url.AbsoluteUri, "(?<=access_token=)[\\da-z]+").ToString();
            client_id = Regex.Match(webBrowser1.Url.AbsoluteUri, "(?<=user_id=)\\d+").ToString();

            if (access_token == "")
                throw new Exception("Пустой токен доступа.");
            Console.WriteLine("Done");
            client_id_text.Text = client_id;
            textBox_access_token.Text = access_token;

            HtmlElement auth = webBrowser1.Document.GetElementById("install_allow");
            auth.InvokeMember("click");
        }

        public void Auth()
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (!timer1.Enabled)
            {
                textBox6.Text = "Disabled";
                Random_Time();
            }
            else
            {
                textBox6.Text = "Enabled";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                textBox6.Text = "Timer is already disabled";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text)){
                string[] s = textBox1.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < s.Length; i++)
                {
                    for (int j = 0; j < s[i].Length; j++)
                    {
                        if (s[i][j] == 'i' && s[i][j + 1] == 'd')
                        {
                            user_id.Add(s[i].Substring(j + 2, s[i].Length - (j + 2)));
                        }
                        //textBox6.Text = user_id[i];
                    }
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            //textBox6.Text = count.ToString();
        }

        

        public void Random_Time()
        {
            Random rnd = new Random();
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text))
            {
                Rand = rnd.Next(Int32.Parse(textBox4.Text), Int32.Parse(textBox5.Text));
            }
            else
            {
                Rand = rnd.Next(0, 45);
            }
            BreakTime = 0;
            timer1.Start();
            //textBox6.Text = "Here";
        }

        public void SendMessage()
        {
            webBrowser1.Navigate(string.Format("https://api.vk.com/method/messages.send?user_id={0}&message=Test&v=5.60&access_token={1}",user_id[User_index], access_token));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Complete_text = textBox2.Text;
            Complete_text = tp.GetInnerString(textBox2.Text);
            textBox6.Text = Complete_text;
        }
        
        //https://api.vk.com/method/messages.send?user_id=150583345&message=Test&v=5.60&access_token=bbc90e88913476b669215df4029f502a88eec1d17c3fab162ede7668d1b1d5f1eaa57de9dc45b1d5f5162
    }
}
