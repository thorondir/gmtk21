using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            SceneManager.LoadScene(0);
        }
    }

    public void Lose() {
        this.gameObject.SetActive(true);
        anim.Play();
    }
}
