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

namespace Client
{
    /// <summary>
    /// Assists in dealing with the different states of our GUI/Logic.
    /// </summary>
    public class GUIStateHelper
    {
        /// <summary>
        /// The instance of this GUIStateHelper.
        /// </summary>
        private static GUIStateHelper instance;

        /// <summary>
        /// The instance of this GUIStateHelper.
        /// </summary>
        public static GUIStateHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GUIStateHelper();
                }
                return instance;
            }
        }

        /// <summary>
        /// The position of this GUIStateHelper.
        /// </summary>
        private int position;

        /// <summary>
        /// The private constructor for the GUIStateHelper.
        /// </summary>
        private GUIStateHelper() {
            position =-1;
        }

        /// <summary>
        /// Switches to the next UserControl defined.
        /// </summary>
        public void NextUserControl()
        {
            position++;
            SwitchUserControl();
        }

        /// <summary>
        /// Switches to the previous UserControl defined.
        /// </summary>
        public void PreviousUserControl()
        {
            position--;
            SwitchUserControl();
        }

        /// <summary>
        /// Switches to the current userControl.
        /// </summary>
        private void SwitchUserControl()
        {
            UserControl userControl = new UserControl();
            Grid root = App.Current.RootVisual as Grid;
            root.Children.Clear();

            switch (position) {
                case 0:
                    userControl = new LoadingScreen();
                    break;
                case 1:
                    userControl = new Lobby();
                    break;
                case 2:
                    userControl = new GameLobby();
                    break;
                case 3:
                    userControl = new Game();
                    root.KeyDown += (s, e) => userControl.GetType().InvokeMember("MyKeyDown", System.Reflection.BindingFlags.InvokeMethod, null, userControl, new object[] { e });
                    root.KeyUp += (s, e) => userControl.GetType().InvokeMember("MyKeyUp", System.Reflection.BindingFlags.InvokeMethod, null, userControl, new object[] { e });
                    break;
            }

            root.Children.Add(userControl);
        }
    }
}
