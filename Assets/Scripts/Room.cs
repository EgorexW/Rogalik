using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomTemplateType type;
    public EventType eventType = EventType.Empty;
    public bool mainPath;
}
