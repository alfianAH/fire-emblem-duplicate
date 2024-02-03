using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class BaseUnitConnector : Connector<BaseUnitConnector, BaseUnitController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<OnClickUnitMessage>(controller.OnUnitClick);
            Messenger.Default.Subscribe<OnStartDragUnitMessage>(controller.OnStartDragUnit);
            Messenger.Default.Subscribe<OnEndDragUnitMessage>(controller.OnEndDragUnit);
            Messenger.Default.Subscribe<OnClickTerrainMessage>(controller.MoveUnitOnClickedTerrain);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<OnClickUnitMessage>(controller.OnUnitClick);
            Messenger.Default.Unsubscribe<OnStartDragUnitMessage>(controller.OnStartDragUnit);
            Messenger.Default.Unsubscribe<OnEndDragUnitMessage>(controller.OnEndDragUnit);
            Messenger.Default.Unsubscribe<OnClickTerrainMessage>(controller.MoveUnitOnClickedTerrain);
        }
    }
}