using System.Collections.Generic;
using System.Linq;
using RockPaper.GameEnum;
using UnityEngine;

namespace RockPaper.Data
{
    [CreateAssetMenu(menuName = "GameSO/Hand Database", fileName =  "NewHandDatabase")]
    public class HandDatabaseSO : ScriptableObject
    {
        [SerializeField] private List<HandDataSO> _handsData;

        private Dictionary<HandType, HandDataSO> _cache;
        
        public List<HandDataSO> HandsDataList => _handsData;

        public HandDataSO Get(HandType type)
        {
            _cache ??= _handsData.ToDictionary(h => h.Hand);
            return _cache[type];
        }
    }

}