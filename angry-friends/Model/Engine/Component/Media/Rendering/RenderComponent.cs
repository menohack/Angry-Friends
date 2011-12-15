using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Model.Engine.Object;
using Model.Engine.Utilities;

namespace Model.Engine.Component.Media.Rendering {

	/// <summary>
	/// Handles the rendering of a GameObject. 
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
        /// Determines if the renderer should flip this rendercomponent with respect to the Y-Axis.
        /// </summary>
        public bool ShouldFlip { get; set; }

		/// <summary>
		/// Constructor for a new RenderComponent.
		/// </summary>
		/// <param name="animations">The Dictionary of names-to-Animations of this RenderComponent. </param>
		/// <param name="defaultAnimation">The Animation to play when no other Animation is specified to play.</param>
		/// <param name="owner">The GameObject that owns this RenderComponent.</param>
		public RenderComponent(IDictionary<String, Animation> animations, Animation defaultAnimation) : base() {
			this.animations = animations;
			this.defaultAnimation = defaultAnimation;
			this.CurrentAnimation = this.defaultAnimation;
            PlayCurrentAnimation();
		}

		/// <summary>
		/// Constructor for a RenderComponent with one animation.
		/// </summary>
        /// <param name="animation">The single Animation for this RenderComponent.</param>
        /// <param name="owner">The GameObject that owns this RenderComponent.</param>
		public RenderComponent(Animation animation, GameObject owner = null) : this(new Dictionary<String, Animation> { {animation.Name, animation} }, animation) {}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void Play(String animationName) {
            // If we are playing the current animation, then do not re-play it!
            if (CurrentAnimation.Name != animationName) {
                CurrentAnimation = animations[animationName];
                PlayCurrentAnimation();
            }
		}

        /// <summary>
        /// Starts playing the current animation.
        /// </summary>
        private void PlayCurrentAnimation()
        {
            if (CurrentAnimation.FPS != 0)
            {
                // If another animation is already playing:
                if (animationTimer != null)
                {
                    animationTimer.Stop();
                }
                animationTimer = new EngineTimer((int)(1000.00 / CurrentAnimation.FPS), new List<IUpdateable> { CurrentAnimation, this });
                animationTimer.Start();
            }
        }

		/// <summary>
		/// Notify the World that this RenderComponent needs to be re-rendered.
		/// </summary>
		/// <param name="deltaTime">The time in milliseconds since the last Update.</param>
        public void Update(double deltaTime)
        {
            EngineObject.Instance.Camera.Viewport.AddGameObjectToRedrawQueue(Owner);
        }
	}
}