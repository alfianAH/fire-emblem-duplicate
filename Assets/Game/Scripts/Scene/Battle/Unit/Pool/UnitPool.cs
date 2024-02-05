using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit.Pool
{
    public class UnitPool : MonoBehaviour
    {
        private List<BaseUnitController> _playerUnits = new List<BaseUnitController>();
        private List<BaseUnitController> _enemyUnits = new List<BaseUnitController>();
        
        public void AddPlayerUnit(List<BaseUnitController> playerUnits)
        {
            _playerUnits.AddRange(playerUnits);
        }

        public void AddEnemyUnit(List<BaseUnitController> enemyUnits)
        {
            _enemyUnits.AddRange(enemyUnits);
        }
    }
}