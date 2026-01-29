using UnityEngine;

public class BuffManger : MonoBehaviour
{

    float temp;
    float timer;
    float currentDuration;

    bool isBuffed = false;
    bool timerstartedf = false;

    Movement movement;

    // Update is called once per frame
    void Update()
    {
        if (timerstartedf)
        {
            
            timer = timer + Time.deltaTime;
            if (timer >= currentDuration)
            {
                isBuffed = false;
                timerstartedf = false;
                movement.maxSpeed = temp;
            }
        }

    }

    public void applyBuff(Movement movement, float buffDuration, float buffAmount)
    {
       if (isBuffed)
       {
            currentDuration += buffDuration;
            return;
       }
       this. movement = movement;
        temp = movement.maxSpeed;
       movement.maxSpeed = (1+buffAmount)*movement.maxSpeed;
       isBuffed = true;
       StartTimer(buffDuration);
    }

    private void StartTimer(float duration)
    {
        timer = 0f;
        currentDuration = duration;
        timerstartedf = true;
    }
}
