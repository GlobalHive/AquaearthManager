﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace GlobalHive.UI.ModernUI
{
    public class CustomDropdown : MonoBehaviour
    {
        [Header("OBJECTS")]
        public GameObject triggerObject;
        public TextMeshProUGUI selectedText;
        public Image selectedImage;
        public Transform itemParent;
        public GameObject itemObject;
        public GameObject scrollbar;
        private VerticalLayoutGroup itemList;

        [Header("SETTINGS")]
        public bool enableIcon = true;
        public bool enableTrigger = true;
        public bool enableScrollbar = true;
        public bool invokeAtStart = false;
        public bool disableSelectedSwitch = false;
        public AnimationType animationType;

        [Space(10)]
        [SerializeField]
        public List<Item> dropdownItems = new List<Item>();
        private List<Item> imageList = new List<Item>();
        public int selectedItemIndex = 0;
        [Space(10)]

        private Animator dropdownAnimator;
        private TextMeshProUGUI setItemText;
        private Image setItemImage;

        Sprite imageHelper;
        string textHelper;
        bool isOn;

        public enum AnimationType
        {
            FADING,
            SLIDING,
            STYLISH
        }

        [System.Serializable]
        public class Item
        {
            public string itemName = "Dropdown Item";
            public Sprite itemIcon;
            public UnityEvent OnItemSelection;
        }

        void Start()
        {
            dropdownAnimator = this.GetComponent<Animator>();
            itemList = itemParent.GetComponent<VerticalLayoutGroup>();
            //UpdateDropdown();
            /*for (int i = 0; i < dropdownItems.Count; ++i)
            {
                GameObject go = Instantiate(itemObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(itemParent, false);

                setItemText = go.GetComponentInChildren<TextMeshProUGUI>();
                textHelper = dropdownItems[i].itemName;
                setItemText.text = textHelper;

                Transform goImage;
                goImage = go.gameObject.transform.Find("Icon");
                setItemImage = goImage.GetComponent<Image>();
                imageHelper = dropdownItems[i].itemIcon;
                setItemImage.sprite = imageHelper;

                Button itemButton;
                itemButton = go.GetComponent<Button>();
                itemButton.onClick.AddListener(dropdownItems[i].OnItemSelection.Invoke);
                itemButton.onClick.AddListener(Animate);
                int temp = i;
                itemButton.onClick.AddListener(()=>ChangeDropdownInfo(temp));

                if (invokeAtStart == true)
                {
                    dropdownItems[i].OnItemSelection.Invoke();
                }
            }

            if (disableSelectedSwitch == false && dropdownItems.Count != 0)
            {
                selectedText.text = dropdownItems[selectedItemIndex].itemName;
                selectedImage.sprite = dropdownItems[selectedItemIndex].itemIcon;
            }

            if (enableScrollbar == true)
            {
                itemList.padding.right = 25;
                scrollbar.SetActive(true);
            }
            else
            {
                itemList.padding.right = 8;
                Destroy(scrollbar);
            }

            if (enableIcon == false)
            {
                selectedImage.enabled = false;
            }*/
        }

        public void ClearDropDown() {
            for (int i = 0; i < dropdownItems.Count; i++) {
                dropdownItems[i].OnItemSelection.RemoveAllListeners();
                itemParent.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(itemParent.GetChild(i).gameObject);
            }
            dropdownItems.Clear();
        }

        public void UpdateDropdown()
        {
            
            for (int i = 0; i < dropdownItems.Count; ++i)
            {
                int temp = i;
                GameObject go = Instantiate(itemObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(itemParent, false);

                setItemText = go.GetComponentInChildren<TextMeshProUGUI>();
                textHelper = dropdownItems[i].itemName;
                setItemText.text = textHelper;

                Transform goImage;
                goImage = go.gameObject.transform.Find("Icon");
                setItemImage = goImage.GetComponent<Image>();
                imageHelper = dropdownItems[i].itemIcon;
                setItemImage.sprite = imageHelper;

                go.GetComponent<Button>().onClick.AddListener(delegate { Animate(); ChangeDropdownInfo(temp); });
            }
        }

        public void ChangeDropdownInfo(int itemIndex)
        {
            selectedImage.sprite = dropdownItems[itemIndex].itemIcon;
            selectedText.text = dropdownItems[itemIndex].itemName;
            selectedItemIndex = itemIndex;

            if(dropdownItems[itemIndex].OnItemSelection != null)
                dropdownItems[itemIndex].OnItemSelection.Invoke();
        }
        public void ChangeDropdownInfoSilent(int itemIndex) {
            selectedImage.sprite = dropdownItems[itemIndex].itemIcon;
            selectedText.text = dropdownItems[itemIndex].itemName;
            selectedItemIndex = itemIndex;
        }
        public void ChangeDropdownInfoSilent(string name) {
            for (int i = 0; i < dropdownItems.Count; i++) {
                if (dropdownItems[i].itemName == name) {
                    selectedImage.sprite = dropdownItems[i].itemIcon;
                    selectedText.text = dropdownItems[i].itemName;
                    selectedItemIndex = i;
                }
            }
        }

        public void Animate()
        {
            if (isOn == false && animationType == AnimationType.FADING)
            {
                dropdownAnimator.Play("Fading In");
                isOn = true;
            }

            else if (isOn == true && animationType == AnimationType.FADING)
            {
                dropdownAnimator.Play("Fading Out");
                isOn = false;
            }

            else if (isOn == false && animationType == AnimationType.SLIDING)
            {
                dropdownAnimator.Play("Sliding In");
                isOn = true;
            }

            else if (isOn == true && animationType == AnimationType.SLIDING)
            {
                dropdownAnimator.Play("Sliding Out");
                isOn = false;
            }

            else if (isOn == false && animationType == AnimationType.STYLISH)
            {
                dropdownAnimator.Play("Stylish In");
                isOn = true;
            }

            else if (isOn == true && animationType == AnimationType.STYLISH)
            {
                dropdownAnimator.Play("Stylish Out");
                isOn = false;
            }

            if(enableTrigger == true && isOn == false)
            {
                triggerObject.SetActive(false);
            }

            else if (enableTrigger == true && isOn == true)
            {
                triggerObject.SetActive(true);
            }
        }
    }
}