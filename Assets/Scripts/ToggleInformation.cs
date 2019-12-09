using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleInformation : MonoBehaviour
{
    public Button btn;
    public Image image;
    public bool state = false;
    void Awake(){
        btn.onClick.AddListener(TaskOnClick);
        this.GetComponent<Text>().enabled = state;
        image.enabled=state;

	
}

    private void  TaskOnClick(){
        state = !state;
        this.GetComponent<Text>().enabled = state;
        image.enabled=state;

    }
}
