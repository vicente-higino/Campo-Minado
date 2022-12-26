using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{

    public Button playButton, configButton;
    public GameObject panel;

    public Text xTextElement, yTextElement, bombsText;
    public Scrollbar bombsPercentageScrollBarElement;
    // Use this for initialization
    void Awake()
    {

        playButton.GetComponent<Button>().onClick.AddListener(ClickListener);
        configButton.GetComponent<Button>().onClick.AddListener(ClickConfig);
        bombsPercentageScrollBarElement.onValueChanged.AddListener(OnValueChange);


    }
    void ClickListener()
    {
        Config.X = int.Parse(xTextElement.text);
        Config.Y = int.Parse(yTextElement.text);
        Config.BombsPercentage = bombsPercentageScrollBarElement.value;
        SceneManager.LoadScene("campominado");
    }
    void ClickConfig()
    {
        panel.SetActive(true);

    }

    public void OnValueChange(float value)
    {
        int X = int.Parse(xTextElement.text);
        int Y = int.Parse(yTextElement.text);
        int bombs = (int)((value) * (X + 1) * (Y + 1));
        bombsText.text = string.Format("Bombs: {0}", bombs);
    }


}
