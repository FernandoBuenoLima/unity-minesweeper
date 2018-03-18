using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PositionChangedEvent : UnityEvent<ASPosition> { }

public class ASMutablePosition : ASPosition {

    #region Properties

    public PositionChangedEvent onPositionChanged;

    #endregion

    #region Constructors

    public ASMutablePosition(int x, int y) : base(x, y)
    {
        onPositionChanged = new PositionChangedEvent();
    }

    public ASMutablePosition(float x, float y) : this ( (int) x , (int) y ) { }

    public ASMutablePosition(ASPosition pos) : this ( pos.x , pos.y ) { }

    public ASMutablePosition(ASMutablePosition pos) : this ( (ASPosition) pos ) { }

    public ASMutablePosition(Vector3 pos) : this ( pos.x , pos.y ) { }

    public ASMutablePosition(Vector2 pos) : this( pos.x , pos.y ) { }

    #endregion

    #region Methods

    #region Moving

    #region Move

    public void Move(int x, int y)
    {
        this.x += x;
        this.y += y;

        onPositionChanged.Invoke(this);
    }

    public void Move(float x, float y) { this.Move ( (int) x , (int) y ); }

    public void Move(ASPosition p) { this.Move ( p.x , p.y ); }

    public void Move(Vector2 v) { this.Move ( v.x , v.y ); }

    public void Move(Vector3 v) { this.Move ( v.x , v.y ); }

    #endregion

    #region MoveTo

    public void MoveTo(int x, int y)
    {
        this.x = x;
        this.y = y;

        onPositionChanged.Invoke(this);
    }

    public void MoveTo(float x, float y) { this.MoveTo ( (int) x , (int) y ); }

    public void MoveTo(ASPosition p) { this.MoveTo(p.x, p.y); }

    public void MoveTo(Vector2 v) { this.MoveTo ( v.x , v.y ); }

    public void MoveTo(Vector3 v) { this.MoveTo ( v.x , v.y ); }

    #endregion

    #endregion

    #region Conversion

    public override string ToString()
    {
        return string.Format("ASMutablePosition({0},{1})", x, y);
    }

    #endregion

    #endregion
}
