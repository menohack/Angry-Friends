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

using Friendly_Wars.Engine.Object;
using Friendly_Wars.GameLogic;

namespace Friendly_Wars
{
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// This class represents our Silverlight page.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            ///terrain is an image in the xaml. I'm not sure how to draw an image without doing this.
            ///Change it if you figure it out.
            //new Game(terrain);
            new World(World.WORLD_NAME);
        }
    }
}
