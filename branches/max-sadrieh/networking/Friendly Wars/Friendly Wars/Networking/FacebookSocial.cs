using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System;

namespace Friendly_Wars.Networking
{
    public class FacebookSocial : ISocial
    {
        string id;

        public FacebookSocial(string id)
        {
            if (id == null)
                throw new ArgumentNullException("FacebookID cannot be null");
            if (String.IsNullOrWhiteSpace(id))
                throw new ArgumentException("FacebookID cannot be empty");

            this.id = id;
        }

        public IList<IFriend> GetFriends()
        {
            return null;
        }

        public void Publish(string message)
        {
        }

        public void MakeRequest(IFriend friend)
        {
        }
    }
}
