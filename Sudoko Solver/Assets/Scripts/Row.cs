using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row
{
    List<int> usedNumbers = new List<int>();

    public Cell[] cells;
    public Row(int y, Cell[,] cells, int boardDim) {
        this.cells = new Cell[boardDim];
        for (int x = 0; x < boardDim; x++)
        {
            this.cells[x] = cells[x, y];
        }
    }

    public void SolveEntropy() {
        foreach (Cell cell in cells)
        {
            switch (cell.state)
            {
                case State.EMPTY:
                    break;
                case State.ONE:
                    usedNumbers.Add(1);
                    break;
                case State.TWO:
                    usedNumbers.Add(2);
                    break;
                case State.THREE:
                    usedNumbers.Add(3);
                    break;
                case State.FOUR:
                    usedNumbers.Add(4);
                    break;
                case State.FIVE:
                    usedNumbers.Add(5);
                    break;
                case State.SIX:
                    usedNumbers.Add(6);
                    break;
                case State.SEVEN:
                    usedNumbers.Add(7);
                    break;
                case State.EIGHT:
                    usedNumbers.Add(8);
                    break;
                case State.NINE:
                    usedNumbers.Add(9);
                    break;
            }    
        }

        foreach (Cell cell in cells)
        {
            foreach (int entr in usedNumbers)
            {
                cell.RemoveEntropy(entr);
            }
        }
        usedNumbers.Clear();
    }
}
