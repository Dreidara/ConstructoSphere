using EasyUI.Toast;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyUI.Helpers
{
    [CreateAssetMenu(fileName = "Toast UI", menuName = "Easy UI SO/Toast UI")]
    public class SO_EasyUI_ToastUI : ScriptableObject
    {
        [System.Serializable]
        public class ToastUIReference
        {
            public Enum_EasyUIKeys keyName;
            public float toastDuration = 2f;
            public Enum_ToastColor color = Enum_ToastColor.Red;
            public Enum_ToastPosition toastPosition = Enum_ToastPosition.TopCenter;
            [TextArea(3, 5)] public string description;

            public string GetName() => $"{keyName} == {toastPosition}";
        }

        [Header("Toast Colors :")]
        public Color[] colors;

        [ListDrawerSettings(ShowIndexLabels = true, NumberOfItemsPerPage = 100 , ListElementLabelName = "GetName")]
        public List<ToastUIReference> toastUIReferences;
    }

}