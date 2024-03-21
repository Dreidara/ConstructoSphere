using ConstructoSphere.Gameplay;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConstructoSphere.Descriptions
{
    public class DescriptionDisplayManager : MonoBehaviour
    {

        public List<DescriptionCard> DescriptionCardList = new();
        public Image DisplatyImage;
        //public TextMeshProUGUI TitleText;
        public TextMeshProUGUI DescriptionText;
        public Button NextButton;
        public Button PrevButton;

        private int currentIndex = 0;
        private DescriptionCard _currentDescriptionCard;

        [Header("Collapsible")]
        [SerializeField] bool _isShown;
        [SerializeField] Transform _objectToMove;

        private void Start()
        {
            int levelModeUnlock = GameDataManager.Instance.currentLevelId;
            _currentDescriptionCard = DescriptionCardList[levelModeUnlock - 1];


            NextButton.onClick.AddListener(Next);
            PrevButton.onClick.AddListener(Prev);

            UpdateDisplay();
        }

        private void Next()
        {
            if (currentIndex < _currentDescriptionCard.DescriptionItems.Count - 1)
            {
                currentIndex++;
                UpdateDisplay();
            }
        }

        private void Prev()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            Camera cameraView = UIManager.Instance.viewModeCamera.transform.GetChild(0).GetComponent<Camera>();
            cameraView.transform.position = _currentDescriptionCard.DescriptionItems[currentIndex].CameraPosition;
            cameraView.transform.rotation = Quaternion.Euler(_currentDescriptionCard.DescriptionItems[currentIndex].CameraRotation);

            //TitleText.text = _currentDescriptionCard.DescriptionItems[currentIndex].DisplayTitle;
            DescriptionText.text = _currentDescriptionCard.DescriptionItems[currentIndex].DisplayDescriptions;
            DisplatyImage.sprite = _currentDescriptionCard.DescriptionItems[currentIndex].DisplaySprite;

            //IndexText.SetText($"{currentIndex + 1}");

            // Disable the "Previous" button if the first text is displayed
            PrevButton.gameObject.SetActive(currentIndex != 0);

            //Disable the "Next" button if the last text is displayed
            NextButton.gameObject.SetActive(currentIndex != _currentDescriptionCard.DescriptionItems.Count - 1);


        }

        public void Collapsible()
        {
            Vector3 hidePosition = new Vector3(1095, 0, 0);
            Vector3 showPosition = new Vector3(0, 0, 0);

            // Use DOTween to move the object
            if (_isShown)
                _objectToMove.DOLocalMove(hidePosition, 0.5f);
            else
                _objectToMove.DOLocalMove(showPosition, 0.5f);

            // Toggle the state
            _isShown = !_isShown;
        }
    }
}


