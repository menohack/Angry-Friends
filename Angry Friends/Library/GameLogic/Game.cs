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
            Viewport = viewport;
			world = World.Instance;

			//
			//THIS IS TEMPORARY FOR TESTING
			//

			int width = 200;
			int height = 200;

			double xOffset = 0.0;
			double yOffset = 0.0;

			int numFrames = 25;
			double duration = 1.0;
			List<Frame> frames = new List<Frame>(5);
			WriteableBitmap drawMe;
			for (int f = 0; f < numFrames; f++) {
				drawMe = new WriteableBitmap(width, height);

				Point center = new Point(width / 2.0, height / 2.0);
				double radius;
				if (f < numFrames / 2)
					radius = ((double)2 * f / numFrames) * 50 + 20;
				else
					radius = (1.0 - 2.0 * (f - numFrames / 2) / numFrames) * 50 + 20;
				for (int j = 0; j < height; j++)
					for (int i = 0; i < width; i++) {
						double x = Math.Abs(i - center.X);
						double y = Math.Abs(j - center.Y);
						double distance = Math.Sqrt(x * x + y * y);
						if (distance < radius)
							drawMe.Pixels[j * width + i] = ConvertToARGB32(Color.FromArgb(255, 0, 0, 200));
					}

			    drawMe.Invalidate();

				Image image = new Image();
				image.Source = drawMe;
				image.Width = width;
				image.Height = height;

				frames.Insert(f, new Frame(image, new Point(xOffset, yOffset)));
                new Tests();
			}

			Dictionary<String, Animation> animations = new Dictionary<String, Animation>();
			Animation animation = new Animation(frames, 1000 * duration / numFrames, 60, "grow");
			animations.Add("grow", animation);

			GameObject go2 = new GameObject("derf");

			width = 50;
			height = 50;
			WriteableBitmap wb = new WriteableBitmap(width, height);
			for (int i=0; i < width*height; i++)
				wb.Pixels[i] = ConvertToARGB32(Color.FromArgb(255, 255, 0, 0));
			wb.Invalidate();
			Image box = new Image();
			box.Source = wb;
			go2.RenderComponent = new RenderComponent(new Animation(new Frame(box, new Point(0, 0)), "default"), go2);
			go2.TransformComponent = new TransformComponent(new Point(475, 0), 0, new Point(50, 50), go2);

			GameObject go3 = new GameObject("herf");
			wb = new WriteableBitmap(width, height);
			for (int i = 0; i < width * height; i++)
				wb.Pixels[i] = ConvertToARGB32(Color.FromArgb(255, 0, 255, 0));
			wb.Invalidate();
			Image box2 = new Image();
			box2.Source = wb;
			go3.RenderComponent = new RenderComponent(new Animation(new Frame(box2, new Point(0, 0)), "default"), go3);
			go3.TransformComponent = new TransformComponent(new Point(550, 0), 0, new Point(50, 50), go3);
			
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