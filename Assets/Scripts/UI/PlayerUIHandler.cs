using System;
using System.Collections.Generic;
using System.Collections;
using RockPaper.Core;
using RockPaper.GameEnum;
using RockPaper.StaticHelper;
using RockPaper.UI;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaper
{
    public class PlayerUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _handOptionPrefab;
        [SerializeField] private GameObject _handOptionContent;
        
        [Header("Timer")]
        [SerializeField] private int _timerCount = 2;
        [SerializeField] private Slider _timerSlider;
        
        private List<HandOption> _handOptionList = new();
        private Coroutine _timerCoroutine;

        private void Start()
        {
            SpawnHandOptions();

            StartTimer();
        }

        private void OnEnable()
        {
            GameEvent.OnResetGame += OnResetGame;
        }
        
        private void OnDisable()
        {
            GameEvent.OnResetGame -= OnResetGame;
        }

        private void SpawnHandOptions()
        {
            if (_handOptionPrefab == null)
            {
                Debug.LogError("[Null Reference] Hand option prefab is null");
                return;
            }
            
            if (_handOptionContent == null)
            {
                Debug.LogError("[Null Reference] Hand option Content is null");
                return;
            }

            RemoveExistedOptions();
            SpawnOptions();
        }

        private void RemoveExistedOptions()
        {
            for (var i = _handOptionContent.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(_handOptionContent.transform.GetChild(i).gameObject);
            }
        }
        
        private void SpawnOptions()
        {
            var nameIndex = 0;
            
            var handDataBase = GameManager.Instance.HandDatabase;

            foreach (var handData in handDataBase.HandsDataList)
            {
                var handOptionObj = Instantiate(_handOptionPrefab, _handOptionContent.transform);
                var handOption = handOptionObj.GetComponent<HandOption>();
                handOption.SetUserType(UserType.Player);
                handOption.SetHandType(handData.Hand);
                handOption.SetPlayerUICallback(OnHandOptionClicked);
                
                handOption.name = $"HandOption_{handData.HandName}_{nameIndex}";
                
                nameIndex++;
                
                _handOptionList.Add(handOption);
            }
        }

        private void OnHandOptionClicked()
        {
            StopTimer();
        }
        
        private void OnResetGame()
        {
            StartTimer();
        }

        private void StartTimer()
        {
            StopTimer();
            
            _timerCoroutine = StartCoroutine(SmoothShowSlider());
        }

        private void StopTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }

        private IEnumerator SmoothShowSlider()
        {
            var elapsedTime = 0f;
            _timerSlider.maxValue = _timerCount;
            _timerSlider.value = 0f;

            while (elapsedTime < _timerCount)
            {
                elapsedTime += Time.deltaTime;
                _timerSlider.value = Mathf.Lerp(0f, _timerCount, elapsedTime / _timerCount);
                yield return null;
            }

            _timerSlider.value = _timerCount;

            GameManager.Instance.ForceEndGame();
        }
        
    }
}
