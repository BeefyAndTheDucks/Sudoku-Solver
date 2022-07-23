using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [HideInInspector]
    public bool calculating = false;

    [Header("Important")]
    public Transform canvas;
    public GameObject cellPrefab;
    public GameObject entropyPrefab;
    public Sprite[] sprites = new Sprite[9];
    Sprite[,] sprites2D = new Sprite[3,3];

    [Header("Settings")]
    public bool realTimeCalculations = true;
    public bool loadPremadeData = true;
    [Range(0,1)]
    public float timeItShowsBoardBeforeRetry = 0.5f;

    public Board board;

    IEnumerator coroutine;

    void Start() {
        int idx = -1;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                idx++;

                sprites2D[x, y] = sprites[idx];
            }
        }

        board = new Board(canvas, cellPrefab, entropyPrefab, loadPremadeData, this, GetComponent<AudioSource>(), sprites2D);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            calculating = !calculating;
            board.SaveStates();
            
            if (calculating) {
                coroutine = board.Solve(realTimeCalculations, timeItShowsBoardBeforeRetry);
                StartCoroutine(coroutine);
            } else {
                StopCoroutine(coroutine);
                
                board.ResetStates();
            }
        }
    }
}
