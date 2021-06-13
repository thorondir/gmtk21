using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    Animation anim;
    bool active;
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        anim = GetComponent<Animation>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(1,1,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && active) {
            SceneManager.LoadScene(0);
        }
    }

    public void Lose() {
        active = true;
        this.gameObject.SetActive(true);
        anim.Play();
    }
}
