using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Countdown : MonoBehaviour
{
    float time;
    public Player player;
    public Text countdownText;
    [SerializeField] float min, max;

    private void OnEnable()
    {
        countdownText.gameObject.SetActive(true);
        GetRandomTime();
    }
    void GetRandomTime()
    {
        time = Random.Range(min, max);
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        countdownText.text = "Time remaining: " + time.ToString("0.00");
        if(time<=0)
        {
            player.HatchEgg();
            countdownText.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
