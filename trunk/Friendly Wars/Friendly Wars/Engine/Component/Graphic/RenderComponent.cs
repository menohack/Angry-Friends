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

namespace Friendly_Wars.Engine.Component.Graphic
{
    /// <summary>
    /// Handles the rendering of a GameObject.
    /// </summary>
    public class RenderComponent
    {
        /// <summary>
        /// The bitmap image that needs to be rendered.
        /// </summary>
        private Image bitmap;

        /// <summary>
        /// Constructs a RenderComponent for a GameObject.
        /// </summary>
        /// <param name="owner"> The owner of this RenderComponent. </param>
        public RenderComponent(GameObject owner)
        {

        }

        /// <summary>
        /// Getters and setters for modifying the bitmap.
        /// </summary>
        public Image Bitmap
        {
            get
            {
                return bitmap;
            }
            set
            {
                bitmap = value;
            }
        }
    }
}
