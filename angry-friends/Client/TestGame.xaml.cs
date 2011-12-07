using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Library.Engine.Object;

namespace Client
{
    public partial class TestGame : UserControl
    {
        public TestGame()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// The hook for the game.
        /// </summary>
        public void Initialize() {
            new EngineObjectHelper(canvas);
        }
    }
}
