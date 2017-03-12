//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		GM Assistant
//	File Name:		Character.cs
//	Description:	Class for containing all info on a character
//	Author:			Kevin Jackson, kevinjcksn249@gmail.com, "A Modern Mage"
//	Created:		
//	Copyright:		Kevin Jackson, 2017
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GMAssistantConsole
{
    class Character
    {
        #region Properties
        public string Name { get; set; }
        public string Handle { get; set; }
        public int Age { get; private set; }
        public CharacterClass Class { get; private set; }
        public BuildType Build { get; private set; }
        public int CharacterPoints { get; set; }
        public StatTable Stats { get; private set; }
        public int BodyTypeModifier { get; private set; }

        #endregion

        #region Methods
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character()
        {
            Name = "Punk";
            Handle = "(None)";
            Class = CharacterClass.Other;
            Build = GetBuild();
            CharacterPoints = 50;
            Stats = new StatTable();
            BodyTypeModifier = GetBTM();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="n">The character's name.</param>
        /// <param name="h">The character's handle.</param>
        /// <param name="c">The character's class.</param>
        /// <param name="points">The number of points the character gets.</param>
        /// <param name="lucky">Whether or not the character get points for luck.</param>
        public Character(string n = "Punk", 
                         string h = "(None)", 
                         CharacterClass c = CharacterClass.Other, 
                         int? points = 50, 
                         bool? lucky = false)
        {
            Name = n;
            Handle = h;
            Class = c;
            Build = GetBuild();
            CharacterPoints = (int)points;
            Stats = new StatTable(Build, (int)points, (bool)lucky);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="stats">The stats.</param>
        /// <param name="n">The character's name.</param>
        /// <param name="h">The character's handle.</param>
        /// <param name="c">The character's class.</param>
        /// <param name="b">The character's build type.</param>
        public Character(List<int> stats,
                         string n = "Punk",
                         string h = "(None)",
                         CharacterClass c = CharacterClass.Other,
                         BuildType? b = BuildType.Balanced)
        {
            Name = n;
            Handle = h;
            Class = c;
            Build = (BuildType)b;
            CharacterPoints = GetPoints(stats);
            Stats = new StatTable(stats);
        }
        public Character(List<int> stats,
                         string n = "Punk",
                         string h = "(None)",
                         CharacterClass c = CharacterClass.Other)
        {
            Name = n;
            Handle = h;
            Class = c;
            Build = GetBuild();
            CharacterPoints = GetPoints(stats);
            Stats = new StatTable(stats);
        }
        #endregion
        #region Calculations
        /// <summary>
        /// Rolls a character's stats based on their
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns></returns>

        /// <summary>
        /// Gets the character's rank based on character points.
        /// </summary>
        /// <returns></returns>
        public string GetRank()
        {
            string Rank = "";
            switch (CharacterPoints)
            {
                case 45:
                    Rank = "Peon";
                    break;
                case 50:
                    Rank = "Average punk";
                    break;
                case 60:
                    Rank = "Minor supporting character";
                    break;
                case 70:
                    Rank = "Major supporting character";
                    break;
                case 75:
                    Rank = "Minor hero";
                    break;
                case 80:
                    Rank = "Major hero";
                    break;
                case 85:
                    Rank = "Boss";
                    break;
                default:
                    Rank = "Unknown";
                    break;
            }
            return Rank;
        }

        /// <summary>
        /// Determine the character's build based on its class
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public BuildType GetBuild()
        {
            BuildType type = BuildType.Balanced;
            switch (Class)
            {
                case CharacterClass.Cop:
                    type = BuildType.Balanced;
                    break;
                case CharacterClass.Corp:
                    type = BuildType.EMP;
                    break;
                case CharacterClass.Fixer:
                    type = BuildType.EMP;
                    break;
                case CharacterClass.Media:
                    type = BuildType.Balanced;
                    break;
                case CharacterClass.Medtechie:
                    type = BuildType.INT;
                    break;
                case CharacterClass.Netrunner:
                    type = BuildType.INT;
                    break;
                case CharacterClass.Nomad:
                    type = BuildType.REF;
                    break;
                case CharacterClass.Politician:
                    type = BuildType.EMP;
                    break;
                case CharacterClass.Solo:
                    type = BuildType.REF;
                    break;
                case CharacterClass.Rocker:
                    type = BuildType.EMP;
                    break;
                case CharacterClass.Techie:
                    type = BuildType.INT;
                    break;
                case CharacterClass.Other:
                    type = BuildType.Balanced;
                    break;
                default:
                    type = BuildType.Balanced;
                    break;
            }
            return type;
        }

        private int GetPoints(List<int> l)
        {
            int total = 0;
            foreach (int i in l)
                total += i;
            return total;
        }
        /// <summary>
        /// Determines the character's BTM from the body stat
        /// </summary>
        /// <returns></returns>
        public int GetBTM()
        {
            int BTM = 0;
            switch (this.Stats.Body)
            {
                case 0:
                case 1:
                case 2:
                    BTM = 0;
                    break;
                case 3:
                case 4:
                    BTM = 1;
                    break;
                case 5:
                case 6:
                case 7:
                    BTM = 2;
                    break;
                case 8:
                case 9:
                    BTM = 3;
                    break;
                case 10:
                    BTM = 4;
                    break;
                case 11:
                case 12:
                case 13:
                    BTM = 5;
                    break;
                case 14:
                case 15:
                case 16:
                    BTM = 6;
                    break;
            }
            return BTM;
        }

        private CharacterClass GetClassFromString(string input)
        {
            CharacterClass cl = CharacterClass.Other;
            string inp = input.ToLower();
            if (inp == "rocker")
                cl = CharacterClass.Rocker;
            else if (inp == "cop")
                cl = CharacterClass.Cop;
            else if (inp == "corp")
                cl = CharacterClass.Corp;
            else if (inp == "techie")
                cl = CharacterClass.Techie;
            else if (inp == "netrunner")
                cl = CharacterClass.Netrunner;
            else if (inp == "media")
                cl = CharacterClass.Media;
            else if (inp == "corp")
                cl = CharacterClass.Corp;
            else if (inp == "fixer")
                cl = CharacterClass.Fixer;
            else if (inp == "medtechie")
                cl = CharacterClass.Medtechie;
            else if (inp == "nomad")
                cl = CharacterClass.Nomad;
            else if (inp == "politician")
                cl = CharacterClass.Politician;
            return cl;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Character c = (Character)obj;
            return c.Name == Name;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "\t" + Name + ' '+'"'+ Handle+ '"' +"\t" + Class + "\n" +
                    "\tRank: " + this.GetRank() + "\n" +
                    Stats + "\n";
        }
        #endregion
        #endregion
    }
}
