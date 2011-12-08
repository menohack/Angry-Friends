using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
namespace Library.Engine.Utilities {

    /// <summary>
    /// A representation of Engine-needed external assets, such as images and sounds.
    /// </summary>
	public class ExternalAsset {

		/// <summary>
        /// The types of ExternalAsset.
		/// </summary>
		public enum ExternalAssetType { Text, Image, AudioClip, AssetCollection }

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
    }
}