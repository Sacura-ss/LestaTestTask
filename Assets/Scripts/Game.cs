using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private Lines lines;
    Button[,] buttons;
    Image[] images;

    public void Start()
    {
        lines = new Lines(ShowBox, PlayMusic);
        InitButtons();
        lines.Start();
    }

    public void ShowBox(int x, int y, int unit) { }

    public void PlayMusic() { }

    public void Click()
    {
        Debug.Log("click");
    }

    private void InitButtons()
    {
        buttons = new Button[Lines.SIZE, Lines.SIZE];
        for (int i = 0; i < Lines.SIZE * Lines.SIZE; i++)
        {
            buttons[i % Lines.SIZE, i / Lines.SIZE] = GameObject
                .Find($"Button ({i})")
                .GetComponent<Button>();
        }
    }
}
