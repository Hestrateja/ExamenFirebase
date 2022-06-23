using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Countdown countdown;
    [SerializeField] Text coinsText;
    public SpriteRenderer monster;
    public int costeInvocacion = 10;
    public int coins = 0;
    public Sprite huevoComun, huevoRaro, evolucionComun, evolucionRara;
    bool esRaro = false;
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        monster.sprite = null;
    }
    void UpdateCoinText()
    {
        coinsText.text = coins.ToString();
    }
    public void GetCoin()
    {
        coins++;
        UpdateCoinText();
    }
    public void SpawnEgg()
    {
        if (coins >= costeInvocacion)
        {
            coins -= costeInvocacion;
            UpdateCoinText();
            int random = UnityEngine.Random.Range(0, 100);
            if(random < 80)
            {
                SpawnComun();
            }
            else
            {
                SpawnRaro();
            }
              
        }
        else
        {
            Debug.Log("¡No tienes monedas suficientes!");
        }
    }
    void SpawnComun()
    {
        countdown.gameObject.SetActive(false);
        Debug.Log("Huevo común.");
        monster.sprite = huevoComun;
        esRaro = false;
        countdown.gameObject.SetActive(true);
    }
    void SpawnRaro()
    {
        countdown.gameObject.SetActive(false);
        Debug.Log("¡HUEVO RARO!");
        monster.sprite = huevoRaro;
        esRaro = true;
        countdown.gameObject.SetActive(true);
    }
    public void HatchEgg()
    {
        Debug.Log("¡Eclosionó el huevo!");
        if (esRaro)
        {
            monster.sprite = evolucionRara;
        }
        else
        {
            monster.sprite = evolucionComun;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
