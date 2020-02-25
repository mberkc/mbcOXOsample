using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject gamePanel, countText, sizeInputField;

    public GameObject verticalLayoutPrefab, horizontalLayoutPrefab, buttonPrefab;

    static int matchCount = 0; // Match Count

    int gridSize = 5; // Default Size

    void Start () {
        // Creates Buttons for default 5 x 5 Size
        BuildGridSystem ();
    }

    // Resets Match Count on each build 
    void ResetMatchCount () {
        matchCount = 0;
        UpdateMatchCountText ();
    }

    // Increases Match Count
    public void NewMatch () {
        matchCount++;
        UpdateMatchCountText ();
    }

    void UpdateMatchCountText () {
        countText.GetComponent<TextMeshProUGUI> ().text = "Match Count: " + matchCount;
    }

    public void GridSizeChanged () {
        // Updates Grid Size variable whenever Inputfield is updated
        gridSize = int.Parse (sizeInputField.GetComponent<TMP_InputField> ().text);

    }

    // Creates Buttons : gridSize x gridSize 
    public void BuildGridSystem () {
        // Destroying Old Layout
        if (gamePanel.transform.childCount > 0)
            Destroy (gamePanel.transform.GetChild (0).gameObject);

        // Instantiating new Layout and setting it's offset values
        GameObject verticalLayout = Instantiate (verticalLayoutPrefab);
        verticalLayout.name = "VerticalLayout";
        verticalLayout.transform.SetParent (gamePanel.transform);
        RectTransform vLayoutRT = verticalLayout.GetComponent<RectTransform> ();
        vLayoutRT.offsetMin = new Vector2 (0, 0);
        vLayoutRT.offsetMax = new Vector2 (0, 0);

        // Instantiating Buttons
        for (int i = 0; i < gridSize; i++) {
            GameObject horizontalLayout = Instantiate (horizontalLayoutPrefab);
            horizontalLayout.name = "HorizontalLayout " + (i + 1);
            for (int j = 0; j < gridSize; j++) {
                Instantiate (buttonPrefab).transform.SetParent (horizontalLayout.transform);
            }
            horizontalLayout.transform.SetParent (verticalLayout.transform);
        }
        ResetMatchCount ();
    }
}