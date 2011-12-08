using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;
using Library.GameLogic;
using System.Diagnostics;

namespace Library.Engine.Utilities {

	/// <summary>
	/// DownloadManager is responsible for downloading ExternalAssets.
	/// </summary>
	public class DownloadManager {
		
        /// <summary>
		/// The single instance of DownloadManager.
		/// </summary>
		private static DownloadManager instance;

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
        /// Accessor for the singleton pattern of DownloadManager.
		/// </summary>
		public static DownloadManager Instance {
			get {
                if (instance == null)
                {
                    instance = new DownloadManager();
                }
				return instance;
			}
		}

		/// <summary>
		/// Download an ExternalAsset.
		/// </summary>
        /// <param name="URL">The URL that points to the ExternalAsset.</param>
        /// <param name="externalAssetType">The type of ExternalAsset to download.</param>
		/// <param name="onLoaded">The event to be fired once the download is completed.</param>
		public void Download(String URL, ExternalAsset.ExternalAssetType externalAssetType, Action<ExternalAsset> onLoaded) {
            switch (externalAssetType) {
                case ExternalAsset.ExternalAssetType.Text:
                    DownloadText(URL, data => onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.Text, data)));
                    break;
				case ExternalAsset.ExternalAssetType.Image:
                    DownloadImage(URL, data => onLoaded(new ExternalAsset(URL, ExternalAsset.ExternalAssetType.Image, data)));
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
		private void DownloadText(String URL, Action<String> onLoaded) 
        {
			WebClient webClient = new WebClient();
			webClient.DownloadStringCompleted += (s, events) => 
            { 
                onLoaded(events.Result); 
            };

			webClient.DownloadStringAsync(new Uri(URL));
		}

		/// <summary>
		/// Download an image.
		/// </summary>
		/// <param name="URL">The URL that points to an image.</param>
		/// <param name="onLoaded">The event to be fired once the download is completed.</param>
		/// <returns>The resulting image if onLoaded is null; otherwise null</returns>
		private void DownloadImage(String URL, Action<BitmapImage> onLoaded) 
        {
            BitmapImage bitmapImage = new BitmapImage() 
            { 
                CreateOptions = BitmapCreateOptions.None
            };
            bitmapImage.ImageOpened += (s, e) => 
            {
                onLoaded(bitmapImage);
            };

            bitmapImage.UriSource = new Uri(URL);
        }

		/// <summary>
		/// Download an audio clip.
		/// </summary>
		/// <param name="URL">The URL that points to an image.</param>
		/// <param name="onLoaded">The event to be fired once the download is completed.</param>
		private void DownloadAudioClip(String URL, Action<MediaElement> onLoaded) 
        {
            MediaElement media = new MediaElement();
			media.Loaded += (s, e) => 
            {
                if (media.DownloadProgress == 1)
                {
                    onLoaded(media);
                }
			};

            media.Source = new Uri(URL);
		}

        /// <summary>
        /// Download a collection of Assets.
        /// </summary>
        /// <param name="URL">The URL that points to an XML file that outlines URLs for other ExternalAssets.</param>
        /// <param name="onLoaded">The event to be fired once the download is completed.</param>
        private void DownloadAssetCollection(String URL, Action<Dictionary<String, object>> onLoaded)
        {
            Dictionary<String, object> externalAssets = new Dictionary<String, object>();
            int externalAssetsLength = 0;

            DownloadText(URL, (e) =>
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(e)))
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
                                    externalAssetsLength++;
                                    DownloadImage(externalAssetURL, loadedImage => {
                                        externalAssets.Add(externalAssetName, loadedImage);
                                        DispatchAssetCollection(--externalAssetsLength, externalAssets, onLoaded);
                                    });
                                    break;
                                case EXTERNAL_ASSET_TYPE_AUDIO_CLIP:
                                    //externalAssetsLength++;
                                    DownloadAudioClip(externalAssetURL, loadedAudioClip =>
                                    {
                                        externalAssets.Add(externalAssetName, loadedAudioClip);
                                        DispatchAssetCollection(--externalAssetsLength, externalAssets, onLoaded);
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
        /// <param name="externalAssetLength">The number of assets left to be processed.</param>
        /// <param name="externalAssets">The processed assets.</param>
        /// <param name="onLoaded">The event to fire when the assets have all been downloaded.</param>
        private void DispatchAssetCollection(int externalAssetLength, Dictionary<String, object> externalAssets, Action<Dictionary<String, object>> onLoaded)
        {
            if (externalAssetLength == 0)
            {
                onLoaded(externalAssets);
            }
        }
	}
}