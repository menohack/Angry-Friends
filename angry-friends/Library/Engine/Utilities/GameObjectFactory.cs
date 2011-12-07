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
using Library.Engine.Object;
using Library.Engine.Component;

namespace Library.Engine.Utilities
{
    /// <summary>
    /// Assists in the creation of GameObjects.
    /// </summary>
    public class GameObjectFactory
    {
        private static GameObjectFactory gameObjectHelper;

        public static GameObjectFactory Instance
        {
            get
            {
                if (gameObjectHelper == null)
                {
                    gameObjectHelper = new GameObjectFactory();
                }

                return gameObjectHelper;
            }

            set
            {
                gameObjectHelper = value;
            }
        }

        private GameObjectFactory() { }

        public GameObject MakeDefaultGameObject()
        {
            return null;
        }

        public AudioComponent DefaultAudioComponent()
        {
            return null;   
        }
    }
}
