//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		GM Assistant
//	File Name:		Assistant.cs
//	Description:	Set of tools for assisting a GM in running a Cyberpunk 2020 campaign.
//                  Acts as the controller in the MVC model, allowing for loading characters from a database, as
//                  well as modfying and saving them.
//	Author:			Kevin Jackson, kevinjcksn249@gmail.com, "A Modern Mage"
//	Created:		January 13, 2017
//	Copyright:		Kevin Jackson, 2017
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GMAssistantConsole
{
    class Assistant
    {
        #region Properties
        public List<Character> LoadedCharacters { get; private set; }
        public Dictionary<string, int> CharacterLuck { get; private set; }
        private MySqlConnection NeuralLink { get; set; }
        private string ConnectionString { get; set; }
        #endregion

        #region Methods
        #region Constructors
        public Assistant()
        {
            LoadedCharacters = new List<Character>();
            ConnectionString = @"Server=cp2020db.cj9u9ybtcyyk.us-east-1.rds.amazonaws.com;Database=CP2020Db;Uid=fixit;Password=4w3s0m3!";
            try
            {
                NeuralLink = new MySqlConnection(ConnectionString);
                NeuralLink.Open();

            }
            catch (MySqlException ex)
            {
                Console.Write("Connection failed: ");
                Console.WriteLine(ex);
            }
            finally
            {
                if (NeuralLink != null)
                    NeuralLink.Close();
            }

        }
        #endregion
        #region Rolls and Probabilities
        /// <summary>
        /// Simulates a die roll with the given number of sides
        /// </summary>
        /// <param name="num">The number.</param>
        /// <returns></returns>
        public static int Roll(DiceSides num)
        {
            Random roller = new Random();
            int r = roller.Next((int)num);
            return r + 1;
        }
        #endregion

        #region Character Management
        public Character GetCharacter(string name)
        {
            Character c = new Character(n: name);
            return LoadedCharacters.Find(x => x.Name == name);
        }

        /// <summary>
        /// Creates a new character and adds it to the list of loaded characters.
        /// </summary>
        /// <param name="name">The character's name.</param>
        /// <param name="stats">The character's stats.</param>
        /// <param name="handle">The character's handle.</param>
        /// <param name="c">The character's class.</param>
        /// <returns></returns>
        public Character CreateCharacter(string name, List<int> stats, CharacterClass c, string handle = "(None)")
        {
            Character newChar = new Character(stats: stats, n: name, h: handle, c: c);
            LoadedCharacters.Add(newChar);
            return newChar;
        }

        /// <summary>
        /// Creates a new character and adds it to the list of loaded characters.
        /// </summary>
        /// <param name="name">The character's name.</param>
        /// <param name="characterPoints">The character points.</param>
        /// <param name="c">The character's class.</param>
        /// <param name="handle">The character's handle.</param>
        /// <param name="lucky">Determines if the character gets points in luck.</param>
        /// <returns></returns>
        public Character CreateCharacter(string name, int characterPoints, CharacterClass c, string handle = "(None)", bool? lucky = false)
        {
            Character newChar = new Character(n: name, h: handle, c: c, points: characterPoints, lucky: (bool)lucky);
            LoadedCharacters.Add(newChar);
            return newChar;
        }
        public void SaveCharacter(Character c)
        {
            throw new NotImplementedException();
        }

        public Character LoadCharacter()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
        }
    }
