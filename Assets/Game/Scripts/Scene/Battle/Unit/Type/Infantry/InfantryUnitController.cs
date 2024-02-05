using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Mountain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Ruin;
using System.Collections.Generic;

namespace FireEmblemDuplicate.Scene.Battle.Unit.Type.Infantry
{
    public class InfantryUnitController : BaseUnitController
    {
        public override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(2);
        }

        protected override List<System.Type> ImpassableTerrains()
        {
            List<System.Type> impassableTerrains = base.ImpassableTerrains();
            impassableTerrains.Add(typeof(MountainTerrainController));
            impassableTerrains.Add(typeof(RuinTerrainController));

            return impassableTerrains;
        }
    }
}
