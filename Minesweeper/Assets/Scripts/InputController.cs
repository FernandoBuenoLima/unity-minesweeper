using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetMouseButtonUp(0)) //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Board.board.PositionClicked(getMousePosition(), 0);

        if (Input.GetMouseButtonUp(1))
            Board.board.PositionClicked(getMousePosition(), 1);
    }

    private ASPosition getMousePosition()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (v.x < 0) v.x = -1;
        if (v.y < 0) v.y = -1;

        return new ASPosition(v);
    }
}
