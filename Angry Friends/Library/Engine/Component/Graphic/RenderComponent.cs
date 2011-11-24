﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Library.Engine.Object;
using Library.Engine.Utilities;
using System.Windows;
using System.Windows.Controls;
namespace Library.Engine.Component.Graphic {
	/// <summary>
	/// Handles the rendering of an Object. 
	/// </summary>
	public class RenderComponent : BaseComponent, IUpdateable {
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
		/// <param name="owner">The GameObject that owns this RenderComponent.</param>
		public RenderComponent(IDictionary<String, Animation> animations, Animation defaultAnimation, GameObject owner)
			: base(owner) {
			this.animations = animations;
			this.defaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.defaultAnimation;
			Play(this.defaultAnimation.Name);
		}

		/// <summary>
		/// Constructor for a RenderComponent with one animation.
		/// </summary>
		/// <param name="defaultAnimation">The default animation to play.</param>
		/// <param name="owner">The owner of this RenderComponent.</param>
		public RenderComponent(Animation defaultAnimation, GameObject owner)
			: base(owner)
		{
			this.animations = new Dictionary<String, Animation>();
			this.animations.Add("default", defaultAnimation);
			this.defaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.defaultAnimation;
			Play(this.defaultAnimation.Name);
		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName) {
			Animation animation;
			Debug.Assert(animations.TryGetValue(animationName, out animation), "The Animation: " + animationName + " does not exist.");
			CurrentAnimation = animation;

			animationTimer = new EngineTimer(CurrentAnimation.FPS, new List<IUpdateable> { CurrentAnimation, this });
			animationTimer.Start();
		}
		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to stop playing.</param>
		public void Stop(String animationName) {
			Animation animation;
			Debug.Assert(!animations.TryGetValue(animationName, out animation), "The Animation: " + animationName + " does not exist.");
			animationTimer.Stop();

			Play(defaultAnimation.Name);
		}
		/// <summary>
		/// Accesses the current Frame of the current Animation.
		/// </summary>
		/// <returns>The current frame.</returns>
		public Frame GetRenderContent() {
			return CurrentAnimation.CurrentFrame;
		}
		/// <summary>
		/// Notify the World that this RenderComponent needs to be re-rendered.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds since the last Update.</param>
		public void Update(double deltaTime) {
			Point pos = Owner.TransformComponent.Update(deltaTime);

			CurrentAnimation.CurrentFrame.Image.SetValue(Canvas.LeftProperty, pos.X);
			CurrentAnimation.CurrentFrame.Image.SetValue(Canvas.TopProperty, pos.Y);

			World.Instance.AddToRedrawQueue(Owner);
		}
	}
}