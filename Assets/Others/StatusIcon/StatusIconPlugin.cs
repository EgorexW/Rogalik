using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIconPlugin : MonoBehaviour
{
    List<StatusIcon> statusIcons = new List<StatusIcon>();

    public void Register(StatusIcon statusIcon){
        statusIcons.Add(statusIcon);
    }

    public void Unregister(StatusIcon statusIcon){
        statusIcons.Remove(statusIcon);
    }

    void LateUpdate(){
        List<StatusIcon> tmp_statusIcons = statusIcons;
        int index = 0;
        foreach (StatusIcon st in tmp_statusIcons){
            st.RunUpdate(index);
            index ++;
        }
    }
}
