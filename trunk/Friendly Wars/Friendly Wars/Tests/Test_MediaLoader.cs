using Friendly_Wars.Engine.Utilities;
namespace Friendly_Wars.Tests {
	/// <summary>
	/// Test for loading media
	/// </summary>
	public static class Test_MediaLoader : ITestable {
		/// <summary>
		/// Runs the test
		/// </summary>
		/// <return>True if the test works.</return>
		public static bool RunTest() {
			new MediaLoader();
			string test_case = @"
				<map title='beach'>
					<resources>
						<sound url='explosion_0.wav'/>
						<image url='moving_1.png'/>
						<image url='moving_2.png'/>
						<image url='moving_3.png'/>
						<image url='moving_4.png'/>
						<image url='moving_5.png'/>
					</resources>
				</map>
				";
			MediaLoader.LoadMapXml(test_case);
			return true;
		}
	}
}
