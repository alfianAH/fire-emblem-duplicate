using FireEmblemDuplicate.Core.Singleton;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    public class StageController : Singleton<StageController>
    {
        [SerializeField] private Rect _allowedArea;

        public Rect AllowedArea => _allowedArea;
    }
}
