using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[AddComponentMenu("Global Hive/Tab Navigation")]
public class TabNavigation : MonoBehaviour
{
    EventSystem system;

    void Start()
    {
        system = EventSystem.current;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {
                TMP_InputField inputfield = next.GetComponent<TMP_InputField>();
                if (inputfield != null)
                    inputfield.ActivateInputField();

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }
}
