package engine.components;

import java.awt.Image;
import java.util.List;

import engine.objects.GameObject;

/** PhysicsComponent handles physics for a GameObject. */
public class PhysicsComponent extends BaseComponent {
	/** The alpha-bitmap of this object, which is used for collision.  Alpha = 0 indicates no collision and alpha = 1 indicates collision at any given pixel. */
	private Image bitmap;
	/** The collision layer of this PhysicsComponent.  Collision layers can be used to ignore collisions with other layers. */
	private String collisionLayer;
	/** A constant that represents gravity. */
	private static final Double GRAIVTY = new Double(-9.8);
	
	/**
	 * Check collisions with other all GameObjects in layers that are not ignored.
	 */
	public List<GameObject> checkCollision() {
		return null;
	}
	
	/**
	 * Ignore collisions with a specific layer.
	 * @param collisionLayer The layer that should be ignored.
	 */
	public void ignoreCollision(String collisionLayer) {
		
	}
}
