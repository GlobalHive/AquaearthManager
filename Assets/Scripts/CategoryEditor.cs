using GlobalHive.UI;
using MySql.Data.MySqlClient;
using Sirenix.OdinInspector;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryEditor : Singleton<CategoryEditor>
{
    [FoldoutGroup("References"), SceneObjectsOnly, SerializeField, Required]
    Animator categoryAnimator;
    [FoldoutGroup("References"), SceneObjectsOnly, SerializeField, Required]
    RawImage categoryImage;
    [FoldoutGroup("References"), SceneObjectsOnly, SerializeField, Required]
    TMP_InputField categoryName;
    [FoldoutGroup("References"), SceneObjectsOnly, SerializeField, Required]
    Button _sellButton;
    [FoldoutGroup("References"), SerializeField, Required]
    Texture2D templateTexture;

    int editCategoryID = 0;
    byte[] imageBytes = null;
    public async void OpenCategoryEditor(int categoryID) {
        LoadingScreen.Instance.ShowLoadingScreen("Lade Kategorie");
        _sellButton.interactable = false;
        editCategoryID = categoryID;

        if (categoryID != 0) {
            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += Progress_ProgressChanged;
            Category tempCategory = await Task.Run(() => Manager.Instance.GetCategoryAsync(categoryID, progress));

            if(tempCategory.Image != null)
                categoryImage.texture = tempCategory.Image.ToTexture();

            categoryName.SetTextWithoutNotify(tempCategory.Name);
        }
        Manager.Instance.Tabs.HidePanels();
        categoryAnimator.Play("Panel Open");
        LoadingScreen.Instance.HideLoadingScreen();
    }

    private void Progress_ProgressChanged(object sender, string e) {
        LoadingScreen.Instance.SetText($"Lade Kategorie\n{e}");
    }

    public void CancelCategoryEdit() {
        Manager.Instance.Tabs.ShowPanels();
        categoryName.SetTextWithoutNotify(string.Empty);
        categoryImage.texture = templateTexture;
        categoryAnimator.Play("Panel Close");
    }

    public async void SaveCategoryEdit() {
        try {
            Texture2D image = categoryImage.texture as Texture2D;
            imageBytes = image.EncodeToPNG();
            await Task.Run(() => SaveCategory());
            CancelCategoryEdit();
            Manager.Instance.LoadCategories();
        }
        catch (Exception e) {
            Color errorColor;
            ColorUtility.TryParseHtmlString("#FF7C8B", out errorColor);
            NotificationManager.Instance.ShowNotification("Kategorie bereits vorhanden!", "Diese Kategorie gibt es bereits!", Manager.Instance.ErrorIcon,
                errorColor, Color.white).SetActive(true);

            Debug.Log(e.Message);
        }
    }

    public void SelectImage() {
        var extensions = new[] {
            new SFB.ExtensionFilter("Image Files","jpg", "png", "hvei")
        };
        string[] selectedImage = SFB.StandaloneFileBrowser.OpenFilePanel("Auswählen", "", extensions, false);
        if (selectedImage.Length > 0) {
            System.IO.FileStream fs = new System.IO.FileStream(selectedImage[0], System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            byte[] imageData = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            Texture2D image = new Texture2D(256, 256);
            image.LoadImage(imageData);
            image.Apply();
            categoryImage.texture = image;
            OnValueChanged();
        }
    }

    public void OnValueChanged() {
        _sellButton.interactable = true;  
    }

    async Task SaveCategory() {
        MySqlConnection connection = await GlobalHive.DatabaseAPI.API.GetInstance().GetConnectionAsync();
        MySqlCommand cmd = null;
        string updateString = "UPDATE categorys SET name=@NAME,img=@IMG WHERE id = @ID";
        string insertString = "INSERT INTO categorys (name, img) VALUES (@NAME, @IMG)";

        if (editCategoryID == 0) {
            cmd = new MySqlCommand(insertString, connection);
            cmd.Parameters.Add("@NAME", MySqlDbType.VarChar, 255);
            cmd.Parameters.Add("@IMG", MySqlDbType.MediumBlob);
            cmd.Parameters["@NAME"].Value = categoryName.text;
            cmd.Parameters["@IMG"].Value = imageBytes;
        }
        else {
            cmd = new MySqlCommand(updateString, connection);
            cmd.Parameters.Add("@NAME", MySqlDbType.VarChar, 255);
            cmd.Parameters.Add("@IMG", MySqlDbType.MediumBlob);
            cmd.Parameters.Add("@ID", MySqlDbType.Int32);
            cmd.Parameters["@NAME"].Value = categoryName.text;
            cmd.Parameters["@IMG"].Value = imageBytes;
            cmd.Parameters["@ID"].Value = editCategoryID;
        }

        await cmd.ExecuteNonQueryAsync();
        cmd.Dispose();
        await GlobalHive.DatabaseAPI.API.GetInstance().FreeConnectionAsync(connection);
    }
}
