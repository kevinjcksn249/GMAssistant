#define DEBUG
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
    /// <summary>
    /// A class for assisting a GM in running a Cyberpunk 2020 campaign.
    /// Features methods for creating characters, saving them to and loading
    /// them from a database, and deleting them as well. Also features tools
    /// for managing temporary character values like health and luck.
    /// </summary>
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
        /// <summary>
        /// Gets a character with the given name from <see cref="LoadedCharacters"/>
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
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
        /// <param name="stats">The character's stats.</param>
        /// <param name="handle">The character's handle.</param>
        /// <param name="c">The character's class.</param>
        /// <returns></returns>
        public Character CreateCharacter(string name, List<int> stats, string c, string handle = "(None)")
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

        #region Database interaction
        /// <summary>
        /// Saves a given character to the database.
        /// BE SURE to check if user wishes to potentially override a 
        /// character record with the same name as the given character
        /// BEFORE calling this method.
        /// </summary>
        /// <param name="c">The character being saved to the database.</param>
        public void SaveCharacter(Character c)
        {
            NeuralLink.Close();
            if (CheckCharacterExists(c.Handle))
            {
                string q = "UPDATE CP2020Db.CP_CHARACTERS " +
                            " SET charName = '" + c.Name + "' " +
                            ", charHandle = '" + c.Handle + "' " +
                            ", charPoints = " + c.CharacterPoints +
                            ", charRole = '" + c.Class.ToString() + "' " +
                            ", charAge = " + c.Age +
                            ", charAttract = " + c.Stats.Attract +
                            ", charBody = " + c.Stats.Body +
                            ", charCool = " + c.Stats.Cool +
                            ", charEmpathy = " + c.Stats.Empathy +
                            ", charIntelligence = " + c.Stats.Intell +
                            ", charLuck = " + c.Stats.Luck +
                            ", charMove = " + c.Stats.Move +
                            ", charReflex = " + c.Stats.Reflex +
                            ", charTech = " + c.Stats.Tech +
                            ", charRun = " + c.Stats.Run +
                            ", charLeap = " + c.Stats.Leap +
                            ", charLift = " + c.Stats.Lift +
                            ", charHumanity = " + c.Humanity +
                            " WHERE charHandle = '" + c.Handle + "';";
                NeuralLink.Open();
                runQuery(q);
                NeuralLink.Close();
            }
            else
            {
                string q = "INSERT INTO CP2020Db.CP_CHARACTERS (charName, charHandle, charPoints, charRole, charAge, charAttract, charBody, charCool, charEmpathy, charIntelligence, charLuck, charMove, charReflex, charTech, charRun, charLeap, charLift, charHumanity)" +
                    "VALUES (" +
                    "'" + c.Name + "', " +
                    "'" + c.Handle + "', " +
                    c.CharacterPoints + ", " +
                    "'" + c.Class.ToString() + "', " +
                    c.Age + ", " +
                    c.Stats.Attract + ", " +
                    c.Stats.Body + ", " +
                    c.Stats.Cool + ", " +
                    c.Stats.Empathy + ", " +
                    c.Stats.Intell + ", " +
                    c.Stats.Luck + ", " +
                    c.Stats.Move + ", " +
                    c.Stats.Reflex + ", " +
                    c.Stats.Tech + ", " +
                    c.Stats.Run + ", " +
                    c.Stats.Leap + ", " +
                    c.Stats.Lift + ", " +
                    c.Humanity + ");";
                NeuralLink.Open();
                runQuery(q);
                NeuralLink.Close();
            }
        }

        /// <summary>
        /// Loads a character from the database with the given name.
        /// </summary>
        /// <param name="n">The name of the character to load from the database.</param>
        /// <returns></returns>
        public Character LoadCharacter(string n)
        {
            Character loadedChar = new Character();
            if (CheckCharacterExists(n))
            {
                // Get character info from the database
                string q = "SELECT * FROM CP2020Db.CP_CHARACTERS WHERE charHandle = '" + n + "';";
                NeuralLink.Open();
                MySqlDataReader characterData = runQuery(q);
                characterData.Read();

                string charName = characterData.GetString(1);
                string charHandle = characterData.GetString(2);
                int charPoints = characterData.GetInt32(3);
                string charRole = characterData.GetString(4);
                int charAge = characterData.GetInt32(5);
                int charAttr = characterData.GetInt32(6);
                int charBod = characterData.GetInt32(7);
                int charCool = characterData.GetInt32(8);
                int charEmp = characterData.GetInt32(9);
                int charInt = characterData.GetInt32(10);
                int charLuck = characterData.GetInt32(11);
                int charMove = characterData.GetInt32(12);
                int charRef = characterData.GetInt32(13);
                int charTech = characterData.GetInt32(14);
                int charRun = characterData.GetInt32(15);
                int charLeap = characterData.GetInt32(16);
                int charLift = characterData.GetInt32(17);
                int charHum = characterData.GetInt32(18);
                NeuralLink.Close();

                // Generate new character
                List<int> charStats = new List<int> { charInt, charRef, charTech, charCool, charAttr, charLuck, charMove, charBod, charEmp };
                loadedChar = CreateCharacter(charName, charStats, charRole, handle: charHandle);

            }
            else
            {
                throw new InvalidOperationException("ERROR: could not find character '" + n + "'. Please create a new character.");
            }
            return loadedChar;
        }

        /// <summary>
        /// Deletes a character from the database.
        /// MAKE SURE this is what the user wants
        /// before running this method.
        /// Update to this method is required upon
        /// addition of weapons, gear, and cybernetics
        /// to the database.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <exception cref="InvalidOperationException">ERROR: Attempt to delete non-existent character</exception>
        public void DeleteCharacter(string n)
        {
            if (CheckCharacterExists(n))
            {
                string q = "DELETE FROM CP2020Db.CP_CHARACTERS WHERE charHandle = '" + n + "';";
                NeuralLink.Open();
                runQuery(q);
                NeuralLink.Close();
            }
            else
            {
                throw new InvalidOperationException("ERROR: Attempt to delete non-existent character");
            }
        }

        /// <summary>
        /// Check if the database contains a character with the handle.
        /// <see cref="NeuralLink"/> must be closed before calling this method.
        /// </summary>
        /// <param name="charName">The character's handle.</param>
        /// <returns></returns>
        public bool CheckCharacterExists(string charName)
        {
            bool exists = false;
            string q = "SELECT * FROM CP2020Db.CP_CHARACTERS WHERE charHandle = '" + charName + "' ;";
            NeuralLink.Open();
            MySqlDataReader result = runQuery(q);
            if (result != null)
                exists = true;
            NeuralLink.Close();
            return exists;
        }

 
        /// <summary>
        /// Runs the given query. <see cref="NeuralLink"/> MUST be
        /// opened before running this method and MUST be closed
        /// after running this method.
        /// </summary>
        /// <param name="query">The query.</param>
        public MySqlDataReader runQuery(string query)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand(query, NeuralLink);
                MySqlDataReader data = comm.ExecuteReader();
                return data;
            }
            catch (MySqlException ex)
            {
                #if DEBUG
                Console.WriteLine(ex);
                #endif
                NeuralLink.Close();
                return null;
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
