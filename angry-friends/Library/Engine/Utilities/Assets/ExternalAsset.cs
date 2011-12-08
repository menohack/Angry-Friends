using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Collections.Generic;
namespace Library.Engine.Utilities {

    /// <summary>
    /// A representation of Engine-needed external assets.
    /// </summary>
	public class ExternalAsset {

		/// <summary>
        /// The types of ExternalAsset.
		/// </summary>
		public enum ExternalAssetType { String, BitmapImage, AudioClip, AssetCollection }

		/// <summary>
        /// The name of this ExternalAsset.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
        /// The path to this ExternalAsset.
		/// </summary>
		public string URL { get; private set; }

		/// <summary>
        /// The ExternalAssetType of this ExternalAsset.
		/// </summary>
		public ExternalAssetType Type { get; private set; }

        /// <summary>
        /// The value of this ExternalAsset
        /// </summary>
        public object Value { get; set; }

		/// <summary>
        /// Constructor for an ExternalAsset.
		/// </summary>
        /// <param name="URL">The path to this ExternalAsset.</param>
        /// <param name="externalResourceType">The Type of this ExternalAsset.</param>
        public ExternalAsset(String URL, ExternalAsset.ExternalAssetType externalAssetType, object value)
        {
			this.Name = Path.GetFileNameWithoutExtension(URL);
			this.URL = URL;
            this.Type = externalAssetType;
            this.Value = value;
		}

        /// <summary>
        /// Gets the value of this ExternalAsset as a String.
        /// </summary>
        /// <returns>The value of this ExternalAsset as a String.</returns>
        public String GetString() {
            if (Type == ExternalAssetType.String)
            {
                return (String)Value;
            }
            else
            {
                throw new Exception("The type of this ExternalAsset is not a String.");
            }
        }

        /// <summary>
        /// Gets the value of this ExternalAsset as a BitmapImage.
        /// </summary>
        /// <returns>The value of this ExternalAsset as a BitmapImage.</returns>
        public BitmapImage GetBitmapImage()
        {
            if (Type == ExternalAssetType.BitmapImage)
            {
                return (BitmapImage)Value;
            }
            else
            {
                throw new Exception("The type of this ExternalAsset is not a BitmapImage.");
            }
        }

        /// <summary>
        /// Gets the value of this ExternalAsset as an AudioClip.
        /// </summary>
        /// <returns>The value of this ExternalAsset as an AudioClip.</returns>
        public MediaElement GetAudioClip()
        {
            if (Type == ExternalAssetType.AudioClip)
            {
                return (MediaElement)Value;
            }
            else
            {
                throw new Exception("The type of this ExternalAsset is not an AudioClip.");
            }
        }

        /// <summary>
        /// Gets the value of this ExternalAsset as a Dictionary of Strings-to-ExternalAssets.
        /// </summary>
        /// <returns>The value of this ExternalAsset as a Dictionary of Strings-to-ExternalAssets.</returns>
        public Dictionary<String, ExternalAsset> GetAssetCollection()
        {
            if (Type == ExternalAssetType.AssetCollection)
            {
                return (Dictionary<String, ExternalAsset>)Value;
            }
            else
            {
                throw new Exception("The type of this ExternalAsset is not an AssetCollection.");
            }
        }
    }
}