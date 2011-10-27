using Friendly_Wars.Engine.Utilities;
namespace Friendly_Wars.Tests {
	/// <summary>
	/// Test for downloading media
	/// </summary>
	public static class Test_Web : ITestable {
		/// <summary>
		/// Runs the test
		/// </summary>
		/// <return>True if the test works.</return>
		public static bool RunTest() {
			new Web();
			Web.DownloadImage("http://www.google.com/intl/en_com/images/srpr/logo3w.png");
			Web.DownloadString("http://www.google.com");
			Web.DownloadSound("http://www.choosah.com/example.wav");
			return true;
		}
	}
}
