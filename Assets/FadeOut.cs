using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(0, 1.5f).OnComplete(() => { fadeImage.gameObject.SetActive(false); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
