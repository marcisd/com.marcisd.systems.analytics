
/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		17/06/2020 08:13
===============================================================*/

namespace MSD.Systems.Analytics
{
	public enum EventContextComponentType
	{
		/// <summary>
		/// The different concepts in your game you want to track.
		/// </summary>
		Classification,

		/// <summary>
		/// The entity at which the action of the verb is directed.
		/// </summary>
		Subject,

		/// <summary>
		/// The action carried out to the subject.
		/// </summary>
		Verb,
	}
}
