using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.BattleSystem.View
{
    public class BattleSystemViewConnector : Connector<BattleSystemViewConnector, BattleSystemView>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<OnBattleBeginMessage>(controller.OnBattleBegin);
            Messenger.Default.Subscribe<DecreaseHPMessage>(controller.OnDecreaseHp);
            Messenger.Default.Subscribe<OnBattleFinishMessage>(controller.OnBattleFinish);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<OnBattleBeginMessage>(controller.OnBattleBegin);
            Messenger.Default.Unsubscribe<DecreaseHPMessage>(controller.OnDecreaseHp);
            Messenger.Default.Unsubscribe<OnBattleFinishMessage>(controller.OnBattleFinish);
        }
    }
}
