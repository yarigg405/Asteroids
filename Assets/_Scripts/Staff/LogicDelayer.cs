

using System.Collections.Generic;

public class LogicDelayer : ILogicDelayer
{
    private List<System.Action> delayedActions = new List<System.Action>();


    public void DoDelayedLogic()
    {
        foreach (var action in delayedActions)
        {
            action?.Invoke();
        }
        delayedActions.Clear();
    }

    public void AddDelay(System.Action action)
    {
        delayedActions.Add(action);
    }
}

public interface ILogicDelayer
{
    public void DoDelayedLogic();
    public void AddDelay(System.Action action);
}

