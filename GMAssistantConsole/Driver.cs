//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		GMAssistant
//	File Name:		Driver.cs
//	Description:	Handles I/O for the GM Assistant app
//	Author:			Kevin Jackson, kevinjcksn249@gmail.com
//	Created:		January 13, 2017
//	Copyright:		Kevin Jackson, 2017
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
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
			string userInput = "";
            string[] options = { "help", "generate", "load", "build", "list", "save", "view", "edit", "exit" };
            WriteLine("Type 'help' for available actions");
			while (userInput != "exit")
			{
				Write ("\nGMAssistant: ");
				userInput = ReadLine ();
				string [] userInputs = userInput.Split ();
				switch (userInputs[0])
				{
                    case "generate":
                        if (userInputs.Length == 1)
                        {
                            GenCharacter(ass);
                        }
                        break;
                    case "load":
                        if (userInputs.Length == 1)
                        {
                            
                        }
                        break;
                    default:
                        break;
				}
			}
        }

        #region Main program functions
        /// <summary>
        /// Walks the user through generating a character
        /// </summary>
        /// <param name="a">This session's <see cref="Assistant"/> object.</param>
        static void GenCharacter(Assistant a)
        {
            string name = Utilities.GetString("Enter the character's full name: ");
            string handle = Utilities.GetString("Enter the character's handle, or nickname: ");
            WriteLine("Character ranks:\nPeon: 45\nAverage: 50 \nMinor Supporting character: 60\nMajor Supporting character: 70\nMinor hero: 75\nMajor Hero: 80\nBoss 85");
            int points = Utilities.GetInt("Enter the number of points this character gets: ");
            WriteLine("\n\nAvailable classes");
            var classes = Enum.GetValues(typeof(CharacterClass));
            foreach (CharacterClass c in classes)
                WriteLine(c.ToString());
            CharacterClass role = Utilities.GetRole("Enter the character's class: ");
            bool lucky = false;
            if (points < 80)
                lucky = Utilities.GetBool("Does this character get luck? [y/n]: ");
            WriteLine("Generating.....");
            Character punk = a.CreateCharacter(name, points, role, handle, lucky);
            WriteLine("Finished!\n");
            WriteLine(punk);
        }
        
        static void LoadCharacter(Assistant a)
        {

        }
        #endregion

    }
}
