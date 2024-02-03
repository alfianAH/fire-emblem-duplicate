using FireEmblemDuplicate.Scene.Battle.Terrain.Enum;

namespace FireEmblemDuplicate.Message
{
    public struct ChangeTerrainIndicatorMessage
    {
        public int XPos { get; private set; }
        public int YPos { get; private set; }
        public TerrainIndicator Indicator { get; private set; }

        public ChangeTerrainIndicatorMessage(
            int xPos, int yPos, TerrainIndicator indicator)
        {
            XPos = xPos;
            YPos = yPos;
            Indicator = indicator;
        }
    }
}