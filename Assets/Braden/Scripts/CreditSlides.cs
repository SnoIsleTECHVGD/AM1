using UnityEngine;
using UnityEngine.UI;

public class CreditSlides : MonoBehaviour
{
    private int slideNumber = 0;
    public int amountOfSlides;

    private AudioSource selectSound;
    private Image currentSlide;

    // Start is called before the first frame update
    void Start()
    {
        DisplayNextSlide(Random.Range(1, 6));
        selectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            DisplayNextSlide(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            DisplayNextSlide(-1);
    }

    // Main
    public void DisplayNextSlide(int difference)
    {
        slideNumber += difference;

        if (slideNumber > amountOfSlides)
            slideNumber = 1;
        else if (slideNumber <= 0)
            slideNumber = amountOfSlides;

        if (currentSlide)
            currentSlide.enabled = false;

        GameObject findObject = GameObject.Find("Person" + slideNumber.ToString());
        currentSlide = findObject.GetComponent<Image>();

        currentSlide.enabled = true;

        if (selectSound)
            selectSound.Play();
    }
}
