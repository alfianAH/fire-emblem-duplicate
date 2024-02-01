using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class BaseUnitController : MonoBehaviour, IBaseUnit
    {
        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void OnUnitClick()
        {
            Debug.Log("click");
        }

        public void OnUnitDrag()
        {
            Debug.Log("drag");
        }
    }
}