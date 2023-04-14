using System;
using System.Collections.Generic;

public class LogicDelayer : ILogicDelayer, IService
{
    private readonly List<Action> delayedActions = new();


    public void DoDelayedLogic()
    {
        foreach (var action in delayedActions)
        {
            action?.Invoke();
        }
        delayedActions.Clear();
    }

    public void AddDelay(Action action)
    {
        delayedActions.Add(action);
    }
}

