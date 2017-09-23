using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private static BackgroundMusic audioInstance = null;
    public List<AudioSource> songs;
    private int songPointer;
    private List<bool> songMeta = new List<bool>();
    private List<AudioSource> _songs = new List<AudioSource>();

    public static BackgroundMusic Instance
    {
        get { return audioInstance; }
    } 

    void Awake()
    {
        if ((audioInstance != null) && audioInstance != this){
            Destroy(this.gameObject);
            return;
        }
        else
        {
            audioInstance = this;
        }
        for(int i = 0; i < songs.Count; i++)
        {
            AudioSource song = Instantiate(songs[i]);
            _songs.Add(song);
            DontDestroyOnLoad(song);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        for(int i = 0; i < songs.Count; i++)
        {
            songMeta.Add(false);
        }
        songPointer = 0;
        StartCoroutine(CheckSong());
    }

    IEnumerator CheckSong()
    {
        while (true)
        {
            if (_songs[songPointer] != null && !_songs[songPointer].isPlaying)
            {
                if (!songMeta[songPointer])
                {
                    _songs[songPointer].Play();
                    songMeta[songPointer] = true;
                }
                else
                {
                    songPointer += 1;
                }
            }
            yield return new WaitForSeconds(3); 
        }
        
    }
}
