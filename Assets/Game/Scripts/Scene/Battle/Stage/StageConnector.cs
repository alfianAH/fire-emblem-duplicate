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
            Messenger.Default.Subscribe<ChangeStagePhaseMessage>(controller.SetStagePhase);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<ChangeStageInPhaseMessage>(controller.SetInPhaseEnum);
            Messenger.Default.Unsubscribe<ChangeCurrentUnitOnClickMessage>(controller.SetCurrentUnitOnClick);
            Messenger.Default.Unsubscribe<ChangeStagePhaseMessage>(controller.SetStagePhase);
        }
    }
}