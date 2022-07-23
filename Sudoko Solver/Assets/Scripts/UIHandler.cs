using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text loadTextElement;
    public Text saveTextElement;
    public Text deleteTextElement;
    public Slider slider;
    public CustomToggle toggle;
    public CustomToggle RTToggle;

    public Main main;

    int saveSlot = 1;

    public void OnSliderChanged() {
        saveSlot = (int) slider.value;

        loadTextElement.text = "Load from slot " + saveSlot;
        saveTextElement.text = "Save to slot " + saveSlot;
        deleteTextElement.text = "Delete slot " + saveSlot;
    }

    public void OnLoadButtonClicked() {
        main.board.LoadFromPlayerPrefs(saveSlot.ToString());
    }

    public void OnSaveButtonClicked() {
        main.board.SaveToPlayerPrefs(saveSlot.ToString());
    }

    public void OnDeleteAllClicekd() {
        PlayerPrefs.DeleteAll();
        main.board.ClearCells();
    }

    public void OnDeleteSlotClicked() {
        PlayerPrefs.DeleteKey(saveSlot.ToString());
        main.board.ClearCells();
    }

    public void OnToggleChange() {
        foreach (Cell cell in main.board.cells)
        {
            cell.showEntropy = !toggle.value;
            cell.RepresentState();
        }
    }

    public void OnRTToggleChange() {
        main.realTimeCalculations = !RTToggle.value;
    }

    public void Quit() {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void Clear() {
        main.board.ClearCells();
    }
}
