public class StateStreet : ExcersiseState
{
    public override void OnStart()
    {
        base.OnStart();
        Exercise.ShootingRange.gameObject.SetActive(false);
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
