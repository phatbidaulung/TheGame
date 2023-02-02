using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            int index = Random.Range(0, 2);
            switch (index)
            {
                case 0:
                    SoundManager.Instance.PlaySound(EActionSound.CarBeep01);
                    break;
                case 1:
                    SoundManager.Instance.PlaySound(EActionSound.CarBeep02);
                    break;
                case 2:
                    SoundManager.Instance.PlaySound(EActionSound.CarBeep03);
                    break;
            }
        }
    }
}
