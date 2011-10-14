package engine.objects;

import java.util.List;

import engine.components.BaseComponent;

/**
 * GameObjects represent a base for all in-game objects.  GameObjects are composed of different BaseComponents, which provide core game-functionality, such as rendering, audio, movement, rotation, physics and networking.
 * @author alexanderschiffhauer
 */
public class GameObject {

	/** The name of the GameObject. */
	private String name;
	/** The tag of the GameObject. */
	private String tag;
	/** The Unique Identifier of the GameObject. */
	private String UID;
	/** The components of the GameObject. */
	private List<BaseComponent> components;
	
	/**
	 * Add a child to the GameObject.
	 * @param gameObject The GameObject that should be parented.
	 */
	public void addChild(GameObject gameObject) {
		
	}
	
	/**
	 * Remove a child from the GameObject.
	 * @param gameObject The GameObject that should be de-parented.
	 */
	public void removeChild(GameObject gameObject) {
		
	}
	
	/**
	 * Destroy this GameObject and all instances of its children.
	 */
	public void destroy() {
		
	}
}
