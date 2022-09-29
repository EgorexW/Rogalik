using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField]
    private int nrTurns;
    [SerializeField]
    private float turnsInterval;
    [SerializeField]
    private float turnsBreak;
    [SerializeField] float activateDis;

    private int currentTurn;
    private bool turnEnded = true;
    private float timeToNextTurn;
    private List<List<CharacterObject>> characters = new List<List<CharacterObject>>();

    CharacterObject player = null;
    int playerTurn = -1;

    void Start(){
        currentTurn = 0;
        timeToNextTurn = turnsInterval;
        for(int i=0; i<nrTurns ; i++) {
            getList(i);
        }
    }

    IEnumerator NextTurn(){
        currentTurn += 1;
        turnEnded = false;
        if (currentTurn == nrTurns){
            currentTurn = 0;
        }
        if (currentTurn == playerTurn){
            yield return PlayerTurn();
            timeToNextTurn = turnsBreak;
            turnEnded = true;
        }
        else {
            foreach (CharacterObject character in getList(currentTurn)){
                if (Vector2.Distance(player.transform.position, character.transform.position) <= activateDis){
                    if (character.PlayTurn()){
                        yield return new WaitForSeconds(turnsInterval);
                    }
                }
            }
        }
        timeToNextTurn = turnsBreak;
        turnEnded = true;
    }

    IEnumerator PlayerTurn(){
        while(!player.PlayTurn()){
            yield return null;
        }
    }

    void Update(){
        if (player == null){
            return;
        }
        timeToNextTurn -= Time.deltaTime;
        if (turnEnded && timeToNextTurn <= 0){
            StartCoroutine(NextTurn());
        }
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

    public void RegisterObjectInTurn(int turn, CharacterObject gObject, bool isPlayer = false) {
        if (isPlayer) {
            player = gObject;
            playerTurn = turn;
            return;
        }
        getList(turn).Add(gObject);
    }

    public void UnregisterObjectInTurn(int turn, CharacterObject gObject, bool isPlayer = false) {
        if (isPlayer) {
            player = null;
            playerTurn = -1;
            return;
        }
        getList(turn).Remove(gObject);
    }
}
