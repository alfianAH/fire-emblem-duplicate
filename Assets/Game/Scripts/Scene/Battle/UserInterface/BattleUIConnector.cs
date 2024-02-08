using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.UserInterface
{
    public class BattleUIConnector : Connector<BattleUIConnector, BattleUIController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<UpdateTurnNumberMessage>(controller.UpdateTurnNumber);
            Messenger.Default.Subscribe<WinMessage>(controller.OnWin);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<UpdateTurnNumberMessage>(controller.UpdateTurnNumber);
            Messenger.Default.Unsubscribe<WinMessage>(controller.OnWin);
        }
    }
}