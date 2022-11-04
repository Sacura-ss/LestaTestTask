using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    private Lines lines;
    private Button[,] buttons;
    private Image[] images;

    [SerializeField]
    AudioSource music;

    public void Start()
    {
        lines = new Lines(ShowBox, PlayMusic);
        InitButtons();
        InitImages();
        lines.Start();
    }

    public void ShowBox(int x, int y, int unit)
    {
        buttons[x, y].GetComponent<Image>().sprite = images[unit].sprite;
    }

    public void PlayMusic()
    {
        music.Play();
    }

    public void Click()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int nr = GetNumberOfButtonByName(name);
        int x = nr % Lines.FIELD_SIZE;
        int y = nr / Lines.FIELD_SIZE;
        Debug.Log($"click  + {name} {x} {y}");
        lines.Click(x, y);
    }

    private void InitButtons()
    {
        buttons = new Button[Lines.FIELD_SIZE, Lines.FIELD_SIZE];
        for (int i = 0; i < Lines.FIELD_SIZE * Lines.FIELD_SIZE; i++)
        {
            buttons[i % Lines.FIELD_SIZE, i / Lines.FIELD_SIZE] = GameObject
                .Find($"Button ({i})")
                .GetComponent<Button>();
        }
    }

    private void InitImages()
    {
        images = new Image[Lines.NUMBER_UNITS];
        for (int i = 0; i < Lines.NUMBER_UNITS; i++)
        {
            images[i] = GameObject.Find($"Image ({i})").GetComponent<Image>();
        }
    }

    private int GetNumberOfButtonByName(string name)
    {
        Regex regex = new Regex("\\((\\d+)\\)");
        Match match = regex.Match(name);
        if (!match.Success)
        {
            throw new Exception("Unrecognized object name");
        }
        Group group = match.Groups[1];
        return Convert.ToInt32(group.Value);
    }
}
