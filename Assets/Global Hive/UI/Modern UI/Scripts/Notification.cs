using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GlobalHive.UI.ModernUI;

namespace GlobalHive.Helpers
{
    public class Notification : Singleton<Notification>
    {
        [Header("REFERENCES")]
        [SerializeField] private Image _NotificationIcon;
        [SerializeField] private TMP_Text _NotificationTitle;
        [SerializeField] private TMP_Text _NotificationDescription;
        [SerializeField] private Animator _Animator;


        [Header("SETTINGS")]
        [SerializeField] private string[] AnimationNames;

        private Stack<NotificationInformation> PendingNotifications = new Stack<NotificationInformation>();
        private bool isWorking;

        public void Show(NotificationInformation Notification)
        {
            PendingNotifications.Push(Notification);
            if(!isWorking)
            {
                isWorking = true;
                InvokeRepeating("ShowNotifications", 0f, 5f);
            }

        }

        private void ShowNotifications()
        {
            if (PendingNotifications.Count == 0)
            {
                isWorking = false;
                CancelInvoke("ShowNotifications");
                return;
            }

            _NotificationIcon.sprite = IconManager.Instance.GetIcon(PendingNotifications.Peek().Icon);
            _NotificationTitle.text = PendingNotifications.Peek().Title;
            _NotificationDescription.text = PendingNotifications.Peek().Description;
            _Animator.Play(AnimationNames[(int)PendingNotifications.Peek().Animation]);

            PendingNotifications.Pop();
        }
    }

    public class NotificationInformation
    {
        public string Title;
        public string Description;
        public IconType Icon;
        public NotificationAnimation Animation;
    }

    public enum NotificationAnimation
    { 
        Fading,
        SlideTop,
        SlideRight
    }
}

