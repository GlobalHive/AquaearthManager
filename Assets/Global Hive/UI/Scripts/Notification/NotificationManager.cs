using GlobalHive.UI.ModernUI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalHive.UI {
    public class NotificationManager : Singleton<NotificationManager> {
        [FoldoutGroup("References"), Required, SerializeField, AssetsOnly]
        GameObject NotificationTemplate;
        [FoldoutGroup("References"), Required, SerializeField]
        Transform Parent;
        [FoldoutGroup("Settings"), Required, SerializeField]
        int amountToPool;
        [FoldoutGroup("Settings"), Required, SerializeField]
        bool expandPool;

        List<Notification> notificationPool = new List<Notification>();

        private void Start() {
            for (int i = 0; i < amountToPool; i++) {
                Notification tempNotification = new Notification();
                tempNotification.NotificationObject = Instantiate(NotificationTemplate, Parent);
                notificationPool.Add(tempNotification);
            }
        }

        public Notification ShowNotification(string Title, string Description) {
            Notification tempNotification = GetNotification();
            tempNotification.NotificationObject.transform.Find("Content/Text Area/Title").GetComponent<TMP_Text>().SetText(Title);
            tempNotification.NotificationObject.transform.Find("Content/Text Area/Description").GetComponent<TMP_Text>().SetText(Description);
            tempNotification.NotificationObject.transform.Find("Content/Icon Background/Icon").GetComponent<Image>().enabled = false;

            return tempNotification;
        }

        public Notification ShowNotification(string Title, string Description, Sprite Icon) {
            Notification tempNotification = GetNotification();
            tempNotification.NotificationObject.transform.Find("Content/Text Area/Title").GetComponent<TMP_Text>().SetText(Title);
            tempNotification.NotificationObject.transform.Find("Content/Text Area/Description").GetComponent<TMP_Text>().SetText(Description);
            tempNotification.NotificationObject.transform.Find("Content/Icon Background/Icon").GetComponent<Image>().enabled = true;
            tempNotification.NotificationObject.transform.Find("Content/Icon Background/Icon").GetComponent<Image>().sprite = Icon;

            return tempNotification;
        }

        public Notification ShowNotification(string Title, string Description, Sprite Icon, Color BackgroundColor, Color IconColor) {
            Notification tempNotification = GetNotification();
            tempNotification.NotificationObject.transform.Find("Content/Text Area/Title").GetComponent<TMP_Text>().SetText(Title);
            tempNotification.NotificationObject.transform.Find("Content/Text Area/Description").GetComponent<TMP_Text>().SetText(Description);
            tempNotification.NotificationObject.transform.Find("Content/Icon Background/Icon").GetComponent<Image>().enabled = true;
            tempNotification.NotificationObject.transform.Find("Content/Icon Background/Icon").GetComponent<Image>().sprite = Icon;
            tempNotification.NotificationObject.transform.Find("Content/Icon Background").GetComponent<Image>().color = BackgroundColor;
            tempNotification.NotificationObject.transform.Find("Content/Icon Background/Icon").GetComponent<Image>().color = IconColor;

            return tempNotification;
        }

        public void HideNotifications() {
            foreach (var item in notificationPool) {
                item.NotificationObject.SetActive(false);
            }
        }

        public Notification GetNotification() {
            foreach (var item in notificationPool) {
                if (!item.NotificationObject.activeInHierarchy) {
                    return item;
                }
            }

            if (expandPool) {
                Notification tempNotification = new Notification();
                tempNotification.NotificationObject = Instantiate(NotificationTemplate, Parent);
                notificationPool.Add(tempNotification);
                return tempNotification;
            }

            return null;
        }

        public class Notification {
            public string Title = string.Empty;
            public string Description = string.Empty;
            public Sprite Icon;
            public GameObject NotificationObject = null;
            public Color IconBackgroundColor;
            public Color IconColor = Color.white;

            public void SetActive(bool isActive) {
                NotificationObject.SetActive(isActive);
            }
        }

    }
}