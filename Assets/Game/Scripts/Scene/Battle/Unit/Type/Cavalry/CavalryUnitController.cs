namespace FireEmblemDuplicate.Scene.Battle.Unit.Type.Cavalry
{
    public class CavalryUnitController : BaseUnitController
    {
        public override void SetupUnit()
        {
            base.SetupUnit();
            unit.SetMovementSpace(3);
        }
    }
}
