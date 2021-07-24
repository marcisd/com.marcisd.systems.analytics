
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
		/// The entity which is carrying out the action.
		/// </summary>
		Subject,

		/// <summary>
		/// The action being done by the subject.
		/// </summary>
		Verb,
	}
}
