using System.Collections;
using UnityEngine;

public class AudioController : PersistentSingleton<AudioController>

{
    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip jump;
[SerializeField] private AudioClip death;
  [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource SFX;
    public void PlayJump()
    {
        SFX.clip = jump;
        SFX.Play();
    }
    public void Playdeath()
    {
        SFX.clip = death;
        SFX.Play();
    }
    public void PlayBackground()
    {
        music.clip = background;
        music.Play();
       
    }
    private IEnumerator Start()
    {
        Debug.Log("audioContoller start");
        yield return new WaitForSeconds(4f);
        Debug.Log("music start");
        PlayBackground();
    }
}
