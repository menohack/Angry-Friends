using System;
using System.Collections.Generic;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Utilities;
using System.Collections.ObjectModel;

namespace Friendly_Wars.Engine.Component.Graphic
{
	/// <summary>
	/// Handles the rendering of a GameObject. 
	/// </summary>
	public class RenderComponent : BaseComponent, IUpdateable
	{
		/// <summary>
		/// A Dictionary of &lt;String Animation&gt; that contains all of the animations of this RenderComponent.
		/// </summary>
		internal IDictionary<String, Animation> Animations { get; private set; }

		/// <summary>
		/// The current animation.
		/// </summary>
		public Animation CurrentAnimation { get; private set; }

		/// <summary>
		/// The default animation for this RenderComponent.
		/// </summary>
		public Animation DefaultAnimation { get; private set; }

		/// <summary>
		/// The timer that handles updating this RenderComponent.
		/// </summary>
		private static EngineTimer updateTimer;

		/// <summary>
		/// The width of the entire SilverLight canvas.
		/// </summary>
		public static readonly double CanvasWidth = MainPage.mainPage.LayoutRoot.Width;

		/// <summary>
		/// The height of the entire SilverLight canvas.
		/// </summary>
		public static readonly double CanvasHeight = MainPage.mainPage.LayoutRoot.Height;

		/// <summary>
		/// Constructor for a new RenderComponent.
		/// </summary>
		/// <param name="owner"> The owner of this RenderComponent. </param>
		/// <param name="animations">The Dictionary of names-to-Animations for this RenderComponent. </param>
		/// <param name="defaultAnimation">The animation to play when none is selected.</param>
		public RenderComponent(GameObject owner, IDictionary<String, Animation> animations, Animation defaultAnimation) : base(owner)
		{
			this.Animations = animations;
			this.DefaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.DefaultAnimation;
			//Play(this.defaultAnimation.name);
		}

		//TODO: Should these play methods return a bool or throw exceptions? I'm leaning towards exceptions because it is a mistake.
		/// <summary>
		/// Adds a new animation.
		/// </summary>
		/// <param name="animation"> The new animation. </param>
		/// <returns> True if the animation was added, false if it already exists. </returns>
		public bool AddAnimation(Animation animation)
		{
			KeyValuePair<String, Animation> value = new KeyValuePair<String, Animation>(animation.Name, animation);
			if (Animations.Contains(value))
				return false;

			Animations.Add(value);
			return true;
		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName)
		{
			Animation animation;

			//TODO: Exception handling?
			if (!Animations.TryGetValue(animationName, out animation))
				return;

			CurrentAnimation = animation;

			//updateTimer = new EngineTimer(CurrentAnimation.FPS, new List<IUpdateable> { this });
			CurrentAnimation.Play();

			//Collection<IUpdateable> listeners = new Collection<IUpdateable>();
			//listeners.Add(this);
			updateTimer = new EngineTimer(animation.Length, this);
			updateTimer.Start();
		}

		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to stop playing.</param>
		public void Stop(String animationName)
		{
			Animation animation;
			Animations.TryGetValue(animationName, out animation);

			if (CurrentAnimation.Name == animation.Name)
			{
				Play(DefaultAnimation.Name);
				updateTimer.Stop();
			}
		}

		/// <summary>
		/// Updates the frame.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds since the last Update.</param>
		public void Update(double deltaTime)
		{
			CurrentAnimation.UpdateFrame(deltaTime);
			//World.AddToRedrawQueue(base.owner);
		}
	}
}
