using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System;

namespace Friendly_Wars.Networking
{
    public class FacebookPerson : ISocialPerson
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
        /// Creates a new FacebookPerson
        /// </summary>
        /// <param name="id"></param>
        public FacebookPerson(string id)
        {
            if (id == null)
                throw new ArgumentNullException("FacebookID cannot be null");
            if (String.IsNullOrWhiteSpace(id))
                throw new ArgumentException("FacebookID cannot be empty");

            //FIXME: Further testing by querying Facebook

            this.Id = id;
        }

        /// <summary>
        /// Publishes a message on behalf of a person
        /// </summary>
        /// <param name="message"></param>
        void Publish(string message)
        {
        }

        /// <summary>
        /// Makes a request to a friend of the person on his or her behalf
        /// </summary>
        /// <param name="friend"></param>
        void MakeRequest(ISocialPerson friend)
        {
        }
    }
}
