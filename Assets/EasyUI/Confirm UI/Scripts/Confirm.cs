using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using EasyUI.Helpers;
using static EasyUI.Helpers.SO_EasyUI_ConfirmUI;
using System.Linq;

namespace EasyUI.Confirm
{
    public static class Confirm
    {
        public static bool _isLoaded = false;

        private static ConfirmUI confirmUI;

        private static void Prepare()
        {
            if (!_isLoaded)
            {
                GameObject instance = Object.Instantiate(Resources.Load<GameObject>("ConfirmUI"));
                instance.name = "[ CONFIRM UI ]";
                confirmUI = instance.GetComponent<ConfirmUI>();
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

        public static void Show(Enum_EasyUIKeys keyname, UnityAction oncOkAction, UnityAction oncancelAction)
        {
            Prepare();
            ConfirmUIReference scriptableObjectConfirnUI = confirmUI.so_EasyUI_ConfirmUI.confirmUIReferences.FirstOrDefault(item => item.keyName == keyname);

            if (scriptableObjectConfirnUI != null)
            {
                string title = scriptableObjectConfirnUI.title;
                string description = scriptableObjectConfirnUI.description;
                string okButtonString = scriptableObjectConfirnUI.okButtonString;
                string cancelButtonString = scriptableObjectConfirnUI.cancelButtonString;
                confirmUI.Show(title, description , okButtonString, cancelButtonString, Enum_ConfirmColor.Black, Enum_ConfirmColor.Black, oncOkAction, oncancelAction);
            }

            else
            {
                Debug.LogError($"The key {keyname} is not found!.");
            }
        }



        public static void Dismiss()
        {
            if (_isLoaded)
                confirmUI.Dismiss();
        }
    }
}
