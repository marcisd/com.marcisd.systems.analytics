using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		24/07/2021 12:20
===============================================================*/

namespace MSD.Systems.Analytics
{
    /// <summary>
	/// Converts the <see cref="EventContext"/> into a name for the event.
	/// </summary>
    internal class EventNameFormatSpecifier
    {
        private readonly string _separator;

        private readonly int _maxCharacterCount;

        public string Separator => _separator;

        public int MaxCharacterCount => _maxCharacterCount;

        public EventNameFormatSpecifier(string separator, int maxCharacterCount)
		{
            _separator = separator;
            _maxCharacterCount = Mathf.Clamp(maxCharacterCount, 1, 100);
        }

        public string FormatEventName(EventContext context)
        {
            string full = string.Join(_separator, context.NameSections());
            return full.Length < _maxCharacterCount ? full : full.Substring(0, _maxCharacterCount);
        }
    }
}
