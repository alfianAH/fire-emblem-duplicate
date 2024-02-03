using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    public class BaseTerrainConnector : Connector<BaseTerrainConnector, BaseTerrainController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<ChangeTerrainIndicatorMessage>(controller.ChangeTerrainIndicator);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<ChangeTerrainIndicatorMessage>(controller.ChangeTerrainIndicator);
        }
    }
}