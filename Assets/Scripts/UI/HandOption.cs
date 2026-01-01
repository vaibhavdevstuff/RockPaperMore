using System;
using RockPaper.Core;
using RockPaper.GameEnum;
using RockPaper.StaticHelper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaper.UI
{
    public class HandOption : MonoBehaviour
    {
        [Header("Color")] [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _selectedColor = Color.white;

        [Header("UI Elements")] 
        [SerializeField] private Image _handImage;
        [SerializeField] private TextMeshProUGUI _handText;
        [SerializeField] private Button _handButton;
        
        private HandType _handType;
        private UserType _userType;

        private Action _playerUICallBack;

        public HandType HandType => _handType;

        public void SetUserType(UserType userType)
        {
            _userType = userType;
            OnReset();
        }

        private void Awake()
        {
            SetNormalImage();
            SetupButton();
        }

        private void OnEnable()
        {
            GameEvent.OnResetGame += OnReset;
            GameEvent.OnRoundEnd += OnRoundEnd;
        }
        
        private void OnDisable()
        {
            GameEvent.OnResetGame -= OnReset;
            GameEvent.OnRoundEnd -= OnRoundEnd;
        }

        private void OnRoundEnd(RoundResult obj)
        {
            SetButtonInteraction(false);
        }

        private void OnReset()
        {
            if(_userType == UserType.Computer)
                _handText.text = "";
            
            SetNormalImage();
            SetButtonInteraction(true);
        }

        private void SetupButton()
        {
            _handButton.onClick.RemoveAllListeners();
            _handButton.onClick.AddListener(OnButtonClicked);
        }

        public void SetHandType(HandType handType)
        {
            _handType = handType;
            _handText.text = handType.ToString();
        }

        public void DisableButton()
        {
            _handButton.onClick.RemoveAllListeners();
            _handButton.enabled = false;
        }

        private void OnButtonClicked()
        {
            GameManager.Instance.PlayRound(_handType);
            _playerUICallBack?.Invoke();
        }
        
        public void SetPlayerUICallback(Action callback)
        {
            _playerUICallBack = callback;
        }
        
        private void SetButtonInteraction(bool value)
        {
            Debug.Log("SetButtonInteraction " + value);
            
            if(_handButton.enabled)
                _handButton.interactable = value;
        }

        public void ResetImage()
        {
            SetNormalImage();
        }

        public void SelectHand()
        {
            SetSelectedImage();
        }

        private void SetNormalImage()
        {
            _handImage.color = _normalColor;
        }

        private void SetSelectedImage()
        {
            _handImage.color = _selectedColor;
        }
    }
}