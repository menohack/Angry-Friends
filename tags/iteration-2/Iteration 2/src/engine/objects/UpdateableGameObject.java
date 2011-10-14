package engine.objects;

/**  UpdateableGameObjects are all somewhat autonomous in that they will be updated at, either a given time interval, or as frequently as possible (whenever WorldObject is updated). */
public class UpdateableGameObject extends GameObject {
	/** The specific update time for this GameObject.  If set to 0, it will update as frequently as WorldObject. */
	private Double updateTime;
	
	/**
	 * Update this GameObject on a regular interval.
	 */
	public void update() {
		
	}
}
