using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;

namespace Model.Engine.Object.GameObjects
{
    /// <summary>
    /// Assists in the creation of GameObjects.
    /// </summary>
    public class GameObjectFactory
    {
        /// <summary>
        /// The default name for a GameObject.
        /// </summary>
        private readonly String DEFAULT_NAME = "GameObject";

        /// <summary>
        /// The default position of a GameObject.
        /// </summary>
        private readonly Point DEFAULT_POSITION = new Point(0, 0);

        /// <summary>
        /// The default size of a GameObject.
        /// </summary>
        private readonly Point DEFAULT_SIZE = new Point(1, 1);

        /// <summary>
        /// The default rotation of a GameObject.
        /// </summary>
        private readonly int DEFAULT_ROTATION = 0;

        /// <summary>
        /// The path to an embedded sprite sheet.
        /// </summary>
        private readonly String PATH_TO_DEFAULT_SPRITE_SHEET = "Library;/Engine/Assets/default_sprite_sheet.png";

        /// <summary>
        /// The path to an embedded audio clip.
        /// </summary>
        private readonly String PATH_TO_DEFAULT_AUDIO_CLIP = "Library;/Engine/Assets/default_audio_clip.mp3";

        /// <summary>
        /// The singleton instance of a GameObjectFactory.
        /// </summary>
        private static GameObjectFactory gameObjectHelper;

        /// <summary>
        /// The singleton pattern for a GameObjectFactory.
        /// </summary>
        public static GameObjectFactory Instance
        {
            get
            {
                if (gameObjectHelper == null)
                {
                    gameObjectHelper = new GameObjectFactory();
                }

                return gameObjectHelper;
            }

            set
            {
                gameObjectHelper = value;
            }
        }

        /// <summary>
        /// The private constructor for a GameObjectFactory.
        /// </summary>
        private GameObjectFactory() { }

        /// <summary>
        /// Creates a default GameObject.
        /// </summary>
        /// <returns>REturns a default GameObject</returns>
        public GameObject MakeDefaultGameObject()
        {
            String name = DEFAULT_NAME;
            TransformComponent transformComponent = DefaultTransformComponent();
            AudioComponent audioComponent = DefaultAudioComponent();
            RenderComponent renderComponent = DefaultRenderComponent();

            GameObject gameObject = new GameObject(DEFAULT_NAME, transformComponent, audioComponent, renderComponent);
            return gameObject;
        }

        /// <summary>
        /// Creates a default TransformComponent.
        /// </summary>
        /// <returns>Returns a default TransformComponent.</returns>
        private TransformComponent DefaultTransformComponent()
        {
            return new TransformComponent(DEFAULT_POSITION, DEFAULT_ROTATION, DEFAULT_SIZE);
        }

        /// <summary>
        /// Creates a default RenderComponent.
        /// </summary>
        /// <returns>Returns a default RenderComponent.</returns>
        private RenderComponent DefaultRenderComponent()
        {
            return new RenderComponent(DefaultAnimation());
        }
        
        /// <summary>
        /// Creates the default Animation associated with the default RenderComponent.
        /// </summary>
        /// <returns>Returns the default Animation associated with the default RenderComponent.</returns>
        private Animation DefaultAnimation()
        {
            return null;
        }

        /// <summary>
        /// Creates a default AudioComponent.
        /// </summary>
        /// <returns>Returns a default AudioComponent.</returns>
        private AudioComponent DefaultAudioComponent()
        {
            return new AudioComponent(DefaultAudioClips());
        }

        /// <summary>
        /// Creates the default AudioClips associated with the default AudioComponent.
        /// </summary>
        /// <returns>Returns the default AudioClips associated with the default AudioComponent.</returns>
        private IDictionary<String, MediaElement> DefaultAudioClips()
        {
            return null;
        }
    }
}
