using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Item.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;
using FireEmblemDuplicate.Utility;
using SuperMaxim.Messaging;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.Battle.Item
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private GameObject _itemSelectionScreen;
        [SerializeField] private Transform _itemParent;
        [SerializeField] private Button _applyButton, _cancelButton;
        [SerializeField] private GameObject _warningText;

        private ItemView _itemPrefab;
        private List<ItemScriptableObject> _itemSOList = new List<ItemScriptableObject>();
        private List<ItemView> _itemList = new List<ItemView>();
        private BaseUnitController _currentClickUnit;

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
        
        public void OnClickAddItem(OnClickAddItemMessage message)
        {
            _itemSelectionScreen.SetActive(true);
            _warningText.SetActive(false);
            foreach(ItemView itemView in _itemList)
            {
                itemView.ResetToggle();
            }

            _currentClickUnit = message.Unit;
        }

        private void SetItemList()
        {
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
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
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
                    ItemScriptableObject itemSO = itemView.ItemSO;
                    Messenger.Default.Publish(new AddItemEffectMessage(
                        _currentClickUnit, itemSO.Type, itemSO.Amount));
                }
                _itemSelectionScreen.SetActive(false);
            }
        }

        private void OnClickCancel()
        {
            Messenger.Default.Publish(new PlaySFXMessage(AudioName.SFX_BUTTON_PRESSED));
            _itemSelectionScreen.SetActive(false);
        }
    }
}
