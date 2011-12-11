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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Engine.Utilities;

namespace Model.GameLogic.HighScores
{
    /// <summary>
    /// Manages seralizing HighScores to and from the Server.
    /// </summary>
    public class HighScoreManager
    {

        /// <summary>
        /// The singleton instance of this HighScoreManager.
        /// </summary>
        private static HighScoreManager instance;

        /// <summary>
        /// The singleton accessor of this HighScoreManager.
        /// </summary>
        public static HighScoreManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HighScoreManager();
                }

                return instance;
            }
        }

        /// <summary>
        /// The private constructor for the HighScoreManager.
        /// </summary>
        private HighScoreManager() {}

        /// <summary>
        /// Adds a high score to the server.
        /// </summary>
        /// <param name="highScore">The high score to add to the server.</param>
        public void AddHighScore(HighScore highScore)
        {
            AssetManager.Instance.Download("http://luisgrimaldo.com/angryfriends/room.php?action=get&id=1", ExternalAsset.ExternalAssetType.String, (e) =>
            {
                String list = e.GetString();
                var result = list.Split(';')[2];
                var r = result + highScore.Name + " " + highScore.Score;
                AssetManager.Instance.Download("http://luisgrimaldo.com/angryfriends/room.php?action=set&id=1&players="+r, ExternalAsset.ExternalAssetType.String, (f) => {});
            });
        }

        /// <summary>
        /// Access all of the high scores from the server.
        /// </summary>
        /// <param name="onLoaded">The high scores from the server.</param>
        public void GetHighScores(Action<String> onLoaded)
        {
            AssetManager.Instance.Download("http://luisgrimaldo.com/angryfriends/room.php?action=get&id=1", ExternalAsset.ExternalAssetType.String, (e) =>
            {
                onLoaded(e.GetString().Split(';')[2]);
            });
        }
    }
}