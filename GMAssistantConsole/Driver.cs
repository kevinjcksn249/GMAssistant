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
            Assistant helper = new Assistant();
			string command = "";
            Help();
            while (command != "exit")
			{
				Write ("\nGMAssistant: ");
				command = ReadLine ();
				string [] userInputs = command.Split ();
				switch (userInputs[0].ToLower())
				{
                    case "help":
                        if (userInputs.Length == 1)
                            Help();
                        else
                            Err();
                        break;
                    case "list":
                        if (userInputs.Length == 1)
                            List(helper);
                        else
                            Err();
                        break;
                    case "generate":
                        if (userInputs.Length == 1)
                            GenCharacter(helper);
                        else
                            Err();
                        break;
                    case "build":
                        if (userInputs.Length == 1)
                            BuildCharacter(helper);
                        else
                            Err();
                        break;
                    case "load":
                        if (userInputs.Length == 1)
                            Load(helper);
                        else if (userInputs.Length == 2)
                            Load(helper, userInputs[1]);
                        else
                            Err();
                        break;
                    case "loadall":
                        if (userInputs.Length == 1)
                        {
                            WriteLine("Loading.....");
                            helper.LoadAll();
                            WriteLine("Finished!");
                        }
                        else
                            Err();
                        break;
                    case "save":
                        if (userInputs.Length == 1)
                            SaveCharacter(helper);
                        else if (userInputs.Length == 2)
                            SaveCharacter(helper, userInputs[1]);
                        else
                            Err();
                        break;
                    case "saveall":
                        if (userInputs.Length == 1)
                        {
                            WriteLine("Saving......");
                            helper.SaveAll();
                            WriteLine("Finished!");
                        }
                        else
                            Err();
                        break;
                    case "csheet":
                        if (userInputs.Length == 1)
                            CSheet(helper);
                        else if (userInputs.Length == 2)
                            CSheet(helper, userInputs[1]);
                        else
                            Err();
                        break;
                    case "exit":
                        break;
                    default:
                        Err();
                        break;
				}
			}
        }

        static void Err()
        {
            WriteLine("Command not recognized. Type 'help' for available commands.");
        }
        #region Main program functions
        /// <summary>
        /// Display all possible commands within the program to the user.
        /// </summary>
        static void Help()
        {
            string h =  "\n\n\n    GM Assistant: a database for the CP2020 GM \n\n" +
                        "                         help - display this message\n" +
                        "                         list - list all characters that have been loaded this session\n" +
                        "                        build - create a new character, specifying each stat\n" +
                        "                      loadall - load all characters currently in the database\n" +
                        "                      saveall - save all characters in this session\n" +
                        "                     generate - generate a new character\n" +
                        "      save [character handle] - save a character to the database\n" +
                        "      load [character handle] - load a character from the database\n" +
                        "    csheet [character handle] - look at a character's character sheet\n" +
                      //  "    unload [character handle] - unload a character's data from memory\n" +
                      //  "    delete [character handle] - unload a character and delete it from the database\n" +
                      //  "      edit [character handle] - edit a character's stats, handle, and name\n" +
                        "                         exit - exit the program\n\n\n";
			WriteLine (h);
        }

        /// <summary>
        /// Walks the user through generating a character
        /// </summary>
        /// <param name="a">This session's <see cref="Assistant"/> object.</param>
        static void GenCharacter(Assistant a)
        {
            string name = Utilities.GetString("Enter the character's full name: ");
            string handle = Utilities.GetString("Enter the character's handle, or nickname: ");
            WriteLine("\n\nCharacter ranks:\nPeon: 45\nAverage: 50 \nMinor Supporting character: 60\nMajor Supporting character: 70\nMinor hero: 75\nMajor Hero: 80\nBoss 85");
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
                WriteLine(handle + " loaded successfully!");
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

        static void LoadAll(Assistant a)
        {
            a.LoadAll();
        }
        #endregion


        #region Save
        static void SaveCharacter(Assistant a)
        {
            string handle = Utilities.GetString("Enter the character's handle: ");
            var c = a.GetCharacter(handle);
            if (c != null)
                a.SaveCharacter((Character)c);
            else
                WriteLine("Character not found.");
        }

        static void SaveCharacter(Assistant a, string handle)
        {
            var c = a.GetCharacter(handle);
            if (c != null)
                a.SaveCharacter((Character)c);
            else
                WriteLine("Character not found.");
        }

        static void SaveAll(Assistant a)
        {
            a.SaveAll();
        }
        #endregion

        static void List(Assistant a)
        {
            foreach (Character c in a.LoadedCharacters)
                WriteLine(c.Handle);
            WriteLine("\n\n");
        }

        #region CSheet
        static void CSheet(Assistant a)
        {
            string handle = Utilities.GetString("Enter the character's handle: ");
            var c = a.GetCharacter(handle);
            if (c != null)
                WriteLine(c);
            else
                WriteLine("Character not found.");
        }
        static void CSheet(Assistant a, string handle)
        {
            var c = a.GetCharacter(handle);
            if (c != null)
                WriteLine(c);
            else
                WriteLine("Character not found.");
        }

        #endregion
        #endregion

    }
}
