using System;
using UnityEngine;

[DisallowMultipleComponent]
public class MonsterDestroyedEvent : MonoBehaviour
{
    public event Action<MonsterDestroyedEvent, MonsterDestroyedEventArgs> OnMonsterDestroyed;

    public void CallMonsterDestroyedEvent(Vector3 point)
    {
        OnMonsterDestroyed?.Invoke(this, new MonsterDestroyedEventArgs()
        {
            point = point
        });
    }
}

public class MonsterDestroyedEventArgs : EventArgs
{
    public Vector3 point; // ÆÄ±« À§Ä¡
}