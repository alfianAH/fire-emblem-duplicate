namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public interface IBaseUnit
    {
        public void OnUnitClick();
        public void OnUnitDrag();
        public void Attack();
        public void Move();
    }
}