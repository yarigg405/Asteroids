using System.Collections.Generic;

public class PositionsHandler : IPositionsHandler, IService
{
    public TransformInfo PlayerTransform { get; set; }
    private readonly Dictionary<(int, int), FieldCell> gameField;

    public PositionsHandler(MinMaxBounds bounds)
    {
        gameField = new Dictionary<(int, int), FieldCell>();
        for (var i = (int)bounds.MinX; i <= (int)bounds.MaxX; i++)
            for (var j = (int)bounds.MinY; j <= (int)bounds.MaxY; j++)
            {
                gameField.Add((i, j), new FieldCell());
            }
    }


    public IFieldCell GetCell(TransformInfo trInfo)
    {
        var cell = gameField[((int)trInfo.Position.x, (int)trInfo.Position.y)];
        return cell;
    }
}






