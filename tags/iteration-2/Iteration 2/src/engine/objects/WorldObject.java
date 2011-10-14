package engine.objects;

import java.util.List;

/**
 * WorldObject contains is the "universe" of an instance of a game created with this engine.  The WorldObject not only contains all GameObjects, but it also provides functionality for finding GameObjects and drawing GameObjects at appropriate times.
 * @author alexanderschiffhauer
 */
public class WorldObject extends UpdateableGameObject {
	/** All of the GameObjects in the WorldObject, excluding itself. */
	private List<GameObject> gameObjects;
	/** The queue for GameObjects that need to be drawn the next time WordObject updates. */
	private List<GameObject> redrawQueue;
	/** Autonomous GameObjects that are UpdateableGameObjects like WorldObject. */
	private List<GameObject> updateableGameObjects;
	
	/** Access all of the GameObjects that contain a specific name.
	 * @param name The name that will be searched.
	 * @return All of the GameObjects that contain the specific name. 
	 * */
	 public List<GameObject> getGameObjectsByName(String name) {
		 return null;
	 }
	 
	/** Access all of the GameObjects that contain a specific tag.
	 * @param name The tag that will be searched.
	 * @return All of the GameObjects that contain the specific tag. 
	 * */
	 public List<GameObject> getGameObjectsByTag(String tag) {
		 return null;
	 }
	 
	 /** Access all of the GameObjects that contain a specific UID.
	 * @param name The UID that will be searched.
	 * @return All of the GameObjects that contain the specific UID. 
	 * */
	 public List<GameObject> getGameObjectsByUID(String UID) {
		 return null;
	 }
	 
	 /**
	  * Updates this WorldObject on a regular interval.
	  */
	 @Override
	 public void update() {
		 
	 }
}
