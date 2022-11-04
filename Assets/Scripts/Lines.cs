using System;

public delegate void ShowBox(int x, int y, int unit);
public delegate void PlayMusic();

public class Lines
{
    public const int FIELD_SIZE = 5;
    public const int NUMBER_UNITS = 5;

    private ShowBox showBox;
    private PlayMusic playMusic;
    private int[,] field;

    public Lines(ShowBox showBox, PlayMusic playMusic)
    {
        this.showBox = showBox;
        this.playMusic = playMusic;
        field = new int[FIELD_SIZE, FIELD_SIZE];
    }

    public void Start()
    {
        //ClearField();
        InitialFillingField();
    }

    public void Click(int x, int y)
    {
        showBox(x, y, (x + y) % NUMBER_UNITS);
        playMusic();
    }

    public void InitialFillingField()
    {
        // set blocks
        SetField(1, 0, 3);
        SetField(3, 0, 3);
        SetField(1, 2, 3);
        SetField(3, 2, 3);
        SetField(1, 4, 3);
        SetField(3, 4, 3);

        // set yellow
        SetField(0, 2, 4);
        SetField(2, 0, 4);
        SetField(2, 1, 4);
        SetField(4, 3, 4);
        SetField(4, 4, 4);

        // set blue
        SetField(0, 0, 1);
        SetField(0, 3, 1);
        SetField(2, 3, 1);
        SetField(4, 0, 1);
        SetField(4, 1, 1);

        // set red
        SetField(0, 1, 2);
        SetField(0, 4, 2);
        SetField(2, 4, 2);
        SetField(2, 2, 2);
        SetField(4, 2, 2);

        // set empty
        SetField(1, 1, 0);
        SetField(1, 3, 0);
        SetField(3, 1, 0);
        SetField(3, 3, 0);
    }

    private void ClearField()
    {
        for (int i = 0; i < Lines.FIELD_SIZE * Lines.FIELD_SIZE; i++)
        {
            SetField(i % Lines.FIELD_SIZE, i / Lines.FIELD_SIZE, 0);
        }
    }

    private void SetField(int x, int y, int unit)
    {
        field[x, y] = unit;
        showBox(x, y, unit);
    }
}
