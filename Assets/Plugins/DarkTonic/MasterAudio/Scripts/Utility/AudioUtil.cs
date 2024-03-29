using UnityEngine;

// ReSharper disable once CheckNamespace
namespace DarkTonic.MasterAudio {
    /// <summary>
    /// This class contains frequently used methods for audio in general.
    /// </summary>
    // ReSharper disable once CheckNamespace
    public static class AudioUtil {
        /*! \cond PRIVATE */
        public const float DefaultMinOcclusionCutoffFrequency = 22000f;
        public const float DefaultMaxOcclusionCutoffFrequency = 0f;

        private const float SemitonePitchChangeAmt = 1.0594635f;

        private static float CutoffRange(SoundGroupVariationUpdater updater) {
            return updater.MinOcclusionFreq - updater.MaxOcclusionFreq;
        }

        private static float MaxCutoffFreq(SoundGroupVariationUpdater updater) {
            return updater.MaxOcclusionFreq;
        }

        public static float MinCutoffFreq(SoundGroupVariationUpdater updater) {
            return updater.MinOcclusionFreq;
        }

        public static float FixedDeltaTime {
            get { return UnityEngine.Time.fixedDeltaTime; }
        }

        public static float FrameTime {
            get { return UnityEngine.Time.unscaledDeltaTime; }
        }

        public static float Time {
            get { return UnityEngine.Time.unscaledTime; }
        }

        public static int FrameCount {
            get { return UnityEngine.Time.frameCount; }
        }

        public static float GetOcclusionCutoffFrequencyByDistanceRatio(float distRatio, SoundGroupVariationUpdater updater) {
            return MaxCutoffFreq(updater) + (distRatio * CutoffRange(updater));
        }

        public static float GetSemitonesFromPitch(float pitch) {
            float pitchSemitones;

            if (pitch < 1f && pitch > 0) {
                var pitchBelow = 1 / pitch;
                pitchSemitones = Mathf.Log(pitchBelow, SemitonePitchChangeAmt) * -1;
            } else {
                pitchSemitones = Mathf.Log(pitch, SemitonePitchChangeAmt);
            }

            return pitchSemitones;
        }

        public static float GetPitchFromSemitones(float semitones) {
            if (semitones >= 0) {
                return Mathf.Pow(SemitonePitchChangeAmt, semitones);
            }

            var newPitch = 1 / Mathf.Pow(SemitonePitchChangeAmt, Mathf.Abs(semitones));
            return newPitch;
        }

        public static float GetDbFromFloatVolume(float vol) {
            return Mathf.Log10(vol) * 20;
        }

        public static float GetFloatVolumeFromDb(float db) {
            return Mathf.Pow(10, db / 20);
        }
        /*! \endcond */

        /// <summary>
        /// This method will tell you the percentage of the clip that is done Playing (0-100).
        /// </summary>
        /// <param name="source">The Audio Source to calculate for.</param>
        /// <returns>(0-100 float)</returns>
        public static float GetAudioPlayedPercentage(AudioSource source) {
            if (source.clip == null || source.time == 0f) {
                return 0f;
            }

            var playedPercentage = (source.time / source.clip.length) * 100;
            return playedPercentage;
        }

        /// <summary>
        /// This method returns whether an AudioSource is paused or not.
        /// </summary>
        /// <param name="source">The Audio Source in question.</param>
        /// <returns>True or false</returns>
        public static bool IsAudioPaused(AudioSource source) {
            return !source.isPlaying && GetAudioPlayedPercentage(source) > 0f;
        }

        public static void ClipPlayed(AudioClip clip, GameObject actor) {
            if (AudioClipWillPreload(clip)) {
                return;
            }

            AudioLoaderOptimizer.AddNonPreloadedPlayingClip(clip, actor);
        }

        /*! \cond PRIVATE */
#if UNITY_5 || UNITY_2017
        public static void UnloadNonPreloadedAudioData(AudioClip clip, GameObject actor) {
            if (clip == null) {
				return;
			}

			if (AudioClipWillPreload(clip)) {
				return;
			}

            AudioLoaderOptimizer.RemoveNonPreloadedPlayingClip(clip, actor);

            if (AudioLoaderOptimizer.IsAnyOfNonPreloadedClipPlaying(clip)) {
				return;
			}

			clip.UnloadAudioData(); // restore memory
        }
#else
        public static void UnloadNonPreloadedAudioData(AudioClip clip, GameObject actor) {
            // do nothing
        }
#endif

#if UNITY_5 || UNITY_2017
        public static bool AudioClipWillPreload(AudioClip clip) {
            if (clip == null) {
                return false;
            }

            return clip.preloadAudioData;
        }
#else
        public static bool AudioClipWillPreload(AudioClip clip) {
            return true;
        }
#endif

        public static bool IsClipReadyToPlay(this AudioClip clip) {
#if UNITY_5 || UNITY_2017
            return clip != null && clip.loadType != AudioClipLoadType.Streaming;
#else
            return clip != null && clip.isReadyToPlay;
#endif
        }
        /*! \endcond */
    }
}