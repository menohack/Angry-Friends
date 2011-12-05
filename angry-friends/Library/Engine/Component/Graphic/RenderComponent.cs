using System;
using System.Collections.Generic;
using System.Diagnostics;
using Library.Engine.Object;
using Library.Engine.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization;
namespace Library.Engine.Component.Graphic {
	/// <summary>
	/// Handles the rendering of an Object. 
	/// </summary>
	[DataContract]
    public class RenderComponent : BaseComponent, IUpdateable {
		/// <summary>
		/// A Dictionary of names-to-Animations that contains all of the Animations of this RenderComponent.
		/// </summary>
        [DataMember]
        private IDictionary<String, Animation> animations;

		/// <summary>
		/// The Animation that is currently playing.
		/// </summary>
        [DataMember]
        public Animation CurrentAnimation { get; private set; }

		/// <summary>
		/// The default Animation for this RenderComponent.  It will play when no other Animation is specified to play.
		/// </summary>
        [DataMember]
        private Animation defaultAnimation;

		/// <summary>
		/// The EngineTimer that handles updating this RenderComponent's Animations.
		/// </summary>
        [DataMember]
        private EngineTimer animationTimer;

		/// <summary>
		/// Constructor for a new RenderComponent.
		/// </summary>
		/// <param name="animations">The Dictionary of names-to-Animations of this RenderComponent. </param>
		/// <param name="defaultAnimation">The Animation to play when no other Animation is specified to play.</param>
		/// <param name="owner">The GameObject that owns this RenderComponent.</param>
		public RenderComponent(IDictionary<String, Animation> animations, Animation defaultAnimation, GameObject owner) : base(owner) {
			this.animations = animations;
			this.defaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.defaultAnimation;
			Play(this.defaultAnimation.Name);
		}

		/// <summary>
		/// Constructor for a RenderComponent with one animation.
		/// </summary>
        /// <param name="animation">The single Animation for this RenderComponent.</param>
        /// <param name="owner">The GameObject that owns this RenderComponent.</param>
		public RenderComponent(Animation animation, GameObject owner) : this(new Dictionary<String, Animation> { {animation.Name, animation} }, animation, owner) {}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName) {
			Animation animation;
			Debug.Assert(animations.TryGetValue(animationName, out animation), "The Animation: " + animationName + " does not exist.");
			CurrentAnimation = animation;

            if (CurrentAnimation.FPS != 0)
            {
                animationTimer = new EngineTimer(CurrentAnimation.FPS, new List<IUpdateable> { CurrentAnimation, this });
                animationTimer.Start();
            }
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
		/// Notify the World that this RenderComponent needs to be re-rendered.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds since the last Update.</param>
        public void Update(double deltaTime)
        {
            World.Instance.AddToRedrawQueue(Owner);
        }
	}
}