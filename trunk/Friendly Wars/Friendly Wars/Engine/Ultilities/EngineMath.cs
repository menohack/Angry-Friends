using System;

namespace Friendly_Wars.Engine.Ultilities
{
	/// <summary>
	/// EngineMath process Engine-related math.
	/// </summary>
	public class EngineMath
	{
		/// <summary>
		/// The cut-off value for an approximation.
		/// </summary>
		private static readonly Double EPSILON = .01;

		/// <summary>
		/// Determines if two Doubles are approximately the same value.
		/// </summary>
		/// <param name="firstNumber">The first number.</param>
		/// <param name="secondNumber">The second number.</param>
		/// <returns> True if the numbers are approximately the same. </returns>
		public static bool Approximately(Double firstNumber, Double secondNumber)
		{
			if (Math.Abs(firstNumber - secondNumber) < EPSILON)
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
