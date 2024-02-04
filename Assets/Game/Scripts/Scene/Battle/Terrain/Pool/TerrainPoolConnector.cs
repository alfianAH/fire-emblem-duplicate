using FireEmblemDuplicate.Core.PubSub;
using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;

namespace FireEmblemDuplicate.Scene.Battle.Terrain.Pool
{
    public class TerrainPoolConnector : Connector<TerrainPoolConnector, TerrainPoolController>
    {
        public override void Subscribe()
        {
            Messenger.Default.Subscribe<MakeTerrainMessage>(controller.MakeTerrains);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<MakeTerrainMessage>(controller.MakeTerrains);
        }
    }
}
