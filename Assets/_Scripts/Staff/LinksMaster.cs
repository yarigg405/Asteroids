
public class LinksMaster
{
    public ISpawner Spawner { get; set; }
    public IDespawner Despawner { get; set; }
    public IUpdater Updater { get; set; }
    public IPositionsHandler PositionsHandler { get; set; }
    public MinMaxBounds MinMaxBounds { get; set; }
    public ILogicDelayer LogicDelayer{ get; set; }

}
