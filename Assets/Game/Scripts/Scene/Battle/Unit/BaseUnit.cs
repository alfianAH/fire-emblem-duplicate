using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using FireEmblemDuplicate.Scene.Battle.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField] private Sprite _unitTypeSprite;
        [SerializeField] private UnitPhase _unitPhase = UnitPhase.Idle;
        [SerializeField] private WeaponController _weaponController;
        
        public Color UnitColor { get; private set; }
        public Sprite UnitTypeSprite => _unitTypeSprite;
        public UnitPhase UnitPhase => _unitPhase;
        public WeaponController WeaponController => _weaponController;
        public BaseUnitStats UnitStats { get; private set; }
        public BaseUnitScriptableObject BaseUnitSO { get; private set; }
        public BaseTerrainController OriginTerrainController { get; private set; }
        public BaseTerrainController TerrainController { get; private set; }
        public List<Vector2> BlockedTerrain { get; private set; } = new List<Vector2>();
        public int MovementSpace { get; private set; } = 0;

        public void SetUnitColor(Color color)
        {
            UnitColor = color;
        }

        public void SetUnitStats(BaseUnitStats unitStats)
        {
            UnitStats = new BaseUnitStats(
                unitStats.BaseHP, unitStats.BaseATK, 
                unitStats.BaseDEF, unitStats.BaseRES, unitStats.BaseLUK);
        }

        public void SetUnitSO(BaseUnitScriptableObject unit)
        {
            BaseUnitSO = unit;
        }

        public void SetMovementSpace(int movementSpace)
        {
            MovementSpace = movementSpace;
        }

        public void SetOriginTerrain(BaseTerrainController originTerrain)
        {
            OriginTerrainController = originTerrain;
        }

        public void SetTerrain(BaseTerrainController terrainController)
        {
            TerrainController = terrainController;
        }

        public void SetUnitPhase(UnitPhase unitPhase)
        {
            _unitPhase = unitPhase;
        }

        public void AddBlockedTerrain(Vector2 point)
        {
            if(BlockedTerrain.Contains(point)) return;

            BlockedTerrain.Add(point);
        }

        public void RemoveBlockedTerrain()
        {
            BlockedTerrain.Clear();
        }

        public void DecreaseHP(float amount)
        {
            UnitStats.BaseHP -= amount;

            if (UnitStats.BaseHP < 0)
                UnitStats.BaseHP = 0;
        }

        public void IncreaseHP(float amount)
        {
            UnitStats.BaseHP += amount;
        }

        public void IncreaseATK(float amount)
        {
            UnitStats.BaseATK += amount;
        }
    }
}
