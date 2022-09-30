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
        timeToNextTurn = turnsBreak;
        if (currentTurn == nrTurns){
            currentTurn = 0;
        }
        if (currentTurn == playerTurn){
            yield return PlayerTurn();
            timeToNextTurn = turnsBreak;
            turnEnded = true;
            yield break;
        }
        CharacterObject[] charactersTmp = getList(currentTurn).ToArray();
        if (charactersTmp.Length == 0){
            yield return NextTurn();
            yield break;
        }
        foreach (CharacterObject character in charactersTmp){
            if (Vector2.Distance(player.transform.position, character.transform.position) <= activateDis){
                if (character.PlayTurn()){
                    yield return new WaitForSeconds(turnsInterval);
                }
            }
        }
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
        if (turn + 1 > nrTurns){
            nrTurns = turn + 1;
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
