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
using Library.Engine.Object;

namespace Library.GameLogic
{
    public class TestGameObject : GameObject
    {

        public TestGameObject() : base("Something")
        {
        }

        override public void Update(double deltaTime) {
            double seconds = deltaTime/1000.00;
            TransformComponent.Translate(new Point(10 * seconds, 0)); 
        }
    }
}
