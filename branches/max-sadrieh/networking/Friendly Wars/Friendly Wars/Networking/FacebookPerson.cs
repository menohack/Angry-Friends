using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System;
using Facebook;
using System.Text;

namespace Friendly_Wars.Networking
{
    /// <summary>
    /// A user on Facebook.
    /// </summary>
    public class FacebookPerson : ISocialPerson
    {


        /// <summary>
        /// The unique identifier of the person, qualified by a namespace
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The public name of the person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The list of friends of a person
        /// </summary>
        public IList<ISocialPerson> Friends { get; set; }

        private static FacebookOAuthClient OAuth;

        private static FacebookClient FacebookClient;

        public static FacebookPerson()
        {
            OAuth = new FacebookOAuthClient { AppId = "287397801291137", AppSecret = "364a98d9291bb78b437be48ce43e1435" };
            string[] extendedPermissions = new[] { "publish_stream", "offline_access" };

            Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "response_type", "token" }, // might want "code"
                        { "display", "popup" }
                    };
            if (extendedPermissions != null && extendedPermissions.Length > 0)
                {
                    var scope = new StringBuilder();
                    scope.Append(string.Join(",", extendedPermissions));
                    parameters["scope"] = scope.ToString();
                }

            FacebookClient = new FacebookClient("token");
        }

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

            FacebookClient.GetCompleted += (o, e) =>
                {
                    if (e.Error == null)
                    {
                        this.Name = (string)((IDictionary<string, object>)e.GetResultData())["name"];
                    }

                };

            FacebookClient.GetAsync(id.ToString());

            this.Id = id;
        }

        /// <summary>
        /// Publishes a message on behalf of a person
        /// </summary>
        /// <param name="message"></param>
        public void Publish(string message)
        {
            Dictionary<string, object> parameters = new Dictionary<string,object>();
            parameters.Add("message", message);
            FacebookClient.PostAsync(this.Id + "/feed", parameters);
        }

        /// <summary>
        /// Makes a request to a friend of the person on his or her behalf
        /// </summary>
        /// <param name="friend"></param>
        public void MakeRequest(ISocialPerson friend, String message)
        {
            if(this.Friends.Contains(friend))
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("message", message);
                FacebookClient.PostAsync(friend.Id + "/apprequests", parameters);
            }
            else
            {
                // Throw exception (unless it's possible to make a request to a non-friend?)
            }
        }
    }
}
