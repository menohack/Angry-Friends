package engine.components;

import java.util.List;

import engine.objects.GameObject;

/** Handles basic IO for GameObjects between multiple connected clients. */
public class NetworkComponent extends BaseComponent {
	/** The GameObjects that listen to this NetworkComponent. */
	private List<GameObject> listeners;
	
	/**
	 * Dispatch an update to all listeners.
	 */
	public void dispatchUpdate() {
		
	}
	
	/**
	 * Receive an update from a GameObject.
	 * @param gameObject The GameObject, whose NetworkComponent  dispatched an event.
	 */
	 public void recieveUpdate(GameObject gameObject) {
		 
	 }
	 
	 /** 
	  * Add a GameObject to listen to.
	  * @param listener The GameObject to listen to.
	  */
	 public void addListener(GameObject listener) {
		 
	 }
	 
	 /** 
	  * Stop listening to a specific GameObject.
	  * @param listener The GameObject to stop listening to.
	  */
	 public void removeListener(GameObject listener) {
		 
	 }
}
