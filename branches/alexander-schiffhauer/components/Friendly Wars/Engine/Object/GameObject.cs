using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Friendly_Wars.Engine.Component;
using Friendly_Wars.Engine.Component.Interfaces;
using Friendly_Wars.Engine.Component.Graphic;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// GameObject represent a base for all in-game objects. 
	/// GameObject are composed of different BaseComponents, which provide core game-functionality, such as rendering, audio, movement, rotation, physics and networking.
	/// </summary>
	public class GameObject : ITransformable, ICollidable, IAudible, IRenderable
	{
		/// <summary>
		/// This GameObject's TransformComponent.
		/// </summary>
		private TransformComponent TransformComponent { get; set; }
		/// <summary>
		/// This GameObject's CollisionComponent.
		/// </summary>
		private CollisionComponent CollisionComponent { get; set; }
		/// <summary>
		/// The GameObject's AudioComponent.
		/// </summary>
		private AudioComponent AudioComponent { get; set; }
		/// <summary>
		/// This GameObject's RenderComponent.
		/// </summary>
		private RenderComponent RenderComponent { get; set; }

		/// <summary>
		/// This GameObject's name.
		/// </summary>
		public String Name { get; private set; }
		/// <summary>
		/// This GameObject's tag.
		/// </summary>
		public String Tag { get; private set; }
		/// <summary>
		/// This GameObject's UID.
		/// </summary>
		public int UID { get; private set; }
		/// <summary>
		/// The last UID assigned to a GameObject.
		/// </summary>
		private static int CurrentUID { get; set; }
		/// <summary>
		/// This GameObject's children.
		/// </summary>
		public IList<GameObject> Children { get; private set; }

		//TODO: Pass in components through constructor.
		/// <summary>
		/// The Constructor for a GameObject.
		/// </summary>
		/// <param name="name">The name of the GameObject.</param>
		/// <param name="tag">The tag of the GameObject.</param>
		public GameObject(String name, String tag = null)
		{
			this.Name = name;
			this.Tag = tag;
			this.UID = NextUID();

			Children = new List<GameObject>();

			TransformComponent = new TransformComponent(this, new Point());
			CollisionComponent = new PhysicsComponent(this, new BoundingBox(new TransformComponent(this, new Point())));
			AudioComponent = new AudioComponent(this, new Dictionary<String, MediaElement>());
			RenderComponent = new RenderComponent(this, new Dictionary<string, Animation>(), null);
		}

		/// <summary>
		/// Adds a child GameObject to the current GameObject, creating a parent-child relationship.
		/// </summary>
		/// <param name="child">The child GameObject that will be appended to the current GameObject (the parent).</param>
		public void AddChild(GameObject child)
		{
			Children.Add(child);
		}

		/// <summary>
		/// Removes a child GameObject from the current GameObject.  This does not delete the child GameObject.  
		/// It only removes the parent-child relationship between the two gameObjects.
		/// </summary>
		/// <param name="child">The child GameObject that will be removed from the current GameObject (the parent).</param>
		public void RemoveChild(GameObject child)
		{
			Children.Remove(child);
		}

		/// <summary>
		/// Creates a UID for a GameObject.
		/// </summary>
		/// <returns>Returns a UID for a GameObject</returns>
		private static int NextUID()
		{
			return ++CurrentUID;
		}

		/// <summary>
		/// Destroys a given GameObject.
		/// </summary>
		/// <param name="gameObject">The GameObject that will be destroyed.</param>
		public static void Destroy(GameObject gameObject)
		{
			World.Instance.RemoveGameObject(gameObject);
		}
	}
}
