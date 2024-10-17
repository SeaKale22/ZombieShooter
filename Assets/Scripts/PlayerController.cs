using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxHealth;
    public float maxMana;
    public float manaRegenRate;
    
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text scoreText;

    private float _currentHealth;
    private float _currentMana;

    public float pointsToWin;
    private float _currentPoints = 0;

    private bool gainMana = true;
    void Start()
    {
        _currentHealth = maxHealth;
        _currentMana = maxMana;
    }
    
    void Update()
    {
        healthText.text = $"Health: {_currentHealth}";
        manaText.text = $"Mana: {_currentMana}";
        if (gainMana)
        {
            StartCoroutine(ManaGen());
        }

        scoreText.text = $"Score: {_currentPoints}";
    }

    IEnumerator ManaGen()
    {
        if (_currentMana < maxMana)
        {
            _currentMana += 1;
            gainMana = false;
            yield return new WaitForSeconds(manaRegenRate);
            gainMana = true;
        }
        
    }
    public void LooseHealth(float value)
    {
        _currentHealth -= value;
        //check if alive
        LifeCheck();
        
    }

    public void LifeCheck()
    {
        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene("LooseScene");
        }
    }

    public void ScorePoint()
    {
        _currentPoints += 1;
        if (_currentPoints >= pointsToWin)
        {
            SceneManager.LoadScene(3);
        }
    }
    public void LooseMana(float value)
    {
        _currentMana -= value;
    }

    public bool ManaCheck(float manaCost)
    {
        if (manaCost <= _currentMana)
        {
            return true;
        }

        return false;
    }
}
