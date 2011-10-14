package engine.components;

import engine.objects.GameObject;

public class BaseComponent {
	/** Is this BaseComponent enabled? */
	private boolean enabled = true;
	/** The GameObject that is the owner of this component. */
	private GameObject owner;
	
	/**
	 * Disable this BaseComponent.
	 */
	public void disable() {
		
	}
	
	/**
	 * Enable this BaseComponent.
	 */
	public void enable () {
		
	}
}
