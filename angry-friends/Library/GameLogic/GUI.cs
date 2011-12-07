using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Generic;

namespace Library.GameLogic
{

	public class Portrait : StackPanel
	{
		private Image image;
		private TextBlock name;

		public Portrait(Image image, string name) : base()
		{
			this.image = image;
			this.name = new TextBlock();
			this.name.Text = name;

			this.Orientation = Orientation.Vertical;

			this.Children.Add(image);
			this.Children.Add(this.name);
		}
	}

	public class Row : StackPanel
	{
		private static readonly int NUM_ITEMS = 5;
		private List<StackPanel> items;

		public Row(Portrait friend1, Portrait friend2)
		{
			this.Children.Add(friend1);
			this.Children.Add(friend2);

			this.Orientation = Orientation.Horizontal;
		}
	}

	public class GUI
	{
		private ListBox availablePlayers;
		private ListBox currentPlayers;
		private Button playButton;


		public event PlayGameHandler PlayGame;
		public delegate void PlayGameHandler();

		public GUI(ListBox available, ListBox current, Button play)
		{
			availablePlayers = available;
			currentPlayers = current;
			playButton = play;

			int width = 50;
			int height = 50;
			WriteableBitmap wb1 = new WriteableBitmap(width, height);
			for (int i = 0; i < width * height; i++)
				wb1.Pixels[i] = ConvertToARGB32(Colors.Blue);
			wb1.Invalidate();
			WriteableBitmap wb2 = new WriteableBitmap(width, height);
			wb2.FromByteArray(wb1.ToByteArray());

			Image box1 = new Image();
			box1.Source = wb1;
			Image box2 = new Image();
			box2.Source = wb2;
			

			//Row row1 = new Row(new Portrait(box1, "Alex"), new Portrait(box2, "Max"));

			available.Items.Add(new Portrait(box1, "Alex"));
			current.Items.Add(new Portrait(box2, "Max"));


			available.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(available_MouseLeftButtonDown);

			available.DragLeave += new System.Windows.DragEventHandler(available_DragLeave);

			current.AllowDrop = true;
			current.DragEnter += new System.Windows.DragEventHandler(available_DragEnter);
			current.Drop += new System.Windows.DragEventHandler(current_Drop);
			current.DragLeave += new System.Windows.DragEventHandler(current_DragLeave);

			//panel.Children.Add(row1);

			//panel.MouseEnter += new System.Windows.Input.MouseEventHandler(panel_MouseEnter);
			

			playButton.Click += new System.Windows.RoutedEventHandler(playButton_Click);
		}

		void current_DragLeave(object sender, System.Windows.DragEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		void current_Drop(object sender, System.Windows.DragEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		void available_DragLeave(object sender, System.Windows.DragEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		void available_DragEnter(object sender, System.Windows.DragEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		void available_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			object nig = e.OriginalSource;
			int t = 2;
		}

		void panel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			
		}

		public GUI(Grid layoutRoot)
		{
			//this might be a bad idea
		}

		void playButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (PlayGame != null)
				PlayGame();
		}

		/// <summary>
		/// TEMPORARY HELPER FUNCTION, DONT KILL ME ALEX
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		private int ConvertToARGB32(Color color)
		{
			return ((color.R << 16) | (color.G << 8) | (color.B << 0) | (color.A << 24));
		}
	}
}
