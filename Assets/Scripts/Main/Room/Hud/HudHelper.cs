public enum EHubState
{
    None,
    Self, //我方
    Enemy, //敌人
    ReverseEnemy, //敌人临时转变 为我方
}
public class HudHelper
{
    public static string GetHubRes(EHubState hubState)
    {
        string resPath = "";
        switch (hubState)
        {
            case EHubState.Self:
                resPath = "Hud/SelfHud";
                break;
            case EHubState.Enemy:
                resPath = "Hud/EnemyHud";
                break;
            case EHubState.ReverseEnemy:
                resPath = "Hud/ReverseEnemyHud";
                break;
            default:
                resPath = "Hud/DefaultHud";
                break;
        }
        return resPath;
    }
    
}