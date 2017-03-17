//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		GMAssistant
//	File Name:		Driver.cs
//	Description:	Handles I/O for the GM Assistant
//	Author:			Kevin Jackson, kevinjcksn249@gmail.com, "A Modern Mage"
//	Created:		
//	Copyright:		Kevin Jackson, 2017
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMenus;
using static System.Console;
using System.IO;

namespace GMAssistantConsole
{
    class Driver
    {
        static void Main(string[] args)
        {
            Assistant ass = new Assistant();
            Character c = ass.LoadCharacter("Fixit");

           
            ass.DeleteCharacter("Fixit");
            ass.SaveCharacter(c);
            ReadKey();
        }

        static void MainMenu(string ExitMessage, Assistant A)
        {
            Clear();
            WriteLine("\n" + ExitMessage + "\n");
            Menu mainMenu = new Menu(t: "What do you wish to do?", pr: " ", d: '-', p: 8);
            mainMenu.AddChoice("Create a character randomly");
            mainMenu.AddChoice("Create a character with my own stats");
            Write(mainMenu);
            string userChoice = ReadKey().KeyChar.ToString();
            switch (userChoice)
            {
                case "1":
                    GenerateCharacterMenu(A, ExitMessage);
                    break;
                case "2":
                    break;
                case "x":
                    return;
                default:
                    MainMenu(ExitMessage, A);
                    break;
            }
        }

        static void GenerateCharacterMenu(Assistant A, string em)
        {
            Clear();
            string Name = GetString("\n\tPlease enter the character's name: ");
            string Handle = GetString("\n\tPlease enter the character's handle: ");
            int points = GetInt("\n\tEnterthe total points this character gets: ");
            Menu classPicker = new Menu(p: 8, pr: "Choose a class: ", t: "Available classes:");
            classPicker.AddChoice("Solo");
            classPicker.AddChoice("Rocker");
            classPicker.AddChoice("Netrunner");
            classPicker.AddChoice("Techie");
            classPicker.AddChoice("Media");
            classPicker.AddChoice("Cop");
            classPicker.AddChoice("Corp");
            classPicker.AddChoice("Fixer");
            classPicker.AddChoice("Medtechie");
            classPicker.AddChoice("Nomad");
            classPicker.AddChoice("Politician");
            classPicker.AddChoice("Other");
            Write(classPicker);
            string userChoice = ReadLine();
            CharacterClass role;
            switch (userChoice)
            {
                case "1":  role = CharacterClass.Solo; break;
                case "2":  role = CharacterClass.Rocker; break;
                case "3":  role = CharacterClass.Netrunner; break;
                case "4":  role = CharacterClass.Techie; break;
                case "5":  role = CharacterClass.Media; break;
                case "6":  role = CharacterClass.Cop; break;
                case "7":  role = CharacterClass.Corp; break;
                case "8":  role = CharacterClass.Fixer; break;
                case "9":  role = CharacterClass.Medtechie; break;
                case "10": role = CharacterClass.Nomad; break;
                case "11": role = CharacterClass.Politician; break;
                case "12": role = CharacterClass.Other; break;
                default: role = CharacterClass.Other; break;
            }
            Character c = A.CreateCharacter(Name, points, role, Handle, true);
            WriteLine("\n" + c);
            WriteLine("\n\tPress any key to continue...");
            ReadKey();
            MainMenu(em, A);
        }

        static void CustomStatsMenu()
        {
            Clear();

        }

        static int GetInt(string prompt)
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

        static string GetString(string prompt)
        {
            Write("\n" + prompt);
            string input = ReadLine();
            return input;
        }

        static void CreateInsert()
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
    }
}
