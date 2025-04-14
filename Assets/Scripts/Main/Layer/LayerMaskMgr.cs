using UnityEngine;

public class LayerMaskMgr
{
    public static string PetLayerName = "Pet";
    public static string BreakInteractiveLayerName = "Break";
    public static string PlayerLayerName = "Player";
    public static LayerMask PlayerLayerMask = LayerMask.GetMask(PlayerLayerName);
    public static LayerMask BreakInteractiveLayerMask = LayerMask.GetMask(BreakInteractiveLayerName);
}