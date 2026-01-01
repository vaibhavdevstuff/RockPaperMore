using System;
using RockPaper.StaticHelper;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaper.UI
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Start()
        {
            SetupPlayButton();
        }

        private void SetupPlayButton()
        {
            if (_playButton == null)
            {
                Debug.LogError($"[Null Reference] Play Button is null");
                return;
            }
            
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(() => SceneLoader.LoadSingle(SceneName.GAME));
        }
    }
}