using System;
using RockPaper.Core;
using RockPaper.GameEnum;
using RockPaper.StaticHelper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaper.UI
{
    public class ResultUIHandler : MonoBehaviour
    {
        [Header("Score")]
        [SerializeField] private string _scorePrefix;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [Header("Result")]
        [SerializeField] private GameObject _resultObject;
        [SerializeField] private Image _resultImage;
        [SerializeField] private TextMeshProUGUI _resultText;
        [Header("Color")]
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _winColor;
        [SerializeField] private Color _loseColor;

        private void Start()
        {
            OnResetGame();
            UpdateScore();
        }

        private void OnEnable()
        {
            GameEvent.OnRoundEnd += OnRoundEnd;
            GameEvent.OnResetGame += OnResetGame;
        }
        
        private void OnDisable()
        {
            GameEvent.OnRoundEnd -= OnRoundEnd;
            GameEvent.OnResetGame -= OnResetGame;
        }

        private void OnRoundEnd(RoundResult result)
        {
            switch (result)
            {
                case RoundResult.Win:
                    _resultImage.color = _winColor;
                    break;
                case RoundResult.Lose:
                    _resultImage.color = _loseColor;
                    break;
                case RoundResult.Draw:
                    _resultImage.color = _normalColor;
                    break;
            }
            
            _resultText.text = result.ToString();
            _resultObject.SetActive(true);
            
            UpdateScore(GameManager.Instance.Score);
        }

        private void UpdateScore(int score = 0)
        {
            if (_scoreText == null)
            {
                Debug.LogError("[Null Reference] ScoreText is null!");
                return;
            }

            _scoreText.text = $"{_scorePrefix} {score}";
        }

        private void OnResetGame()
        {
            _resultObject.SetActive(false);
        }
    }
}
