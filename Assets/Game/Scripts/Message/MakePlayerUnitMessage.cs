using FireEmblemDuplicate.Scene.Battle.Stage;
using System.Collections.Generic;

namespace FireEmblemDuplicate.Message
{
    public struct MakePlayerUnitMessage
    {
        public List<StageUnitData> UnitDatas { get; private set; }

        public MakePlayerUnitMessage(List<StageUnitData> unitDatas)
        {
            UnitDatas = unitDatas;
        }
    }
}
