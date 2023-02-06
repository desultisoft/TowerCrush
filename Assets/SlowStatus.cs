using UnityEngine;

[CreateAssetMenu(menuName = "Create/LightSlowStatus")]
public class LightSlowStatus : Status
{
    public float slowAmount = 0.1f;

    public override void Tick(Enemy e)
    {
        base.Tick(e);
    }

    public override void OnStatusStart(Enemy e)
    {
        e.pathController.ChangeSpeed(-slowAmount);
    }

    public override void OnStatusEnd(Enemy e)
    {
        e.pathController.ChangeSpeed(+slowAmount);
    }
}
