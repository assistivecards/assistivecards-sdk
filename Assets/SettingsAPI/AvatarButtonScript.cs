using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarButtonScript : MonoBehaviour
{
    AssistiveCardsSDK assistiveCardsSDK;
    public GameObject SDK;
    private Image avatarButtonImage;
    private Texture2D avatarTexture;
    private Sprite sprite;
    SettingsAPI settingsAPI;


    private void Awake()
    {
        assistiveCardsSDK = SDK.GetComponent<AssistiveCardsSDK>();
        settingsAPI = Camera.main.GetComponent<SettingsAPI>();
    }

    async void Start()
    {
        avatarTexture = await assistiveCardsSDK.GetAvatarImage(gameObject.name);
        sprite = Sprite.Create(avatarTexture, new Rect(0.0f, 0.0f, avatarTexture.width, avatarTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        avatarButtonImage = gameObject.GetComponent<Image>();
        avatarButtonImage.sprite = sprite;
        gameObject.GetComponent<Button>().onClick.AddListener(async () =>
        {
            settingsAPI.SetAvatarImage(gameObject.name);
            settingsAPI.selectAvatarButton.image.sprite = await settingsAPI.GetAvatarImage();
        }
        );
    }



}
