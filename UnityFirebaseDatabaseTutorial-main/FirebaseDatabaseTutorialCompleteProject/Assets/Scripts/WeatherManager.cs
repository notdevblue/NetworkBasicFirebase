using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    static public WeatherManager Instance { get; private set; }

    public ParticleSystem snow;
    public ParticleSystem rain;

    public string _currentWeather{ get; set; }
    public string CurrentWeather {
        get {
            return _currentWeather;
        } 
        set {
            _currentWeather = value;

            switch(_currentWeather)
            {
                case "snow":
                    Instantiate(snow.gameObject, snow.transform.position, snow.transform.rotation);
                    snow.Play();
                    break;
                case "rain":
                    Instantiate(rain.gameObject, rain.transform.position, rain.transform.rotation);
                    rain.Play();
                    break;
            }
        }
    }


    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
