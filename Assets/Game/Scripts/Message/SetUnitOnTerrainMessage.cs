using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct SetUnitOnTerrainMessage
    {
        public int XPos { get; private set; }
        public int YPos { get; private set; }
        public BaseUnitController Unit { get; private set; }

        public SetUnitOnTerrainMessage(int xPos, int yPos, BaseUnitController unit)
        {
            XPos = xPos;
            YPos = yPos;
            Unit = unit;
        }
    }
}
