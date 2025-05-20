using UnityEngine;
using UnityEngine.UI;

public class bossHPbar : MonoBehaviour
{
    public Slider Hpbar;
    public KnightBossLogic boss;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Hpbar.value = boss.HP;
        Hpbar.maxValue = boss.maxHP;

        {
            
        }
        if (boss.HP <= 0)
        {
            Hpbar.gameObject.SetActive(false);
        }
    }

}
