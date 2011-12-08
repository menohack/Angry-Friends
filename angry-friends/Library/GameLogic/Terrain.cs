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
		public Terrain(string name, TransformComponent tc, AudioComponent ac, RenderComponent rc) : base(name, tc, ac, rc)
		{
			//TransformComponent = new PixelTransformComponent(tc.Position, tc.Rotation, tc.Size, this);

		}

	}
}
