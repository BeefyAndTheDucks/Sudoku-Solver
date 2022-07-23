using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell
{
    GameObject cell;
    GameObject entropyPrefab;
    Transform entropyParent;
    public State state = State.EMPTY;
    public List<int> entropy = new List<int>();
    Button button;
    Text text;
    AudioSource audioSource;

    public bool showEntropy;

    public Cell(GameObject cell, GameObject entropyPrefab, Board board, State state, AudioSource audioSource, Sprite sprite) {
        this.cell = cell;
        this.entropyPrefab = entropyPrefab;
        this.state = state;
        this.audioSource = audioSource;
        entropyParent = cell.transform.Find("Entropy");

        cell.GetComponent<Image>().sprite = sprite;

        RefillEntropy();
        button = cell.GetComponentInChildren<Button>();
        text = cell.GetComponentInChildren<Text>();

        button.onClick.AddListener(ChangeState);
        button.onClick.AddListener(board.SolveEntropy);
        RepresentState();
    }

    public void RemoveEntropy(int entropy) {
        this.entropy.Remove(entropy);
    }

    public void RefillEntropy() {
        entropy.Clear();
        for (int i = 1; i < 10; i++) {
            entropy.Add(i);
        }

        foreach (Transform child in entropyParent)
        {
            Object.Destroy(child.gameObject);
        }
    }

    void ChangeState() {
        switch (state)
        {
            case State.EMPTY:
                state = State.ONE;
                break;
            case State.ONE:
                state = State.TWO;
                break;
            case State.TWO:
                state = State.THREE;
                break;
            case State.THREE:
                state = State.FOUR;
                break;
            case State.FOUR:
                state = State.FIVE;
                break;
            case State.FIVE:
                state = State.SIX;
                break;
            case State.SIX:
                state = State.SEVEN;
                break;
            case State.SEVEN:
                state = State.EIGHT;
                break;
            case State.EIGHT:
                state = State.NINE;
                break;
            case State.NINE:
                state = State.EMPTY;
                break;
        }
        RepresentState();
    }

    public void RepresentState(bool dontPlaySounds=false) {
        foreach (Transform child in entropyParent)
        {
            Object.Destroy(child.gameObject);
        }
        bool evaluated = true;
        switch (state)
        {
            case State.EMPTY:
                text.text = "";
                evaluated = false;
                break;
            case State.ONE:
                text.text = "1";
                break;
            case State.TWO:
                text.text = "2";
                break;
            case State.THREE:
                text.text = "3";
                break;
            case State.FOUR:
                text.text = "4";
                break;
            case State.FIVE:
                text.text = "5";
                break;
            case State.SIX:
                text.text = "6";
                break;
            case State.SEVEN:
                text.text = "7";
                break;
            case State.EIGHT:
                text.text = "8";
                break;
            case State.NINE:
                text.text = "9";
                break;
        }

        if (!evaluated && showEntropy) {
            foreach (int ent in entropy)
            {
                GameObject entr = (GameObject) Object.Instantiate(entropyPrefab, entropyParent);
                entr.GetComponent<Text>().text = ent.ToString();
            }
        }

        if (!dontPlaySounds) {
            audioSource.Play();
        }
    }
}
