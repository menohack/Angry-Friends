using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Model.Engine.Utilities {

	/// <summary>
	/// AssetManager is responsible for downloading ExternalAssets.
	/// </summary>
	public class AssetManager {
		
        /// <summary>
		/// The single instance of AssetManager.
		/// </summary>
		private static AssetManager instance;

        /// <summary>
        /// A constant used to represent the XML identifier "externalAsset".
        /// </summary>
        private static readonly String EXTERNAL_ASSET = "externalAsset";

        /// <summary>
        /// A constant used to represent the XML identifier "type".
        /// </summary>
        private static readonly String EXTERNAL_ASSET_TYPE = "type";

        /// <summary>
        /// A constant used to represent the XML identifier "image".
        /// </summary>
        private const String EXTERNAL_ASSET_TYPE_IMAGE = "image";

        /// <summary>
        /// A constant used to represent the XML identifier "audioClip".
        /// </summary>
        private const String EXTERNAL_ASSET_TYPE_AUDIO_CLIP = "audioClip";

        /// <summary>
        /// A constant used to represent the XML identifier "name".
        /// </summary>
        private static readonly String EXTERNAL_ASSET_NAME = "name";

        /// <summary>
        /// A constant used to represent the XML identifier "url".
        /// </summary>
        private static readonly String EXTERNAL_ASSET_URL = "url";

        /// <summary>
        /// Accessor for the dictionary of ExternalAssets downloaded.
        /// </summary>
        public Dictionary<String, ExternalAsset> ExternalAssets { get; private set; }
		
        /// <summary>
        /// Accessor for the singleton pattern of AssetManager.
		/// </summary>
		public static AssetManager Instance {
			get {
                if (instance == null)
                {
                    instance = new AssetManager();
                }
				return instance;
			}
		}

        /// <summary>
        /// Constructor for a new AssetManager.
        /// </summary>
        private AssetManager()
        {
            ExternalAssets = new Dictionary<String, ExternalAsset>();
        }

		/// <summary>
		/// Download an ExternalAsset.
		/// </summary>
        /// <param name="URL">The URL that points to the ExternalAsset.</param>
        /// <param name="externalAssetType">The type of ExternalAsset to download.</param>
		/// <param name="onLoaded">The event to be fired once the download is completed.</param>
		public void Download(String URL, ExternalAsset.ExternalAssetType externalAssetType, Action<ExternalAsset> onLoaded) {
            switch (externalAssetType) {
                case ExternalAsset.ExternalAssetType.String:
                    DownloadString(URL, data => onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.String, data)));
                    break;
				case ExternalAsset.ExternalAssetType.BitmapImage:
                    DownloadBitmapImage(URL, data => onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.BitmapImage, data)));
                    break;
				case ExternalAsset.ExternalAssetType.AudioClip:
                    DownloadAudioClip(URL, data => onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.AudioClip, data)));
                    break;
                case ExternalAsset.ExternalAssetType.AssetCollection:
                    DownloadAssetCollection(URL, data => onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.AssetCollection, data)));
                    break;
			}
		}
		/// <summary>
		/// Download text.
		/// </summary>
		/// <param name="URL">The URL that points to the text.</param>
		/// <param name="onLoaded">The event to be fired once the download is completed.</param>
		private void DownloadString(String URL, Action<ExternalAsset> onLoaded) 
        {
			WebClient webClient = new WebClient();
			webClient.DownloadStringCompleted += (s, events) => 
            { 
                onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.String, events.Result)); 
            };

			webClient.DownloadStringAsync(new Uri(URL));
		}

		/// <summary>
		/// Download an image.
		/// </summary>
		/// <param name="URL">The URL that points to an image.</param>
		/// <param name="onLoaded">The event to be fired once the download is completed.</param>
		/// <returns>The resulting image if onLoaded is null; otherwise null</returns>
		private void DownloadBitmapImage(String URL, Action<ExternalAsset> onLoaded) 
        {
            BitmapImage bitmapImage = new BitmapImage() 
            { 
                CreateOptions = BitmapCreateOptions.None
            };
            bitmapImage.ImageOpened += (s, e) => 
            {
                onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.BitmapImage, bitmapImage));
            };

            bitmapImage.UriSource = new Uri(URL);
        }

		/// <summary>
		/// Download an audio clip.  This DOES NOT work.  I couldn't tell you why, either...
		/// </summary>
		/// <param name="URL">The URL that points to an image.</param>
		/// <param name="onLoaded">The event to be fired once the download is completed.</param>
		private void DownloadAudioClip(String URL, Action<ExternalAsset> onLoaded) 
        {
            MediaElement media = new MediaElement();
			media.Loaded += (s, e) => 
            {
                if (media.DownloadProgress == 1)
                {
                    onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.AudioClip, media));
                }
			};

            media.Source = new Uri(URL);
		}

        /// <summary>
        /// Download a collection of Assets.
        /// </summary>
        /// <param name="URL">The URL that points to an XML file that outlines URLs for other ExternalAssets.</param>
        /// <param name="onLoaded">The event to be fired once the download is completed.</param>
        private void DownloadAssetCollection(String URL, Action<Dictionary<String, ExternalAsset>> onLoaded)
        {
            int externalAssetsToProcess = 0;

            DownloadString(URL, (e) =>
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(e.GetString())))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == EXTERNAL_ASSET) {

                            String externalAssetType = reader.GetAttribute(EXTERNAL_ASSET_TYPE);
                            String externalAssetName = reader.GetAttribute(EXTERNAL_ASSET_NAME);
                            String externalAssetURL = reader.GetAttribute(EXTERNAL_ASSET_URL);

                            switch (externalAssetType)
                            {
                                case EXTERNAL_ASSET_TYPE_IMAGE:
                                    externalAssetsToProcess++;
                                    DownloadBitmapImage(externalAssetURL, loadedImage => {
                                        ExternalAssets.Add(externalAssetName, loadedImage);
                                        DispatchAssetCollection(--externalAssetsToProcess, onLoaded);
                                    });
                                    break;
                                case EXTERNAL_ASSET_TYPE_AUDIO_CLIP:
                                    externalAssetsToProcess++;
                                    DownloadAudioClip(externalAssetURL, loadedAudioClip =>
                                    {
                                        ExternalAssets.Add(externalAssetName, loadedAudioClip);
                                        DispatchAssetCollection(--externalAssetsToProcess, onLoaded);
                                    });
                                    break;
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// If all the assets have been loaded for a given AssetCollection, the map's contents will be dispatched via an event.
        /// </summary>
        /// <param name="externalAssetsToProcess">The number of assets left to be processed.</param>
        /// <param name="onLoaded">The event to fire when the assets have all been downloaded.</param>
        private void DispatchAssetCollection(int externalAssetsToProcess, Action<Dictionary<String, ExternalAsset>> onLoaded)
        {
            if (externalAssetsToProcess == 0)
            {
                onLoaded(ExternalAssets);
            }
        }
	}
}