using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Pool;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using FireEmblemDuplicate.Scene.Battle.Weapon;
using SuperMaxim.Messaging;
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

        public void OnUnitDead(UnitDeadMessage message)
        {
            switch (message.Unit.Unit.BaseUnitSO.Side)
            {
                case UnitSide.Player:
                    _unitPool.RemovePlayerUnit(message.Unit);

                    if (_unitPool.RemainingPlayerUnit() == 0)
                    {
                        Messenger.Default.Publish(new WinMessage(UnitSide.Player));
                    }
                    break;

                case UnitSide.Enemy:
                    _unitPool.RemoveEnemyUnit(message.Unit);
                    if (_unitPool.RemainingEnemyUnit() == 0)
                    {
                        Messenger.Default.Publish(new WinMessage(UnitSide.Player));
                    }
                    break;
            }
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

        public void OnAllUnitImmovable(ImmovableUnitMessage message)
        {
            switch (message.UnitSide)
            {
                case UnitSide.Player:
                    if (!_unitPool.AreAllPlayerUnitsImmovable()) return;
                    break;

                case UnitSide.Enemy:
                    if (!_unitPool.AreAllEnemyUnitsImmovable()) return;
                    break;

                default: break;
            }

            // Change phase after one side is immovable
            StagePhase newPhase = StageController.Instance.Stage.Phase == StagePhase.PlayerPhase ? StagePhase.EnemyPhase : StagePhase.PlayerPhase;
            Messenger.Default.Publish(new ChangeStagePhaseMessage(newPhase));
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
                duplicateUnit.SetupUnit(unitColor, unitSO, terrain, weaponSO);

                units.Add(duplicateUnit);
            }

            return units;
        }
    }
}
