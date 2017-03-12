using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenus
{
    class Menu
    {
        public string Title { get; private set; }
        public int Padding { get; set; }
        public char Delim { get; private set; }
        public List<string> Choices { get; private set; }
        public string Prompt { get; private set; }
        public int NumChoices { get; private set; }

        public Menu(int? p = 0, string pr = "Press any key to continue: ", string t = "Sample Menu", char? d = ':')
        {
            Title = t;
            Prompt = pr;
            Padding = (int)p;
            Delim = (char)d;
            Choices = new List<string>();
            NumChoices = 0;
        }
        
        public void AddChoice(string c)
        {
            NumChoices++;
            Choices.Add(NumChoices.ToString() + Delim + " " + c);
        }

        public override string ToString()
        {
            string msg = "\n";
            msg = Pad(msg);
            msg += Title + "\n";
            foreach (string choice in Choices)
            {
                msg = Pad(msg);
                msg += choice + "\n";
            }
            msg += "\n";
            msg = Pad(msg);
            msg += Prompt;
            return msg;
        }

        private string Pad(string msg)
        {
            for (int i = 0; i < Padding; i++)
                msg += " ";
            return msg;
        }
    }
}
