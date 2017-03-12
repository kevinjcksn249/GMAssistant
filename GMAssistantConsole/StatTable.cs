//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		GM Assistant
//	File Name:		StatTable.cs
//	Description:	A data structure that contains a character's stats
//	Author:			Kevin Jackson, kevinjcksn249@gmail.com, "A Modern Mage"
//	Created:		January 13, 2017
//	Copyright:		Kevin Jackson, 2017
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAssistantConsole
{
    class StatTable
    {
        #region Properties
        private List<int> MasterStatsList { get; set; }
        public int Intell { get; private set; }
        public int Reflex { get; private set; }
        public int Tech { get; private set; }
        public int Cool { get; private set; }
        public int Attract { get; private set; }
        public int Luck { get; private set; }
        public int Move { get; private set; }
        public int Body { get; private set; }
        public int Empathy { get; private set; }
        public int Run { get; private set; }
        public int Leap { get; private set; }
        public int Lift { get; private set; }
        
        #endregion

        #region Methods
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StatTable"/> class.
        /// </summary>
        public StatTable()
        {
            int[] l = RollStats(50, false, BuildType.Balanced);
            MasterStatsList = new List<int>(l);
            PullFromList(MasterStatsList);
            ComputeDependents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatTable"/> class.
        /// </summary>
        /// <param name="stats">The stats.</param>
        public StatTable(List<int> stats)
        {
            MasterStatsList = stats;
            PullFromList(stats);
            ComputeDependents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatTable"/> class.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <param name="points">The points.</param>
        /// <param name="lucky">if set to <c>true</c> [lucky].</param>
        public StatTable(BuildType build, int points, bool lucky)
        {
            int[] l = RollStats(points, lucky, build);
            MasterStatsList = new List<int>(l);
            PullFromList(MasterStatsList);
            ComputeDependents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatTable"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public StatTable(int points)
        {
            int[] l = RollStats(points, false, BuildType.Balanced);
            MasterStatsList = new List<int>(l);
            PullFromList(MasterStatsList);
            ComputeDependents();
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "\t-------------------------\n" +
                   "\tINT:  " + Intell + "\n" +
                   "\tREF:  " + Reflex + "\n" +
                   "\tTECH: " + Tech + "\n" +
                   "\tCOOL: " + Cool + "\n" +
                   "\tATTR: " + Attract + "\n" +
                   "\tLUCK: " + Luck + "\n" +
                   "\tMA:   " + Move + "\n" +
                   "\tBODY: " + Body + "\n" +
                   "\tEMP:  " + Empathy + "\n" +
                   "\tRUN:  " + Run + "\n" +
                   "\tLEAP: " + Leap + "\n" +
                   "\tLIFT: " + Lift + "\n" +
                  "\t-------------------------\n" ;
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Int32"/> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="System.Int32"/>.
        /// </value>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public int this[int i]
        {
            get
            {
                return MasterStatsList[i];
            }

            set
            {
                MasterStatsList[i] = value;
            }
        }
        #endregion
        #region Calculations
        /// <summary>
        /// Computes a character's dependent stats - Run, Leap, and Lift.
        /// </summary>
        private void ComputeDependents()
        {
            Run = ComputeRun();
            Leap = ComputeLeap();
            Lift = ComputeLift();
        }
        /// <summary>
        /// Computes the character's run stat.
        /// </summary>
        /// <returns>Movement allowance times 3 -- the character's run speed</returns>
        private int ComputeRun()
        {
            return Move * 3;
        }

        /// <summary>
        /// Computes the character's leap stat.
        /// </summary>
        /// <returns>Run stat divided by 4 -- the distance the character can leap</returns>
        private int ComputeLeap()
        {
            return (int) Run / 4;
        }

        /// <summary>
        /// Computes the character's lift stat.
        /// </summary>
        /// <returns> The maximum weight the character can lift and carry</returns>
        private int ComputeLift()
        {
            return Body * 10;
        }

        /// <summary>
        /// Randomly generates a balanced set of character stats.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="lucky">if set to <c>true</c> [lucky].</param>
        /// <param name="build">The build.</param>
        /// <returns></returns>
        public int[] RollStats(int points, bool lucky, BuildType build)
        {
            int min = 3;    // changes the minimum number of points a character can have
            Random picker = new Random();
            int[] stats = new int[9];
            int baseline = 0;
            switch (build)
            {
                #region Balanced distribution of points
                case BuildType.Balanced:
                    baseline = points / 9;
                    int diff = points % 9;
                    for (int i = 0; i < stats.Length; i++)
                        stats[i] = baseline;
                    if(!lucky)
                    {
                        diff += stats[5];
                        stats[5] = 0;
                    }
                    DistributeDiff(diff, stats, picker, lucky);
                    break;
                #endregion

                #region Distribution of points favoring EMP and COOL
                case BuildType.EMP:
                    DistributeBase(points, lucky, min, stats, out baseline, out diff);
                    stats[8] += baseline / 2;
                    stats[3] += baseline / 2;
                    DistributeDiff(diff, stats, picker, lucky);
                    break;
                #endregion

                #region Distribution of points favoring REF and BODY
                case BuildType.INT:
                    DistributeBase(points, lucky, min, stats, out baseline, out diff);
                    stats[0] = baseline / 2;
                    stats[2] = baseline / 2;
                    DistributeDiff(diff, stats, picker, lucky);
                    break;
                #endregion

                #region Distribution of points favoring REF and BODY
                case BuildType.REF:
                    DistributeBase(points, lucky, min, stats, out baseline, out diff);
                    stats[1] = baseline / 2 + 1;
                    stats[7] = baseline / 2 - 1;
                    DistributeDiff(diff, stats, picker, lucky);
                    break;
                #endregion
            }
            return stats;
        }

        private void DistributeBase(int points, bool lucky, int? min, int[] stats, out int baseline, out int diff)
        {
            baseline = points / 4;
            if (baseline >= 20)
                baseline = 20;
            diff = points - baseline;
            diff += points % 4;
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = (int)min;
                diff -= (int)min;
            }
            if (!lucky)
            {
                diff += stats[5];
                stats[5] = 0;
            }
        }

        /// <summary>
        /// Distributes the difference.
        /// </summary>
        /// <param name="diff">The difference.</param>
        /// <param name="statList">The stat list.</param>
        /// <param name="picker">The picker.</param>
        /// <param name="lucky">if set to <c>true</c> [lucky].</param>
        /// <returns></returns>
        private int[] DistributeDiff(int diff, int[] statList, Random picker, bool lucky)
        {
            while (diff > 0)
            {
                int sub = picker.Next(8);
                if (statList[sub] < 10)
                {
                    if (statList[sub] < 10)
                    {
                        statList[sub]++;
                        diff--;
                    }
                    if (!lucky)
                    {
                        diff += statList[5];
                        statList[5] = 0;
                    }
                }
            }
            return statList;
        }

        /// <summary>
        /// Pulls the character's stats from a given list of integers
        /// </summary>
        /// <param name="l">The l.</param>
        private void PullFromList(List<int> l)
        {
            Intell = l[0];
            Reflex = l[1];
            Tech = l[2];
            Cool = l[3];
            Attract = l[4];
            Luck = l[5];
            Move = l[6];
            Body = l[7];
            Empathy = l[8];
        }
        #endregion
        #endregion
    }
}
