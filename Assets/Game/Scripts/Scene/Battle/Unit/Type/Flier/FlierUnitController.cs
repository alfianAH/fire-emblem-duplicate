using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Ruin;
using System.Collections.Generic;

namespace FireEmblemDuplicate.Scene.Battle.Unit.Type.Flier
{
    public class FlierUnitController : BaseUnitController
    {
        public override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(2);
        }

        protected override List<UnitImpassableTerrain> ImpassableTerrains()
        {
            List<UnitImpassableTerrain> impassableTerrains = base.ImpassableTerrains();
            impassableTerrains.Add(new UnitImpassableTerrain(typeof(RuinTerrainController)));

            return impassableTerrains;
        }
    }
}
