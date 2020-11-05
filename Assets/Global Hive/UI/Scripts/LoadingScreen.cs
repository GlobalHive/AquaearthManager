using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreen : Singleton<LoadingScreen>
{
    [FoldoutGroup("References"), Required, SerializeField]
    TMP_Text loaderText;

    private void Start() {
        gameObject.SetActive(false);
    }

    public void ShowLoadingScreen(string loadingText = "") {
        loaderText.SetText(loadingText);
        gameObject.SetActive(true);
    }

    public void SetText(string loadingText) {
        loaderText.SetText(loadingText);
    }

    public void HideLoadingScreen() {
        gameObject.SetActive(false);
    }
    
}
