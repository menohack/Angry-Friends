using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Model.Engine.Component.Media;
using Model.Engine.Component.Media.Rendering;
using Model.Engine.Component.Transform;
using Model.Engine.Object;
using Model.Engine.Utilities;

namespace Model.GameLogic
{
    /// <summary>
    /// Factory assists in creating GameObjects useful in-game.
    /// </summary>
    public class Factory
    {

        /// <summary>
        /// Accessor for the Terrain that this Factory made.
        /// </summary>
        public GameObject Terrain { get; private set; }

        /// <summary>
        /// The name of Yoshi.
        /// </summary>
        public static readonly String YOSHI_NAME = "yoshi";

        /// <summary>
        /// The speed of Yoshi.
        /// </summary>
        public static readonly Point YOSHI_SPEED = new Point(200, 500);

        /// <summary>
        /// Yoshi's initial position.
        /// </summary>
        public static readonly Point YOSHI_INITIAL_POSITION = new Point(20, 0);

        /// <summary>
        /// The size of Yoshi, in pixels.
        /// </summary>
        public static readonly Point YOSHI_SIZE = new Point(29, 39);

        /// <summary>
        /// The list of asset names corresponding to each frame in Yoshi's walk animation.
        /// </summary>
        public static readonly IList<String> YOSHI_WALK_ANIMATION_LIST = new List<String>
        {
            "yoshi_walk_01",
            "yoshi_walk_02",
            "yoshi_walk_03"
        };

        public static readonly IList<String> YOSHI_JUMP_ANIMATION_LIST = new List<String> 
        {
            "yoshi_jump_01"
        };

        /// <summary>
        /// The name of the Yoshi walk animation.
        /// </summary>
        public static readonly String YOSHI_WALK_ANIMATION_NAME = "walk";
        
        /// <summary>
        /// The name of the Yoshi jump animation.
        /// </summary>
        public static readonly String YOSHI_JUMP_ANIMATION_NAME = "jump";

        /// <summary>
        /// The length, in seconds, of Yoshi's walk animation.
        /// </summary>
        public static readonly double YOSHI_WALK_ANIMATION_LENGTH = 1;

        /// <summary>
        /// The FPS of Yoshi's walk animation.
        /// </summary>
        public static readonly int YOSHI_WALK_ANIMATION_FPS = 3;

        /// <summary>
        /// The default name of the background, which is used to look up the image of the background in AssetManager.
        /// </summary>
        public static readonly String NAME_OF_BACKGROUND_ASSET = "default_background";

        /// <summary>
        /// The default name of the terrain, which is used to look up the image of the terrain in AssetManager.
        /// </summary>
        public static readonly String NAME_OF_TERRAIN_ASSET = "default_terrain";

        /// <summary>
        /// The size of the terrain asset.
        /// </summary>
        public static readonly Point SIZE_OF_TERRAIN = new Point(800, 100);

        /// <summary>
        /// The position of the terrain.
        /// </summary>
        public static readonly Point POSITION_OF_TERRAIN = new Point(400, 550);

        /// <summary>
        /// The name of the terrain.
        /// </summary>
        public static readonly String NAME_OF_TERRAIN = "TERRAIN";

        /// <summary>
        /// The name of an apple.
        /// </summary>
        public static readonly String NAME_OF_APPLE = "apple";

        /// <summary>
        /// The size of an apple.
        /// </summary>
        public static readonly Point SIZE_OF_APPLE = new Point(20, 20);

        /// <summary>
        /// The singleton instance of Factory.
        /// </summary>
        public static Factory instance;

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

            RenderComponent renderComponent = new RenderComponent(new Animation(new Frame(terrainAsset, new Point(0, 0))));
            TransformComponent transformComponent = new TransformComponent(POSITION_OF_TERRAIN, SIZE_OF_TERRAIN);
            AudioComponent audioComponent = new AudioComponent(new Dictionary<String, MediaElement>());

            Terrain = new GameObject(NAME_OF_TERRAIN, transformComponent, audioComponent, renderComponent);

            return Terrain;
        }

        /// <summary>
        /// Creates a new instance of Player -- Yoshi.
        /// </summary>
        /// <returns>A nwe instance of a Player -- Yoshi.</returns>
        public Yoshi CreateYoshi()
        {
            Dictionary<string, ExternalAsset> assetDictionary = AssetManager.Instance.ExternalAssets;

            Dictionary<String, Animation> animations = new Dictionary<string, Animation>();

            // Create walk animation.
            IList<Frame> walkFrames = new List<Frame> { 
                GetFrameFromBitmapImage(assetDictionary[YOSHI_WALK_ANIMATION_LIST[0]].GetBitmapImage()), 
                GetFrameFromBitmapImage(assetDictionary[YOSHI_WALK_ANIMATION_LIST[1]].GetBitmapImage()),
                GetFrameFromBitmapImage(assetDictionary[YOSHI_WALK_ANIMATION_LIST[2]].GetBitmapImage()),
            };
            Animation walkAnimation = new Animation(walkFrames, YOSHI_WALK_ANIMATION_LENGTH, YOSHI_WALK_ANIMATION_FPS, YOSHI_WALK_ANIMATION_NAME);

            // Create jump animation.
            Frame jumpFrame = GetFrameFromBitmapImage(assetDictionary[YOSHI_JUMP_ANIMATION_LIST[0]].GetBitmapImage());
            Animation jumpAnimation = new Animation(jumpFrame, YOSHI_JUMP_ANIMATION_NAME);

            animations.Add(YOSHI_WALK_ANIMATION_NAME, walkAnimation);
            animations.Add(YOSHI_JUMP_ANIMATION_NAME, jumpAnimation);

            RenderComponent renderComponent = new RenderComponent(animations, walkAnimation);
            AudioComponent audioComponent = new AudioComponent(new Dictionary<String, MediaElement>());
            TransformComponent transformComponent = new TransformComponent(YOSHI_INITIAL_POSITION, YOSHI_SIZE);

            return new Yoshi(YOSHI_NAME, YOSHI_SPEED, transformComponent, audioComponent, renderComponent);
        }

        /// <summary>
        /// Creates a new Apple.
        /// </summary>
        /// <param name="point">The position of the apple.</param>
        /// <returns>A new Apple.</returns>
        public GravitationalGameObject CreateApple(Point point)
        {
            Dictionary<string, ExternalAsset> assetDictionary = AssetManager.Instance.ExternalAssets;
            Frame frame = new Frame(GetImageFromBitmapImage(assetDictionary[NAME_OF_APPLE].GetBitmapImage()), new Point(0, 0));
            RenderComponent renderComponent = new RenderComponent(new Animation(frame));
            AudioComponent audioComponent = new AudioComponent(new Dictionary<String, MediaElement>());
            TransformComponent transformComponent = new TransformComponent(point, SIZE_OF_APPLE);
            return new GravitationalGameObject(NAME_OF_APPLE, transformComponent, audioComponent, renderComponent);
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
            return new Frame(GetImageFromBitmapImage(bitmapImage), new Point(0, 0));
        }
    }
}
