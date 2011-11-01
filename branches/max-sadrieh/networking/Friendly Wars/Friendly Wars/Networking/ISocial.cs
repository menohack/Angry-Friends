using System.Collections.Generic;

namespace Friendly_Wars.Networking
{
    public interface ISocial
    {
        IList<IFriend> GetFriends();

        void Publish(string message);

        void MakeRequest(IFriend friend);

    }
}
