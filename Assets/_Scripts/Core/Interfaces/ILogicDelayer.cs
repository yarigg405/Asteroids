public interface ILogicDelayer
{
    public void DoDelayedLogic();
    public void AddDelay(System.Action action);
}

