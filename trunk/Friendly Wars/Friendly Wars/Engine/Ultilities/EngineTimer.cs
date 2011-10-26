using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;

namespace Friendly_Wars.Engine.Ultilities
{
    /// <summary>
    /// An interface for listening to an EngineTimer. It recieves the time elapsed from the last interval.
    /// </summary>
    public interface IUpdateable
    {
        void Update(Double deltaTime);
    }

    /// <summary>
    /// An EngineTimer dispatches events to IUpdateables. 
    /// It dispatches the change in time from the last update, all while trying to update at a constant interval.
    /// EngineTimer is frame-rate-independent and will account for a drop in frame rate.
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
        public int interval { get; private set; }

        /// <summary>
        /// The DateTime associated with the last update.
        /// </summary>
        private DateTime previousTime;

        /// <summary>
        /// All of the objects that are listening for dispatched events from this EngineTimer.
        /// </summary>
        private ICollection<IUpdateable> eventListeners;

        /// <summary>
        /// Constructor for a new EngineTimer.
        /// </summary>
        /// <param name="interval">The interval, in miliseconds, at which the timer will try to update.</param>
        /// <param name="callBackFunction">The function that will be called back from this Timer.</param>
        public EngineTimer(int interval, ICollection<IUpdateable> eventListeners)
        {
            this.interval = interval;
            this.eventListeners = eventListeners;
        }

        /// <summary>
        /// Adds an IUpdateable to this EngineTimer.
        /// </summary>
        /// <param name="eventListener">The IUpdateable to add to this EngineTimer.</param>
        public void AddEventListener(IUpdateable eventListener)
        {
            eventListeners.Add(eventListener);
        }

        /// <summary>
        /// Removes an IUpdateable from this EngineTimer.
        /// </summary>
        /// <param name="eventListener">The IUpdateable to remove from this EngineTimer.</param>
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
            updateTimer.Interval = TimeSpan.FromMilliseconds(interval);
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
        public static int FromHertzToMiliSeconds(int hertz)
        {
            Double seconds = 1.00 / (1.00 * hertz);
            Double miliseconds = 1000 * seconds;
            return (int)(miliseconds);
        }

        /// <summary>
        /// Converts a given amount of seconds to miliseconds.
        /// </summary>
        /// <param name="amount">The amount of seconds to convert into miliseconds.</param>
        /// <returns>The amount of miliseconds in the given amount of seconds.</returns>
        public static int SecondsToMiliseconds(Double amount) {
            return (int)(1000.00 * amount);
        }
    }
}
