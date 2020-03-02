using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeWin : MonoBehaviour
{
    public GameObject playerCollider;
    Collider otherCollider;
    LevelChanger changer;
    
    public static bool win;
    public Text winText;


    // Start is called before the first frame update
    void Start()
    {
        otherCollider = playerCollider.GetComponent<Collider>();
        win = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(win);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            otherCollider.enabled = !otherCollider.enabled;
            winText.color = Color.green;
            winText.text = ("Objetivo cumplido");
            win = true;

        }
    }
}
