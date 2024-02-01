using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class BaseUnitConnector : Connector<BaseUnitConnector>
    {
        private BaseUnitController _controller;

        private void Awake()
        {
            _controller = GetComponent<BaseUnitController>();
        }

        public override void Subscribe()
        {
            Messenger.Default.Subscribe<OnClickUnit>(_controller.OnUnitClick);
            Messenger.Default.Subscribe<OnStartDragUnit>(_controller.OnStartDragUnit);
            Messenger.Default.Subscribe<OnEndDragUnit>(_controller.OnEndDragUnit);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<OnClickUnit>(_controller.OnUnitClick);
            Messenger.Default.Unsubscribe<OnStartDragUnit>(_controller.OnStartDragUnit);
            Messenger.Default.Unsubscribe<OnEndDragUnit>(_controller.OnEndDragUnit);
        }
    }
}