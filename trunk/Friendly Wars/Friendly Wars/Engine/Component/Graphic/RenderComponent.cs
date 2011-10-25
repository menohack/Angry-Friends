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
    public class RenderComponent
    {
        private Image bitmap;

        public RenderComponent(GameObject owner)
        {

        }

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
