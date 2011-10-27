using System;
using System.Collections.Generic;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Utilities;

namespace Friendly_Wars.Engine.Component.Graphic
{
	/// <summary>
	/// Handles the rendering of a GameObject. 
	/// </summary>
	public class RenderComponent : BaseComponent, IUpdateable
	{
		/// <summary>
		/// A Dictionary of <String, Animation> that contains all of the animations of this RenderComponent.
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
		/// Constructor for a new RenderComponent.
		/// </summary>
		/// <param name="owner"> The owner of this RenderComponent. </param>
		/// <param name="animations">The Dictionary of names-to-Animations for this RenderComponent. </param>
		public RenderComponent(GameObject owner, IDictionary<String, Animation> animations, Animation defaultAnimation) : base(owner)
		{
			this.Animations = animations;
			this.DefaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.DefaultAnimation;
			//Play(this.defaultAnimation.name);
		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName)
		{
			Animation animation;
		    Animations.TryGetValue(animationName, out animation);
			CurrentAnimation = animation;

			updateTimer = new EngineTimer(CurrentAnimation.FPS, new List<IUpdateable> { this });
			CurrentAnimation.Play();
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
		/// <param name="deltaTime">The time since the last Update.</param>
		public void Update(double deltaTime)
		{
			CurrentAnimation.UpdateFrame(deltaTime);
			//World.AddToRedrawQueue(base.owner);
		}
	}
}
