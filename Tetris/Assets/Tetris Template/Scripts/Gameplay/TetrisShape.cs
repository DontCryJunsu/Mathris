using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public enum ShapeType
{
    I,
    T,
    O,
    L,
    J,
    S,
    Z
}

public class TetrisShape : MonoBehaviour
{
    public ShapeType type;
    public ShapeMovementController movementController;
    [SerializeField] Color color;
    void Awake()
    {
        movementController = GetComponent<ShapeMovementController>();
        //AssignRandomColor();
    }

    void Start()
    {
        // Default position not valid? Then it's game over
        if (!Managers.Grid.IsValidGridPosition(this.transform))
        {
            Managers.Game.SetState(typeof(GameOverState));
            Destroy(this.gameObject);
        }
    }

    public void AssignColor()
    {
        //Color temp = Managers.Palette.TurnRandomColorFromTheme();
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>().ToList())
            renderer.color = color;
    }
}
