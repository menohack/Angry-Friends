using System;
using System.Collections.Generic;
using System.Linq;
using Library.Engine.Object;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test {
	[TestClass]
	public class WorldTest {
		[TestMethod]
		public void Create() {
			Assert.IsNotNull(EngineObject.Instance);
		}

		[TestMethod]
		public void CreateAndRetrieveSameReference() {
			EngineObject first = EngineObject.Instance;
			EngineObject second = EngineObject.Instance;
			Assert.ReferenceEquals(first, second);
			Assert.AreSame(first, second);
		}

		[TestMethod]
		public void EmptyOnStart() {
			Assert.IsTrue(EngineObject.Instance.GetGameObjects().Count == 0);
		}

		[TestMethod]
		public void AddGameObject() {
			EngineObject w = EngineObject.Instance;
			int start = w.GetGameObjects().Count;
			w.AddGameObject(new GameObject("test"));

			Assert.IsTrue(w.GetGameObjects().Count == start + 1);
		}

		[TestMethod]
		public void AddGameObjectWithTag() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.AddGameObject(test);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void AddGameObjectTwice() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.AddGameObject(test);
			EngineObject.Instance.AddGameObject(test);
		}

		[TestMethod]
		public void AddGameObjectSameName() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test");
			EngineObject.Instance.AddGameObject(first);
			EngineObject.Instance.AddGameObject(second);
			Assert.IsTrue(EngineObject.Instance.GetGameObjects().Count == 2);
		}

		[TestMethod]
		public void AddGameObjectDifferentNames() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("other");
			EngineObject.Instance.AddGameObject(first);
			EngineObject.Instance.AddGameObject(second);
			Assert.IsTrue(EngineObject.Instance.GetGameObjects().Count == 2);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void AddNullGameObject() {
			EngineObject.Instance.AddGameObject(null);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RemoveNoAddGameObject() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.RemoveGameObject(test);
		}

		[TestMethod]
		public void AddRemoveGameObject() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.AddGameObject(test);
			EngineObject.Instance.RemoveGameObject(test);
			Assert.IsTrue(EngineObject.Instance.GetGameObjects().Count == 0);
		}

		[TestMethod]
		public void AddGameObjectsRemoveOne() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test2");

			EngineObject.Instance.AddGameObject(first);
			EngineObject.Instance.AddGameObject(second);

			EngineObject.Instance.RemoveGameObject(first);

			Assert.IsTrue(EngineObject.Instance.GetGameObjects().Count == 1);
		}

		[TestMethod]
		public void AddRemoveGameObjects() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test2");

			EngineObject.Instance.AddGameObject(first);
			EngineObject.Instance.AddGameObject(second);

			EngineObject.Instance.RemoveGameObject(first);
			EngineObject.Instance.RemoveGameObject(second);

			Assert.IsTrue(EngineObject.Instance.GetGameObjects().Count == 0);
		}

		[TestMethod]
		public void FindObjectByName() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.AddGameObject(test);
			GameObject retrieved = EngineObject.Instance.FindGameObjectsWithName("test").First();
			Assert.IsNotNull(retrieved);
			Assert.AreSame(test, EngineObject.Instance.FindGameObjectsWithName("test").First());
		}

		[TestMethod]
		public void FindObjectByNullName() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.AddGameObject(test);
			Assert.IsTrue(EngineObject.Instance.FindGameObjectsWithName(null).Count == 0);
		}

		[TestMethod]
		public void FindObjectByNonMatchingName() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.AddGameObject(test);
			Assert.IsTrue(EngineObject.Instance.FindGameObjectsWithName("hello").Count == 0);
		}

		[TestMethod]
		public void FindObjectsBySameName() {
			GameObject first = new GameObject("test");
			GameObject second = new GameObject("test");
			GameObject third = new GameObject("test");
			EngineObject.Instance.AddGameObject(first);
			EngineObject.Instance.AddGameObject(second);
			ICollection<GameObject> results = EngineObject.Instance.FindGameObjectsWithName("test");
			Assert.AreEqual(results.Count, 2);
			Assert.IsTrue(results.Contains(first));
			Assert.IsTrue(results.Contains(second));
			Assert.IsFalse(results.Contains(third));
		}

		[TestMethod]
		public void FindObjectByUid() {
			GameObject test = new GameObject("test");
			EngineObject.Instance.AddGameObject(test);
			GameObject result = EngineObject.Instance.FindGameObjectWithUID(test.UID);
			Assert.IsNotNull(result);
			Assert.AreSame(test, result);
		}

		[TestMethod]
		public void FindObjectByNonmatchingUid() {
			GameObject test = new GameObject("test");
			GameObject result = EngineObject.Instance.FindGameObjectWithUID(test.UID);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void FindObjectByNegativeUid() {
			GameObject result = EngineObject.Instance.FindGameObjectWithUID(-1);
			Assert.IsNull(result);
		}

		[TestMethod]
		public void UpdateEmptyWorld() {
			EngineObject.Instance.Update(1.0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldNegativeDelta() {
			EngineObject.Instance.Update(-1.0);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldPInfiniteDelta() {
			EngineObject.Instance.Update(Double.PositiveInfinity);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldNInfiniteDelta() {
			EngineObject.Instance.Update(Double.NegativeInfinity);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void UpdateEmptyWorldNaNDelta() {
			EngineObject.Instance.Update(Double.NaN);
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