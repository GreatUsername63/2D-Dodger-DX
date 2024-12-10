using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimationEvent : MonoBehaviour
{
    public StartGame startGame;

    void PlayJingle()
    {
        startGame.PlayJingle();
    }
}
