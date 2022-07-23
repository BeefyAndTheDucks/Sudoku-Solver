using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board
{
    const int blockDim = 3;
    const int boardDim = 9;

    public State[,] startStates = new State[boardDim, boardDim] { 
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY },
        { State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY, State.EMPTY }
    };

    public Cell[,] cells = new Cell[boardDim,boardDim];
    Block[,] blocks = new Block[3,3];
    public GameObject[,] blockGOs = new GameObject[3,3];
    Row[] rows = new Row[boardDim];
    Coloumn[] coloumns = new Coloumn[boardDim];
    AudioClip pieceUpdate;
    AudioSource audioSource;

    Main main;

    public Board(Transform canvas, GameObject cellPrefab, GameObject entropyPrefab, bool loadData, Main main, AudioSource audioSource, Sprite[,] sprites) {
        this.main = main;
        this.audioSource = audioSource;

        canvas.GetComponent<GridLayoutGroup>().constraintCount = boardDim;

        if (!loadData) {
            for (int y = 0; y < boardDim; y++)
            {
                for (int x = 0; x < boardDim; x++)
                {
                    startStates[x, y] = State.EMPTY;
                }
            }
        }

        // Create Cells
        for (int y = 0; y < boardDim; y++)
        {
            for (int x = 0; x < boardDim; x++)
            {
                GameObject newCell = (GameObject) Object.Instantiate(cellPrefab, canvas);
                cells[x,y] = new Cell(newCell, entropyPrefab, this, startStates[y, x], audioSource, sprites[x % 3, y % 3]);
            }
        }

        // Create Blocks
        int cellY = -1;
        for (int y = 0; y < boardDim; y += blockDim)
        {
            cellY += 1;
            int cellX = -1;
            for (int x = 0; x < boardDim; x += blockDim)
            {
                cellX += 1;
                blocks[cellX, cellY] = new Block(x, y, cells, blockDim);
            }
        }

        // Create Rows
        for (int y = 0; y < boardDim; y++)
        {
            rows[y] = new Row(y, cells, boardDim);
        }

        // Create Coloumns
        for (int x = 0; x < boardDim; x++)
        {
            coloumns[x] = new Coloumn(x, cells, boardDim);
        }

        SolveEntropy();
    }

    public void SaveStates() {
        for (int y = 0; y < boardDim; y++)
        {
            for (int x = 0; x < boardDim; x++)
            {
                startStates[y, x] = cells[x, y].state;
            }
        }
    }

    public void LoadFromPlayerPrefs(string saveSlot) {
        string saved = PlayerPrefs.GetString(saveSlot, "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");

        int i = -1;
        for (int y = 0; y < boardDim; y++)
        {
            for (int x = 0; x < boardDim; x++)
            {
                i++;
                switch (saved[i].ToString())
                {
                    case "e":
                        cells[x, y].state = State.EMPTY;
                        break;
                    case "1":
                        cells[x, y].state = State.ONE;
                        break;
                    case "2":
                        cells[x, y].state = State.TWO;
                        break;
                    case "3":
                        cells[x, y].state = State.THREE;
                        break;
                    case "4":
                        cells[x, y].state = State.FOUR;
                        break;
                    case "5":
                        cells[x, y].state = State.FIVE;
                        break;
                    case "6":
                        cells[x, y].state = State.SIX;
                        break;
                    case "7":
                        cells[x, y].state = State.SEVEN;
                        break;
                    case "8":
                        cells[x, y].state = State.EIGHT;
                        break;
                    case "9":
                        cells[x, y].state = State.NINE;
                        break;
                }
                cells[x, y].RefillEntropy();
                cells[x, y].RepresentState();
            }
        }
        SaveStates();
    }

    public void ClearCells() {
        foreach (Cell cell in cells)
        {
            cell.state = State.EMPTY;
            cell.RefillEntropy();
            cell.RepresentState();
            SaveStates();
        }
    }

    public void SaveToPlayerPrefs(string saveSlot) {
        SaveStates();
        string result = "";

        for (int y = 0; y < boardDim; y++)
        {
            for (int x = 0; x < boardDim; x++)
            {
                switch (startStates[y, x])
                {
                    case State.EMPTY:
                        result += "e";
                        break;
                    case State.ONE:
                        result += "1";
                        break;
                    case State.TWO:
                        result += "2";
                        break;
                    case State.THREE:
                        result += "3";
                        break;
                    case State.FOUR:
                        result += "4";
                        break;
                    case State.FIVE:
                        result += "5";
                        break;
                    case State.SIX:
                        result += "6";
                        break;
                    case State.SEVEN:
                        result += "7";
                        break;
                    case State.EIGHT:
                        result += "8";
                        break;
                    case State.NINE:
                        result += "9";
                        break;
                }
            }
        }

        PlayerPrefs.SetString(saveSlot, result);
        PlayerPrefs.Save();
    }

    public void ResetStates() {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                cells[x, y].state = startStates[y, x];
                cells[x, y].RepresentState();
            }
        }
    }

    public IEnumerator Solve(bool realTimeCalculations, float timeToShow) {
        while (true) {
            SolveEntropy();

            Cell lowestEntropyCell = GetLowestEntropy();

            if (lowestEntropyCell == null || CheckForInvalid()) {
                Debug.Log("Couldn't solve... Trying Again!");
                for (int y = 0; y < boardDim; y++)
                {
                    for (int x = 0; x < boardDim; x++)
                    {
                        cells[x, y].state = startStates[y, x];
                    }
                }

                yield return new WaitForSeconds(timeToShow);

                continue;
            }

            int chosenEntropyIdx = Random.Range(0, lowestEntropyCell.entropy.Count);
            //int chosenEntropyIdx = 0;
            int chosenEntropy = lowestEntropyCell.entropy[chosenEntropyIdx];

            switch (chosenEntropy)
            {
                case 1:
                    lowestEntropyCell.state = State.ONE;
                    break;
                case 2:
                    lowestEntropyCell.state = State.TWO;
                    break;
                case 3:
                    lowestEntropyCell.state = State.THREE;
                    break;
                case 4:
                    lowestEntropyCell.state = State.FOUR;
                    break;
                case 5:
                    lowestEntropyCell.state = State.FIVE;
                    break;
                case 6:
                    lowestEntropyCell.state = State.SIX;
                    break;
                case 7:
                    lowestEntropyCell.state = State.SEVEN;
                    break;
                case 8:
                    lowestEntropyCell.state = State.EIGHT;
                    break;
                case 9:
                    lowestEntropyCell.state = State.NINE;
                    break;
            }

            lowestEntropyCell.RepresentState();

            bool done = true;

            foreach (Cell cell in cells)
            {
                if (cell.state == State.EMPTY) {
                    done = false;
                    break;
                }
            }

            if (done) {
                Debug.Log("Solved!");
                SolveEntropy();
                main.calculating = false;
                yield return null;
                break;
            } else {
                if (!realTimeCalculations) {
                    // yield return new WaitForSeconds(0);
                } else {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        
    }

    public void SolveEntropy() {
        foreach (Cell cell in cells)
        {
            cell.RefillEntropy();
        }
        foreach (Block block in blocks)
        {
            block.SolveEntropy();
        }
        foreach (Row row in rows)
        {
            row.SolveEntropy();
        }
        foreach (Coloumn coloumn in coloumns)
        {
            coloumn.SolveEntropy();
        }

        foreach (Cell cell in cells)
        {
            cell.RepresentState(true);
        }
    }

    Cell GetLowestEntropy() {
        SolveEntropy();
        Cell bestCell = null;
        foreach (Cell cell in cells)
        {
            if (cell.entropy.Count > 0 && cell.state == State.EMPTY) {
                bestCell = cell;
                break;
            }
        }
        if (bestCell != null) {

            foreach (Cell cell in cells)
            {
                if (cell.entropy.Count != 0 && cell.state == State.EMPTY) {
                    //return null;
                }
                if (cell.entropy.Count < bestCell.entropy.Count) {
                    if (cell.entropy.Count > 0 && cell.state == State.EMPTY) {
                        bestCell = cell;
                        
                    }
                }
            }

            return bestCell;
        }
        return null;
    }

    bool CheckForInvalid() {
        foreach (Cell cell in cells)
        {
            if (cell.entropy.Count == 0 && cell.state == State.EMPTY)
                return true;
        }
        return false;
    }
}
