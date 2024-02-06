using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Forest;
using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Mountain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Type.Ruin;
using FireEmblemDuplicate.Scene.Battle.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit.Type.Infantry
{
    public class InfantryUnitController : BaseUnitController
    {
        public override void SetupUnit(
            Color unitColor, BaseUnitScriptableObject unitSO,
            BaseTerrainController terrain,
            WeaponScriptableObject weaponSO)
        {
            base.SetupUnit(unitColor, unitSO, terrain, weaponSO);
            unit.SetMovementSpace(2);
        }

        protected override List<UnitImpassableTerrain> ImpassableTerrains()
        {
            List<UnitImpassableTerrain> impassableTerrains = base.ImpassableTerrains();
            impassableTerrains.Add(new UnitImpassableTerrain(typeof(MountainTerrainController)));
            impassableTerrains.Add(new UnitImpassableTerrain(typeof(RuinTerrainController)));
            impassableTerrains.Add(new UnitImpassableTerrain(typeof(ForestTerrainController), 1));

            return impassableTerrains;
        }
    }
}
