using System;
using System.Collections.Generic;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Ultilities;

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
		internal IDictionary<String, Animation> animations { get; private set; }

		/// <summary>
		/// The current animation.
		/// </summary>
		public Animation currentAnimation { get; private set; }

		/// <summary>
		/// The default animation for this RenderComponent.
		/// </summary>
		public Animation defaultAnimation { get; private set; }

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
			this.animations = animations;
			this.defaultAnimation = defaultAnimation;
			currentAnimation = this.defaultAnimation;
			//Play(this.defaultAnimation.name);
		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName)
		{
			Animation animation;
			animations.TryGetValue(animationName, out animation);
			currentAnimation = animation;

			updateTimer = new EngineTimer(currentAnimation.FPS, new List<IUpdateable> { this });
			currentAnimation.Play();
		}

		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to stop playing.</param>
		public void Stop(String animationName)
		{
			Animation animation;
			animations.TryGetValue(animationName, out animation);

			if (currentAnimation.name == animation.name)
			{
				Play(defaultAnimation.name);
				updateTimer.Stop();
			}
		}

		/// <summary>
		/// Updates the frame.
		/// </summary>
		/// <param name="deltaTime">The time since the last Update.</param>
		public void Update(double deltaTime)
		{
			currentAnimation.UpdateFrame(deltaTime);
			//World.AddToRedrawQueue(base.owner);
		}
	}
}
