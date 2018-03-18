using UnityEngine;

public class ASPosition
{
    #region Properties

    private int _x, _y;

    public int x { get { return _x; } protected set { _x = value; } }
    public int y { get { return _y; } protected set { _y = value; } }

    #endregion

    #region Constructors

    public ASPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public ASPosition(float x, float y) : this ( (int) x , (int) y) { }

    public ASPosition(Vector3 pos) : this( pos.x , pos.y ) { }

    public ASPosition(Vector2 pos) : this((Vector3)pos) { }

    #endregion

    #region Methods

    #region Distance

    public static int Distance(ASPosition p1, ASPosition p2)
    {
        return Mathf.Abs(p1.x - p2.x) + Mathf.Abs(p1.y - p2.y);
    }

    public static int Distance(Vector3 p1, Vector3 p2)
    {
        return ASPosition.Distance(new ASPosition(p1), new ASPosition(p2));
    }

    public static int Distance(Vector2 p1, Vector2 p2)
    {
        return ASPosition.Distance((Vector3)p1, (Vector3)p2);
    }

    public int DistanceFrom(ASPosition p)
    {
        return Mathf.Abs(this.x - p.x) + Mathf.Abs(this.y - p.y);
    }

    public int DistanceFrom(Vector3 p)
    {
        return Mathf.Abs((int)(this.x - p.x)) + Mathf.Abs((int)(this.y - p.y));
    }

    #endregion

    #region Conversion

    public Vector3 vector3 { get { return new Vector3(x, y); } }

    public Vector2 vector2 { get { return new Vector2(x, y); } }

    public override string ToString()
    {
        return string.Format("ASPosition({0},{1})", x, y);
    }

    #endregion

    #region Comparison

    public static bool operator ==(ASPosition p1, ASPosition p2)
    {
        return (p1.x == p2.x) && (p1.y == p2.y);
    }

    public static bool operator !=(ASPosition p1, ASPosition p2)
    {
        return !(p1 == p2);
    }

    public override bool Equals(object obj)
    {
        if (obj is ASPosition)
        {
            ASPosition p = (ASPosition)obj;
            return this == p;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() + y.GetHashCode();
    }

    #endregion

    #endregion
}
