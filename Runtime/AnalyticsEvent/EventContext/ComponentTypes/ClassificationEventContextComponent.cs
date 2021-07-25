using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		25/07/2021 08:58
===============================================================*/

namespace MSD.Systems.Analytics
{
    [CreateAssetMenu(menuName = "MSD/Systems/Analytics/Classification Event Context Component", order = 51)]
    public class ClassificationEventContextComponent : EventContextComponent
    {
        private void Awake()
        {
            _type = EventContextComponentType.Classification;
        }

		private void OnValidate()
		{
            _type = EventContextComponentType.Classification;
        }
	}
}
