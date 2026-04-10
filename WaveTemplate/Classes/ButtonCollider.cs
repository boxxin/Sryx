using UnityEngine;
using static StupidTemplate.Menu.Main;
using static StupidTemplate.Settings;

namespace StupidTemplate.Classes
{
    public class Button : MonoBehaviour
    {
        public string relatedText;
        public static float buttonCooldown = 0.15f;

        public void OnTriggerEnter(Collider collider)
        {
            Sound sound = Sounds.List[Sounds.soundValue];
            if (Time.time > buttonCooldown && collider == buttonCollider && menu != null)
            {
                buttonCooldown = Time.time + 0.15f;
                if (sound.soundgtag)
                {
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(sound.soundId, true, 4f);
                }
                else
                {
                    Soundboard.PlayAudioCS(sound.soundUrl, sound.wav);
                }
                Toggle(relatedText);
            }
        }
    }
}
