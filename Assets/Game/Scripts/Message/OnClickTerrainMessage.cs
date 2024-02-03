using FireEmblemDuplicate.Scene.Battle.Terrain;

namespace FireEmblemDuplicate.Message
{
    public struct OnClickTerrainMessage
    {
        public BaseTerrainController TerrainController { get; private set; }

        public OnClickTerrainMessage(BaseTerrainController terrainController)
        {
            TerrainController = terrainController;
        }
    }
}