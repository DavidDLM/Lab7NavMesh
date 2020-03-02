using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovimientoEnemigo : MonoBehaviour
{
    private NavMeshAgent agent;  

    public Transform[] patrolPoints;

    private int indexControlPoint = 0;
    public float radioVision = 7f;

    Transform target;

    public Text text;
    public Text retry;

    public bool gameLost;
    public bool win;

    //public LevelChanger changer;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        gameLost = false;
        
    }
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            bool patrol = false;
            //MoveToNextPoint();
            patrol = patrolPoints.Length > 0;

            if (!patrol)
                agent.SetDestination(transform.position);
            // Patrolling between points if there are patrol points
            if (patrol)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    MoveToNextPoint();
            }

        }

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance < radioVision)
        {
            agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance)
            {
                faceTarget();
            }
        }

        if (gameLost)
        {
            retry.text = ("R para reintentar \nESC para salir al menu");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void MoveToNextPoint()
    {
        if (patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[indexControlPoint].position;

            indexControlPoint++;
            indexControlPoint %= patrolPoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.SetActive(false);
            text.color = Color.red;
            text.text = ("Escape frustrado.");
            Time.timeScale = 0f;
            gameLost = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioVision);
    }

    void faceTarget()
    {
        Vector3 direction = (target.position + transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 5f);
    }
}
