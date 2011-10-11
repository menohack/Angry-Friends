using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
namespace Stix {
	public partial class MainPage : UserControl {
		public static MainPage page;
		Player player1;

		public MainPage() {
			// Initializes the window
			InitializeComponent();
			// Share this instance statically for printing
			page = this;
			// Removes default right-click Silverlight context menu
			this.MouseRightButtonDown += (s, e) => { e.Handled = true; };

			Get.Start(delegate {
				player1 = new Player(LayoutRoot, 150, -100);
				canvas.HorizontalAlignment = HorizontalAlignment.Center;
				canvas.VerticalAlignment = VerticalAlignment.Center;
				canvas.Source = Get.Map;
				DispatcherTimer timer = new DispatcherTimer();
				timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
				timer.Tick+=(s,e)=>{
					player1.Do();
				};
				timer.Start();
			});
		}

		protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e) {
			if (e.Key == System.Windows.Input.Key.Left) {
				player1.Push(20);
			}
			else if (e.Key == System.Windows.Input.Key.Right) {
				player1.Push(-20);
			}
		}
	}
	public class Get {
		public static Dictionary<string, WriteableBitmap> Image;
		static string Path = "http://choosah.com/facebook/";
		public static void Start(Action Loaded) {
			MainPage.page.console.Text = "History";
			Image = new Dictionary<string, WriteableBitmap>();
			System.Net.WebClient w = new System.Net.WebClient();
			w.DownloadStringCompleted += (s, e) => {
				List<string> res = new List<string>(e.Result.Split('\n'));
				UnrollResources(res, 0, delegate {
					Map = Image["map_0"];
					//Map.FillEllipse(500, 300, 100, 100, Color.FromArgb(255, 255, 255, 255));
					Loaded();
				});
			};
			w.DownloadStringAsync(new Uri(Path + "resources"));
		}
		static void UnrollResources(List<string> resources, int index, Action Loaded) {
			string[] res = resources[index].Split(' ');
			PrintLine(string.Format("Unrolling {0}: {1}", res[1], res[0]));
			MultiLoadImage(res[0], Convert.ToInt32(res[1]), delegate {
				if (index == resources.Count - 1) { Loaded(); }
				else UnrollResources(resources, index + 1, Loaded);
			});
		}
		public static void MultiLoadImage(string name, int count, Action a) {
			string filename = name + "_" + count.ToString();
			PrintLine(string.Format("\tLoading: {0} ... ", filename));
			LoadImage(filename + ".png", x => {
				Print("OK");
				Image.Add(filename, new WriteableBitmap(x));
				if (count == 0) { if (a != null) a(); }
				else MultiLoadImage(name, count - 1, a);
			});
		}
		public static void LoadImage(string URL, Action<BitmapImage> w) {
			var bi = new BitmapImage(new Uri(Path + URL)) { CreateOptions = BitmapCreateOptions.None };
			bi.ImageOpened += (s, e) => w(bi);
		}
		public static Color Pixel(double x, double y) {
			x = Map.PixelWidth / 2 + x;
			y = Map.PixelHeight / 2 + y + 37.5;
			return Map.GetPixel((int)x, (int)y);
		}
		public static void Print(string msg) {
			MainPage.page.console.Text += msg;
		}
		public static void PrintLine(string msg) {
			MainPage.page.console.Text += Environment.NewLine + msg;
		}
		public static void Show(string msg) {
			MainPage.page.console.Text = msg;
		}
		public static WriteableBitmap Map;
	}
	public class Player {
		Image sprite = new Image();
		double x = 0, y = 0, slide = 0, inclination;
		public Point Location {
			get {
				return new Point(x, y);
			}
			set {
				x = value.X;
				y = value.Y;
				sprite.Margin = new Thickness(value.X, value.Y, -value.X, -value.Y);
			}
		}
		public double X {
			get { return x; }
			set {
				x = value;
				Location = new Point(x, y);
			}
		}
		public double Y {
			get { return y; }
			set {
				y = value;
				Location = new Point(x, y);
			}
		}
		public Player(Grid layout, double x, double y) {
			sprite.Source = Get.Image["player_0"];
			this.sprite.Stretch = Stretch.None;
			Location = new Point(x, y);
			layout.Children.Add(sprite);
		}
		bool Falling(double x, double y) {
			return Get.Pixel(x, y).Equals(Color.FromArgb(255, 255, 255, 255));
		}
		public void Do() {

			try {
				double Y = y, X = x, speed = 3, gravity = 10;
				if (Falling(X, Y)) Y += gravity;
				if (Math.Abs(slide) > 2) {
					if (slide > 0) {
						X -= speed;
						slide -= speed;
					}
					else {
						X += speed;
						slide += speed;
					}
				}
				else slide = 0;
				while (!Falling(X, Y)) {
					Y--;
					if (Y < y - 40) {
						Y = y;
						X = x;
						slide = 0;
						break;
					}
				}
				if (slide != 0) {
					var r = new RotateTransform();
					r.CenterX = 37.5;
					r.CenterY = 75;
					r.Angle = Math.Atan2(Y - y, X - x) * 180 / Math.PI;
					if (r.Angle > -90 && r.Angle < 90) {
						sprite.FlowDirection = FlowDirection.LeftToRight;
					}
					else {
						sprite.FlowDirection = FlowDirection.RightToLeft;
						r.Angle = 180 - r.Angle;
					}
					inclination = r.Angle;
					sprite.RenderTransform = r;
				}
				Location = new Point(X, Y);
			}
			catch {
				Get.Show("Out of bounds");
			}
		}
		public void Push(double X) {
			slide = X;
		}
	}
}