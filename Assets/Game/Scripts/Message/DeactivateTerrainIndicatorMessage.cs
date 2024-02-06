namespace FireEmblemDuplicate.Message
{
    public struct DeactivateTerrainIndicatorMessage
    {
        public int XPos { get; private set; }
        public int YPos { get; private set; }

        public DeactivateTerrainIndicatorMessage(int x, int y)
        {
            XPos = x;
            YPos = y;
        }
    }
}