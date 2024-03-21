using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using EasyUI.Toast;

namespace EasyUI.Helpers
{
    public class ToastUI : MonoBehaviour
    {
        public SO_EasyUI_ToastUI so_EasyUI_ToastUI;

        [Header("UI References :")]
        [SerializeField] private CanvasGroup uiCanvasGroup;
        [SerializeField] private RectTransform uiRectTransform;
        [SerializeField] private VerticalLayoutGroup uiContentVerticalLayoutGroup;
        [SerializeField] private Image uiImage;
        [SerializeField] private TextMeshProUGUI uiText;

        [Header("Toast Fade In/Out Duration :")]
        [Range(.1f, .8f)]
        [SerializeField] private float fadeDuration = .3f;


        private int maxTextLength = 300;


        void Awake()
        {
            uiCanvasGroup.alpha = 0f;
        }

        public void Init(string description, float duration, Enum_ToastColor color, Enum_ToastPosition position)
        {
            Show(description, duration, color, position);
        }

        private void Show(string description, float duration, Enum_ToastColor color, Enum_ToastPosition position)
        {
            uiText.text = (description.Length > maxTextLength) ? description.Substring(0, maxTextLength) + "..." : description;
            uiImage.color = so_EasyUI_ToastUI.colors[(int)color];

            uiContentVerticalLayoutGroup.childAlignment = (TextAnchor)((int)position);


            Dismiss();
            StartCoroutine(FadeInOut(duration, fadeDuration));
        }

        private IEnumerator FadeInOut(float toastDuration, float fadeDuration)
        {
            yield return null;

            uiContentVerticalLayoutGroup.CalculateLayoutInputHorizontal();
            uiContentVerticalLayoutGroup.CalculateLayoutInputVertical();
            uiContentVerticalLayoutGroup.SetLayoutHorizontal();
            uiContentVerticalLayoutGroup.SetLayoutVertical();

            yield return null;

            // Anim start
            yield return Fade(uiCanvasGroup, 0f, 1f, fadeDuration);
            yield return new WaitForSeconds(toastDuration);
            yield return Fade(uiCanvasGroup, 1f, 0f, fadeDuration);
            // Anim end
        }

        private IEnumerator Fade(CanvasGroup cGroup, float startAlpha, float endAlpha, float fadeDuration)
        {
            float startTime = Time.time;
            float alpha = startAlpha;

            if (fadeDuration > 0f)
            {
                //Anim start
                while (alpha != endAlpha)
                {
                    alpha = Mathf.Lerp(startAlpha, endAlpha, (Time.time - startTime) / fadeDuration);
                    cGroup.alpha = alpha;

                    yield return null;
                }
            }

            cGroup.alpha = endAlpha;
        }

        public void Dismiss()
        {
            StopAllCoroutines();
            uiCanvasGroup.alpha = 0f;
        }

        private void OnDestroy()
        {
            EasyUI.Toast.Toast.isLoaded = false;
        }
    }

}
