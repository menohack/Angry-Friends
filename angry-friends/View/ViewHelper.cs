﻿using System;
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

namespace View
{
    /// <summary>
    /// Assists in dealing with the different states of our GUI/Logic.
    /// </summary>
    public class ViewHelper
    {
        /// <summary>
        /// The instance of this ViewHelper.
        /// </summary>
        private static ViewHelper instance;

        /// <summary>
        /// The instance of this ViewHelper.
        /// </summary>
        public static ViewHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ViewHelper();
                }
                return instance;
            }
        }

        /// <summary>
        /// The position of this ViewHelper.
        /// </summary>
        private int position;

        /// <summary>
        /// The private constructor for the ViewHelper.
        /// </summary>
        private ViewHelper() {
            position = -1;
        }

        /// <summary>
        /// Access the current drawable root.
        /// </summary>
        /// <returns>Returns the current drawable root.</returns>
        public UIElement GetRootDrawable()
        {
            return (App.Current.RootVisual);
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
            Grid root = App.Current.RootVisual as Grid;
            root.Children.Clear();

            UserControl userControl = new UserControl();

            switch (position) {
                case 0:
                    userControl = new LoadingScreen();
                    break;
                case 1:
                    userControl = new MainMenu();
                    break;
                case 2:
                    userControl = new Game();
                    root.KeyDown += (s, e) => userControl.GetType().InvokeMember("MyKeyDown", System.Reflection.BindingFlags.InvokeMethod, null, userControl, new object[] { e });
                    root.KeyUp += (s, e) => userControl.GetType().InvokeMember("MyKeyUp", System.Reflection.BindingFlags.InvokeMethod, null, userControl, new object[] { e });
                    break;
            }

            root.Children.Add(userControl);
        }
    }
}
