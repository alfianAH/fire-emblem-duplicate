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

        public void MakePlayerUnit(MakePlayerUnitMessage message)
        {
            MakeUnit(message.UnitDatas, _playerUnitParent, _playerUnitColor);
        }

        public void MakeEnemyUnit(MakeEnemyUnitMessage message)
        {
            MakeUnit(message.UnitDatas, _enemyUnitTransform, _enemyUnitColor);
        }

        private void MakeUnit(List<StageUnitData> unitDatas, Transform unitParent, Color unitColor)
        {
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
                duplicateUnit.Unit.SetTerrain(terrain);
                duplicateUnit.SetupUnit();
            }
        }
    }
}
