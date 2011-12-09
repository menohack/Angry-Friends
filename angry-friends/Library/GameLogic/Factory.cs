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
using Model.Engine.Object.GameObjects;
using Model.Engine.Utilities;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Component.Media;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Model.Engine.Object;

namespace Model.GameLogic
{
    /// <summary>
    /// Factory assists in creating GameObjects useful in-game.
    /// </summary>
    public class Factory
    {

        private static readonly String MARIO_NAME = "mario";

        /// <summary>
        /// The speed of Mario.
        /// </summary>
        private static readonly Point MARIO_SPEED = new Point(50, 50);

        /// <summary>
        /// Mario's initial position.
        /// </summary>
        private static readonly Point MARIO_INITIAL_POSITION = new Point(0, 0);

        /// <summary>
        /// The size of Mario, in pixels.
        /// </summary>
        private static readonly Point MARIO_SIZE = new Point(26, 29);

        /// <summary>
        /// The list of asset names corresponding to each frame in Mario's walk animation.
        /// </summary>
        private static readonly IList<String> MARIO_WALK_ANIMATION_LIST = new List<String>
        {
            "mario_walk_01",
            "mario_walk_02",
            "mario_walk_03"
        };

        private static readonly String MARIO_WALK_ANIMATION_NAME = "walk";

        /// <summary>
        /// The length, in seconds, of Mario's walk animation.
        /// </summary>
        private static readonly double MARIO_WALK_ANIMATION_LENGTH = 1.0;

        /// <summary>
        /// The FPS of Mario's walk animation.
        /// </summary>
        private static readonly int MARIO_WALK_ANIMATION_FPS = 3;

        /// <summary>
        /// The default name of the background, which is used to look up the image of the background in AssetManager.
        /// </summary>
        private static readonly String NAME_OF_BACKGROUND_ASSET = "default_background";

        /// <summary>
        /// The default name of the terrain, which is used to look up the image of the terrain in AssetManager.
        /// </summary>
        private static readonly String NAME_OF_TERRAIN_ASSET = "default_terrain";

        /// <summary>
        /// The size of the terrain asset.
        /// </summary>
        private static readonly Point SIZE_OF_TERRAIN = new Point(800, 100);

        /// <summary>
        /// The position of the terrain.
        /// </summary>
        private static readonly Point POSITION_OF_TERRAIN = new Point(0, 500);

        /// <summary>
        /// The name of the terrain.
        /// </summary>
        private static readonly String NAME_OF_TERRAIN = "TERRAIN";

        /// <summary>
        /// The singleton instance of Factory.
        /// </summary>
        private static Factory instance;

        /// <summary>
        /// The singleton accessor of Factory.
        /// </summary>
        public static Factory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Factory();
                }

                return instance;
            }
        }

        /// <summary>
        /// The private constructor for Factory.
        /// </summary>
        private Factory() {}

        /// <summary>
        /// Creates the background for the Game.
        /// </summary>
        public void CreateBackground()
        {
            BitmapImage backgroundBitmap = AssetManager.Instance.ExternalAssets[NAME_OF_BACKGROUND_ASSET].GetBitmapImage();
            Image background = new Image();
            background.Source = backgroundBitmap;
            EngineObject.Instance.Camera.Viewport.AddBackgroundToViewPort(background);
        }

        /// <summary>
        /// Creates a terrain GameObject.
        /// </summary>
        /// <returns>Terrain usable in-game.</returns>
        public GameObject CreateTerrain()
        {
            Image terrainAsset = new Image();
            terrainAsset.Source = AssetManager.Instance.ExternalAssets[NAME_OF_TERRAIN_ASSET].GetBitmapImage();

            RenderComponent renderComponent = new RenderComponent(new Animation(new Frame(terrainAsset)));
            TransformComponent transformComponent = new TransformComponent(POSITION_OF_TERRAIN, SIZE_OF_TERRAIN);
            AudioComponent audioComponent = new AudioComponent(new Dictionary<String, MediaElement>());

            return new GameObject(NAME_OF_TERRAIN, transformComponent, audioComponent, renderComponent);
        }

        /// <summary>
        /// Creates a new instance of Player -- Mario.
        /// </summary>
        /// <returns>A nwe instance of a Player -- Mario.</returns>
        public Player CreateMario()
        {
            Dictionary<string, ExternalAsset> assetDictionary = AssetManager.Instance.ExternalAssets;
            IList<Frame> frames = new List<Frame> { 
                GetFrameFromBitmapImage(assetDictionary[MARIO_WALK_ANIMATION_LIST[0]].GetBitmapImage()), 
                GetFrameFromBitmapImage(assetDictionary[MARIO_WALK_ANIMATION_LIST[1]].GetBitmapImage()),
                GetFrameFromBitmapImage(assetDictionary[MARIO_WALK_ANIMATION_LIST[2]].GetBitmapImage()) 
            };

            RenderComponent renderComponent = new RenderComponent(new Animation(frames, MARIO_WALK_ANIMATION_LENGTH, MARIO_WALK_ANIMATION_FPS, MARIO_WALK_ANIMATION_NAME));
            AudioComponent audioComponent = new AudioComponent(new Dictionary<String, MediaElement>());
            TransformComponent transformComponent = new TransformComponent(MARIO_INITIAL_POSITION, MARIO_SIZE);

            return new Player(MARIO_NAME, MARIO_SPEED, transformComponent, audioComponent, renderComponent);
        }

        /// <summary>
        /// Creates an Image from a BitmapImage.
        /// </summary>
        /// <param name="bitmapImage">The BitmapImage to convert.</param>
        /// <returns>The corresponding Image of the given BitmapImage</returns>
        private Image GetImageFromBitmapImage(BitmapImage bitmapImage)
        {
            Image image = new Image();
            image.Source = bitmapImage;
            image.Height = bitmapImage.PixelHeight;
            image.Width = bitmapImage.PixelWidth;
            return image;
        }

        /// <summary>
        /// Creates a Frame from a BitMapImage.
        /// </summary>
        /// <param name="bitmapImage">The BitmapImage to convert.</param>
        /// <returns>The corresponding Frame of the given BitmapImage.</returns>
        private Frame GetFrameFromBitmapImage(BitmapImage bitmapImage)
        {
            return new Frame(GetImageFromBitmapImage(bitmapImage));
        }
    }
}
