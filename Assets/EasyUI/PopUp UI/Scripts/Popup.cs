using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using EasyUI.Helpers;
using static EasyUI.Helpers.SO_EasyUI_PopUpUI;


namespace EasyUI.Popup
{
    public static class Popup
    {
        public static bool _isLoaded = false;

        private static PopupUI popupUI;


        private static void Prepare()
        {
            if (!_isLoaded)
            {
                GameObject instance = Object.Instantiate(Resources.Load<GameObject>("PopupUI"));
                instance.name = "[ POPUP UI ]";
                popupUI = instance.GetComponent<PopupUI>();
                _isLoaded = true;

                CheckForEventSystem();
            }
        }

        private static void CheckForEventSystem()
        {
            // Check if there is an EventSystem in the scene (if not, add one)
            EventSystem es = Object.FindObjectOfType<EventSystem>();
            if (object.ReferenceEquals(es, null))
            {
                GameObject esGameObject = new GameObject("EventSystem");
                esGameObject.AddComponent<EventSystem>();
                esGameObject.AddComponent<StandaloneInputModule>();
            }
        }

        public static void Show(Enum_EasyUIKeys keyname)
        {
            PopUIReference scriptableObjectPopUpUI = GetPopUIReference(keyname);

            if (scriptableObjectPopUpUI != null)
            {
                string title = scriptableObjectPopUpUI.title;
                string description = scriptableObjectPopUpUI.description;
                string okButtonString = scriptableObjectPopUpUI.okButtonString;
                Enum_PopupColor enum_PopupColor = scriptableObjectPopUpUI.popupColor;
                popupUI.Show(title, description, okButtonString, enum_PopupColor, null);
            }
        }

        public static void Show(Enum_EasyUIKeys keyname, UnityAction onCloseAction)
        {
            PopUIReference scriptableObjectPopUpUI = GetPopUIReference(keyname);

            if (scriptableObjectPopUpUI != null)
            {
                string title = scriptableObjectPopUpUI.title;
                string description = scriptableObjectPopUpUI.description;
                string okButtonString = scriptableObjectPopUpUI.okButtonString;
                Enum_PopupColor enum_PopupColor = scriptableObjectPopUpUI.popupColor;

                popupUI.Show(title, description, okButtonString, enum_PopupColor, onCloseAction);
            }
        }

        public static void Show(Enum_EasyUIKeys keyname, string description , UnityAction onCloseAction)
        {
            PopUIReference scriptableObjectPopUpUI = GetPopUIReference(keyname);

            if (scriptableObjectPopUpUI != null)
            {
                string title = scriptableObjectPopUpUI.title;
                string okButtonString = scriptableObjectPopUpUI.okButtonString;
                Enum_PopupColor enum_PopupColor = scriptableObjectPopUpUI.popupColor;

                popupUI.Show(title, description, okButtonString, enum_PopupColor, onCloseAction);
            }
        }

        private static PopUIReference GetPopUIReference(Enum_EasyUIKeys keyname)
        {
            Prepare();
            PopUIReference scriptableObjectPopUpUI = popupUI.so_EasyUI_PopUpUI.popUpUIReferences.Find(item => item.keyName == keyname);

            if (scriptableObjectPopUpUI == null)
            {
                Debug.LogError($"The key {keyname} is not found!.");
            }

            return scriptableObjectPopUpUI;
        }



        public static void Dismiss()
        {
            if (_isLoaded)
                popupUI.Dismiss();
        }
    }
}
