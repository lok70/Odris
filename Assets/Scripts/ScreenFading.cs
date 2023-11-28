using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFading : MonoBehaviour
{

    private static Image image;

    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        image.enabled = false;
    }

    //Затемнить экран
    public static IEnumerator ShadeScreen(float fade_speed = 1f)
    {

        image.enabled = true;

        Color color = image.color;

        while (color.a < 1f)
        {
            color.a += fade_speed * Time.deltaTime;
            image.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
    }

    //Из темного экрана в обычный
    public static IEnumerator UnshadeScreen(float fade_speed = 1f)
    {
        Color black_a = new Color(0, 0, 0, 1);

        image.color = black_a;

        image.enabled = true;

        Color color = image.color;

        while (color.a > 0f)
        {
            color.a -= fade_speed * Time.deltaTime;
            image.color = color;
            yield return null;
        }
    }

}
