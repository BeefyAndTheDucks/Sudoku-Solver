using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour
{
    public bool value = false;

    public Sprite OffTexture;
    public Sprite SwitchingTexture;
    public Sprite OnTexture;

    Image image;

    void Start() {
        image = GetComponent<Image>();

        if (value) {
            image.sprite = OnTexture;
        } else {
            image.sprite = OffTexture;
        }
    }

    public void OnClick() {
        value = !value;

        StartCoroutine("SwitchState");
    }

    IEnumerator SwitchState() {
        image.sprite = SwitchingTexture;

        yield return new WaitForSeconds(0.05f);

        if (value) {
            image.sprite = OnTexture;
        } else {
            image.sprite = OffTexture;
        }
    }
}
