using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Model.GameLogic.HighScores
{
    /// <summary>
    /// Represents a user's HighScore in our game.
    /// </summary>
    public class HighScore
    {

        /// <summary>
        /// The name of the user playing our game.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// The user's score in our game.
        /// </summary>
        public Double Score { get; private set; }

        /// <summary>
        /// Constructor for a new HighScore.
        /// </summary>
        /// <param name="name">The name of the user playing our game.</param>
        /// <param name="score">The user's score in our game.</param>
        public HighScore(String name, Double score)
        {
            this.Name = name;
            this.Score = score;
        }
    }
}
