using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsAPI : MonoBehaviour
{
    public TMP_InputField nicknameInputField;
    public TMP_Text greetingMessage;
    public Button selectAvatarButton;
    public Toggle dailyReminderToggle;
    public Toggle weeklyReminderToggle;
    public Toggle usabilityTipsToggle;
    public Toggle promotionsNotificationToggle;
    public ToggleGroup languages;
    AssistiveCardsSDK assistiveCardsSDK;
    public GameObject SDK;

    private string nickname;
    private string language;
    private string reminderPreference;
    private bool isUsabilityTipsActive;
    private bool isPromotionsNotificationActive;


    private async void Awake()
    {
        assistiveCardsSDK = SDK.GetComponent<AssistiveCardsSDK>();
        nickname = GetNickname();
        language = GetLanguage();
        isUsabilityTipsActive = GetUsabilityTipsPreference() == 1 ? true : false;
        isPromotionsNotificationActive = GetPromotionsNotificationPreference() == 1 ? true : false;
        nicknameInputField.text = nickname;
        selectAvatarButton.image.sprite = await GetAvatarImage();
    }
    private void Start()
    {
        reminderPreference = GetReminderPreference();
        usabilityTipsToggle.isOn = GetUsabilityTipsPreference() == 1 ? true : false;
        promotionsNotificationToggle.isOn = GetPromotionsNotificationPreference() == 1 ? true : false;
        foreach (var toggle in languages.GetComponentsInChildren<Toggle>())
        {
            if (toggle.name == language)
            {
                toggle.isOn = true;
            }
        }

        greetingMessage.text = "Hello " + nickname + ", you have selected the language " + language + ". Your reminder period preference is " + reminderPreference + ". You " + (isUsabilityTipsActive ? "will" : "won't") + " receive usability tips. You " + (isPromotionsNotificationActive ? "will" : "won't") + " receive promotion notifications.";
        if (reminderPreference == "Daily")
        {
            dailyReminderToggle.isOn = true;
        }
        else
        {
            weeklyReminderToggle.isOn = true;
        }

    }
    private void Update()
    {
        nickname = GetNickname();
        language = GetLanguage();
        reminderPreference = GetReminderPreference();
        isUsabilityTipsActive = GetUsabilityTipsPreference() == 1 ? true : false;
        isPromotionsNotificationActive = GetPromotionsNotificationPreference() == 1 ? true : false;
        greetingMessage.text = "Hello " + nickname + ", you have selected the language " + language + ". Your reminder period preference is " + reminderPreference + ". You " + (isUsabilityTipsActive ? "will" : "won't") + " receive usability tips. You " + (isPromotionsNotificationActive ? "will" : "won't") + " receive promotion notifications.";
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

    public void SetReminderPreference(string period)
    {
        PlayerPrefs.SetString("ReminderPeriod", period);
    }

    public string GetReminderPreference()
    {
        return PlayerPrefs.GetString("ReminderPeriod", "Weekly");
    }

    public void SetUsabilityTipsPreference(int isUsabilityTipsActive)
    {
        PlayerPrefs.SetInt("UsabilityTipsPreference", isUsabilityTipsActive);
    }

    public int GetUsabilityTipsPreference()
    {
        return PlayerPrefs.GetInt("UsabilityTipsPreference", 0);
    }

    public void SetPromotionsNotificationPreference(int isPromotionsNotificationActive)
    {
        PlayerPrefs.SetInt("PromotionsNotificationPreference", isPromotionsNotificationActive);
    }

    public int GetPromotionsNotificationPreference()
    {
        return PlayerPrefs.GetInt("PromotionsNotificationPreference", 0);
    }

    public void SaveSettings()
    {
        SetNickname(nicknameInputField.text);
        SetLanguage(languages.ActiveToggles().FirstOrDefault().GetComponentInChildren<Text>().text);
        SetReminderPreference(dailyReminderToggle.isOn ? "Daily" : "Weekly");
        SetUsabilityTipsPreference(usabilityTipsToggle.isOn ? 1 : 0);
        SetPromotionsNotificationPreference(promotionsNotificationToggle.isOn ? 1 : 0);
    }
}
