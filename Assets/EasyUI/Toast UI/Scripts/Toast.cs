using UnityEngine;
using EasyUI.Helpers;
using static EasyUI.Helpers.SO_EasyUI_ToastUI;

namespace EasyUI.Toast
{
    public static class Toast
    {
        public static bool isLoaded = false;

        private static ToastUI toastUI;

        private static void Prepare()
        {
            if (!isLoaded)
            {
                GameObject instance = MonoBehaviour.Instantiate(Resources.Load<GameObject>("ToastUI"));
                instance.name = "[ TOAST UI ]";
                toastUI = instance.GetComponent<ToastUI>();
                isLoaded = true;
            }
        }

        public static void Show(Enum_EasyUIKeys keyname)
        {
            Prepare();

            ToastUIReference scriptableObjectToastUI = toastUI.so_EasyUI_ToastUI.toastUIReferences.Find(item => item.keyName == keyname);

            if (scriptableObjectToastUI != null)
            {
                string description = scriptableObjectToastUI.description;
                float toastDuration = scriptableObjectToastUI.toastDuration;
                Enum_ToastColor color = scriptableObjectToastUI.color;
                Enum_ToastPosition toastPosition = scriptableObjectToastUI.toastPosition;

                toastUI.Init(description, toastDuration, color, toastPosition);
            }
            else
            {
                Debug.LogError($"The key {keyname} is not found!.");
            }
        }

        public static void Dismiss()
        {
            toastUI.Dismiss();
        }
    }

}
