using System.Collections;
using RockPaper.Data;
using RockPaper.Evaluator;
using RockPaper.GameEnum;
using RockPaper.HandSelector;
using RockPaper.StaticHelper;
using UnityEngine;

namespace RockPaper.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private HandDatabaseSO _handDatabase;
        [SerializeField] private float _gameResetTime;

        private int _score;

        private bool _loadMenu = false;
        
        private Coroutine _resetRoutine;
        
        private IHandOutcomeEvaluator _evaluator;
        private IHandSelectable _player;
        private IHandSelectable _computer;
        
        // Getter Setters
        public int Score => _score;
        public HandDatabaseSO HandDatabase => _handDatabase;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            
            CreateSingleton();

            Start();
        }
        
        private bool CreateSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return true;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            return false;
        }

        private void Start()
        {
            // Core setup
            _evaluator = new HandOutcomeEvaluator(_handDatabase);
            _player = new PlayerHandSelector();
            _computer = new ComputerHandSelector();

            LoadMenu();
        }
        
        private void LoadMenu()
        {
            SceneLoader.LoadSingle(SceneName.MENU);
        }

        public void PlayRound(HandType playerHand)
        {
            ((PlayerHandSelector)_player).SetInput(playerHand);

            var result = _evaluator.Evaluate(
                _player.SelectedHand(),
                _computer.SelectedHand()
            );

            switch (result)
            {
                case RoundResult.Win:
                    _score++;
                    break;
                case RoundResult.Draw:
                    break;
                case RoundResult.Lose:
                    EndGame();
                    break;
            }
            
            GameEvent.OnRoundEnd.Invoke(result);
            
            Debug.Log($"Round {_score}: {result}");
            
            ResetGame();
        }

        private void EndGame()
        {
            _loadMenu = true;
        }

        public void ForceEndGame()
        {
            EndGame();
            GameEvent.OnRoundEnd.Invoke(RoundResult.Lose);
            ResetGame();
        }

        private void ResetGame()
        {
            if(_resetRoutine != null)
                StopCoroutine(_resetRoutine);

            _resetRoutine = StartCoroutine(CR_ResetGame());
        }
        
        private IEnumerator CR_ResetGame()
        {
            yield return new WaitForSeconds(_gameResetTime);

            GameEvent.OnResetGame.Invoke();
            
            if (_loadMenu)
            {
                _loadMenu = false;
                _score = 0;
                LoadMenu();
            }
            
        }
        
    }
}