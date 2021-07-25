using System;
using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		24/07/2021142:12
===============================================================*/

namespace MSD.Systems.Analytics.Editor
{
    internal static class Styles
    {
		private static readonly Lazy<GUIStyle> s_richHelpBox = new Lazy<GUIStyle>(() => {
			return new GUIStyle(EditorStyles.helpBox) {
				richText = true,
			};
		});

		public static GUIStyle RichHelpBox => s_richHelpBox.Value;
	}
}
