using System;
using RockPaper.GameEnum;
using RockPaper.StaticHelper;
using UnityEngine;

namespace RockPaper.UI
{
    public class ComputerUIHandler : MonoBehaviour
    {
        [SerializeField] private HandOption _handOption;

        private void Start()
        {
            _handOption.SetUserType(UserType.Computer);
            _handOption.DisableButton();
        }

        private void OnEnable()
        {
            GameEvent.OnComputerHandSelected += OnComputerHandSelected;
        }

        private void OnDisable()
        {
            GameEvent.OnComputerHandSelected -= OnComputerHandSelected;
        }

        private void OnComputerHandSelected(HandType handOption)
        {
            if (_handOption == null)
            {
                Debug.LogError("[Null Reference] Hand Option Element Not Found");
                return;
            }
            
            _handOption.SetHandType(handOption);
        }
    }
}