using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Vector3Event_SO : ScriptableObject
{
    private List<Vector3EventListener> listeners = new List<Vector3EventListener>();

    public void Rais(Vector3 intToSend)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(intToSend);
        }
    }
    public void RegisterListener(Vector3EventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    public void UnregisterListener(Vector3EventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
