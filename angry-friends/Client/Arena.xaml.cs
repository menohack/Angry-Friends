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
using System.Runtime;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
namespace Client {
	public partial class Arena : UserControl {
		public Arena() {
			InitializeComponent();
			DataContractSerializer ser = new DataContractSerializer(typeof(test));
			MemoryStream stream = new MemoryStream();
			ser.WriteObject(stream, new test(5));
			sending.Text = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Position);
		}
	}
	[DataContract]
	public class test {
		[DataMember]
		public Point e { get; set; }
		public test(int a) {
			e = new Point(a,1);
		}
	}
}