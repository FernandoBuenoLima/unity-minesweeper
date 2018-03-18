using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Playing, NotPlaying }

public class Board : MonoBehaviour
{
    public static Board board;

    public GameState gameState;

    public Text gameOverText;
    public Text winText;
    public Text bombsLeft;

    public Color closedColor;
    public Color markedColor;
    public Color opennedColor;
    public Color bombColor;
    public Color textColor;

    public GameObject squarePrefab;

    public int width, height;
    public int numBombs;
    public int revealedSquares;
    public int markedSquares;

    public Square[,] squares;

    public Sprite[] numberSprites;

    private void Awake()
    {
        board = this;
        gameState = GameState.NotPlaying;
    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        this.setBoard();

        revealedSquares = 0;
        markedSquares = 0;
        gameState = GameState.Playing;
        gameOverText.color = new Color(1, 1, 1, 0);
        winText.color = new Color(1, 1, 1, 0);
        bombsLeft.text = "Bombs Left: " + numBombs;
    }

    private List<Square> getAllSquares()
    {
        List<Square> ret = new List<Square>();

        foreach (Square s in squares)
        {
            ret.Add(s);
        }

        return ret;
    }

    private void addNeighbours(int x, int y)
    {
        Square s = squares[x, y];

        for (int i = (x - 1); i <= (x + 1); i++)
        {
            for (int j = (y - 1); j <= (y + 1); j++)
            {
                if (i < 0 || j < 0 || i >= width || j >= height || (i == x && j == y))
                    continue;

                s.AddNeighbour(squares[i, j]);
            }
        }
    }

    private void addNeighbours()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                addNeighbours(x, y);
            }
        }
    }

    private void addBombs()
    {
        List<Square> squareList = getAllSquares();
        int bombs = numBombs;
        int i;
        Square square;

        while (bombs > 0 && squareList.Count > 0)
        {
            i = Random.Range(0, squareList.Count - 1);

            square = squareList[i];
            square.SetBomb();
            squareList.RemoveAt(i);

            bombs--;
        }
    }

    private void checkWinCondition()
    {
        if (revealedSquares + numBombs == (width * height) && gameState == GameState.Playing)
            GameWon();
    }

    public void setBoard()
    {
        if (width <= 0 || height <= 0)
        {
            Debug.LogError("Board.setBoard(): Invalid width/height (" + width + "," + height + ")");
            return;
        }

        if (squares != null)
        {
            foreach (Square s in squares)
            {
                Destroy(s.gameObject);
            }
        }

        squares = new Square[width, height];

        Square square;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                square = Instantiate<GameObject>(squarePrefab).GetComponent<Square>();
                square.transform.parent = this.transform;

                square.position = new ASPosition(x, y);
                square.Close();

                squares[x, y] = square;
            }
        }

        addNeighbours();
        addBombs();

        /* Shows the position of all the bombs for debugging
         * 
        foreach (Square s in squares)
        {
            if (s.isBomb)
                Debug.Log("Square: " + s.name + " is a bomb");
        }
        */

        foreach (Square s in squares)
        {
            s.ResetNeighbourBombText();
        }
    }

    public void PositionClicked(ASPosition position, int button)
    {
        if (gameState == GameState.NotPlaying) return;

        if (position.x < 0 || position.y < 0 || position.x >= width || position.y >= height)
            return;

        squares[position.x, position.y].Clicked(button);

        bombsLeft.text = "Bombs Left: " + (numBombs - markedSquares);

        checkWinCondition();
    }

    public void BombClicked()
    {
        gameState = GameState.NotPlaying;

        foreach (Square s in squares)
        {
            s.Reveal(true);
        }

        gameOverText.color = new Color(0, 0, 0, 1);
    }

    private void GameWon()
    {
        gameState = GameState.NotPlaying;

        winText.color = new Color(0, 0, 0, 1);
    }

}
