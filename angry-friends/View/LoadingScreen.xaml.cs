using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Model.Engine.Utilities;
using System.Diagnostics;
using Model.Engine.Object;
using System.Windows.Media.Imaging;

namespace View
{
    /// <summary>
    /// Displays a loading screen while requierd assets are being downloaded.
    /// </summary>
    public partial class LoadingScreen : UserControl
    {

        /// <summary>
        /// The path to the required assets on XML.
        /// </summary>
        private static readonly String PATH_TO_REQUIRED_ASSETS = "http://alexanderschiffhauer.com/AngryFriends/Assets/AssetCollections/required_assets.XML";

        /// <summary>
        /// The constructor for a new LoadingScreen.
        /// </summary>
        public LoadingScreen ()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// Initializes this LoadingScreen.
        /// </summary>
        public void Initialize()
        {
            AssetManager.Instance.Download(PATH_TO_REQUIRED_ASSETS, ExternalAsset.ExternalAssetType.AssetCollection, (e) =>
                {
                    Dictionary<String, ExternalAsset> dictionary = (Dictionary<String, ExternalAsset>)e.GetAssetCollection();
                    ViewHelper.Instance.NextUserControl();
                });
        }
    }
}
