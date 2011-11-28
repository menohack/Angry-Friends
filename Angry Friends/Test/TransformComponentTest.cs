using System.Windows;
using Library.Engine.Component;
using Library.Engine.Object;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test {
	[TestClass]
	public class TransformComponentTest {
		GameObject go1, go2;

		[TestInitialize]
		public void Initialize() {
			World world = World.Instance;

			go1 = new GameObject("go1", new Point(200.0, 300.0), 0, new Point(100, 100));
			go2 = new GameObject("go2", new Point(400.0, 300.0), 0, new Point(100, 100));
		}

		[TestMethod]
		public void Translate() {
			double xshift = 50.0;
			double yshift = -70.0;
			Point startPosition = go1.TransformComponent.Position;
			Point oldPosition = startPosition;
			go1.TransformComponent.Translate(new Point(xshift, yshift));

			Point newPosition = go1.TransformComponent.Position;
			Assert.IsTrue(newPosition.X == oldPosition.X + xshift, newPosition.X.ToString() + " =/= " + (oldPosition.X + xshift).ToString());
			Assert.IsTrue(newPosition.Y == oldPosition.Y + yshift, newPosition.Y.ToString() + " =/= " + (oldPosition.Y + yshift).ToString());

			oldPosition = go1.TransformComponent.Position;
			go1.TransformComponent.Translate(new Point(-xshift, -yshift));

			newPosition = go1.TransformComponent.Position;
			Assert.IsTrue(newPosition.X == oldPosition.X - xshift, newPosition.ToString() + " =/= " + (oldPosition.X - xshift).ToString());
			Assert.IsTrue(newPosition.Y == oldPosition.Y - yshift, newPosition.ToString() + " =/= " + (oldPosition.Y - yshift).ToString());

			Assert.IsTrue(newPosition.Equals(startPosition), newPosition.ToString() + " =/= " + startPosition.ToString());
		}

		[TestMethod]
		public void Collide() {
			//Try spawning a GameObject on top of another GameObject
			try {
				GameObject go3 = new GameObject("go3", new Point(250.0, 300.0), 0, new Point(100.0, 100.0));
			}
			catch (TransformComponent.CollisionException e) {
				Assert.IsTrue(typeof(TransformComponent.CollisionException).Equals(e.GetType()));
			}

			//Move a GameObject completely around another GameObject without hitting it
			Point shift = new Point(200.0, 210.0);
			Point oldPosition = go1.TransformComponent.Position;
			go1.TransformComponent.Translate(shift);
			Assert.IsTrue(go1.TransformComponent.Position.Equals(new Point(oldPosition.X + shift.X, oldPosition.Y + shift.Y)));

			shift = new Point(200.0, -210.0);
			oldPosition = go1.TransformComponent.Position;
			go1.TransformComponent.Translate(shift);
			Assert.IsTrue(go1.TransformComponent.Position.Equals(new Point(oldPosition.X + shift.X, oldPosition.Y + shift.Y)));

			shift = new Point(-200.0, -210.0);
			oldPosition = go1.TransformComponent.Position;
			go1.TransformComponent.Translate(shift);
			Assert.IsTrue(go1.TransformComponent.Position.Equals(new Point(oldPosition.X + shift.X, oldPosition.Y + shift.Y)));

			shift = new Point(-200.0, 210.0);
			oldPosition = go1.TransformComponent.Position;
			go1.TransformComponent.Translate(shift);
			Assert.IsTrue(go1.TransformComponent.Position.Equals(new Point(oldPosition.X + shift.X, oldPosition.Y + shift.Y)));
		}
	}
}