using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Friendly_Wars.Engine.Component;
using Friendly_Wars.Engine.Component.Interfaces;
using Friendly_Wars.Engine.Component.Graphic;
using Friendly_Wars.Engine.Component.Realizations;

namespace Friendly_Wars.Engine.Object
{
	/// <summary>
	/// GameObject represent a base for all in-game objects. 
	/// GameObject are composed of different BaseComponents, which provide core game-functionality, such as rendering, audio, movement, rotation, physics and networking.
	/// </summary>
	public class GameObject : Transformable, ICollidable, IAudible, IRenderable
	{
		/// <summary>
		/// This GameObject's CollisionComponent.
		/// </summary>
		private CollisionComponent collisionComponent;
		/// <summary>
		/// The GameObject's AudioComponent.
		/// </summary>
		private AudioComponent audioComponent;
		/// <summary>
		/// This GameObject's RenderComponent.
		/// </summary>
		public RenderComponent RenderComponent { get; private set; }

		public TransformComponent TransformComponent { get; private set; }

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
		}

		/// <summary>
		/// Determines if there is any collision from a linear path from the currentPosition to the desiredPosition.
		/// </summary>
		/// <param name="currentPosition">The current position.</param>
		/// <param name="desiredPosition">The position that is desired.</param>
		/// <returns>Returns desiredPosition if there is no collision.
		/// If there is collision, it returns the closest modified position, such that there is no collision.</returns>
		public Point CheckCollision(Point currentPosition, Point desiredPosition)
		{
			return collisionComponent.CheckCollision(currentPosition, desiredPosition);
		}

		/// <summary>
		/// Plays a specific AudioClip.
		/// </summary>
		/// <param name="name">The AudioClip to play.</param>
		public void PlayAudioClip(string name)
		{
			audioComponent.Play(name);
		}

		/// <summary>
		/// Stops playing a specific AudioClip.
		/// </summary>
		/// <param name="name">The AudioClip to stop playing.</param>
		public void StopAudioClip(string name)
		{
			audioComponent.Stop(name);
		}

		/// <summary>
		/// Plays a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to play.</param>
		public void PlayAnimation(String animationName)
		{
			renderComponent.Play(animationName);
		}

		/// <summary>
		/// Stops playing a specific Animation.
		/// </summary>
		/// <param name="animationName">The name of the Animation to stop playing.</param>
		public void StopAnimation(String animationName)
		{
			renderComponent.Play(animationName);
		}

		/// <summary>
		/// Access the content that should be rendered.
		/// </summary>
		/// <returns>The content that needs to be rendered.</returns>
		public Image GetRenderContent()
		{
			return renderComponent.CurrentAnimation.CurrentFrame.Image;
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
