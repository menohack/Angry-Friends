using Library.Engine.Object;
using Library.Engine.Component;
using System.Windows;
using Library.Engine.Component.Graphic;

namespace Library.GameLogic
{
    /// <summary>
    /// Terrain is the land on which the team falls and fights.
    /// </summary>
	public class Terrain : GameObject
	{
		public Terrain(string name, TransformComponent tc, AudioComponent ac, RenderComponent rc) : base(name, null, ac, rc)
		{
			TransformComponent = new PixelTransformComponent(tc.Position, tc.Rotation, tc.Size, this);

		}

	}

	public class PixelTransformComponent : TransformComponent
	{
		public PixelTransformComponent(Point position, int rotation, Point size, GameObject owner) : base(position, rotation, size, owner)
		{
		}

		public override Point Collide(Point desiredPosition, TransformComponent a, TransformComponent b)
		{
			return new Point();
		}
	}
}
