using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKBot
{
    class TextProcessing
    {
        public int Rand;

        public int User_index = 0;

        public string Complete_text;

        public bool ReadyToEdit;

        string[] TextBlocks;

        string CurSubstr;
        public string GetInnerString(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                int count = str.Count(s => s == '{');
                if (count > 0)
                {
                    int i = 0;
                    int n = 0;

                    for (i = 0; str[i] != '{' && i < str.Length - 1; i++)
                    {
                        //    Complete_text += str[i];
                    }
                    //str.Remove(0, i);
                    int countB = 0;
                    for (n = i + 1; n < str.Length - 1; n++)
                    {
                        if (str[n] == '}' && countB == 0)
                        {
                            break;
                        }
                        if (str[n] == '{')
                        {
                            countB++;
                        }
                        else if (str[n] == '}')
                        {
                            countB--;
                        }

                    }
                    //Parse_Strig(str.Substring(i+1, n-i-1)); 
                    //textBox6.Text += str.Substring(i + 1, n - i - 1);
                    str = str.Replace(str.Substring(i, n - i + 1), Parse_Strig(str.Substring(i + 1, n - i - 1)));
                    if (str.Count(s => s == '{') > 0)
                    {
                        Complete_text = str;
                        GetInnerString(str);
                    }
                    else
                    {
                        Complete_text = str;
                        return Complete_text;
                    }
                    //Parse_Strig(str.Substring(i + 1, n - i - 1));
                    // if (str.Substring(n+1, str.Length - 2-n).Count(s=>s == '{')>0)
                    //{
                    //GetInnerString(str.Substring(n + 1, str.Length - 2 - n));
                    //}
                    //textBox6.Text = Complete_text;
                }
                else
                {
                    Complete_text = str;
                    //textBox6.Text = Complete_text;
                }
            }
            return Complete_text;
        }

        public string Parse_Strig(string str)
        {
            int counter = 0;
            int Bcounter = 0;
            int count = 0;
            //textBox6.Text = str;

            for (int i = 0; i <= str.Length - 1; i++)
            {
                if (str[i] == '{')
                {
                    Bcounter++;
                }
                if (str[i] == '}')
                {
                    Bcounter--;
                }
                if (str[i] == '|' && Bcounter == 0)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                TextBlocks = new string[count + 1];
                int bp = 0;

                //textBox6.Text = string.Empty;

                for (int i = 0; i <= str.Length - 1; i++)
                {
                    if (str[i] == '{')
                    {
                        Bcounter++;
                    }
                    if (str[i] == '}')
                    {
                        Bcounter--;
                    }
                    if ((str[i] == '|' || i == str.Length - 1) && Bcounter == 0)
                    {
                        if (i == str.Length - 1)
                        {
                            TextBlocks[counter] = str.Substring(bp, i - bp + 1);
                        }
                        else
                        {
                            TextBlocks[counter] = str.Substring(bp, i - bp);
                        }
                        counter++;
                        bp = i + 1;
                        //textBox6.Text += TextBlocks[counter - 1] + "\n";
                    }
                }
                string RandText = TextBlocks[Random_Text(TextBlocks.Length)];
                CurSubstr = RandText;
                //textBox6.Text = CurSubstr + " here";
                //GetInnerString(CurSubstr);
                return CurSubstr;
            } // }
            else
            {
                //Complete_text += str;
                return str;
            }

            //textBox6.Text = RandText;
        }

        public int Random_Text(int num)
        {
            Random rnd = new Random();
            int rand;
            rand = rnd.Next(0, num);
            return rand;
        }
    }
}
