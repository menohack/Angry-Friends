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
	public interface ITransformable
	{
		private TransformComponent TransformComponent { private get; private set; }
		public void MoveBy(Point deltaPosition);
		public void MoveTo(Point absolutePosition);
		public void RotateBy(int deltaRotation);
		public void RotateTo(int absolutePosition);
		public void ResizeBy(Point deltaSize);
		public void ResizeTo(Point absoluteSize);
	}
}
