using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		25/07/2021 08:58
===============================================================*/

namespace MSD.Systems.Analytics
{
    [CreateAssetMenu(menuName = "MSD/Systems/Analytics/Subject Event Context Component", order = 52)]
    public class SubjectEventContextComponent : EventContextComponent
    {
        private void Awake()
        {
            _type = EventContextComponentType.Subject;
        }

        private void OnValidate()
        {
            _type = EventContextComponentType.Subject;
        }
    }
}
