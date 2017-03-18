//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	File Name:		Utilities.cs
//	Description:	My personal set of C# utilities
//	Author:			Kevin Jackson, kevinjcksn249@gmail.com
//	Created:		March 18, 2017
//	Copyright:		Kevin Jackson, 2017
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace GMAssistantConsole
{
    static class Utilities
    {
        #region Utilities
        /// <summary>
        /// Gets integer input from the user.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        /// <returns></returns>
        public static int GetInt(string prompt)
        {
            int input = 0;
            Write("\n" + prompt);
            string strInput = ReadLine();
            if (!Int32.TryParse(strInput, out input))
            {
                WriteLine("ERROR! Numeric input is required!");
                ReadKey();
                input = GetInt(prompt);
            }
            return input;
        }

        /// <summary>
        /// Gets string input from the user.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        /// <returns></returns>
        public static string GetString(string prompt)
        {
            Write("\n" + prompt);
            string input = ReadLine();
            return input;
        }

        /// <summary>
        /// Gets yes/no input from the user
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        /// <returns></returns>
        public static bool GetBool(string prompt)
        {
            Write("\n" + prompt);
            string input = ReadLine();
            bool ret = false;
            if (input.ToLower() == "y" || input.ToLower() == "yes")
                ret = true;
            else if (input.ToLower() == "n" || input.ToLower() == "no")
                ret = false;
            else
                ret = GetBool(prompt);
            return ret;
        }

        /// <summary>
        /// Gets a character's role from the user.
        /// </summary>
        /// <param name="prompt">The prompt.</param>
        /// <returns></returns>
        public static CharacterClass GetRole(string prompt)
        {
            CharacterClass role = CharacterClass.Other;
            Write("\n" + prompt);
            string input = ReadLine();
            input = input.ToLower();
            switch (input)
            {
                case "solo":
                    role = CharacterClass.Solo;
                    break;
                case "rocker":
                    role = CharacterClass.Rocker;
                    break;
                case "netrunner":
                    role = CharacterClass.Netrunner;
                    break;
                case "techie":
                    role = CharacterClass.Techie;
                    break;
                case "media":
                    role = CharacterClass.Media;
                    break;
                case "nomad":
                    role = CharacterClass.Nomad;
                    break;
                case "fixer":
                    role = CharacterClass.Fixer;
                    break;
                case "cop":
                    role = CharacterClass.Cop;
                    break;
                case "corp":
                    role = CharacterClass.Corp;
                    break;
                case "medtechie":
                    role = CharacterClass.Medtechie;
                    break;
                case "politician":
                    role = CharacterClass.Politician;
                    break;
                default:
                    WriteLine("Character class not recognized.");
                    break;
            }
            WriteLine("Character class '" + role.ToString() + "' chosen.");
            return role;
        }

        /// <summary>
        /// Creates an SQL insert script from the text file "skillList.txt"
        /// </summary>
        public static void CreateInsert()
        {
            string insertLine = "INSERT INTO CP2020Db.SKILLS (skillName, skillType) VALUES";
            StreamReader r = new StreamReader(@"G:\Scripts\skillList.txt");
            StreamWriter wr = new StreamWriter(@"G:\Scripts\CreateSkills.sql");
            string line;
            while ((line = r.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                string skill = words[0];
                string cata = words[1];
                wr.WriteLine(insertLine);
                wr.WriteLine("(" + '"' + skill + '"' + ", " + '"' + cata + '"' + ");");
            }
            r.Close();
            wr.Close();
        }
        #endregion

    }
}
