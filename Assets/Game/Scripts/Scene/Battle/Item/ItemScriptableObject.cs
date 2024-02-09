using FireEmblemDuplicate.Scene.Battle.Item.Enum;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Item
{
	[CreateAssetMenu(fileName = "New Item", menuName = "SO/Item")]
	public class ItemScriptableObject : ScriptableObject
	{
		[SerializeField] private string _itemName;
		[SerializeField, TextArea(2, 3)] private string _description;
		[SerializeField] private ItemType _itemType;
		[SerializeField] private float _amount;
		[SerializeField] private Sprite _itemSprite;

		public string ItemName => _itemName;
		public string Description => _description;
		public ItemType Type => _itemType;
		public float Amount => _amount;
		public Sprite ItemSprite => _itemSprite;
	}
}
