using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SpriteRenderer playerGraphic;
    public GameObject arrowObject;

    [Header("STATS")]
    public float power;
    public float energyConsumption;
    public float regenEnergy;
    public Image energyUI;

    [Header("SOUNDS")]
    public AudioClip jumpSound;
    public AudioClip checkpointSound;

    private float currentEnergy;
    private Rigidbody2D playerRb;
    private Vector3 checkpointPos;
    private AudioSource audioSource;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpointPos = transform.position;
        currentEnergy = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.singleton.IsPlaying) return;

        UpdateAngle();
        InputPlayer();
        CheckPlayerFacing();

        RegenEnergy();
        UpdateEnergyUI();
    }

    private void InputPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void UpdateAngle()
    {
        var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = currentMousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        arrowObject.transform.rotation = angleAxis;
    }

    private void Jump()
    {
        if (currentEnergy - energyConsumption < 0) return;

        Vector3 direction = Quaternion.AngleAxis(arrowObject.transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.right;

        playerRb.AddForce(direction * power);

        audioSource.PlayOneShot(jumpSound);

        currentEnergy -= energyConsumption;
    }

    private void CheckPlayerFacing()
    {
        if (playerRb.velocity.x < 0)
        {
            playerGraphic.flipX = true;
        }
        else if (playerRb.velocity.x > 0)
        {
            playerGraphic.flipX = false;
        }
    }

    private void ResetPosToCheckpoint()
    {
        playerRb.velocity = Vector3.zero;
        transform.position = checkpointPos;
    }

    #region Energy
    private void RegenEnergy()
    {
        if (currentEnergy == 100) return;

        currentEnergy += regenEnergy * Time.deltaTime;

        if (currentEnergy > 100)
        {
            currentEnergy = 100;
        }
    }

    private void UpdateEnergyUI()
    {
        energyUI.fillAmount = currentEnergy / 100;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.singleton.IsPlaying) return;

        if (collision.tag == "Checkpoint")
        {
            checkpointPos = collision.gameObject.transform.position;
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(checkpointSound);
        }

        if (collision.tag == "Lose")
        {
            ResetPosToCheckpoint();
        }

        if (collision.tag == "Win")
        {
            GameManager.singleton.Win();
        }
    }
}
