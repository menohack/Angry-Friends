using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Friendly_Wars.Engine.Utilities
{
	/// <summary>
	/// An interface for listening to an engineTimer. It recieves the time elapsed from the last interval.
	/// </summary>
	public interface IUpdateable
	{
		/// <summary>
		/// Dispatches an event with the elapsed time, in miliseconds, from the previous update.
		/// </summary>
		/// <param name="deltaTime">The elapsed time, in miliseconds, from the previous update.</param>
		void Update(Double deltaTime);
	}

	/// <summary>
	/// An engineTimer dispatches events to IUpdateables. 
	/// It dispatches the change in time from the last update, all while trying to update at a constant interval.
	/// engineTimer is frame-rate-independent and will account for a drop in frame rate.
	/// </summary>
	public class EngineTimer
	{
		/// <summary>
		/// The timer that tries to update at a given interval.
		/// </summary>
		private DispatcherTimer updateTimer;

		/// <summary>
		/// The interval, in miliseconds, at which the timer will try to update.
		/// </summary>
		public int Interval { get; private set; }

		/// <summary>
		/// The DateTime associated with the last update.
		/// </summary>
		private DateTime previousTime;

		/// <summary>
		/// All of the objects that are listening for dispatched events from this engineTimer.
		/// </summary>
		private ICollection<IUpdateable> eventListeners;

		/// <summary>
		/// Constructor for a new EngineTimer.
		/// </summary>
		/// <param name="interval">The interval, in miliseconds, at which the timer will try to update.</param>
		/// <param name="eventListeners">The EngineTimer's eventListeners.</param>
		public EngineTimer(int interval, ICollection<IUpdateable> eventListeners)
		{
			this.Interval = interval;
			this.eventListeners = eventListeners;
		}

		/// <summary>
		/// Adds an IUpdateable to this EngineTimer.
		/// </summary>
		/// <param name="eventListener">The IUpdateable to add to this engineTimer.</param>
		public void AddEventListener(IUpdateable eventListener)
		{
			eventListeners.Add(eventListener);
		}

		/// <summary>
		/// Removes an IUpdateable from this EngineTimer.
		/// </summary>
		/// <param name="eventListener">The IUpdateable to remove from this engineTimer.</param>
		public void RemoveEventListener(IUpdateable eventListener)
		{
			eventListeners.Remove(eventListener);
		}

		/// <summary>
		/// Start dispatching events.
		/// </summary>
		public void Start()
		{
			updateTimer = new DispatcherTimer();
			updateTimer.Interval = TimeSpan.FromMilliseconds(Interval);
			updateTimer.Tick += new EventHandler(DispatchEvent);
			previousTime = DateTime.Now;
			updateTimer.Start();
		}

		/// <summary>
		/// Stop dispatching events.
		/// </summary>
		public void Stop()
		{
			updateTimer.Stop();
		}

		/// <summary>
		/// Dispatches an event to all listeners.
		/// </summary>
		/// <param name="sender">The Object that called this function.</param>
		/// <param name="e">The event that corresponds to this function.</param>

		private void DispatchEvent(object sender, EventArgs e)
		{
			DateTime currentTime = DateTime.Now;
			
			Double deltaTime = currentTime.Millisecond - previousTime.Millisecond;
			// If we elapsed one second
			if (deltaTime <= 0)
			{
				deltaTime = 1000 - previousTime.Millisecond + currentTime.Millisecond;
			}

			foreach (IUpdateable eventListener in eventListeners)
			{
				eventListener.Update(deltaTime);
			}

			previousTime = DateTime.Now;
		}

		/// <summary>
		/// Converts a given amount of hertz (30 Hz = 1/30) to miliseconds.
		/// </summary>
		/// <param name="hertz">The amount of hertz to convert.</param>
		/// <returns>The converted amount of hertz, now in Miliseconds.</returns>
		public static int FromHertzToMiliSeconds(Int32 hertz)
		{
			Double seconds = 1.00 / Convert.ToDouble(hertz);
			Double miliseconds = 1000 * seconds;
			return Convert.ToInt32(miliseconds);
		}

		/// <summary>
		/// Converts a given amount of seconds to miliseconds.
		/// </summary>
		/// <param name="amount">The amount of seconds to convert into miliseconds.</param>
		/// <returns>The amount of miliseconds in the given amount of seconds.</returns>
		public static int SecondsToMiliseconds(Double amount)
		{
			return Convert.ToInt32(1000.00 * amount);
		}
	}
}
