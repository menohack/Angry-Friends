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
            TransformComponent transformComponent = new TransformComponent(POSITION_OF_TERRAIN, 0, SIZE_OF_TERRAIN);
            AudioComponent audioComponent = new AudioComponent(new Dictionary<String, MediaElement>(), null);

            return new GameObject(NAME_OF_TERRAIN, transformComponent, audioComponent, renderComponent);
        }
    }
}
