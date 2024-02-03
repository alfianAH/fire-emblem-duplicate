using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    public class StageConnector : Connector<StageConnector, StageController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<ChangeStageInPhaseMessage>(controller.SetInPhaseEnum);
            Messenger.Default.Subscribe<ChangeCurrentUnitOnClickMessage>(controller.SetCurrentUnitOnClick);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<ChangeStageInPhaseMessage>(controller.SetInPhaseEnum);
            Messenger.Default.Unsubscribe<ChangeCurrentUnitOnClickMessage>(controller.SetCurrentUnitOnClick);
        }
    }
}