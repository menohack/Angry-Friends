using System;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Friendly_Wars.Engine.Component;
using Friendly_Wars.Engine.Component.Graphic;
using System.Collections.Generic;

namespace Friendly_Wars.Engine.Object
{
    /// <summary>
    /// GameObjects represent a base for all in-game objects. 
    /// GameObjects are composed of different BaseComponents, which provide core game-functionality, such as rendering, audio, movement, rotation, physics and networking.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// This GameObject's TransformComponent.
        /// </summary>
        public TransformComponent transformComponent { get; private set; }
        /// <summary>
        /// This GameObject's PhysicsComponent.
        /// </summary>
        public PhysicsComponent physicsComponent { get; private set; }
        /// <summary>
        /// The GameObject's AudioComponent.
        /// </summary>
        public AudioComponent audioComponent { get; private set; }
        /// <summary>
        /// This GameObject's RenderComponent.
        /// </summary>
        public RenderComponent renderComponent { get; private set; }

        /// <summary>
        /// This GameObject's name.
        /// </summary>
        public String name { get; private set; }
        /// <summary>
        /// This GameObject's tag.
        /// </summary>
        public String tag { get; private set; }

        /// <summary>
        /// This GameObject's UID.
        /// </summary>
        public int UID { get; private set; }

        /// <summary>
        /// The last UID assigned to a GameObject.
        /// </summary>
        public static int currentUID { get; private set; }

        /// <summary>
        /// This GameObject's children.
        /// </summary>
        public IList<GameObject> children { get; private set; }

        /// <summary>
        /// The Constructor for a GameObject.
        /// </summary>
        /// <param name="name">The name of the GameObject.</param>
        /// <param name="tag">The tag of the GameObject.</param>
        public GameObject(String name, String tag = null)
        {
            this.name = name;
            this.tag = tag;
            this.UID = NextUID();

            children = new List<GameObject>();

            transformComponent = new TransformComponent(this);
            physicsComponent = new PhysicsComponent(this);
            audioComponent = new AudioComponent(this);
            renderComponent = new RenderComponent(this);
        }

        /// <summary>
        /// Adds a child GameObject to the current GameObject, creating a parent-child relationship.
        /// </summary>
        /// <param name="child">The child GameObject that will be appended to the current GameObject (the parent).</param>
        public void AddChild(GameObject child)
        {
            children.Add(child);
        }

        /// <summary>
        /// Removes a child GameObject from the current GameObject.  This does not delete the child GameObject.  
        /// It only removes the parent-child relationship between the two GameObjects.
        /// </summary>
        /// <param name="child">The child GameObject that will be removed from the current GameObject (the parent).</param>
        public void RemoveChild(GameObject child)
        {
            children.Remove(child);
        }

        /// <summary>
        /// Destroys a given GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject that will be destroyed.</param>
        public static void Destroy(GameObject gameObject)
        {
            // TODO: Need to remove the GameObject from World.
            gameObject = null;
        }

        /// <summary>
        /// Creates a UID for a GameObject.
        /// </summary>
        /// <returns>Returns a UID for a GameObject</returns>
        private static int NextUID()
        {
            return ++currentUID;
        }
    }
}
