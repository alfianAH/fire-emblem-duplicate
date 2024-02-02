namespace FireEmblemDuplicate.Scene.Battle.Unit.Infantry
{
    public class InfantryUnitController : BaseUnitController
    {
        protected override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(2);
        }
    }
}
