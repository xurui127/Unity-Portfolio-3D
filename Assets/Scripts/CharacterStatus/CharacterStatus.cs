using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character.Animation;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] CharacterStatus_SO characterStatus;
    CharacterAnimationHandler anim;
    [SerializeField] GameObject floatingTextprefab;

    public float MaxHealth
    {
        get { if (characterStatus != null) return characterStatus.MaxHealth; else return 0; }
        set { characterStatus.MaxHealth = value; }
    }
    public float CurrentHealth
    {
        get { if (characterStatus != null) return characterStatus.CurrentHealth; else return 0; }
        set { characterStatus.CurrentHealth = value; }
    }
    public float MaxMana
    {
        get { if (characterStatus != null) return characterStatus.MaxMana; else return 0; }
        set { characterStatus.MaxMana = value; }
    }
    public float CurrentMana
    {
        get { if (characterStatus != null) return characterStatus.CurrentMana; else return 0; }
        set { characterStatus.CurrentMana = value; }
    }
    public float MaxDamage
    {
        get { if (characterStatus != null) return characterStatus.MaxDamage; else return 0; }
        set { characterStatus.MaxDamage = value; }
    }
    public float MinDamage
    {
        get { if (characterStatus != null) return characterStatus.MinDamage; else return 0; }
        set { characterStatus.MinDamage = value; }
    }

    public void IsDead()
    {
        if (CurrentHealth <= 0)
        {
            anim.Death = true;
            anim.SetDeath();
        }
    }
   
    public void TakeDamage(CharacterStatus attacker)
    {
        CurrentHealth -= attacker.GetDamage();
    }
    public void FloatingDamageText(CharacterStatus attacker)
    {
        //Transform targetPos = attacker.gameObject.transform;
        Quaternion quaternion = Camera.main.transform.rotation;
        var text = Instantiate(floatingTextprefab, gameObject.transform.position, quaternion);
        text.GetComponent<TextMesh>().text = attacker.GetDamage().ToString();
    }
    public int GetDamage()
    {
        return  (int)Random.Range(MinDamage, MaxDamage);
    }

   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<CharacterAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fix Later In GameManger 
        IsDead();
   
    }

}
