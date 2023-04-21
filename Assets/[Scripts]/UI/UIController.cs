using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject miniMap;
    public TMP_Text startButtonLabel;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (miniMap != null)
        {
            // Toggle MiniMap
            if (Input.GetKeyDown(KeyCode.M))
            {
                miniMap.SetActive(!miniMap.activeInHierarchy);
            }
        }
        
    }

    public void OnStartButton_Pressed()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButton_Pressed()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnStartButton_Down()
    {
        startButtonLabel.rectTransform.localPosition = new Vector3(0.0f, -7.5f);
    }

    public void OnStartButton_Up()
    {
        startButtonLabel.rectTransform.localPosition = new Vector3(0.0f, 6.0f);
    }
}
