using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.BattleSystem
{
    public class BattleSystemConnector : Connector<BattleSystemConnector, BattleSystemController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<StartBattleMessage>(controller.Fight);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<StartBattleMessage>(controller.Fight);
        }
    }
}
