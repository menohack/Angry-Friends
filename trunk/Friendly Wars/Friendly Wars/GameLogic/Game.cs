
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Utilities;
using Friendly_Wars.Engine.Component.Graphic;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.Windows;

namespace Friendly_Wars.GameLogic
{
	/// <summary>
	/// This class represents the Game. All game-logic stems from here.
	/// </summary>
	public class Game
	{
		/// <summary>
		/// The world of the game.
		/// </summary>
		private World world;

		/// <summary>
		/// The terrain of the game.
		/// </summary>
		public Terrain terrain {get; private set; }

		/// <summary>
		/// The Web of the game.
		/// </summary>
		public Web web { get; private set; }

		/// <summary>
		/// Constructor for the Game.
		/// </summary>
		public Game()
		{
			world = new World("Friendly Wars");
			web = new Web();

			//
			//THIS IS TEMPORARY FOR TESTING
			//
			int canvasWidth = (int)RenderComponent.CanvasWidth;
			int canvasHeight = (int)RenderComponent.CanvasHeight;

			//Build a temporary map
			WriteableBitmap drawMe = new WriteableBitmap(canvasWidth, canvasHeight);

			for (int j = 0; j < canvasHeight; j++)
				for (int i = 0; i < canvasWidth; i++)
					if (j > (canvasHeight * 2) / 3)
						drawMe.Pixels[j * canvasWidth + i] = ConvertToARGB32(Color.FromArgb(255, 0, 200, 0));

			drawMe.Invalidate();

			Image slopedBackground = new Image();
			slopedBackground.Source = drawMe;

			terrain = new Terrain(slopedBackground, "Level 1");
			world.AddGameObject(terrain);


			GameObject projectile = new GameObject("projectile");
			world.AddGameObject(projectile);
			world.AddToRedrawQueue(projectile);

			//Build a temporary projectile image

			int width = 200;
			int height = 200;

			double xOffset = 0.0;
			double yOffset = 0.0;

			int numFrames = 25;
			double duration = 1.0;
			List<Frame> frames = new List<Frame>(5);
			for (int f = 0; f < numFrames; f++)
			{
				drawMe = new WriteableBitmap(width, height);

				Point center = new Point(width / 2.0, height / 2.0);
				double radius;
				if (f < numFrames / 2)
					radius = ((double)2 * f / numFrames) * 50 + 20;
				else
					radius = (1.0 - 2.0*(f - numFrames/2)/numFrames) * 50 + 20;
				for (int j = 0; j < height; j++)
					for (int i = 0; i < width; i++)
					{
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


				
			}

			//TODO: Figure out why 0,0 corresponds to the center of the screen
			projectile.AddAnimation(new Animation(frames, 1000*duration/numFrames, 60, "grow"));
			projectile.Play("grow");

			world.Start();
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
