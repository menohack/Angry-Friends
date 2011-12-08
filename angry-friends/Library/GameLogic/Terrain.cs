using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Object.GameObjects;
using Model.Engine.Component.Transform;

namespace Model.GameLogic
{
    /// <summary>
    /// Terrain is the land on which the team falls and fights.
    /// </summary>
	public class Terrain : GameObject
	{
		public Terrain(string name, TransformComponent tc, AudioComponent ac, RenderComponent rc) : base(name, tc, ac, rc)
		{

		}
	}
}
