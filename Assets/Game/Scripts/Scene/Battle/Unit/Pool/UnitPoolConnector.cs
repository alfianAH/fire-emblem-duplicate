using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Unit.Pool
{
    public class UnitPoolConnector : Connector<UnitPoolConnector, UnitPoolController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<MakePlayerUnitMessage>(controller.MakePlayerUnit);
            Messenger.Default.Subscribe<MakeEnemyUnitMessage>(controller.MakeEnemyUnit);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<MakePlayerUnitMessage>(controller.MakePlayerUnit);
            Messenger.Default.Unsubscribe<MakeEnemyUnitMessage>(controller.MakeEnemyUnit);
        }
    }
}