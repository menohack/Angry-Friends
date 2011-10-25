﻿using System;
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
    public class PhysicsComponent
    {
        /// <summary>
        /// The bounding box that this object is within.
        /// </summary>
        private BoundingBox boundingBox;

        /// <summary>
        /// The PhysicsComponent constructor.
        /// </summary>
        /// <param name="Owner"> The GameObject associated with this PhysicsComponent. </param>
        public PhysicsComponent(GameObject Owner)
        {

        }

        /// <summary>
        /// Checks whether a GameObject is colliding with this object.
        /// </summary>
        /// <param name="gameObject"> The other object. </param>
        /// <returns> True if the objects are colliding. </returns>
        public bool Collide(GameObject gameObject)
        {
            if (boundingBox.Collide(gameObject.physicsComponent.boundingBox))
                return true;
            else
                return false;
        }

    }
}
