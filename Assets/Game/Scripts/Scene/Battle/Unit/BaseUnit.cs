using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField] private Sprite _unitTypeSprite;
        [SerializeField] private UnitPhase _unitPhase = UnitPhase.Idle;

        public Sprite UnitTypeSprite => _unitTypeSprite;
        public UnitPhase UnitPhase => _unitPhase;
        public BaseUnitScriptableObject BaseUnitSO { get; private set; }
        public BaseTerrainController OriginTerrainController { get; private set; }
        public BaseTerrainController TerrainController { get; private set; }
        public int MovementSpace { get; private set; } = 0;

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
    }
}
