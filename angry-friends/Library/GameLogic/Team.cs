using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model.GameLogic
{
    /// <summary>
    /// Team represents a collection of Players that are on the same side and fighting together.
    /// </summary>
	[DataContract]
    public class Team
	{
        /// <summary>
        /// The number of Players on this team.
        /// </summary>
		[DataMember]
        public int Count { get; private set; }

        /// <summary>
        /// The Players on this team.
        /// </summary>
		[DataMember]
        public List<Player> Players { get; private set; }

        /// <summary>
        /// The name of this team.
        /// </summary>
		[DataMember]
        public string Name { get; private set; }
	}
}
