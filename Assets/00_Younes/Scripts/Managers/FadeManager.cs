using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager _instance;

    [SerializeField][Range(.1f, 3f)] float _defaultTransitionDuration = 1f;
    [SerializeField] Image _screenFader;
    [SerializeField] Color _normalColor;
    [SerializeField] Color _blackColor;


    private void Awake() {
        if (_instance == null){
            _instance = this;
        } else{
            Destroy(this);
        }
    }

    public IEnumerator FadeToBlack(){

        float timeElapsed = 0;

        while (timeElapsed < _defaultTransitionDuration){

            timeElapsed += Time.deltaTime;
            _screenFader.color = Color.Lerp(_normalColor, _blackColor, timeElapsed / _defaultTransitionDuration);
            yield return null;    
        }
    }

    public IEnumerator FadeToNormal(){

        float timeElapsed = 0;

        while (timeElapsed < _defaultTransitionDuration){

            timeElapsed += Time.deltaTime;
            _screenFader.color = Color.Lerp(_blackColor, _normalColor, timeElapsed / _defaultTransitionDuration);
            yield return null;    
        }
    }

    [ContextMenu("Fade To Black")]
    public void FadeToBlackButton(){
        StartCoroutine(FadeToBlack());
    }

    [ContextMenu("Fade To Normal")]
    public void FadeToNormalButton(){
        StartCoroutine(FadeToNormal());
    }

}
