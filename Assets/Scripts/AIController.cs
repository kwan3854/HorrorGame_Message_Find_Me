using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private AudioClip chasingSound;
    [SerializeField] private AudioClip patrolSound;
    [SerializeField] private float startWaitTime = 1;
    [SerializeField] private float timeToRotate = 1;
    [SerializeField] private float walkSpeed = 6;
    [SerializeField] private float runSpeed = 9;

    [SerializeField] private float viewRadius = 60;
    [SerializeField] private float viewAngle = 360;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float meshResolution = 1f;
    [SerializeField] private int edgeIterations = 4;
    [SerializeField] private float edgeDistance = 0.5f;

    [SerializeField] private Transform[] waypoints;
    private int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;
    GameObject player;
    ParticleSystem particle;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_IsPlayerNear;
    bool m_IsPatroling;
    bool m_CaughtPlayer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Assert(player != null, "Player not found");

        particle = GetComponentInChildren<ParticleSystem>();
        Debug.Assert(particle != null, "Particle not found");

        m_PlayerPosition = Vector3.zero;
        m_IsPatroling = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void Update()
    {
        EnvironmentView();

        if (!m_IsPatroling)
        {
            //playing chasing sound by starting the coroutine if not already playing
            if (GetComponent<AudioSource>().clip != chasingSound)
            {
                StartCoroutine(PlayChasingSound());
                particle.startColor = Color.blue;
            }
            Chasing();
        }
        else
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                StartCoroutine(PlayPatrolingSound());
                particle.startColor = Color.red;
            }
            Patroling();
        }
    }

    private void Chasing()
    {
        m_IsPlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(runSpeed);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }
        else
        {
            //Caught Player
            // play sound
            // disapear
            // game over
        }
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            Debug.Log("TESTESTESTTEST");
            if (m_WaitTime <= 0 && !m_CaughtPlayer &&
            Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 30f)
            {
                m_IsPatroling = true;
                m_IsPlayerNear = false;
                Move(walkSpeed);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                Debug.Log("ChasingOut");
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 15f)
                {
                    Debug.Log("ChaseStop");
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Patroling()
    {
        if (m_IsPlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(walkSpeed);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_IsPlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(walkSpeed);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3f)
        {
            if (m_WaitTime <= 0)
            {
                m_IsPlayerNear = false;
                Move(walkSpeed);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatroling = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
        }
        if (m_PlayerInRange)
        {
            m_PlayerPosition = player.transform.position;
        }
    }

    private IEnumerator PlayChasingSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = chasingSound;
        audioSource.spatialBlend = 0;
        audioSource.Play();
        yield return new WaitForSeconds(chasingSound.length);
        audioSource.Stop();
    }

    private IEnumerator PlayPatrolingSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = patrolSound;
        audioSource.spatialBlend = 1;
        audioSource.Play();
        yield return new WaitForSeconds(patrolSound.length);
        audioSource.Stop();
    }
}
