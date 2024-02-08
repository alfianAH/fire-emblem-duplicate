using FireEmblemDuplicate.Scene.Battle.Terrain.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    public class BaseTerrain
    {
        private int _xPos;
        private int _yPos;

        public BaseTerrain(int x, int y)
        {
            _xPos = x;
            _yPos = y;
        }

        public int XPos => _xPos;
        public int YPos => _yPos;
        public TerrainIndicator Indicator { get; private set; }
        public BaseUnitController UnitOnTerrain { get; private set; }
        public bool CanBeUsed { get; private set; }

        public void SetCanBeUsed(bool canBeUsed)
        {
            CanBeUsed = canBeUsed;
        }

        public void SetUnitOnTerrain(BaseUnitController unitOnTerrain)
        {
            if(unitOnTerrain != null)
            {
                Debug.Log($"{XPos}, {YPos}: {unitOnTerrain.gameObject.name} {unitOnTerrain.Unit.BaseUnitSO.Side}");
            }
            else
            {
                Debug.Log($"{XPos}, {YPos}: null");
            }
            
            UnitOnTerrain = unitOnTerrain;
        }

        public void SetTerrainIndicator(TerrainIndicator indicator)
        {
            Indicator = indicator;
        }
    }
}
