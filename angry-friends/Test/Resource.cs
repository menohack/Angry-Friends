using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
namespace Library.Engine.Utilities {
	public class Resource {
		/// <summary>
		/// List of resource types
		/// </summary>
		public enum Types { Image, Sound }
		/// <summary>
		/// List of resource priorities
		/// </summary>
		public enum Priorities { Sync, Async }
		/// <summary>
		/// Name of the resource
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Address of the resource
		/// </summary>
		public string URL { get; set; }
		/// <summary>
		/// Resource type
		/// </summary>
		public Types Type { get; set; }
		/// <summary>
		/// Resource priority
		/// </summary>
		public object Value { get; set; }
		public Priorities Priority { get; set; }
		/// <summary>
		/// Constructs an image resource with value
		/// </summary>
		/// <param name="url"></param>
		/// <param name="image"></param>
		public Resource(string url, BitmapImage image)
			: this(url, "image", "sync") {
			Value = image;
		}
		/// <summary>
		/// Constructs a sound resource with value
		/// </summary>
		/// <param name="url"></param>
		/// <param name="image"></param>
		public Resource(string url, MediaElement sound)
			: this(url, "sound", "sync") {
			Value = sound;
		}
		/// <summary>
		/// Constructs a resource
		/// </summary>
		/// <param name="url">Address of the resource</param>
		/// <param name="type">Resource type</param>
		/// <param name="priority">Resource priority</param>
		public Resource(string url, string type, string priority) {
			this.Name = Path.GetFileNameWithoutExtension(url);
			this.URL = url;
			this.Type = (Types)Enum.Parse(typeof(Types), type, true);
			if (priority == null) { // Defaults
				switch (this.Type) {
					case Types.Image: this.Priority = Priorities.Sync; break;
					case Types.Sound: this.Priority = Priorities.Async; break;
				}
			}
			else this.Priority = (Priorities)Enum.Parse(typeof(Priorities), priority, true);
		}
	}
}