using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace EasyUI.Helpers
{
    [CreateAssetMenu(fileName ="Confirm UI" , menuName = "Easy UI SO/ConfirmUI")]
    public class SO_EasyUI_ConfirmUI : ScriptableObject
    {
        [System.Serializable]
        public class ConfirmUIReference
        {
            public Enum_EasyUIKeys keyName;
            [TextArea(1, 3)] public string title;
            [TextArea(3,5)] public string description;
            public string okButtonString;
            public string cancelButtonString;
        }

        [Header("Confirm UI Colors :")]
        public Color[] colors;

        [ListDrawerSettings(ShowIndexLabels = true , NumberOfItemsPerPage = 100 , ListElementLabelName = "keyName")]
        public List<ConfirmUIReference> confirmUIReferences;
    }

}

