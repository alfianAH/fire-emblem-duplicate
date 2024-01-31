using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class BaseUnit
    {
        [SerializeField] private BaseUnitScriptableObject _baseUnitSO;
        [SerializeField] private UnitAffinity _affinity;
        private int _movementSpace;
    }
}
