
public class LinksMaster
{
    public ISpawner Spawner { get; set; }
    public IDespawner Despawner { get; set; }
    public IUpdater Updater { get; set; }
    public IPositionsHandler PositionsHandler { get; set; }
    public MinMaxBounds MinMaxBounds { get; set; }
    public ILogicDelayer LogicDelayer { get; set; }
    public PlayerShipConditionLogger PlayerLogger { get; set; }
    public PlayerScoresContainer PlayerScoresContainer { get; set; }
    public IGameOverWindow GameOverWindow { get; set; }

}
