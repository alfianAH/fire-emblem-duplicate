using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class BaseUnitConnector : Connector<BaseUnitConnector, BaseUnitController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<OnClickUnit>(controller.OnUnitClick);
            Messenger.Default.Subscribe<OnStartDragUnit>(controller.OnStartDragUnit);
            Messenger.Default.Subscribe<OnEndDragUnit>(controller.OnEndDragUnit);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<OnClickUnit>(controller.OnUnitClick);
            Messenger.Default.Unsubscribe<OnStartDragUnit>(controller.OnStartDragUnit);
            Messenger.Default.Unsubscribe<OnEndDragUnit>(controller.OnEndDragUnit);
        }
    }
}