/// <summary>
/// Enumeration for decisionmaking.
/// </summary>
public enum DecisionStatus
{
    DidNotDecide,
    ShouldDecide,
    DidDecide
}

/// <summary>
/// Logic behind changing the NPCs state.
/// </summary>
public abstract class Decision
{
    public abstract bool Decide(AIController owner);
}
