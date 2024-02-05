namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class UnitImpassableTerrain
    {
        public System.Type BlockedTerrainType { get; private set; }
        public int StartBlockRange { get; private set; }

        public UnitImpassableTerrain(System.Type blockedTerrainType, int startBlockRange = 0)
        {
            BlockedTerrainType = blockedTerrainType;
            StartBlockRange = startBlockRange;
        }
    }
}
