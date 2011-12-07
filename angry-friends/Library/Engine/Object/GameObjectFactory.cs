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

namespace Library.Engine.Object
{
    public class GameObjectFactory
    {

    }
}

/*
 		/// <summary>
		/// The Constructor for a GameObject.
		/// </summary>
		/// <param name="name">The name of the GameObject.</param>
		public GameObject(IDictionary<String, Animation> animations, Animation defaultAnimation, String name) {
			EngineObject.Instance.AddGameObject(this);
			this.Name = name;
			this.UID = NextUID();

			Children = new List<GameObject>();
			RenderComponent = new RenderComponent(animations, defaultAnimation, this);
            EngineObject.Instance.AddGameObject(this);
		}

		/// <summary>
		/// The Constructor for a GameObject. GameObjects are automatically added to the World.
		/// </summary>
		/// <param name="name">The name of the GameObject.</param>
		/// <param name="tag">The tag of the GameObject.</param>
		public GameObject(String name) {
			EngineObject.Instance.AddGameObject(this);
			this.Name = name;
			this.UID = NextUID();

			Children = new List<GameObject>();
            EngineObject.Instance.AddGameObject(this);
		}

		/// <summary>
		/// The Constructor for a GameObject. GameObjects are automatically added to the World.
		/// </summary>
		/// <param name="name">The name of the GameObject.</param>
		/// <param name="position">The starting position of the GameObject.</param>
		/// <param name="rotation">The starting rotation of the GameObject.</param>
		/// <param name="size">The starting size of the GameObject.</param>
		public GameObject(String name, Point position, int rotation, Point size) {
			EngineObject.Instance.AddGameObject(this);
			this.Name = name;
			this.UID = NextUID();

			Children = new List<GameObject>();
			TransformComponent = new TransformComponent(position, rotation, size, this);
            EngineObject.Instance.AddGameObject(this);
		}
 */
