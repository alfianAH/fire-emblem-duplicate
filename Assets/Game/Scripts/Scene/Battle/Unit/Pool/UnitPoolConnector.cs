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
            Messenger.Default.Subscribe<UnitDeadMessage>(controller.OnUnitDead);
            Messenger.Default.Subscribe<ImmovableUnitMessage>(controller.OnAllUnitImmovable);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<MakePlayerUnitMessage>(controller.MakePlayerUnit);
            Messenger.Default.Unsubscribe<MakeEnemyUnitMessage>(controller.MakeEnemyUnit);
            Messenger.Default.Unsubscribe<UnitDeadMessage>(controller.OnUnitDead);
            Messenger.Default.Subscribe<ImmovableUnitMessage>(controller.OnAllUnitImmovable);
        }
    }
}
