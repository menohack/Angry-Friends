using System.Collections.Generic;

namespace Friendly_Wars.Networking
{
    /// <summary>
    /// A general person with friends.
    /// </summary>
    public interface ISocialPerson
    {
        /// <summary>
        /// The unique identifier of the person, qualified by a namespace
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// The public name of the person
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The list of friends of a person
        /// </summary>
        IList<ISocialPerson> Friends { get; set; }

        /// <summary>
        /// Publishes a message on behalf of a person
        /// </summary>
        /// <param name="message">The message to publish</param>
        void Publish(string message);

        /// <summary>
        /// Makes a request to a friend of the person on his or her behalf
        /// </summary>
        /// <param name="friend">The friend to make a request to</param>
        void MakeRequest(ISocialPerson friend, string message);
    }
}
