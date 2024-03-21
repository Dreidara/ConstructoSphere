using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ConstructoSphere.Descriptions
{
    [Serializable]
    public class Descriptions
    {
        public string DisplayTitle;
        public Sprite DisplaySprite;
        public Vector3 CameraPosition = new Vector3(0 , 2f , 0f);
        public Vector3 CameraRotation;
        [TextArea(5, 8)] public string DisplayDescriptions;
    }

    [CreateAssetMenu(fileName = "Description Card", menuName = "New Description")]
    public class DescriptionCard : ScriptableObject
    {
        [ListDrawerSettings(ShowIndexLabels = true , ListElementLabelName = "DisplayTitle")]
        public List<Descriptions> DescriptionItems;
    }
}
