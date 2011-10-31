package engine.components.graphics;

import java.util.List;

import engine.components.BaseComponent;

/** Handles all aspects of rendering for any GameObject. */
public class RenderComponent extends BaseComponent {

	/** All animations of this RenderComponent. */
	private List<Animation> animations;
	/** The current frame of the current animation. */
	private Frame currentFrame;
	/** The current animation. */
	private Animation currentAnimation;
	
	/** 
	 * Render this component.
	 */
	private void render() {
		
	}
	
	/**
	 * Play a specific animation.
	 * @param animationName The name of the animation to play.
	 */
	public void play(String animationName) {
		
	}
	
	/**
	 * Stop playing the current animation.
	 */
	public void stop() {
		
	}
}
