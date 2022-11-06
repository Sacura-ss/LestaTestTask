using System;

public delegate void ShowBox(int x, int y, int unit);
public delegate void PlayMusic();
public delegate void StopGame();

public class Lines
{
    public const int FIELD_SIZE = 5;
    public const int NUMBER_UNITS = 5;

    private ShowBox showBox;
    private PlayMusic playMusic;
    private StopGame stopGame;
    private int[,] field;
    private int movedFromX,
        movedFromY;
    private bool isAnimalSelected;
    private const int BLOCK_ID = 3;
    private const int EMPTY_ID = 0;
    private const int YELLOW_ID = 4;
    private const int RED_ID = 2;
    private const int BLUE_ID = 1;

    public Lines(ShowBox showBox, PlayMusic playMusic, StopGame stopGame)
    {
        this.showBox = showBox;
        this.playMusic = playMusic;
        this.stopGame = stopGame;
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
            if (checkState())
            {
                stopGame();
            }
        }
    }

    public void InitialFillingField()
    {
        // set blocks
        SetField(1, 0, BLOCK_ID);
        SetField(3, 0, BLOCK_ID);
        SetField(1, 2, BLOCK_ID);
        SetField(3, 2, BLOCK_ID);
        SetField(1, 4, BLOCK_ID);
        SetField(3, 4, BLOCK_ID);

        // set yellow
        SetField(0, 2, YELLOW_ID);
        SetField(2, 0, YELLOW_ID);
        SetField(2, 1, YELLOW_ID);
        SetField(4, 3, YELLOW_ID);
        SetField(4, 4, YELLOW_ID);

        // set blue
        SetField(0, 0, BLUE_ID);
        SetField(0, 3, BLUE_ID);
        SetField(2, 3, BLUE_ID);
        SetField(4, 0, BLUE_ID);
        SetField(4, 1, BLUE_ID);

        // set red
        SetField(0, 1, RED_ID);
        SetField(0, 4, RED_ID);
        SetField(2, 4, RED_ID);
        SetField(2, 2, RED_ID);
        SetField(4, 2, RED_ID);

        // set empty
        SetField(1, 1, EMPTY_ID);
        SetField(1, 3, EMPTY_ID);
        SetField(3, 1, EMPTY_ID);
        SetField(3, 3, EMPTY_ID);
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
        isAnimalSelected = false;
    }

    private void SetField(int x, int y, int unit)
    {
        field[x, y] = unit;
        showBox(x, y, unit);
    }

    private bool CanMove(int x, int y)
    {
        if (
            Math.Abs(x - movedFromX) == 1 && y == movedFromY
            || Math.Abs(y - movedFromY) == 1 && x == movedFromX
        )
            return true;
        return false;
    }

    private bool checkState()
    {
        bool yellowIsCollected = field[0, 0] == YELLOW_ID;
        bool blueIsCollected = field[2, 0] == BLUE_ID;
        bool redIsCollected = field[4, 0] == RED_ID;

        for (int y = 0; y < FIELD_SIZE - 1; y++)
        {
            if (!yellowIsCollected || !blueIsCollected || !redIsCollected)
                return false;
            yellowIsCollected = field[0, y] == field[0, y + 1];
            blueIsCollected = field[2, y] == field[2, y + 1];
            redIsCollected = field[4, y] == field[4, y + 1];
        }

        if (yellowIsCollected && blueIsCollected && redIsCollected)
            return true;
        return false;
    }
}
