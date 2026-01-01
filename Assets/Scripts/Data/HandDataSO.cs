using System.Collections.Generic;
using RockPaper.GameEnum;
using UnityEngine;

namespace RockPaper.Data
{
    [CreateAssetMenu(menuName = "GameSO/Hand Data",  fileName = "HD_")]
    public class HandDataSO : ScriptableObject
    {
        [SerializeField] private HandType _hand;
        [SerializeField] private List<HandType> _handBeats;
        
        public HandType Hand => _hand;
        public string HandName => Hand.ToString();
        public List<HandType> HandBeats => _handBeats;
    }
}
