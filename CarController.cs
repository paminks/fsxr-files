using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{

    public int highestCoins;
    public TextMeshProUGUI highScoreText;
    public double speed;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI gearText;
    public int gear;
    public GameObject androidControls;
    public GameObject androidControls2;
    public GameObject androidControls3;
    public float accelRatio;
    public AIController AICar1;
    public TMP_InputField lapInput;
    public void Start()
    {
        lapsToPlay = 1;
        AICar1 = GameObject.FindGameObjectWithTag("AI1").GetComponent<AIController>();
        accelerationFactor = 1;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();


        csu = GetComponent<CarChooseUI>();
        gear = 0;

        Time.timeScale = 1;

    }

    public TextMeshProUGUI lapTimeText;
    public TextMeshProUGUI validUnvalid;


    public void Update()
    {
        Application.targetFrameRate = 120;
        changeSprite();
        speed = velocitysUp * 3.3;
        int ss = Convert.ToInt32(speed);
        speedText.text = "KM/H: " + ss.ToString();
        int gear1 = Convert.ToInt32(gearText);
        gear1 = gear;
        gearText.text = "" + gear1.ToString();
	//yeah SO EFFICIENT
        if (true)
        {
            if (speed < 0)
            {
                //gear = -1;//r
                gearText.text = "R";
                accelRatio = 2;
            }

            if (40 > speed && speed >= 0)
            {
                gear = 1;
                accelRatio = 4;
            }

            if (speed < 90 && speed > 40)
            {
                gear = 2;
                accelRatio = 2.3f;
            }

            if (speed < 140 && speed > 90)
            {
                gear = 3;
                accelRatio = 2;
            }

            if (speed < 190 && speed > 140)
            {
                gear = 4;
                accelRatio = 1.6f;
            }

            if (speed < 230 && speed > 190)
            {
                gear = 5;
                accelRatio = 1.1f;
            }

            if (speed < 270 && speed > 230)
            {
                gear = 6;
                accelRatio = 1;
            }

            if (speed < 330 && speed > 270)
            {
                gear = 7;
                accelRatio = 0.7f;
            }

            if (speed > 330)
            {
                gear = 8;
                accelRatio = 0.4f;
            }
        }
        lapText.text = completedLaps.ToString() + "/"+lapsToPlay;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        int.TryParse(lapInput.text, out int result);
        lapsToPlay = result;

        enableERS();
        updateErsText();
        LapTime();
        finishRace();

    }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        killOrtaVelocity();
        ApplySteering();
        checkClassification();
//uncomment these for androd builds
        //#if UNITY_ANDROID
        //StartCoroutine(SmoothBrkAND());
        //androidControls.SetActive(true);
        //androidControls2.SetActive(true);
        //androidControls3.SetActive(true);
        //#endif

        StartCoroutine(SmoothBrk());
    }

    #region defines

    public float accelerationFactor = 30f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float accelerationInput;
    public float steeringInput;
    public float rotationAngle;
    public float maxSpeed = 100;
    public float minSpeed = 0;
    public float velocitysUp;
    public float breakInput;
    public CarChooseUI csu;
    public SpriteRenderer spriteRenderer;
//please dont sue me ferrari-mclaren-astonMartin-mercedes :D
    public Sprite ferrariF13;
    public Sprite mclarenMp22;
    public Sprite RenaultR29;
    public Sprite MercedesW11;
    public Sprite AMR23;
    public int orderNum;

    #endregion
	//for choosing the car
    public void changeSprite()
    {
        if (orderNum == 2)
        {
            spriteRenderer.sprite = ferrariF13;
            spriteRenderer.flipY = true;
        }

        if (orderNum == 1)
        {
            spriteRenderer.sprite = mclarenMp22;
        }

        if (orderNum == 3)
        {
            spriteRenderer.sprite = RenaultR29;
        }

        if (orderNum == 4)
        {
            spriteRenderer.sprite = MercedesW11;
        }

        if (orderNum == 5)
        {
            spriteRenderer.sprite = AMR23;

        }
    }
	//the engine code 
    public void ApplyEngineForce()
    {
        velocitysUp = Vector2.Dot(transform.up, rb.velocity);
        if (velocitysUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (velocitysUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }


        if (accelerationInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }

        Vector2 engineForceVector = transform.up * accelerationFactor;
        rb.AddForce(engineForceVector, ForceMode2D.Force);

        Vector2 breakeForce = transform.up * -accelerationFactor * breakInput;
    }

    public int maxAccelFactor = 110;

	//smooth breaking for pc
    public IEnumerator SmoothBrk()
    {
        if (accelerationInput < 0)
        {
            if (accelerationFactor > minSpeed)
            {
                accelerationFactor = accelerationFactor - accelRatio;

            }
        }

        if (1>speed && speed >0 && accelerationFactor >0 && accelerationInput <0 )
        {

                accelerationFactor = accelerationFactor - 180;


        }

        if (breakInput == 0 && accelerationInput > 0 && maxAccelFactor > accelerationFactor)
        {

            accelerationFactor += accelRatio;

        }

        if (accelerationInput == 0 && accelerationFactor > 0)
        {
            accelerationFactor -= 0.6f;
        }
        else
        {
            yield break;
        }
    }
	//for android smooth breaking
    public IEnumerator SmoothBrkAND()
    {
        if (breakInput > 0)
        {
            if (accelerationFactor > minSpeed)
            {

                accelerationFactor = accelerationFactor - 1;

            }
        }

        if (breakInput == 0 && accelerationInput > 0 && maxAccelFactor > accelerationFactor)
        {
            accelerationFactor += 3;
        }

        if (accelerationInput == 0 && accelerationFactor > 0)
        {
            accelerationFactor -= 1;
        }
        else
        {
            yield break;
        }
    }
	//some basic steering but feels good
    public void ApplySteering()
    {
        float minSpeedBeforeAllowingTurn = (rb.velocity.magnitude / 8);
        minSpeedBeforeAllowingTurn = Mathf.Clamp01(minSpeedBeforeAllowingTurn);

        rotationAngle = -steeringInput * turnFactor * minSpeedBeforeAllowingTurn;
        rb.MoveRotation(rb.rotation + rotationAngle);
    }
//this is for the little drift effect
    public void killOrtaVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public void SetInputVector2(Vector2 inputVector2)
    {
        breakInput = inputVector2.y;
    }

    public List<float> laplist = new List<float>();

    public int completedLaps;
    public int maxMapPoints;
    public int mapPoints;        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);

    public float lapTime;
    public TextMeshProUGUI lapvalidatorDevMenu;
    public bool valid;
    public float playerCheckpoints;
    public void OnTriggerEnter2D(Collider2D other)
    {	//checks if the player cuts the road or not
        if (other.CompareTag("MapValidator"))
        {
            mapPoints++;
            lapvalidatorDevMenu.text = "passed " + mapPoints + " validator points";
        }
	//this is for calculating the lap time
        if (other.CompareTag("Checkpoint"))
        {
            playerCheckpoints++;

        }
	//yeah as you can see if the map points are not enough its simply not a valid lap 
        if (other.CompareTag("FinishLine"))
        {

            if (mapPoints >= maxMapPoints)
            {
                validUnvalid.color = new Color(0, 255, 0);
                valid = true;
                completedLaps++;
                return;

            }
            else
            {
                valid = false;
                validUnvalid.color = new Color(250, 0, 0);
                validUnvalid.text = "n/a";

            }
        }

        if (other.CompareTag("SetZero"))
        {
            lapTime = 0.00000f;
            mapPoints = 0;
            Debug.Log(completedLaps + " laps completed");

        }
    }

    public TextMeshProUGUI classificationText;
    public int playerPosition;
	//this is NOT the most efficient way of checking classification PLEASE HELP
    public void checkClassification()
    {
        if (AICar1.AIcheckpoints > playerCheckpoints)
        {
            classificationText.text = "2/2";
            playerPosition = 2;
        }

        if (AICar1.AIcheckpoints <= playerCheckpoints)
        {
            classificationText.text = "1/2";
            playerPosition = 1;
        }


    }

    public int lapsToPlay;
    public TextMeshProUGUI lapText;
