using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Team : ScriptableObject
{
    public List<Team> friends = new List<Team>();
    public List<Team> foes = new List<Team>();
    public List<Team> neutral = new List<Team>();

    public int turn;

    void Awake(){
        if (!friends.Contains(this)){
            friends.Add(this);
        }
    }

    public FriendOrFoe GetFriendOrFoe(Team team){
        if (friends.Contains(team)){
            return FriendOrFoe.Friend;
        }
        if (foes.Contains(team)){
            return FriendOrFoe.Foe;
        }
        if (neutral.Contains(team)){
            return FriendOrFoe.Neutral;
        }
        return FriendOrFoe.Unknown;
    }
}
