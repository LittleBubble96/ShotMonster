using UnityEngine;

public class GameConst
{
    public static Vector3 DefaultActorPosition = new Vector3(0, 0, 0);
    public static Quaternion DefaultActorRotation = Quaternion.identity;
    
    public static Vector3 CharacterSpawnPosition = new Vector3(70, 25, 36);
    public static Vector3 CharacterSpawnRotation = new Vector3(0, 0, 0);
    
    public const string AnimationPreNameFloat = "Float";
    public const string AnimationPreNameBool = "Bool";
    public const string AnimationPreNameInt = "Int";
    
    public const string ColonStr = ":";
}