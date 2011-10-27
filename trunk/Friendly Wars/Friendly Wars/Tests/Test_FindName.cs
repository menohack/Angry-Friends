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
	/// Tests the name-finding ability of GameObjects
	/// </summary>
	public class Test_FindName : ITestable
	{
		/// <summary>
		/// Tests to see if GameObjects are returned properly with names.
		/// </summary>
		/// <returns>True if the test works.</returns>
		public bool RunTest()
		{
			GameObject go1 = new GameObject("1");
			GameObject go2 = new GameObject("2");

			ICollection<GameObject> list1 = World.FindGameObjectsWithName("1");
			ICollection<GameObject> list2 = World.FindGameObjectsWithName("2");

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
