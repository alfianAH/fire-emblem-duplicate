using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Unit.View
{
    public class BaseUnitViewConnector : Connector<BaseUnitConnector, BaseUnitView>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<SetCurrentUnitOnClickMessage>(controller.SetView);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<SetCurrentUnitOnClickMessage>(controller.SetView);
        }
    }
}
