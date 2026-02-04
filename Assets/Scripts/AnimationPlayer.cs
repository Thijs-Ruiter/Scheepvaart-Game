using UnityEngine;
using UnityEngine.Video;

public class AnimationPlayer : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoClip[] vClips;
    public Animation[] animations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        //animations[0] = new Animation();
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

    public class Animation
    {
        int clip_index;
        VideoClip clip;

        public Animation(int clip_index, VideoClip clip)
        {
            this.clip_index = clip_index;
            this.clip = clip;
        }
    }
}
