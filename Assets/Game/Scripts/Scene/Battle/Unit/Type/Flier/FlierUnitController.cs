namespace FireEmblemDuplicate.Scene.Battle.Unit.Type.Flier
{
    public class FlierUnitController : BaseUnitController
    {
        public override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(2);
        }
    }
}
