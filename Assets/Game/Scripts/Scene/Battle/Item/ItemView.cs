using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.Battle.Item
{
    public class ItemView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Toggle _itemToggle;

        public ItemScriptableObject ItemSO { get; private set; }
        public Toggle ItemToggle => _itemToggle;

        public void SetItemSO(ItemScriptableObject itemSO)
        {
            ItemSO = itemSO;
            SetItemView();
        }

        public void ResetToggle()
        {
            _itemToggle.isOn = false;
        }

        private void SetItemView()
        {
            _title.text = ItemSO.ItemName;
            _description.text = ItemSO.Description;
            _itemImage.sprite = ItemSO.ItemSprite;
            ResetToggle();
        }
    }
}
