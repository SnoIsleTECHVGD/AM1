using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotWalkingAway : MonoBehaviour
{
    float speed = 0.4f;
    float time = 0;

    bool active = true;

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            float deltaTime = Time.deltaTime;
            time += deltaTime;

            RectTransform picture = GetComponent<RectTransform>();
            picture.anchoredPosition = new Vector2(picture.anchoredPosition.x, picture.anchoredPosition.y + (speed * deltaTime));
            picture.localScale *= 0.9993f;

            if (time >= 2.4f)
                Win();
        }
    }

    // Win

    void Win()
    {
        active = false;
        GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(loadScreen());
    }

    IEnumerator loadScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
    }
}
