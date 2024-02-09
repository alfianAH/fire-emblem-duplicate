using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Item
{
    public class ItemConnector : Connector<ItemConnector, ItemController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<OnClickAddItemMessage>(controller.OnClickAddItem);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<OnClickAddItemMessage>(controller.OnClickAddItem);
        }
    }
}