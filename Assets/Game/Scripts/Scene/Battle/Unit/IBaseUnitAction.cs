using FireEmblemDuplicate.Message;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public interface IBaseUnitAction
    {
        public void DecreaseHP(DecreaseHPMessage message);
        public void Move();
    }
}