using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance { get; private set; }

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] sfxClips;

    public static int MUSIC_MENU_THEME = 0;
    public static int MUSIC_LOSE_THEME = 1;
    public static int MUSIC_VICTORY_THEME = 2;
    public static int MUSIC_GAME_THEME = 3;

    public static int SFX_CAT_DEATH = 0;
    public static int SFX_CAT_JUMP = 1;
    public static int SFX_UI_PLAY = 2;
    public static int SFX_UI_BUTTON = 3;
    public static int SFX_GAME_ITEM = 4;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (AudioManagerInstance != null && AudioManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            AudioManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (!GameStateManager.inGame)
        {
            PlayMusic(MUSIC_MENU_THEME);
        }
    }

    public void PlayMusic(int musicIndex, float delayToStart = 0)
    {
        audioSource.resource = musicClips[musicIndex];

        if (delayToStart > 0)
        {
            StartCoroutine(delayToPlay(delayToStart));
        }

        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlaySFX(int sfxIndex)
    {
        audioSource.PlayOneShot(sfxClips[sfxIndex]);
    }

    private IEnumerator delayToPlay(float delayAmount)
    {
        yield return new WaitForSeconds(delayAmount);
    }
}
