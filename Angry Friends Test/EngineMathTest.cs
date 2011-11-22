using System;
using Angry_Friends.Library.Engine.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Friendly_Wars_Test {
	[TestClass]
	public class EngineMathTest {
		[TestMethod]
		public void ExactlyEqual() {
			Assert.IsTrue(EngineMath.Approximately(1.0, 1.0));
		}

		[TestMethod]
		public void ApproximatelyEqual() {
			Assert.IsTrue(EngineMath.Approximately(1.0, 1.0 - Double.Epsilon));
		}

		[TestMethod]
		public void NonEqual() {
			Assert.IsFalse(EngineMath.Approximately(1.0, 5.0));
		}

		[TestMethod]
		public void ZerosEqual() {
			Assert.IsTrue(EngineMath.Approximately(0.0, 0.0));
		}

		[TestMethod]
		public void ZerosAndOtherNonEqual() {
			Assert.IsFalse(EngineMath.Approximately(1.0, 0.0));
		}

		[TestMethod]
		public void NaNNotEqual() {
			Assert.IsFalse(EngineMath.Approximately(Double.NaN, 1.0));
		}

		[TestMethod]
		public void InfinityEqual() {
			Assert.IsTrue(EngineMath.Approximately(double.PositiveInfinity, double.PositiveInfinity));
		}

		[TestMethod]
		public void ClampNoNeed() {
			Assert.AreEqual(1.0, EngineMath.Clamp(1.0, 0.5, 1.5));
		}

		[TestMethod]
		public void ClampFromUnder() {
			Assert.AreEqual(1.0, EngineMath.Clamp(0.5, 1.0, 1.5));
		}

		[TestMethod]
		public void ClampFromAbove() {
			Assert.AreEqual(1.5, EngineMath.Clamp(2.0, 1.0, 1.5));
		}

		[TestMethod]
		public void ClampNaN() {
			Assert.AreEqual(Double.NaN, EngineMath.Clamp(Double.NaN, 1.0, 2.0));
		}

		[TestMethod]
		public void ClampPositiveInfinity() {
			Assert.AreEqual(2.0, EngineMath.Clamp(Double.PositiveInfinity, 1.0, 2.0));
		}
	}
}