//everything with WO is related to win or loss text
    public TextMeshProUGUI wonOrLossText;
    public GameObject WOltGameObject;
    public GameObject woltButton;
    //decides if the player or AI won 
    public void finishRace()
    {
        if (completedLaps == lapsToPlay && playerPosition ==1 && orderNum >0)
        {
            WOltGameObject.SetActive(true);
            woltButton.SetActive(true);
            wonOrLossText.color = new Color(52, 173, 236);
            wonOrLossText.text = "YOU WON!\n Restart?";
            Time.timeScale = 0f;
        }
        if (completedLaps == lapsToPlay && playerPosition == 2 && orderNum >0)
        {
            woltButton.SetActive(true);
            WOltGameObject.SetActive(true);
            wonOrLossText.color = Color.black;
            wonOrLossText.text = "AI 1 WON\n Restart?";
            Time.timeScale = 0f;
        }

    }
    public TextMeshProUGUI bestLap;
	//finisihes the lap 
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FinishLine"))
        {

            if (valid)
            {
                laplist.Add(lapTime);
                bestLap.text = "Best Lap : " + laplist.Min().ToString("F");
                validUnvalid.text = "Last Valid Lap " + lapTime.ToString("F");
            }

            if (!valid)
            {
                lapTime = 0f;
                mapPoints = 0;
            }


        }

    }

    public float bestLaptime;
	//lap time UI and math
    public void LapTime()
    {
        lapTime += Time.deltaTime;
        lapTimeText.text = "Time : " + lapTime.ToString("F");

    }

    public TextMeshProUGUI ersPercentage;
    public TextMeshProUGUI ersEnabledText;
    public float ersTime;
    public bool ersEnabled;
	//updates UI of the ERS, this can be done in the other function but seperating them is always helpful
    public void updateErsText()
    {
        if (ersEnabled)
        {
            ersEnabledText.color = Color.green;
            ersEnabledText.text = "ERS Enabled";
        }
        else
        {
            ersEnabledText.color = Color.red;
            ersEnabledText.text = "ERS Disabled";
        }

        if (ersTime > 0)
        {
            ersPercentage.color = Color.black;
            ersPercentage.text = "%"+ (ersTime * 10).ToString("F");
        }
        else
        {
            ersPercentage.color = Color.red;
            ersPercentage.text = "%0";
        }
    }
    //initializes ERS 
    public void enableERS()
    {
        if (ersEnabled == false && ersTime <= 10f)
        {
            ersTime += 0.00167f;

        }

        if (ersTime <= 0)
        {
            maxAccelFactor = 180;
            accelerationFactor = 180;
            ersEnabled = false;
        }

        if (ersEnabled == false && ersTime >0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                maxAccelFactor = 230;
                ersEnabled = true;
                StartCoroutine(startErsCount());

            }
        }
        else if(ersEnabled == true)
        {
            if (Input.GetKeyDown((KeyCode.LeftShift)))
            {
                maxAccelFactor = 180;
                accelerationFactor = 180;
                ersEnabled = false;
            }
        }

    }
	//eats ERS
    public IEnumerator startErsCount()
    {
        while(ersTime > 0)
        {
            if (ersEnabled == false)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
            ersTime -= 0.1f;
        }
    }



    
}
