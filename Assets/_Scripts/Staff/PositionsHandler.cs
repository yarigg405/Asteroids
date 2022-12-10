using System.Collections.Generic;

public class PositionsHandler : IPositionsHandler
{
    public TransformInfo PlayerTransform { get; set; }
    private Dictionary<(int, int), FieldCell> gameField;

    public PositionsHandler(MinMaxBounds bounds)
    {
        gameField = new Dictionary<(int, int), FieldCell>();
        for (int i = (int)bounds.minX; i <= (int)bounds.maxX; i++)
            for (int j = (int)bounds.minY; j <= (int)bounds.maxY; j++)
            {
                gameField.Add((i, j), new FieldCell());
            }
    }


    public IFieldCell GetCell(TransformInfo trInfo)
    {
        var cell = gameField[((int)trInfo.position.x, (int)trInfo.position.y)];
        return cell;
    }
}

public interface IPositionsHandler
{
    public IFieldCell GetCell(TransformInfo trInfo);

    public TransformInfo PlayerTransform { get; set; }
}






