using UnityEngine;
using UnityEngine.Video;

public class AnimationPlayer : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoClip[] vClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(int clip_index)
    {
        vp.clip = vClips[clip_index];
        vp.Play();
    }
}
