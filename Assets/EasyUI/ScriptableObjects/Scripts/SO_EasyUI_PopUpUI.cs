using EasyUI.Popup;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace EasyUI.Helpers
{
    [CreateAssetMenu(fileName = "PopUp UI", menuName = "Easy UI SO/PopUpUI")]
    public class SO_EasyUI_PopUpUI : ScriptableObject
    {
        [System.Serializable]
        public class PopUIReference
        {
            public Enum_EasyUIKeys keyName;
            [TextArea(1, 3)] public string title;
            [TextArea(3, 5)] public string description;
            public Enum_PopupColor popupColor = Enum_PopupColor.Alpha;
            public string okButtonString = "CLOSE";

            public string GetName() => $"{keyName} == {title} == {description}";
        }

        [Header("Popup Colors :")]
        public Color[] colors;

        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 100, ListElementLabelName = "GetName")]
        public List<PopUIReference> popUpUIReferences;
    }

}

