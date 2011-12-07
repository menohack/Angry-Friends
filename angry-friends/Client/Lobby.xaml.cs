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
using System.Windows.Navigation;
using Library.GameLogic;

namespace Client
{
	public partial class Lobby : Page
	{

		public Lobby()
		{
			InitializeComponent();

			GUI gui = new GUI(listbox, party, playButton);
			//GUI gui = new GUI(LayoutRoot);
			gui.PlayGame += new GUI.PlayGameHandler(gui_PlayGame);
		}

		void gui_PlayGame()
		{
			App.screen.Children.Clear();
			App.screen.Children.Add(new MainPage());
		}

		private void party_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
