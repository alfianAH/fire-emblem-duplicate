using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.MainMenu.Audios.SoundEffect
{
    public class SoundEffectConnector : Connector<SoundEffectConnector, SoundEffectController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<PlaySFXMessage>(controller.Play);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<PlaySFXMessage>(controller.Play);
        }
    }
}