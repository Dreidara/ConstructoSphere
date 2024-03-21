using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EasyUI.Popup;
using UnityEngine.Events;
using TMPro;
using EasyUI.Confirm;

namespace EasyUI.Helpers
{
    public class ConfirmUI : MonoBehaviour
    {
        public SO_EasyUI_ConfirmUI so_EasyUI_ConfirmUI;

        [Header("UI References :")]
        [SerializeField] AudioClip popUpOpen;
        [SerializeField] AudioClip popUpClose;
        [SerializeField] private GraphicRaycaster uiCanvasGraphicRaycaster;
        [SerializeField] private CanvasGroup uiCanvasGroup;
        [SerializeField] private GameObject uiHeader;
        [SerializeField] private TextMeshProUGUI uiTitle;
        [SerializeField] private TextMeshProUGUI uiText;

        [Header("Buttons Reference :")]
        [SerializeField] private Image uiOkButtonImage;
        [SerializeField] private TextMeshProUGUI uiOkText;
        private Button uiOkButton;

        [SerializeField] private Image uiCancelButtonImage;
        [SerializeField] private TextMeshProUGUI uiCancelText;
        private Button uiCancelButton;

        [Header("Confirm UI Fade Duration :")]
        [Range(.1f, .8f)] [SerializeField] private float fadeInDuration = .3f;
        [Range(.1f, .8f)] [SerializeField] private float fadeOutDuration = .3f;

        [Space]
        public int maxTextLength = 200;

        private UnityAction _onOkAction;
        private UnityAction _onCancelAction;
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

            uiOkButton = uiOkButtonImage.GetComponent<Button>();
            uiOkButton.onClick.RemoveAllListeners();
            uiOkButton.onClick.AddListener(() => {
                if (_onOkAction != null)
                {
                    _onOkAction.Invoke();
                    _onOkAction = null;
                }
                _audioObject.PlayOneShot(popUpClose);
                StartCoroutine(FadeOut(fadeOutDuration));
            });

            uiCancelButton = uiCancelButtonImage.GetComponent<Button>();
            uiCancelButton.onClick.RemoveAllListeners();
            uiCancelButton.onClick.AddListener(() => {
                if (_onCancelAction != null)
                {
                    _onCancelAction.Invoke();
                    _onCancelAction = null;
                }
                _audioObject.PlayOneShot(popUpClose);
                StartCoroutine(FadeOut(fadeOutDuration));
            });
        }

        public void Show(string title, string text, string okText, string cancelText, Enum_ConfirmColor okButtonColor, Enum_ConfirmColor cancelButtonColor, UnityAction oncOkAction, UnityAction oncancelAction)
        {
            if (string.IsNullOrEmpty(title.Trim()))
                uiHeader.SetActive(false);
            else
            {
                uiHeader.SetActive(true);
                uiTitle.text = title;
            }

            uiText.text = (text.Length > maxTextLength) ? text.Substring(0, maxTextLength) + "..." : text;

            uiOkText.text = okText;
            uiCancelText.text = cancelText;

            Color c = so_EasyUI_ConfirmUI.colors[(int)cancelButtonColor];
            Color ct = c;
            ct.a = .75f;
            //uiTitle.color = ct;
            uiCancelButtonImage.color = c;

            this._onOkAction = oncOkAction;
            this._onCancelAction = oncancelAction;

            Dismiss();
            StartCoroutine(FadeIn(fadeInDuration));
        }

        private IEnumerator FadeIn(float duration)
        {
            uiCanvasGraphicRaycaster.enabled = true;
            yield return Fade(uiCanvasGroup, 0f, 1f, duration);
            uiCanvasGroup.interactable = true;
            _audioObject.PlayOneShot(popUpOpen);
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
           Confirm.Confirm._isLoaded = false;
        }
    }

}
