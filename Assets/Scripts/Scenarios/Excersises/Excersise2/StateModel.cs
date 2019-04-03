using System;

public class StateModel : ExcersiseState
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnUpdate()
    {
        UpdateGUI();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
