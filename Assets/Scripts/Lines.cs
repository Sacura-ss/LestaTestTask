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
    private int movedFromX,
        movedFromY;
    private bool isAnimalSelected;
    private const int BLOCK_ID = 3;
    private const int EMPTY_ID = 0;

    public Lines(ShowBox showBox, PlayMusic playMusic)
    {
        this.showBox = showBox;
        this.playMusic = playMusic;
        field = new int[FIELD_SIZE, FIELD_SIZE];
    }

    public void Start()
    {
        InitialFillingField();
        isAnimalSelected = false;
    }

    public void Click(int x, int y)
    {
        if (field[x, y] > 0)
        {
            TakeUnit(x, y);
        }
        else
        {
            MoveUnit(x, y);
            playMusic();
        }
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

    private void TakeUnit(int x, int y)
    {
        if (field[x, y] != BLOCK_ID)
        {
            movedFromX = x;
            movedFromY = y;
            isAnimalSelected = true;
        }
    }

    private void MoveUnit(int x, int y)
    {
        if (!isAnimalSelected)
            return;
        if (!CanMove(x, y))
            return;
        SetField(x, y, field[movedFromX, movedFromY]);
        SetField(movedFromX, movedFromY, EMPTY_ID);
    }

    private void SetField(int x, int y, int unit)
    {
        field[x, y] = unit;
        showBox(x, y, unit);
    }

    private bool CanMove(int x, int y)
    {
        // ДОПИСАТЬ
        return true;
    }
}
