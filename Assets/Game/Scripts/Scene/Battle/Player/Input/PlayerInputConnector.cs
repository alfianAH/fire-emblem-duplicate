using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Player.Input
{
    public class PlayerInputConnector : Connector<PlayerInputConnector, PlayerInputController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<PauseGameMessage>(controller.PauseGame);
            Messenger.Default.Subscribe<ResumeGameMessage>(controller.ResumeGame);
            Messenger.Default.Subscribe<WinMessage>(controller.OnGameOver);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<PauseGameMessage>(controller.PauseGame);
            Messenger.Default.Unsubscribe<ResumeGameMessage>(controller.ResumeGame);
            Messenger.Default.Unsubscribe<WinMessage>(controller.OnGameOver);
        }
    }
}