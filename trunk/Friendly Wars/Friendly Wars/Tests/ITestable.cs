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

namespace Friendly_Wars.Tests
{
	/// <summary>
	/// Interface that allows testing.
	/// </summary>
	public interface ITestable
	{
		/// <summary>
		/// Runs this given test.
		/// </summary>
		/// <returns>True if the test ran successfully.</returns>
		bool RunTest();
	}
}
