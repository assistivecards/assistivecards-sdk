using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_InputField outputArea;
    public RawImage rawImage;
    public TMP_InputField avatarImageSizeInput;
    public TMP_InputField avatarIdInput;
    public TMP_InputField packImageSizeInput;
    public TMP_InputField packSlugInput;
    public TMP_InputField cardImagePackSlugInput;
    public TMP_InputField cardImageCardSlugInput;
    public TMP_InputField cardImageSizeInput;
    public TMP_InputField cardLanguageInput;
    public TMP_InputField cardPackSlugInput;
    public TMP_InputField languageCodeInput;
    public TMP_InputField activitySlugInput;
    public TMP_InputField cardBySlugInput;
    public TMP_InputField packBySlugInput;

    private void Start()
    {
        cardImageSizeInput.text = "256";
        avatarImageSizeInput.text = "256";
        packImageSizeInput.text = "256";
    }
}
