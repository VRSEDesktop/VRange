using System;

public abstract class ScenarioLog
{
    private static readonly DateTime startTime = DateTime.Now;

    protected TimeSpan time; 

    public ScenarioLog()
    {
        time = DateTime.Now.Subtract(startTime);
    }
}
