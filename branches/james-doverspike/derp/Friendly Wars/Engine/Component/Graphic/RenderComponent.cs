using System;
using System.Collections.Generic;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
		/// The animation that is currently playing.
		/// </summary>
		public Animation CurrentAnimation { get; private set; }

		/// <summary>
		/// The default animation for this RenderComponent.  It will play when no other animation is specified to play.
		/// </summary>
		public Animation DefaultAnimation { get; private set; }

		/// <summary>
		/// The EngineTimer that handles updating this RenderComponent's animations.
		/// </summary>
		private EngineTimer updateTimer;

		/// <summary>
		/// Constructor for a new RenderComponent.
		/// </summary>
		/// <param name="owner"> The owner of this RenderComponent. </param>
		/// <param name="animations">The Dictionary of names-to-Animations of this RenderComponent. </param>
		/// <param name="defaultAnimation">The animation to play when no other animation is specified to play.</param>
		public RenderComponent(GameObject owner, IDictionary<String, Animation> animations, Animation defaultAnimation) : base(owner)
		{
			this.Animations = animations;
			this.DefaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.DefaultAnimation;
			Play(this.DefaultAnimation.Name);
		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName)
		{
			Animation animation;
			Debug.Assert(!Animations.TryGetValue(animationName, out animation), "The Animation: " + animationName + " does not exist.");
			CurrentAnimation = animation;

			updateTimer = new EngineTimer(CurrentAnimation.FPS, new List<IUpdateable> { CurrentAnimation, this });
			updateTimer.Start();
		}

		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to stop playing.</param>
		public void Stop(String animationName)
		{
			Animation animation;
			Debug.Assert(!Animations.TryGetValue(animationName, out animation), "The Animation: " + animationName + " does not exist.");
			updateTimer.Stop();

			Play(DefaultAnimation.Name);
		}

		/// <summary>
		/// Notify the RenderComponent that it needs to be re-rendered.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds since the last Update.</param>
		public void Update(double deltaTime)
		{
			World.Instance.AddToRedrawQueue(base.Owner);
		}
	}
}
