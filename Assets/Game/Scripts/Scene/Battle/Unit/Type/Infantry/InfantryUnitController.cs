namespace FireEmblemDuplicate.Scene.Battle.Unit.Type.Infantry
{
    public class InfantryUnitController : BaseUnitController
    {
        public override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(2);
        }
    }
}
