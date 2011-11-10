using System;

namespace Friendly_Wars.Engine.Utilities
{
	/// <summary>
	/// EngineMath process Engine-related math.
	/// </summary>
	public class EngineMath
	{
		/// <summary>
		/// The cut-off percent-error for an approximation.
		/// </summary>
		private static readonly Double EPSILON = .05;

		/// <summary>
		/// Determines if two Doubles are approximately the same value, where the threshold value of error is .05.
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

		/// <summary>
		/// Clamps a value between its minimum and maximum value.
		/// </summary>
		/// <param name="value">The value in question.</param>
		/// <param name="min">The minimum possible value of this value.</param>
		/// <param name="max">The maximum possible value of this value.</param>
		/// <returns></returns>
		public static Double Clamp(Double value, Double min, Double max)
		{
			if (value > max)
			{
				return max;
			}
			else if (value < min)
			{
				return min;
			}
			else
			{
				return value;
			}
		}
	}
}
