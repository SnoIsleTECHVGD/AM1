using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotWalkingAway : MonoBehaviour
{
    float positionSpeed = 0.4f;
    float sizeSpeed = 1f;

    float time = 0;

    bool active = true;

    [SerializeField]
    private GameObject textObject;

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            float deltaTime = Time.deltaTime;
            time += deltaTime;

            RectTransform picture = GetComponent<RectTransform>();
            picture.anchoredPosition = new Vector2(picture.anchoredPosition.x, picture.anchoredPosition.y + (positionSpeed * deltaTime));

            if (time >= 0.5f)
            {
                picture.localScale = Vector3.Lerp(picture.localScale, new Vector3(0, 0, 0), deltaTime * sizeSpeed);
            }

            if (time >= 2.4f)
                Win();
        }
    }

    // Win

    void Win()
    {
        active = false;
        GetComponent<SpriteRenderer>().enabled = false;

        textObject.GetComponent<Text>().enabled = true;
        textObject.GetComponent<AudioSource>().Play();

        StartCoroutine(loadScreen());
    }

    IEnumerator loadScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
    }
}
