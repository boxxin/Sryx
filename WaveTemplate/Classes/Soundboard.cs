using Photon.Pun;
using Photon.Voice.Unity;
using StupidTemplate.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace StupidTemplate.Classes
{
    public class Soundboard
    {
        public static float RecoverTime;
        public static AudioSource localSource;
        public static AudioClip sound;

        private static readonly Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip>();

        public static void PlayAudio(string thing)
        {
            if (PhotonNetwork.InRoom)
            {
                GorillaLocomotion.GTPlayer.Instance.StartCoroutine(LoadAndPlay(thing, AudioType.MPEG, clip =>
                {
                    if (clip == null) { Debug.LogError("Failed to load audio."); return; }
                    sound = clip;
                    var recorder = GorillaTagger.Instance.myRecorder;
                    recorder.SourceType = Recorder.InputSourceType.AudioClip;
                    recorder.AudioClip = sound;
                    recorder.RestartRecording(false);
                    recorder.DebugEchoMode = true;
                    recorder.LoopAudioClip = false;
                    RecoverTime = Time.time + sound.length;
                }));
            }
            else
            {
                PlayAudioCS(thing);
            }
        }

        public static void StopSounds()
        {
            GorillaTagger.Instance.myRecorder.SourceType = Recorder.InputSourceType.Microphone;
            GorillaTagger.Instance.myRecorder.AudioClip = null;
            GorillaTagger.Instance.myRecorder.RestartRecording(true);
            GorillaTagger.Instance.myRecorder.DebugEchoMode = false;
            sound = null;
            RecoverTime = -1f;
        }

        public static void PlayAudioCS(string githubRawUrl, bool wav = false)
        {
            AudioType type = wav ? AudioType.WAV : AudioType.MPEG;
            GorillaLocomotion.GTPlayer.Instance.StartCoroutine(LoadAndPlay(githubRawUrl, type, clip =>
            {
                if (clip == null) { Debug.LogError("Failed to load audio."); return; }
                sound = clip;
                if (localSource == null)
                {
                    GameObject audioObj = new GameObject("AudioObj");
                    localSource = audioObj.AddComponent<AudioSource>();
                    localSource.volume = 1f;
                    localSource.playOnAwake = false;
                }
                localSource.transform.position = GorillaLocomotion.GTPlayer.Instance.headCollider.transform.position;
                localSource.clip = sound;
                localSource.loop = false;
                localSource.Play();
                RecoverTime = Time.time + sound.length;
            }));
        }

        public static void playhh()
        {
            PlayAudio("https://raw.githubusercontent.com/boxxin/clcik/main/H.H%20(SONG)%20-%20Kanye%20West%20-%20(Educational%20Content).mp3");
        }

        private static IEnumerator LoadAndPlay(string url, AudioType audioType, Action<AudioClip> onDone)
        {
            if (clipCache.TryGetValue(url, out AudioClip cached))
            {
                onDone(cached);
                yield break;
            }

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    clipCache[url] = clip;
                    onDone(clip);
                }
                else
                {
                    onDone(null);
                }
            }
        }
    }
}