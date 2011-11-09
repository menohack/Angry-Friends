using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Friendly_Wars.Engine.Component;
using Friendly_Wars.Engine.Component.Graphic;
using Friendly_Wars.Engine.Component.Physics;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// GameObject represent a base for all in-game objects. 
	/// GameObject are composed of different BaseComponents, which provide core game-functionality, such as rendering, audio, movement, rotation, physics and networking.
	/// </summary>
	public class GameObject : ITransformable, ICollidable, IAudible, IVisible
	{
		/// <summary>
		/// This GameObject's TransformComponent.
		/// </summary>
		private TransformComponent transformComponent;
		/// <summary>
		/// This GameObject's PhysicsComponent.
		/// </summary>
		private PhysicsComponent physicsComponent;
		/// <summary>
		/// The GameObject's AudioComponent.
		/// </summary>
		private AudioComponent audioComponent;
		/// <summary>
		/// This GameObject's RenderComponent.
		/// </summary>
		private RenderComponent renderComponent;

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

			transformComponent = new TransformComponent(this, new Point());
			physicsComponent = new PhysicsComponent(this, new BoundingBox(new TransformComponent(this, new Point())));
			audioComponent = new AudioComponent(this, new Dictionary<String, MediaElement>());
			renderComponent = new RenderComponent(this, new Dictionary<string, Animation>(), null);
		}

		/// <summary>
		/// Moves this GameObject by a given amount.
		/// </summary>
		/// <param name="deltaPosition">The amount by which to move this GameObject.</param>
		public void MoveBy(Point deltaPosition)
		{
			transformComponent.Translate(deltaPosition);
		}

		public void MoveTo(Point absolutePosition)
		{

		}

		/// <summary>
		/// Rotates this GameObject by a given amount (in degrees.)
		/// </summary>
		/// <param name="deltaRotation">The amount by which to rotate this GameObject.</param>
		public void RotateBy(int deltaRotation)
		{
			transformComponent.Rotate(deltaRotation);
		}

		public void RotateTo(int absolutePosition)
		{

		}

		public void ResizeBy(Point deltaSize)
		{

		}


		public void ResizeTo(Point absoluteSize)
		{

		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The specific Animation to play.</param>
		public void PlayAnimation(String animationName)
		{
			renderComponent.Play(animationName);
		}

		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The Animation to stop playing.</param>
		public void StopAnimation(String animationName)
		{
			renderComponent.Stop(animationName);
		}

		/// <summary>
		/// Plays a specific AudioClip.
		/// </summary>
		/// <param name="name">The AudioClip to play.</param>
		public void PlayAudioClip(String name)
		{
			audioComponent.Play(name);
		}

		/// <summary>
		/// Stops playing a specific AudioClip.
		/// </summary>
		/// <param name="name">The AudioClip to stop playing.</param>
		public void StopAudioClip(String name)
		{
			audioComponent.Stop(name);
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
		/// Destroys a given GameObject.
		/// </summary>
		/// <param name="gameObject">The GameObject that will be destroyed.</param>
		public static void Destroy(GameObject gameObject)
		{
			World.Instance.RemoveGameObject(gameObject);
		}

		/// <summary>
		/// Creates a UID for a GameObject.
		/// </summary>
		/// <returns>Returns a UID for a GameObject</returns>
		private static int NextUID()
		{
			return ++CurrentUID;
		}
	}
}
