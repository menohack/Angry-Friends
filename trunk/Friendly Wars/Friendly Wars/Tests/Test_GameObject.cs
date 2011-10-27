using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Friendly_Wars.Engine.Object;

namespace Friendly_Wars.Tests
{
	/// <summary>
	/// Memory test for GameObjects.
	/// </summary>
	public class Test_GameObject : ITestable
	{
		/// <summary>
		/// Tests to see if the World can handle 10000 GameObjects
		/// </summary>
		/// <returns>True if the test works.</returns>
		public bool RunTest()
		{
			for (int i = 0; i < 10000; i++)
			{
				new GameObject(i.ToString());
			}
			return true;
		}
	}
}
