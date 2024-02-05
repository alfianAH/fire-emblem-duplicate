using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Forest;
using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Mountain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Ruin;
using System.Collections.Generic;

namespace FireEmblemDuplicate.Scene.Battle.Unit.Type.Cavalry
{
    public class CavalryUnitController : BaseUnitController
    {
        public override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(3);
        }

        protected override List<UnitImpassableTerrain> ImpassableTerrains()
        {
            List<UnitImpassableTerrain> impassableTerrains = base.ImpassableTerrains();
            impassableTerrains.Add(new UnitImpassableTerrain(typeof(MountainTerrainController)));
            impassableTerrains.Add(new UnitImpassableTerrain(typeof(RuinTerrainController)));
            impassableTerrains.Add(new UnitImpassableTerrain(typeof(ForestTerrainController)));

            return impassableTerrains;
        }
    }
}
