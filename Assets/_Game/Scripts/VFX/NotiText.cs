using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class NotiText : GameUnit
{
    [SerializeField] private Text notiText;
    public void SetText(string text)
    {
        notiText.text = text;
    }
   
    private IEnumerator OnDespawn()
    {
        yield return new WaitForSeconds(2f);
        SimplePool.Despawn(this);
    }

}
