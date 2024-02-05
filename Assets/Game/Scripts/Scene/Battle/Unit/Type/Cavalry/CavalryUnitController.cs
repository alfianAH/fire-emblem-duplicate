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

        protected override List<System.Type> ImpassableTerrains()
        {
            List<System.Type> impassableTerrains = base.ImpassableTerrains();
            impassableTerrains.Add(typeof(MountainTerrainController));
            impassableTerrains.Add(typeof(RuinTerrainController));
            impassableTerrains.Add(typeof(ForestTerrainController));

            return impassableTerrains;
        }
    }
}
