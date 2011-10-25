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

namespace Friendly_Wars.Engine.Object
{
    /// <summary>
    /// Objects that need to be updated on every frame.
    /// </summary>
    public abstract class UpdateableGameObject : GameObject
    {
        /// <summary>
        /// The Constructor for an UpdateableGameObject.
        /// </summary>
        /// <param name="name"> The name of the object. </param>
        /// <param name="tag"> The tag of the object. </param>
        public UpdateableGameObject(String name, String tag = null) : base(name, tag) {
        
        }

        public abstract void Update();
    }
}
