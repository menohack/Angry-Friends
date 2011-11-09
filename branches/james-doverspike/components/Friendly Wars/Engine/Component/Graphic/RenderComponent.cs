﻿using System;
using System.Collections.Generic;
using Friendly_Wars.Engine.Object;
using Friendly_Wars.Engine.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Friendly_Wars.Engine.Component.Graphic
{
	/// <summary>
	/// Handles the rendering of an Object. 
	/// </summary>
	public class RenderComponent : IUpdateable
	{
		/// <summary>
		/// A Dictionary of names-to-Animations that contains all of the Animations of this RenderComponent.
		/// </summary>
		private IDictionary<String, Animation> animations;
		/// <summary>
		/// The Animation that is currently playing.
		/// </summary>
		public Animation CurrentAnimation { get; private set; }
		/// <summary>
		/// The default Animation for this RenderComponent.  It will play when no other Animation is specified to play.
		/// </summary>
		private Animation defaultAnimation;
		/// <summary>
		/// The EngineTimer that handles updating this RenderComponent's Animations.
		/// </summary>
		private EngineTimer animationTimer;

		/// <summary>
		/// Constructor for a new RenderComponent.
		/// </summary>
		/// <param name="animations">The Dictionary of names-to-Animations of this RenderComponent. </param>
		/// <param name="defaultAnimation">The Animation to play when no other Animation is specified to play.</param>
		public RenderComponent(IDictionary<String, Animation> animations, Animation defaultAnimation)
		{
			this.animations = animations;
			this.defaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.defaultAnimation;
			Play(this.defaultAnimation.Name);
		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName)
		{
			Animation animation;
			Debug.Assert(!animations.TryGetValue(animationName, out animation), "The Animation: " + animationName + " does not exist.");
			CurrentAnimation = animation;

			animationTimer = new EngineTimer(CurrentAnimation.FPS, new List<IUpdateable> { CurrentAnimation, this });
			animationTimer.Start();
		}

		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to stop playing.</param>
		public void Stop(String animationName)
		{
			Animation animation;
			Debug.Assert(!animations.TryGetValue(animationName, out animation), "The Animation: " + animationName + " does not exist.");
			animationTimer.Stop();

			Play(defaultAnimation.Name);
		}

		/// <summary>
		/// Notify the World that this RenderComponent needs to be re-rendered.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds since the last Update.</param>
		public void Update(double deltaTime)
		{
			World.Instance.AddToRedrawQueue(this);
		}
	}
}