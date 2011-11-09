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

namespace Friendly_Wars.Engine.Component
{
	public interface ICollidable
	{
		private CollisionComponent CollisionComponent { private get; private set; }
		public bool CheckForCollisions();
	}
}
