using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeStart = 60;

    public Text text;

    public GameObject E2;
    public GameObject E3;
    public GameObject E4;
    public GameObject escape;

    public Text alert;
    public Text reinf;


    // Start is called before the first frame update
    void Start()
    {
        text.text = timeStart.ToString();
        StartCoroutine(AlertCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        text.text = Mathf.Round(timeStart).ToString();

        if(timeStart <= 0)
        {
            timeStart = 0;
            text.text = ("");
        }
    }
        

   IEnumerator AlertCoroutine()
    {
        yield return new WaitForSeconds(5);
        alert.text = ("Se ha activado la alarma!");
        yield return new WaitForSeconds(5);
        reinf.text = ("Refuerzos han llegado!");
        E2.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Siren");

        yield return new WaitForSeconds(5);
        alert.text = ("");
        reinf.text = ("");

        yield return new WaitForSeconds(10);
        reinf.text = ("Refuerzos han llegado!");
        E3.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Radio1");

        yield return new WaitForSeconds(5);
        reinf.text = ("");

        yield return new WaitForSeconds(15);
        reinf.text = ("Refuerzos han llegado!");
        E4.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Siren");

        yield return new WaitForSeconds(5);
        reinf.text = ("");
        FindObjectOfType<AudioManager>().Play("Radio2");

        yield return new WaitForSeconds(10);
        reinf.text = ("El punto de escape esta listo. Escapa ya!");
        escape.SetActive(true);
        
    }
}
