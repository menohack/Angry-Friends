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
using System.Collections.Generic;

namespace Friendly_Wars.Tests
{
	/// <summary>
	/// Tests the Tag-finding ability of GameObjects
	/// </summary>
	public class Test_FindTag : ITestable
	{
		/// <summary>
		/// Tests to see if tags can be properly found.
		/// </summary>
		/// <returns>True if the test works.</returns>
		public bool RunTest()
		{
			GameObject go1 = new GameObject("1", "1");
			GameObject go2 = new GameObject("2", "1");

			ICollection<GameObject> list1 = World.FindGameObjectsWithTag("1");
			ICollection<GameObject> list2 = World.FindGameObjectsWithTag("2");

			if (list1.Count == 1 && list2.Count == 2)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
	}
}
