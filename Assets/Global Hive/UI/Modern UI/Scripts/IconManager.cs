using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GlobalHive.UI.ModernUI
{ 
    public class IconManager : Singleton<IconManager>
    {
        [Header("ICONS")]
        [SerializeField] private Sprite[] Icons;

        public Sprite GetIcon(IconType iconType)
        {
            return Icons[(int)iconType];
        }
    }

    public enum IconType
    {
        Arrow,
        ArrowBold,
        Book,
        Camera,
        Check,
        CheckWithCircle,
        Clock,
        Cup,
        Discord,
        DollarWithCircle,
        Email,
        Expand,
        Eye,
        Global,
        Heart,
        HeartFilled,
        Help,
        Hint,
        Home,
        KeyboardTab,
        LocationMark,
        Lock,
        LockOpen,
        Medal,
        Message,
        Mic,
        Cross,
        CrossWithCircle,
        NotificationBell,
        PanelSeperator,
        Pause,
        Phone,
        Photo,
        Play,
        Power,
        Refresh,
        SandClock,
        Search,
        Settings,
        Share,
        Star,
        StarFilled,
        StoreBag,
        Trash,
        User,
        Video,
        Warning,
        Web,
        MusicNote,
        Folder
    }
}
