using UnityEngine;

public class Vector3EventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public Vector3Event_SO gameEvent;

    [Tooltip("Response to Invoke When Evemt is raised.")]
    public Vector3Event Response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
    public void OnEventRaised(Vector3 intToSend)
    {
        Response.Invoke(intToSend);
    }

}
