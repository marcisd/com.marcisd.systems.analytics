using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		25/07/2021 08:58
===============================================================*/

namespace MSD.Systems.Analytics
{
    [CreateAssetMenu(menuName = "MSD/Systems/Analytics/Verb Event Context Component", order = 53)]
    public class VerbEventContextComponent : EventContextComponent
    {
        private void Awake()
        {
            _type = EventContextComponentType.Verb;
        }

        private void OnValidate()
        {
            _type = EventContextComponentType.Verb;
        }
    }
}
