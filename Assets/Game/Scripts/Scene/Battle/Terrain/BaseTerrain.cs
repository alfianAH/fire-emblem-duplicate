using FireEmblemDuplicate.Scene.Battle.Terrain.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;

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
            UnitOnTerrain = unitOnTerrain;
        }

        public void SetTerrainIndicator(TerrainIndicator indicator)
        {
            Indicator = indicator;
        }
    }
}
