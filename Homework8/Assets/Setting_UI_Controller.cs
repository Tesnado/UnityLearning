using UnityEngine;
using System.Collections;

public class Setting_UI_Controller : MonoBehaviour {
    Light light;
    AudioSource town_music,fight_music;
    float default_light, default_music;
    float register_light, register_music;
    UISlider music_s, light_s;
    //LightmapSettings a;
	// Use this for initialization
	void Start () {
        register_light= default_light = 0.7f;
        register_music =  default_music = 1;
        light = GameObject.Find("Lighting").GetComponent<Light>();
        Debug.Log(light);
        town_music = GameObject.Find("BGM").transform.Find("Town").GetComponent<AudioSource>();
        fight_music = GameObject.Find("BGM").transform.Find("Fight").GetComponent<AudioSource>();
        music_s = transform.Find("BackGround").transform.Find("Music").transform.Find("Slider").GetComponent<UISlider>();
        light_s = transform.Find("BackGround").transform.Find("Light").transform.Find("Slider").GetComponent<UISlider>();

	}
	
	// Update is called once per frame
	void Update () {
        light.intensity = light_s.value*default_light;
        town_music.volume = music_s.value;
        fight_music.volume = music_s.value;
	}


    public void Quit()
    {
        Application.Quit();
    }

    public void Accept()
    {
        register_light = light_s.value;
        register_music = music_s.value;
        this.gameObject.SetActive(false);
    }

    public void Cancel()
    {
        light_s.value = register_light;
        music_s.value = register_music;
        light.intensity = light_s.value * default_light;
        town_music.volume = music_s.value;
        fight_music.volume = music_s.value;
        this.gameObject.SetActive(false);
    }

    public void Default()
    {
        light_s.value = register_light = default_light;
        music_s.value = register_music = default_music;
    }
}
