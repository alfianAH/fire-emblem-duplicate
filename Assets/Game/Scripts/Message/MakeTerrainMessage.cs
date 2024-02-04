using FireEmblemDuplicate.Scene.Battle.Stage;

namespace FireEmblemDuplicate.Message
{
    public struct MakeTerrainMessage
    {
        public StageTerrainPoolData TerrainPool { get; private set; }

        public MakeTerrainMessage(StageTerrainPoolData terrainPool)
        {
            TerrainPool = terrainPool;
        }
    }
}
