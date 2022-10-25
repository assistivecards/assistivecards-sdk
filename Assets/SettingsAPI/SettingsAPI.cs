using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAPI : MonoBehaviour
{
    public TMP_InputField nicknameInputField;
    public TMP_Dropdown languageDropdown;
    public TMP_Text greetingMessage;
    private Sprite avatarSprite;
    public Button selectAvatarButton;
    AssistiveCardsSDK assistiveCardsSDK;
    public GameObject SDK;

    private string nickname;
    private string language;

    private async void Awake()
    {
        assistiveCardsSDK = SDK.GetComponent<AssistiveCardsSDK>();
        nickname = GetNickname();
        language = GetLanguage();
        nicknameInputField.text = nickname;
        selectAvatarButton.image.sprite = await GetAvatarImage();
    }
    private void Start()
    {
        greetingMessage.text = "Hello, " + nickname + " you have selected the language: " + language;

    }
    private void Update()
    {
        nickname = GetNickname();
        language = GetLanguage();
        greetingMessage.text = "Hello, " + nickname + " you have selected the language: " + language;
    }

    public void SetNickname(string nickname)
    {
        PlayerPrefs.SetString("Nickname", nickname);
    }

    public string GetNickname()
    {
        return PlayerPrefs.GetString("Nickname", "John Doe");
    }

    public void SetLanguage(string language)
    {
        PlayerPrefs.SetString("Language", language);
    }

    public string GetLanguage()
    {
        return PlayerPrefs.GetString("Language", "English");
    }


    public void SetAvatarImage(string avatarID)
    {
        PlayerPrefs.SetString("AvatarID", avatarID);
    }

    public async Task<Sprite> GetAvatarImage()
    {
        var tex = await assistiveCardsSDK.GetAvatarImage(PlayerPrefs.GetString("AvatarID", "default"));
        var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }


    public void SaveSettings()
    {
        SetNickname(nicknameInputField.text);
        SetLanguage(languageDropdown.options[languageDropdown.value].text);
    }
}
