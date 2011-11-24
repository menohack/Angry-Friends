using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
namespace Library.Engine.Utilities {

    /// <summary>
    /// A representation of Engine-needed external resources, such as images and sounds.
    /// </summary>
	public class ExternalResource {

		/// <summary>
        /// The types of ExternalResources.
		/// </summary>
		public enum ResourceType { Image, Sound }

		/// <summary>
        /// The priority of downloading this ExternalResource.
		/// </summary>
		public enum ResourcePriority { Sync, Async }

		/// <summary>
        /// The name of this ExternalResource.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
        /// The path to this ExternalResource.
		/// </summary>
		public string URL { get; private set; }

		/// <summary>
        /// The ResourceType of this ExternalResource.
		/// </summary>
		public ResourceType Type { get; private set; }

		/// <summary>
        /// The ResourcePriority of this ExternalResource
		/// </summary>
		public ResourcePriority Priority { get; private set; }

        /// <summary>
        /// The value of this ExternalResource
        /// </summary>
        public object Value { get; set; }

		/// <summary>
		/// Constructor for an ExternalResource.
		/// </summary>
		/// <param name="url">The path to this ExternalResource.</param>
		/// <param name="resourceType">The Type of this ExternalResource.</param>
		/// <param name="priority">The priority of this ExternalResource.</param>
		public ExternalResource(String url, String resourceType, String priority) {
			this.Name = Path.GetFileNameWithoutExtension(url);
			this.URL = url;
			this.Type = (ResourceType)Enum.Parse(typeof(ResourceType), resourceType, true); // Parse the literal string value to the enumerator.

            if (priority == null)
            {
                switch (this.Type)
                {
                    case ResourceType.Image:
                        this.Priority = ResourcePriority.Sync;
                        break;
                    case ResourceType.Sound:
                        this.Priority = ResourcePriority.Async;
                        break;
                }
            }
            else
            {
                this.Priority = (ResourcePriority)Enum.Parse(typeof(ResourcePriority), priority, true);
            }
		}

        /// <summary>
        /// Constructs an image resource with value
        /// </summary>
        /// <param name="url"></param>
        /// <param name="image"></param>
        public ExternalResource(string url, BitmapImage image) : this(url, "image", "sync") {
            Value = image;
        }

        /// <summary>
        /// Constructs a sound resource with value
        /// </summary>
        /// <param name="url"></param>
        /// <param name="image"></param>
        public ExternalResource(string url, MediaElement sound) : this(url, "sound", "sync")
        {
            Value = sound;
        }
    }
}