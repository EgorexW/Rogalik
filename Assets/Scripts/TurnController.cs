using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField]
    private int maxTurns;
    [SerializeField]
    private float turnsInterval;
    [SerializeField]
    private float turnsBreak;

    private int currentTurn;
    private bool turnBreak = false;
    private float timeToNextTurn;
    private List<List<CharacterObject>> characters = new List<List<CharacterObject>>();

    void Start(){
        currentTurn = 0;
        timeToNextTurn = turnsInterval;
        for(int i=0; i<maxTurns; i++) {
            getList(i);
        }
    }

    private void NextTurn(){
        if (turnBreak) {
            clearAllMoved();
            timeToNextTurn = turnsInterval;
            turnBreak = false;
            currentTurn += 1;
            if (currentTurn >= maxTurns){
                currentTurn = 0;
            }
        } else {
            turnBreak = true;
            timeToNextTurn = turnsBreak;
        }
        // Debug.Log("Turn changed to: " + currentTurn + " break: " + turnBreak);
    }

    void Update(){
        timeToNextTurn -= Time.deltaTime;
        if (timeToNextTurn <= 0) {
            if (turnBreak || CheckIfAllMoved()) {
                NextTurn();
            }
        }
    }

    bool CheckIfAllMoved(){
        foreach (CharacterObject character in getList(currentTurn)){
            if (character.moved == false){
                return false;
            }
        }
        return true;
    }

    void clearAllMoved(){
        foreach (CharacterObject character in getList(currentTurn)){
            character.moved = false;
        }
    }

    public int GetCurrentTurn() {
        if (turnBreak) {
            return -1;
        }
        return currentTurn;
    }

    private List<CharacterObject> getList(int turn) {
        List<CharacterObject> list = null;
        if (characters.Count>turn) {
            list = characters[turn];
        }
        if (list == null) {
            list = new List<CharacterObject>();
            characters.Insert(turn, list);
        }
        return list;
    }

    public void RegisterObjectInTurn(int turn, CharacterObject gObject) {
        getList(turn).Add(gObject);
    }

    public void UnregisterObjectInTurn(int turn, CharacterObject gObject) {
        getList(turn).Remove(gObject);
    }
}
