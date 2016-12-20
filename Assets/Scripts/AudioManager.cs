using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	public AudioEvent[] audioClips;
	AudioSource source;
	float defVolume;
	float defPitch;
	bool isPresent;
	public bool Effect;

	[Header("Teste de AudioEvent:")]
	public string eventName = null;


	void start(){
		Musica("Soundtrack");
	}

	public void playSound(string name){

		if (Effect)
			SoundEffect (name);
		else
			Musica (name);

	}

	public void SoundEffect(string name){


		GameObject Ignore = new GameObject (name);
		Ignore.AddComponent<AudioSource> ();

		AudioSource Sons;
		Sons = GameObject.Find (name).GetComponent<AudioSource> ();

		defVolume = Sons.volume;
		defPitch = Sons.pitch;


		if(name!="null"){
			foreach(AudioEvent a in audioClips){ //procura o AudioEvent especificado por name

				if(a.audioEventName==name){
					isPresent = true;

					Sons.volume = a.volume; //assimila o volume do AudioEvent
					Sons.pitch = a.pitch; //assimila o pitch do AudioEvent
					Sons.priority = a.priority;

					if (a.Loop)
						Sons.loop = true;

					if(a.audioClip!=null){
						Sons.clip = a.audioClip;
						Sons.Play();
					}else{
						Debug.LogError("AudioEvent " + a.audioEventName + " has no AudioClip!");
						break;
					}

					if(!a.Loop)
						Destroy(Ignore,Sons.clip.length);

				}
			}

			Sons.volume = defVolume;
			Sons.pitch = defPitch;



			//source.volume = defVolume;
			//source.pitch = defPitch;
			if(!isPresent){
				Debug.LogError("AudioEvent " + name + " wasn't found! Are you sure you typed it right?");
			}
		}else{
			Debug.LogError("AudioEvent " + name + " is using the default name of 'null'. Rename it and try again.");
		}
		isPresent = false;

	}

	public void Musica(string name){

		source = gameObject.GetComponent<AudioSource>();
		defVolume = source.volume; //preserva o volume original
		defPitch = source.pitch; // preserva o pitch original
		if(name!="null"){
			foreach(AudioEvent a in audioClips){ //procura o AudioEvent especificado por name
				if(a.audioEventName==name){

					if (a.Loop)
						source.loop = true;

					isPresent = true;
					source.volume = a.volume; //assimila o volume do AudioEvent
					source.pitch = a.pitch; //assimila o pitch do AudioEvent
					source.priority = a.priority;


					if(a.audioClip!=null){
						source.clip = a.audioClip;
						source.Play();
					}else{
						Debug.LogError("AudioEvent " + a.audioEventName + " has no AudioClip!");
						break;
					}	
				}
			}
			source.volume = defVolume;
			source.pitch = defPitch;
			if(!isPresent){
				Debug.LogError("AudioEvent " + name + " wasn't found! Are you sure you typed it right?");
			}
		}else{
			Debug.LogError("AudioEvent " + name + " is using the default name of 'null'. Rename it and try again.");
		}
		isPresent = false;

	}

	public void stopSound(){
		source.Stop();
	}

	public float getVolume(string name){
		if(name!="null"){
			foreach(AudioEvent a in audioClips){
				if(a.audioEventName==name){
					isPresent = true;
					return(a.volume);
				}else{
					return 0;
				}	
			}
			if(!isPresent){
				return 0;
			}
		}else{
			return 0;
		}
		isPresent = false;
		return 0;
	}

	public float getPitch(string name){
		if(name!="null"){
			foreach(AudioEvent a in audioClips){
				if(a.audioEventName==name){
					isPresent = true;
					return(a.pitch);
				}else{
					return 0;
				}	
			}
			if(!isPresent){
				return 0;
			}
		}else{
			return 0;
		}
		isPresent = false;
		return 0;
	}

}