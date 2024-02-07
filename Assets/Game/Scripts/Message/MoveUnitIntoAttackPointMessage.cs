using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Unit;

namespace FireEmblemDuplicate.Message
{
    public struct MoveUnitIntoAttackPointMessage
    {
        public BaseUnitController Attacker { get; private set; }
        public BaseTerrainController TerrainController { get; private set; }

        public MoveUnitIntoAttackPointMessage(BaseUnitController attacker, BaseTerrainController terrainController)
        {
            Attacker = attacker;
            TerrainController = terrainController;
        }
    }
}
