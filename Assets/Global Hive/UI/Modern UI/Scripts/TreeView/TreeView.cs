using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GlobalHive.UI.ModernUI
{
    public class TreeView : MonoBehaviour
    {
        [Header("REFERENCES")]
        public GameObject TreeViewItem;
        public Transform ContentTransform;

        [Header("CONTENT")]
        [SerializeField] public List<TreeViewItem> Items = new List<TreeViewItem>();

        public TreeViewItem AddItem(TreeViewType Content, TreeViewItem Target = null)
        {
            TreeViewItem CTVI;
            if(Target != null)
            {
                CTVI = Instantiate(TreeViewItem, Target.ContentTransform).GetComponent<TreeViewItem>();
                Target.Items.Add(CTVI);
                CTVI.SetupItem(Content.Title, Content.Icon, Content.Content);
                if (Target.Items.Count > 0) Target.Expander.enabled = true; else Target.Expander.enabled = false;
                CTVI.ParentItem = Target;
                return CTVI;
            }
            CTVI = Instantiate(TreeViewItem, ContentTransform).GetComponent<TreeViewItem>();
            Items.Add(CTVI);
            CTVI.SetupItem(Content.Title, Content.Icon, Content.Content);
            return CTVI;
        }

        public void Clear()
        {
            for (int i = 0; i < ContentTransform.childCount; i++)
            {
                Destroy(ContentTransform.GetChild(i));
            }
        }
    }
}