using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Square : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteRenderer numberSprite;

    private ASPosition _position;
    public ASPosition position
    {
        get { return _position; }
        set
        {
            _position = value;
            transform.position = position.vector3;
            this.name = "Square(" + position.x + "," + position.y + ")";
        }
    }

    public bool marked;

    private List<Square> neighbours;

    public bool closed { get; private set; }

    public void Close()
    {
        closed = true;
        sprite.color = Board.board.closedColor;
        numberSprite.color = new Color(1, 1, 1, 0);
    }

    public void Reveal(bool forceRevealSelf)
    {
        if (!closed) return;

        closed = false;

        if (this.isBomb)
        {
            sprite.color = Board.board.bombColor;
        }
        else
        {
            sprite.color = Board.board.opennedColor;
            numberSprite.color = Board.board.textColor;

            Board.board.revealedSquares++;

            if (numNeighbourBombs == 0)
            {
                this.numberSprite.color = new Color(1, 1, 1, 0);

                if (!forceRevealSelf)
                {
                    foreach (Square s in neighbours)
                    {
                        if (s.closed && !s.isBomb) s.Reveal(false);
                    }
                }
            }
        }
    }

    public void Clicked(int button)
    {
        if (!closed) return;

        if (button == 0)
        {
            if (!marked)
            {
                if (this.isBomb)
                    Board.board.BombClicked();
                else
                    this.Reveal(false);
            }
        }
        else
        {
            if (this.marked)
            {
                this.marked = false;
                sprite.color = Board.board.closedColor;
                Board.board.markedSquares--;
            }
            else
            {
                this.marked = true;
                sprite.color = Board.board.markedColor;
                Board.board.markedSquares++;
            }
        }
    }

    public bool isBomb { get; private set; }
    public void SetBomb() { isBomb = true; }
    public void RemoveBomb() { isBomb = false; }

    public int numNeighbourBombs
    {
        get
        {
            int ret = 0;

            foreach (Square s in neighbours)
            {
                if (s.isBomb) ret += 1;
            }

            return ret;
        }
    }

    private void Awake()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.numberSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        neighbours = new List<Square>();
        marked = false;
    }

    public void ResetNeighbourBombText()
    {
        numberSprite.sprite = Board.board.numberSprites[this.numNeighbourBombs];
    }

    public void AddNeighbour(Square s)
    {
        neighbours.Add(s);
    }
}
