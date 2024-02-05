using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage;
using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Pool;
using FireEmblemDuplicate.Scene.Battle.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit.Pool
{
    public class UnitPoolController : MonoBehaviour
    {
        [SerializeField] private Transform _playerUnitParent, _enemyUnitTransform;
        [SerializeField] private Color _playerUnitColor, _enemyUnitColor;
        private UnitPool _unitPool;

        public UnitPool UnitPool => _unitPool;

        private void Awake()
        {
            _unitPool = GetComponent<UnitPool>();
        }

        public void MakePlayerUnit(MakePlayerUnitMessage message)
        {
            List<BaseUnitController> units = MakeUnit(message.UnitDatas, _playerUnitParent, _playerUnitColor);
            _unitPool.AddPlayerUnit(units);
        }

        public void MakeEnemyUnit(MakeEnemyUnitMessage message)
        {
            List<BaseUnitController> units = MakeUnit(message.UnitDatas, _enemyUnitTransform, _enemyUnitColor);
            _unitPool.AddEnemyUnit(units);
        }

        private List<BaseUnitController> MakeUnit(List<StageUnitData> unitDatas, 
            Transform unitParent, Color unitColor)
        {
            List<BaseUnitController> units = new List<BaseUnitController>();

            foreach (StageUnitData unitData in unitDatas)
            {
                BaseUnitController unitPrefab = Resources.Load<BaseUnitController>(unitData.UnitPrefab);
                BaseUnitScriptableObject unitSO = Resources.Load<BaseUnitScriptableObject>(unitData.UnitSO);
                WeaponScriptableObject weaponSO = Resources.Load<WeaponScriptableObject>(unitData.UnitWeapon);
                StageUnitPosition unitPosition = unitData.UnitPosition;
                BaseTerrainController terrain = TerrainPoolController.Instance.TerrainPool.Find(
                    t => t.Terrain.XPos == unitPosition.XAxis && t.Terrain.YPos == unitPosition.YAxis);

                BaseUnitController duplicateUnit = Instantiate(unitPrefab, unitParent);
                duplicateUnit.gameObject.name = unitSO.Name;
                duplicateUnit.Unit.SetUnitColor(unitColor);
                duplicateUnit.Unit.SetUnitSO(unitSO);
                duplicateUnit.Unit.WeaponController.SetWeaponSO(weaponSO);
                duplicateUnit.Unit.SetOriginTerrain(terrain);
                duplicateUnit.Unit.SetTerrain(terrain);
                duplicateUnit.SetupUnit();

                units.Add(duplicateUnit);
            }

            return units;
        }
    }
}
