using LittleWitch_Project.Sources._02_Entity;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LittleWitch_Project.Sources._03_System
{
    public class AudioData
    {
        [JsonPropertyName("path")]
        public string path { get; set; }

        [JsonPropertyName("audio")]
        public List<AudioSources> Audio { get; set; }
    }

    public class AudioSources
    {
        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("volume")]
        public float Volume { get; set; }

        [JsonPropertyName("looping")]
        public bool Looping { get; set; }

    }
    public class SFXSource
    {
        public SoundEffect Sound { get; set; }
        public string Name { get; set; }
        public float Volume { get; set; }
        public bool Looping { get; set; }
    }

    public class BGMSource
    {
        public Song Music { get; set; }
        public string Name { get; set; }
        public float Volume { get; set; }
        public bool Looping { get; set; }
    }

    public class GameAudioManager
    {
        private float volume_bgm = 0.2f;
        private float volume_sfx = 0.8f;
        private float volume_vc = 1f;
        private float volume_fadeStep;
        private float volume_fadeDuration = 2f;

        private List<SFXSource> _SFX;
        private List<BGMSource> _BGM;
        private SoundEffectInstance _currentVoiceline;

        public GameAudioManager(ContentManager content)
        {

            _SFX = new List<SFXSource>();
            _BGM = new List<BGMSource>();
            LoadContent(content);

            MediaPlayer.IsMuted = false;
            MediaPlayer.Volume = 1f;
        }

        private void LoadContent(ContentManager content)
        {
            string path = Path.Combine(content.RootDirectory, "data/dataAudio", "sfx_index.json");

            string json = File.ReadAllText(path);

            //------------------------------ LOAD SFX
            AudioData? SFXdata = JsonSerializer.Deserialize<AudioData>(json);

            if (SFXdata == null) return;
            foreach (AudioSources audio in SFXdata.Audio)
            {
                SoundEffect sound = content.Load<SoundEffect>("sfx/" + audio.Group + "/" + audio.Name);

                _SFX.Add(new SFXSource{
                    Sound = sound,
                    Name = audio.Name,
                    Volume = audio.Volume,
                    Looping = audio.Looping
                });
            }

            path = Path.Combine(content.RootDirectory, "data/dataAudio", "music_index.json");

            json = File.ReadAllText(path);

            AudioData? BGMdata = JsonSerializer.Deserialize<AudioData>(json);

            if (BGMdata == null) return;
            foreach (AudioSources audio in BGMdata.Audio)
            {
                Song song = content.Load<Song>("music/" + audio.Group + "/" + audio.Name);

                _BGM.Add(new BGMSource
                {
                    Music = song,
                    Name = audio.Name,
                    Volume = audio.Volume,
                    Looping = audio.Looping
                });
            }
        }
        public void PlayBGM(string bgmName)
        {
            BGMSource? bgm = _BGM.Find(x => x.Name == bgmName);

            if (bgm == null)
                return;

            MediaPlayer.IsRepeating = bgm.Looping;

            MediaPlayer.Volume = 1f;

            MediaPlayer.Play(bgm.Music);

            Console.WriteLine("IS playing" + bgmName);

            Console.WriteLine("Muted: " + MediaPlayer.IsMuted);
            Console.WriteLine("Volume: " + MediaPlayer.Volume);

        }

        public void LowerBGM(bool decision)
        {
            if (decision) MediaPlayer.Volume = 0.05f;
            else MediaPlayer.Volume = volume_bgm;
        }

        public void PlaySFX(string sfxName)
        {
            SFXSource? sfx = _SFX.Find(x => x.Name == sfxName);

            if (sfx == null)
                return;

            SoundEffectInstance instance = sfx.Sound.CreateInstance();

            instance.Volume = sfx.Volume * volume_sfx;
            instance.IsLooped = sfx.Looping;

            instance.Play();

        }

        public void VolumeControl(float bgmVolume, float sfxVolume)
        {
            volume_bgm = Math.Clamp(volume_bgm + bgmVolume, 0f, 1f);
            volume_sfx = Math.Clamp(volume_sfx + sfxVolume, 0f, 1f);
            volume_vc = Math.Clamp(volume_vc + sfxVolume, 0f, 1f);

            MediaPlayer.Volume = volume_bgm;
        }
    }
}
