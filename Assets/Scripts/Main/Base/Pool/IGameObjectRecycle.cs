using UnityEngine;

public class RecycleObject : MonoBehaviour, IRecycle
{
    public string ObjectName { get; set; }

    public virtual void Recycle()
    {
        // Implement the recycling logic here
        Debug.Log("Object recycled");
    }
}