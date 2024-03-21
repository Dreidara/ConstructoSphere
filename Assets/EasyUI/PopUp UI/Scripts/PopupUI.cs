using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EasyUI.Popup;
using UnityEngine.Events;
using TMPro;

namespace EasyUI.Helpers
{
    public class PopupUI : MonoBehaviour
    {
        public SO_EasyUI_PopUpUI so_EasyUI_PopUpUI;

        [Header("UI References :")]
        [SerializeField] AudioClip popUpOpen;
        [SerializeField] AudioClip popUpClose;
        [SerializeField] private GraphicRaycaster uiCanvasGraphicRaycaster;
        [SerializeField] private CanvasGroup uiCanvasGroup;
        [SerializeField] private GameObject uiHeader;
        [SerializeField] private TextMeshProUGUI uiTitle;
        [SerializeField] private TextMeshProUGUI uiText;
        [SerializeField] private Image uiButtonImage;
        [SerializeField] private TextMeshProUGUI uiButtonText;
        private Button uiButton;

        [Header("Popup Fade Duration :")]
        [Range(.1f, .8f)] [SerializeField] private float fadeInDuration = .3f;
        [Range(.1f, .8f)] [SerializeField] private float fadeOutDuration = .3f;

        [Space]
        public int maxTextLength = 200;

        private UnityAction _onCloseAction;
        private AudioSource _audioObject;

        private void OnEnable()
        {
            if (Application.isPlaying == true && _audioObject == null)
            {
                try { _audioObject = GameObject.Find("UI_Audiosource").GetComponent<AudioSource>(); }
                catch { Debug.Log("<b>[UI Element Sound]</b> No Audio Source found.", this); }
            }
        }


         private void Awake()
        {
            uiCanvasGroup.alpha = 0f;
            uiCanvasGroup.interactable = false;
            uiCanvasGraphicRaycaster.enabled = false;

            uiButton = uiButtonImage.GetComponent<Button>();
            uiButton.onClick.RemoveAllListeners();
            uiButton.onClick.AddListener(() => {
                if (_onCloseAction != null)
                {
                    _onCloseAction.Invoke();
                    _onCloseAction = null;
                }
                //_audioObject.PlayOneShot(popUpClose);
                StartCoroutine(FadeOut(fadeOutDuration));
            });
        }

        public void Show(string title, string text, string buttonText, Enum_PopupColor color, UnityAction action)
        {
            if (string.IsNullOrEmpty(title.Trim()))
                uiHeader.SetActive(false);
            else
            {
                uiHeader.SetActive(true);
                uiTitle.text = title;
            }

            uiText.text = (text.Length > maxTextLength) ? text.Substring(0, maxTextLength) + "..." : text;

            uiButtonText.text = buttonText;

            Color c = so_EasyUI_PopUpUI.colors[(int)color];
            Color ct = c;
            ct.a = .75f;
            //uiTitle.color = ct;
            uiButtonImage.color = c;

            _onCloseAction = action;

            Dismiss();
            StartCoroutine(FadeIn(fadeInDuration));
        }

        private IEnumerator FadeIn(float duration)
        {
            uiCanvasGraphicRaycaster.enabled = true;
            yield return Fade(uiCanvasGroup, 0f, 1f, duration);
            uiCanvasGroup.interactable = true;
            //_audioObject.PlayOneShot(popUpOpen);
            //AudioManager.Instance.PlaySound("UI_PopUpShow");
        }

        private IEnumerator FadeOut(float duration)
        {
            yield return Fade(uiCanvasGroup, 1f, 0f, duration);
            uiCanvasGroup.interactable = false;
            uiCanvasGraphicRaycaster.enabled = false;
        }

        private IEnumerator Fade(CanvasGroup cGroup, float startAlpha, float endAlpha, float duration)
        {
            float startTime = Time.time;
            float alpha = startAlpha;

            if (duration > 0f)
            {
                //Anim start
                while (alpha != endAlpha)
                {
                    alpha = Mathf.Lerp(startAlpha, endAlpha, (Time.time - startTime) / duration);
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
            uiCanvasGroup.interactable = false;
            uiCanvasGraphicRaycaster.enabled = false;
        }

        private void OnDestroy()
        {
           Popup.Popup._isLoaded = false;
        }
    }

}
