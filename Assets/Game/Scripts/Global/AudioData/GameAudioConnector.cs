using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Global.AudioData
{
    public class GameAudioConnector : Connector<GameAudioConnector, GameAudioController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<UpdateVolumeMessage>(controller.OnSliderValueChanged);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<UpdateVolumeMessage>(controller.OnSliderValueChanged);
        }
    }
}