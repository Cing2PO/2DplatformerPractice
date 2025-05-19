using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Hpbar;
    public playerLogic player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Hpbar.value = player.HP;
        Hpbar.maxValue = player.maxHP;
    }
}
