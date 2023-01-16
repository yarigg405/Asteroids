

using System.Collections.Generic;

public class LogicDelayer : ILogicDelayer, IService
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

