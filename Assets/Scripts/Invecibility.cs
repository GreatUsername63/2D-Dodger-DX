using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invecibility : MonoBehaviour
{
    AudioManager audioManager;
    public bool isActive;
    bool isActiveLastFrame = false;
    Material mt;
    Color32 originalColor;
    LineRenderer lineRenderer1;
    LineRenderer lineRenderer2;
    Gradient originalLinerendererGradient;
    //These 2 arrays are to set the colors and alphas to the gradients
    GradientColorKey[] colorKeys = {
        new GradientColorKey(Color.HSVToRGB(0,1,1),0.0f),
        new GradientColorKey(Color.HSVToRGB(0,1,1),1.0f)
    };
    GradientAlphaKey[] alphaKeys = {
        new GradientAlphaKey(1f,0f),
        new GradientAlphaKey(1f,1f)
    };

    float rainbowHue = 0;
    float colorSaturation = 1f;
    [SerializeField] float hueSpeed = 0.1f;

    //Time on seconds since invincibility got active
    float activationTime = 0f;
    //Time the invencibility should last
    [SerializeField] float powerupTime = 10f;
    //Time when the ships will become almost white
    float warningBeforeDeactivate;


    // Start is called before the first frame update
    void Start()
    {
        warningBeforeDeactivate = powerupTime - 1;

        mt = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        originalColor = mt.color;

        lineRenderer1 = transform.GetChild(1).GetComponent<LineRenderer>();
        lineRenderer2 = transform.GetChild(2).GetComponent<LineRenderer>();

        originalLinerendererGradient = lineRenderer1.colorGradient;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ascending spike
        if (isActive && !isActiveLastFrame)
        {
            //Randomize hue so that both roids don't get the same color
            rainbowHue = Random.Range(0f, 1f);
            //Start timer
            activationTime = Time.realtimeSinceStartup;
            audioManager.PlayInvencibilityTheme();
        }
        if (isActive)
        {
            //If limit value has been passed, loop the color
            if (rainbowHue > 1)
            {
                rainbowHue = 0;
            }
            //Set the colors to material
            mt.color = Color.HSVToRGB(rainbowHue, colorSaturation, 1);
            //Set color to line renderers
            colorKeys[0].color = Color.HSVToRGB(rainbowHue, colorSaturation, 1);
            colorKeys[1].color = Color.HSVToRGB(rainbowHue, colorSaturation, 1);
            Gradient newGradient = new Gradient();
            newGradient.SetKeys(colorKeys, alphaKeys);
            lineRenderer1.colorGradient = newGradient;
            lineRenderer2.colorGradient = newGradient;
            //Increase hue by speed
            rainbowHue += hueSpeed;

            //Check if enough time has passed
            float timePassed = Time.realtimeSinceStartup - activationTime;
            if (timePassed > powerupTime)
            {
                isActive = false;
            }
            //Change the saturation of the material so it turns white just before returning to normal
            if (timePassed > warningBeforeDeactivate)
            {
                colorSaturation = 0.1f;
            }
        }
        //Descending spike
        if (!isActive && isActiveLastFrame)
        {
            //Return the material and line renderers to normal
            mt.color = originalColor;
            lineRenderer1.colorGradient = originalLinerendererGradient;
            lineRenderer2.colorGradient = originalLinerendererGradient;
            //Return the saturation to max for the next time powerup gets picken up
            colorSaturation = 1f;
            audioManager.PlayGameThemeSkipIntro();
        }
        isActiveLastFrame = isActive;
    }
}
