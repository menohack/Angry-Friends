using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Library.Engine.Component.Graphic;
using Library.Engine.Object;
using Library.Engine.Component;
using Library.Engine;


namespace Library.GameLogic {
	/// <summary>
	/// This class represents the Game. All game-logic stems from here.
	/// </summary>
	public class Game {

		/// <summary>
		/// The instance of the Engine.
		/// </summary>
		private World world;

        /// <summary>
        /// Accessor for the World.
        /// </summary>
		public World World { get { return world; } }

        /// <summary>
        /// The viewport of this Game.
        /// </summary>
        public static Canvas Viewport { get; private set; }

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
		public Game(Canvas viewport) {
			Input.Instance.AimEvent += new Input.AimEventHandler(Instance_AimEvent);
            Viewport = viewport;
			world = World.Instance;

			GameObject go1 = new GameObject("derf");

			int width = 50;
			int height = 50;
			WriteableBitmap wb = new WriteableBitmap(width, height);
			for (int i=0; i < width*height; i++)
				wb.Pixels[i] = ConvertToARGB32(Color.FromArgb(255, 255, 0, 0));
			wb.Invalidate();
			Image box = new Image();
			box.Source = wb;
			go1.RenderComponent = new RenderComponent(new Animation(new Frame(box, new Point(0, 0)), "default"), go1);
			go1.TransformComponent = new TransformComponent(new Point(100, 0), 0, new Point(width, height), go1);
			go1.TransformComponent.Velocity = new Point(50, 0);

			
			GameObject go2 = new GameObject("herf");
			wb = new WriteableBitmap(width, height);
			for (int i = 0; i < width * height; i++)
				wb.Pixels[i] = ConvertToARGB32(Color.FromArgb(255, 0, 255, 0));
			wb.Invalidate();
			Image box2 = new Image();
			box2.Source = wb;
			go2.RenderComponent = new RenderComponent(new Animation(new Frame(box2, new Point(0, 0)), "default"), go2);
			go2.TransformComponent = new TransformComponent(new Point(300, 0), 0, new Point(width, height), go2);
			go2.TransformComponent.Velocity = new Point(0,0);
			
			
			GameObject go3 = new GameObject("herf2electricboogaloo");
			wb = new WriteableBitmap(width, height);
			for (int i = 0; i < width * height; i++)
				wb.Pixels[i] = ConvertToARGB32(Color.FromArgb(255, 0, 255, 0));
			wb.Invalidate();
			Image box3 = new Image();
			box3.Source = wb;
			go3.RenderComponent = new RenderComponent(new Animation(new Frame(box3, new Point(0, 0)), "default"), go3);
			go3.TransformComponent = new TransformComponent(new Point(200, 0), 0, new Point(width, height), go3);
			go3.TransformComponent.Velocity = new Point(0,-2);

			GameObject go4 = new GameObject("herf2electricboogaloo2");
			wb = new WriteableBitmap(width, height);
			for (int i = 0; i < width * height; i++)
				wb.Pixels[i] = ConvertToARGB32(Color.FromArgb(255, 0, 255, 0));
			wb.Invalidate();
			Image box4 = new Image();
			box4.Source = wb;
			go4.RenderComponent = new RenderComponent(new Animation(new Frame(box4, new Point(0, 0)), "default"), go4);
			go4.TransformComponent = new TransformComponent(new Point(250, 150), 0, new Point(width, height), go4);
			go4.TransformComponent.Velocity = new Point(0, -20);
			
			
		}

		void Instance_AimEvent(UIElement sender, AimEventArgs e)
		{
			
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