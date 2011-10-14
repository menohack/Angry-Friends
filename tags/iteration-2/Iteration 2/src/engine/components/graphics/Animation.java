package engine.components.graphics;

import java.util.List;


/** Animation contains a specific animation and its properties such all of its frames, its name, length and frames-per-second. */
public class Animation {
	/** All of the Frame of this animation. */
	private List<Frame> frames;
	/** The name of this animation. */
	private String name;
	/** The length of this animation. */
	private Double length;
	/** The FPS of this animation. */
	private Integer FPS;
	
	/**
	 * Update the frame of this animation.
	 */
	public void updateFrame() {
		
	}
}
