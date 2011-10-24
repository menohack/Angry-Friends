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

namespace Friendly_Wars.Engine.Component
{
    /// <summary>
    /// TransformComponent handles positioning, size and rotation of a GameObject.
    /// </summary>
    public class TransformComponent : BaseComponent
    {

        private static readonly Point globalRight = new Point(1, 0);
        private static readonly Point globalUp = new Point(0, 1);

        /// <summary>
        /// The position of the TransformComponent.
        /// </summary>
        private Point position;

        /// <summary>
        /// The rotation of the TransformComponent [0, 360].
        /// </summary>
        private int rotation;

        /// <summary>
        /// The size of the TransformComponent.
        /// </summary>
        private int size;

        /// <summary>
        /// Constructor for a new instance of TransformComponent.
        /// </summary>
        /// <param name="owner">The owner of this TransformComponent.</param>
        /// <param name="isEnabled">Is this TransformComponent enabled?</param>
        public TransformComponent(GameObject owner) : base(owner)
        {
            
        }
        
        /// <summary>
        /// Access the direction (1, 0).
        /// </summary>
        /// <returns>Returns the global-relative direction of right.</returns>
        public static Point GlobalRight()
        {
            return globalRight;
        }

        /// <summary>
        /// Access the direction (0, 1).
        /// </summary>
        /// <returns>Returns a Point (0, 1).</returns>
        public static Point GlobalUp()
        {
            return globalUp;
        }

        /// <summary>
        /// Access the direction (1, 0) with respect to this TransformComponent.
        /// </summary>
        /// <returns>Returns the Point that corresponds to the right direction of this TransformComponent.</returns>
        public Point LocalRight()
        {
            return new Point();
        }

        /// <summary>
        /// Access the direction (0, 1) with respect to this TransformComponent.
        /// </summary>
        /// <returns>Returns the Point that corresponds to the up direction of this TransformComponent.</returns>
        public Point LocalUp()
        {
            return new Point();
        }

        /// <summary>
        /// Rotates by a given number of degrees.
        /// </summary>
        /// <param name="deltaAngle">The change of rotation.</param>
        public void Rotate(int deltaAngle)
        {
            rotation += deltaAngle;
        }

        /// <summary>
        /// Translates by a given point.
        /// </summary>
        /// <param name="deltaPosition">The change of position.</param>
        public void Translate(Point deltaPosition)
        {
            position.X += deltaPosition.X;
            position.Y += deltaPosition.Y;
        }
    }
}
