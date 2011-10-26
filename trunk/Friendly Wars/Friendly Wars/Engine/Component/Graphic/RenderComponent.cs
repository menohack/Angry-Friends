using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Friendly_Wars.Engine.Object;
using System.Collections.Generic;

namespace Friendly_Wars.Engine.Component.Graphic
{
    /// <summary>
    /// Handles the rendering of a GameObject.
    /// </summary>
    public class RenderComponent : BaseComponent
    {

        /// <summary>
        /// All animations of this RenderComponent.
        /// </summary>
        public ICollection<Animation> animations { get; private set; }

        /// <summary>
        /// The current animation.
        /// </summary>
        public Animation currentAnimation { get; private set; }

        /// <summary>
        /// The default animation that is played when specific animations are stopped and/or no animation is explicitly played.
        /// </summary>
        public Animation defaultAnimation { get; private set; }

        /// <summary>
        /// Constructor for a new RenderComponent.
        /// </summary>
        /// <param name="owner"> The owner of this RenderComponent. </param>
        public RenderComponent(GameObject owner, ICollection<Animation> animations) :base(owner)
        {
            this.animations = animations;

            // Find the default Animation.
            foreach (Animation animation in animations) {
                if (animation.isDefaultAnimation)
                {
                    defaultAnimation = animation;
                    currentAnimation = defaultAnimation;
                    return;
                }
            }
        }

        /// <summary>
        /// Plays a specific Animation.
        /// </summary>
        /// <param name="animationName">The name of the Animation to play.</param>
        public void Play(String animationName)
        {
            foreach (Animation animation in animations)
            {
                if (animation.name == animationName)
                {
                    currentAnimation = animation;
                    animation.Play();
                    return;
                }
            }
        }

        /// <summary>
        /// Stops playing a specific Animation.
        /// </summary>
        /// <param name="animationName">The name of the Animation to stop playing.</param>
        public void Stop(String animationName)
        {

            foreach (Animation animation in animations)
            {
                if (animation.name == animationName)
                {
                    currentAnimation = defaultAnimation;
                    animation.Stop();
                    return;
                }
            }
        }

        private void Render()
        {

        }
    }
}
