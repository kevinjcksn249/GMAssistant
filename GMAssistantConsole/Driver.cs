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
			string command = "";
            Help();
            while (command != "exit")
			{
				Write ("\nGMAssistant: ");
				command = ReadLine ();
				string [] userInputs = command.Split ();
				switch (userInputs[0])
				{
                    case "help":
                        Help();
                        break;
                    case "generate":
                        if (userInputs.Length == 1)
                            GenCharacter(ass);
                        break;
                    case "build":

                        break;
                    case "load":
                        if (userInputs.Length == 1)
                            Load(ass);
                        else if (userInputs.Length == 2)
                            Load(ass, userInputs[1]);
                        break;
                    case "loadall":
                        WriteLine("Loading.....");
                        ass.LoadAll();
                        WriteLine("Finished!");
                        break;
                    case "saveall":
                        WriteLine("Saving......");
                        ass.SaveAll();
                        WriteLine("Finished!");
                        break;
                    case "exit":
                        break;
                    default:
                        WriteLine("Command not recognized. Type 'help' for available commands.");
                        break;
				}
			}
        }

        #region Main program functions
        /// <summary>
        /// Display all possible commands within the program to the user.
        /// </summary>
        static void Help()
        {
            string h =  "\n\n\n    GM Assistant: a database for the CP2020 GM \n\n" +
                        "                         help - display this message\n" +
                        "                     generate - generate a new character\n" +
                        "                        build - create a new character, specifying each stat\n" +
                        "      load [character handle] - load a character from the database\n" +
                        "                      loadall - load all characters currently in the database\n" +
                        "      save [character handle] - save a character to the database\n" +
                        "                      saveall - save all characters in this session\n" +
                        "                         list - list all characters that have been loaded this session\n" +
                        "    csheet [character handle] - look at a character's character sheet\n" +
                        "      edit [character handle] - edit a character's stats, handle, and name\n" +
                        "                         exit - exit the program\n\n\n";
        }

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

        static void BuildCharacter(Assistant a)
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

        }
        #region Load
        /// <summary>
        /// Walks the user through loading a character from the database
        /// </summary>
        /// <param name="a">This session's <see cref="Assistant"/> object.</param>
        static void Load(Assistant a)
        {
            string handle = Utilities.GetString("Enter the handle of the character you want to load: ");
            Character c = a.LoadCharacter(handle);
            if (c.Handle != handle)
                WriteLine("Sorry! That character is not in the database!");
            else
                WriteLine(handle + "loaded successfully!");
        }

        /// <summary>
        /// Attempts to load a character with the given handle
        /// </summary>
        /// <param name="a">This session's <see cref="Assistant"/> object.</param>
        /// <param name="handle">The handle.</param>
        static void Load(Assistant a, string handle)
        {
            Character c = a.LoadCharacter(handle);
            if (c.Handle != handle)
                WriteLine("Sorry! That character is not in the database!");
            else
                WriteLine(handle + "loaded successfully!");
        }
        #endregion


        #endregion

    }
}
