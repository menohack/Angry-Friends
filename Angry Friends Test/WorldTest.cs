using System;
using System.Collections.Generic;
using System.Linq;
using Angry_Friends.Library.Engine.Object;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Friendly_Wars_Test {
	[TestClass]
	public class WorldTest {
		[TestMethod]
		public void Create() {
			Assert.IsNotNull(World.Instance);
		}

		[TestMethod]
		public void CreateAndRetrieveSameReference() {
			World first = World.Instance;
			World second = World.Instance;
			Assert.ReferenceEquals(first, second);
			Assert.AreSame(first, second);
		}

		[TestMethod]
		public void EmptyOnStart() {
			Assert.IsTrue(World.Instance.GetGameObjects().Count == 0);
		}

		[TestMethod]
		public void AddGameObject() {
			World w = World.Instance;
			int start = w.GetGameObjects().Count;
			w.AddGameObject(new GameObject("test"));

			Assert.IsTrue(w.GetGameObjects().Count == start + 1);
		}

		[TestMethod]
		public void AddGameObjectWithTag() {
			GameObject test = new GameObject("test", "mytag");
			World.Instance.AddGameObject(test);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void AddGameObjectTwice() {
			GameObject test = new GameObject("test", "mytag");
			World.Instance.AddGameObject(test);
			World.Instance.AddGameObject(test);
		}

		[TestMethod]
		public void AddGameObjectSameName() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test");
			World.Instance.AddGameObject(first);
			World.Instance.AddGameObject(second);
			Assert.IsTrue(World.Instance.GetGameObjects().Count == 2);
		}

		[TestMethod]
		public void AddGameObjectDifferentNames() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("other");
			World.Instance.AddGameObject(first);
			World.Instance.AddGameObject(second);
			Assert.IsTrue(World.Instance.GetGameObjects().Count == 2);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void AddNullGameObject() {
			World.Instance.AddGameObject(null);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RemoveNoAddGameObject() {
			GameObject test = new GameObject("test");
			World.Instance.RemoveGameObject(test);
		}

		[TestMethod]
		public void AddRemoveGameObject() {
			GameObject test = new GameObject("test");
			World.Instance.AddGameObject(test);
			World.Instance.RemoveGameObject(test);
			Assert.IsTrue(World.Instance.GetGameObjects().Count == 0);
		}

		[TestMethod]
		public void AddGameObjectsRemoveOne() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test2");

			World.Instance.AddGameObject(first);
			World.Instance.AddGameObject(second);

			World.Instance.RemoveGameObject(first);

			Assert.IsTrue(World.Instance.GetGameObjects().Count == 1);
		}

		[TestMethod]
		public void AddRemoveGameObjects() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test2");

			World.Instance.AddGameObject(first);
			World.Instance.AddGameObject(second);

			World.Instance.RemoveGameObject(first);
			World.Instance.RemoveGameObject(second);

			Assert.IsTrue(World.Instance.GetGameObjects().Count == 0);
		}

		[TestMethod]
		public void FindObjectByName() {
			GameObject test = new GameObject("test");
			World.Instance.AddGameObject(test);
			GameObject retrieved = World.Instance.FindGameObjectsWithName("test").First();
			Assert.IsNotNull(retrieved);
			Assert.AreSame(test, World.Instance.FindGameObjectsWithName("test").First());
		}

		[TestMethod]
		public void FindObjectByNullName() {
			GameObject test = new GameObject("test");
			World.Instance.AddGameObject(test);
			Assert.IsTrue(World.Instance.FindGameObjectsWithName(null).Count == 0);
		}

		[TestMethod]
		public void FindObjectByNonMatchingName() {
			GameObject test = new GameObject("test");
			World.Instance.AddGameObject(test);
			Assert.IsTrue(World.Instance.FindGameObjectsWithName("hello").Count == 0);
		}

		[TestMethod]
		public void FindObjectsBySameName() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test");
			GameObject third = new GameObject("test");
			World.Instance.AddGameObject(first);
			World.Instance.AddGameObject(second);
			ICollection<GameObject> results = World.Instance.FindGameObjectsWithName("test");
			Assert.AreEqual(results.Count, 2);
			Assert.IsTrue(results.Contains(first));
			Assert.IsTrue(results.Contains(second));
			Assert.IsFalse(results.Contains(third));
		}

		[TestMethod]
		public void FindObjectByTag() {
			GameObject test = new GameObject("test", "mytag");
			GameObject other = new GameObject("other", "mytag");
			World.Instance.AddGameObject(test);
			ICollection<GameObject> results = World.Instance.FindGameObjectsWithTag("mytag");
			Assert.AreEqual(results.Count, 1);
			Assert.IsTrue(results.Contains(test));
			Assert.IsFalse(results.Contains(other));
		}

		[TestMethod]
		public void FindObjectByNullTag() {
			GameObject test = new GameObject("test", "mytag");
			GameObject other = new GameObject("other", "mytag");
			ICollection<GameObject> results = World.Instance.FindGameObjectsWithTag(null);
			Assert.AreEqual(results.Count, 0);
		}

		[TestMethod]
		public void FindObjectByNonmatchingTag() {
			GameObject test = new GameObject("test", "mytag");
			GameObject other = new GameObject("other", "mytag");
			ICollection<GameObject> results = World.Instance.FindGameObjectsWithTag("othertag");
			Assert.AreEqual(results.Count, 0);
		}

		[TestMethod]
		public void FindObjectsByTag() {
			GameObject test = new GameObject("test", "mytag");
			GameObject second = new GameObject("other", "othertag");
			GameObject other = new GameObject("other", "mytag");
			World.Instance.AddGameObject(test);
			World.Instance.AddGameObject(second);
			World.Instance.AddGameObject(other);
			ICollection<GameObject> results = World.Instance.FindGameObjectsWithTag("mytag");
			Assert.AreEqual(results.Count, 2);
			Assert.IsTrue(results.Contains(test));
			Assert.IsFalse(results.Contains(second));
			Assert.IsTrue(results.Contains(other));
		}

		[TestMethod]
		public void FindObjectByUid() {
			GameObject test = new GameObject("test", "mytag");
			World.Instance.AddGameObject(test);
			GameObject result = World.Instance.FindGameObjectWithUID(test.UID);
			Assert.IsNotNull(result);
			Assert.AreSame(test, result);
		}

		[TestMethod]
		public void FindObjectByNonmatchingUid() {
			GameObject test = new GameObject("test", "mytag");
			GameObject result = World.Instance.FindGameObjectWithUID(test.UID);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void FindObjectByNegativeUid() {
			GameObject result = World.Instance.FindGameObjectWithUID(-1);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void UpdateEmptyWorld() {
			World.Instance.Update(1.0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldNegativeDelta() {
			World.Instance.Update(-1.0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldPInfiniteDelta() {
			World.Instance.Update(Double.PositiveInfinity);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldNInfiniteDelta() {
			World.Instance.Update(Double.NegativeInfinity);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldNaNDelta() {
			World.Instance.Update(Double.NaN);
		}

		[TestMethod]
		public void UpdateGameObject() {
			//TODO: Create something that makes sense.
		}

		[TestMethod]
		public void RedrawQueue() {
			//TODO: Anything makes sense?
		}
	}
}