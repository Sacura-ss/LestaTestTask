using System;

public delegate void ShowBox(int x, int y, int unit);
public delegate void PlayMusic();

public class Lines
{
    public const int FIELD_SIZE = 5;
    public const int NUMBER_UNITS = 5;

    private ShowBox showBox;
    private PlayMusic playMusic;

    public Lines(ShowBox showBox, PlayMusic playMusic)
    {
        this.showBox = showBox;
        this.playMusic = playMusic;
    }

    public void Start() { }

    public void Click(int x, int y)
    {
        showBox(x, y, (x + y) % NUMBER_UNITS);
        playMusic();
    }
}
