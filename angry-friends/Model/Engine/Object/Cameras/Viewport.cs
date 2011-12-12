using System.Collections.Generic;
using System.Windows.Controls;
using Model.Engine.Component.Media.Rendering;
using System.Windows.Media;

namespace Model.Engine.Object.Cameras {
    /// <summary>
    /// Viewport represents the drawable screen of this game.
    /// </summary>
    public class Viewport : Canvas
    {

        /// <summary>
        /// Stores a reference to every GameObject's previous Frame, which allows for removal and updating of Frames on the viewport.
        /// </summary>
        private IDictionary<GameObject, Frame> previousFrames;
       
        /// <summary>
        /// The queue of GameObjects that need to be redrawn the next time EngineObject updates.
        /// </summary>
        private IList<GameObject> redrawQueue;

        /// <summary>
        /// The constructor for a new Viewport.
        /// </summary>
        public Viewport() : base() {
            previousFrames = new Dictionary<GameObject, Frame>();
            redrawQueue = new List<GameObject>();
        }

        /// <summary>
        /// Redraws the GameObjects in this Viewport.
        /// </summary>
        public void RedrawGameObjects()
        {
            // Remove previously drawn GameObjects.
            foreach (GameObject gameObject in redrawQueue)
            {
                if (gameObject == null)
                {
                    continue;
                }

				Frame frame;
                previousFrames.TryGetValue(gameObject, out frame);
                if (frame != null)
                {
                    RemoveFrameFromViewport(frame);
                    previousFrames.Remove(gameObject);
                }
            }

            // Iterate through each RenderComponent in the redrawQueue and redraw it.
            foreach (GameObject gameObject in redrawQueue)
            {
                if (gameObject == null)
                {
                    continue;
                }

                Frame frame = gameObject.RenderComponent.CurrentAnimation.CurrentFrame;
                frame.Image.SetValue(Canvas.LeftProperty, gameObject.TransformComponent.Position.X + frame.Offset.X - gameObject.TransformComponent.Size.X/2);
                frame.Image.SetValue(Canvas.TopProperty, gameObject.TransformComponent.Position.Y + frame.Offset.Y - gameObject.TransformComponent.Size.Y/2);

                // Update the animation with respect to how it should be flipped.
                if (gameObject.RenderComponent.ShouldFlip)
                {
                    ScaleTransform scaleTransform = new ScaleTransform();
                    scaleTransform.ScaleX = -1;
                    frame.Image.RenderTransform = scaleTransform;
                    frame.Image.SetValue(Canvas.LeftProperty, gameObject.TransformComponent.Position.X + frame.Offset.X + gameObject.TransformComponent.Size.X / 2);
                }
                else
                {
                    ScaleTransform scaleTransform = new ScaleTransform();
                    scaleTransform.ScaleX = 1;
                    frame.Image.RenderTransform = scaleTransform;
                }

                AddFrameToViewport(frame);
                previousFrames.Add(gameObject, frame);
            }

            redrawQueue.Clear();
        }

        /// <summary>
        /// Adds a GameObject to the redraw queue.
        /// </summary>
        /// <param name="gameObject">The GameObject to add to the redraw queue.</param>
        public void AddGameObjectToRedrawQueue(GameObject gameObject)
        {
            if (!redrawQueue.Contains(gameObject))
            {
                redrawQueue.Add(gameObject);
            }
        }

        /// <summary>
        /// Adds a background image to the Viewport.  Only one background can be added.
        /// </summary>
        /// <param name="image">The background to add to the Viewport.</param>
        public void AddBackgroundToViewPort(Image image)
        {
            Children.Insert(0, image);
        }

        /// <summary>
        /// Removes a Frame from the Viewport.
        /// </summary>
        /// <param name="image">The Frame to remove from this Viewport.</param>
        private void RemoveFrameFromViewport(Frame frame)
        {
            if (this.Children.Contains(frame.Image))
            {
                this.Children.Remove(frame.Image);
            }
        }

        /// <summary>
        /// Adds a Frame to the Viewport.
        /// </summary>
        /// <param name="image">The Frame to add to this Viewport.</param>
        private void AddFrameToViewport(Frame frame)
        {
            this.Children.Add(frame.Image);
        }
    }
}
