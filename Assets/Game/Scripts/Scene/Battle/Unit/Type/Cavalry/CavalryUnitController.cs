namespace FireEmblemDuplicate.Scene.Battle.Unit.Infantry
{
    public class CavalryUnitController : BaseUnitController
    {
        protected override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(3);
        }
    }
}
