using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.PubSub;
using SuperMaxim.Messaging;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class BaseUnitController : Subscriber<BaseUnitController>, IBaseUnitAction, IBaseUnitInteraction
    {
        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void OnUnitClick(OnClickUnit message)
        {
            Debug.Log("click");
        }

        public void OnUnitDrag(OnDragUnit message)
        {
            Debug.Log("drag");
            Vector3 position = Camera.main.ScreenToWorldPoint(message.MousePosition);
            transform.position = new Vector3(position.x, position.y, 0f);
        }

        public override void Subscribe()
        {
            Messenger.Default.Subscribe<OnClickUnit>(OnUnitClick);
            Messenger.Default.Subscribe<OnDragUnit>(OnUnitDrag);
        }

        public override void Unsubscribe()
        {
            Messenger.Default.Unsubscribe<OnClickUnit>(OnUnitClick);
            Messenger.Default.Unsubscribe<OnDragUnit>(OnUnitDrag);
        }
    }
}