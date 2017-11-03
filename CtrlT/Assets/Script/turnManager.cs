
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnManager : MonoBehaviour {

    public int enviTurn { get; set; }
    public int heroTurn { get; set; }
    public int typeHeroSkill; // 1 = normal, 2 = freeze , 3 = star
    public int countStarWin; // count of star that hero must collect to win
    public List<int> savedTurn { get; set; }
    public int actionPoint;
    public Vector3 winPoint;
    public bool isEndGame { get; set; }
    public bool isWarp;
    void Start () {
        savedTurn = new List<int>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
