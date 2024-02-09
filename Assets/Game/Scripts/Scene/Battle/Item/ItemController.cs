using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.Battle.Item
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private Transform _itemParent;
        [SerializeField] private Button _applyButton, _cancelButton;
        [SerializeField] private GameObject _warningText;

        private ItemView _itemPrefab;
        private List<ItemScriptableObject> _itemSOList = new List<ItemScriptableObject>();
        private List<ItemView> _itemList = new List<ItemView>();
        private const int MAX_SELECTED_ITEM = 2;

        private void Start()
        {
            LoadItemPrefab();
            LoadItemSO();
            SetItemList();

            _applyButton.onClick.RemoveAllListeners();
            _applyButton.onClick.AddListener(OnClickApply);

            _cancelButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.AddListener(OnClickCancel);
        }

        public void SetItemList()
        {
            _warningText.SetActive(false);
            foreach (ItemScriptableObject itemSO in _itemSOList)
            {
                ItemView duplicateItem = Instantiate(_itemPrefab, _itemParent);
                duplicateItem.SetItemSO(itemSO);
                _itemList.Add(duplicateItem);
            }
        }

        private void LoadItemPrefab()
        {
            _itemPrefab = Resources.Load<ItemView>("Prefabs/Item/Item Toggle");
        }

        private void LoadItemSO()
        {
            _itemSOList.AddRange(Resources.LoadAll<ItemScriptableObject>("SO/Item"));
        }

        private void OnClickApply()
        {
            List<ItemView> selectedItems = _itemList.FindAll(i => i.ItemToggle.isOn);
            int selectedItemCount = selectedItems.Count;

            if(selectedItemCount > MAX_SELECTED_ITEM)
            {
                _warningText.SetActive(true);
            }
            else
            {
                foreach(ItemView itemView in selectedItems)
                {
                    Debug.Log(itemView.ItemSO.Description);
                }
            }
        }

        private void OnClickCancel()
        {

        }
    }
}
