package engine.components;

import java.awt.Point;

/** TransformComponent handles positioning, size and rotation of a GameObject. */
public class TransformComponent extends BaseComponent {
	
	/** The position of the TransformComponent. */
	private Point position;
	/** The size of the TransformComponent. */
	private Point size;
	/** The rotation of the TransformComponent. */
	private Integer rotation;
	
	/** 
	 * Translates by a given X and Y units. 
	 * @param deltaPosition The distance to translate, where positive values for X indicate right and Y indicate up.
	 */
	public void translate(Point deltaPosition) {
		
	}
	
	/** 
	 * Rotates by a given number of degrees.
	 * @param angle The angle, in degrees, in which to rotate.
	 */
	public void rotate(Double angle) {
		
	}
	
	/**
	 * Access the direction (1, 0) with respect to this TransformComponent.
	 * @return The direction (1, 0) with respect to this TransformComponent.
	 */
	public Point localRight() {
		return null;
	}
	
	/**
	 * Access the direction (0, 1) with respect to this TransformComponent.
	 * @return The direction (0, 1) with respect to this TransformComponent.
	 */
	public Point localUp() {
		return null;
	}
	
	/**
	 * Access the direction (0, 1).
	 * @return The direction (0, 1).
	 */
	public static Point globalUp() {
		return null;
	}
	
	/**
	 * Access the direction (1, 0).
	 * @return The direction (1, 0).
	 */
	public static Point globalRight() {
		return null;
	}
	
}
