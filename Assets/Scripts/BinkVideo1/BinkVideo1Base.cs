using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace BinkORK.Video1
{
    /// <summary>
    /// Base class for Bink Video 1 operations
    /// </summary>
    public abstract class BinkVideo1Base : MonoBehaviour
    {
        // Bink Video 1 native library imports
        protected const string BINK_DLL = "binkw32";

        [DllImport(BINK_DLL)]
        protected static extern IntPtr BinkOpen(string filename, uint flags);

        [DllImport(BINK_DLL)]
        protected static extern void BinkClose(IntPtr bink);

        [DllImport(BINK_DLL)]
        protected static extern uint BinkDoFrame(IntPtr bink);

        [DllImport(BINK_DLL)]
        protected static extern void BinkGetFrameBuffers(IntPtr bink, IntPtr buffers);

        [DllImport(BINK_DLL)]
        protected static extern uint BinkWait(IntPtr bink);

        [DllImport(BINK_DLL)]
        protected static extern void BinkSetSoundSystem(IntPtr soundSystem, IntPtr soundOpen);

        protected IntPtr binkHandle;
        protected bool isPlaying = false;
        protected bool isPaused = false;

        /// <summary>
        /// Open a Bink Video 1 file
        /// </summary>
        /// <param name="filepath">Path to the .bik file</param>
        /// <returns>True if successful</returns>
        protected bool OpenBinkFile(string filepath)
        {
            try
            {
                binkHandle = BinkOpen(filepath, 0);
                if (binkHandle == IntPtr.Zero)
                {
                    Debug.LogError($"Failed to open Bink Video 1 file: {filepath}");
                    return false;
                }
                Debug.Log($"Successfully opened Bink Video 1 file: {filepath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Exception opening Bink Video 1 file: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Close the currently opened Bink file
        /// </summary>
        protected void CloseBinkFile()
        {
            if (binkHandle != IntPtr.Zero)
            {
                BinkClose(binkHandle);
                binkHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Advance to the next frame
        /// </summary>
        protected void AdvanceFrame()
        {
            if (binkHandle == IntPtr.Zero || !isPlaying)
                return;

            if (isPaused)
                return;

            BinkDoFrame(binkHandle);
        }

        /// <summary>
        /// Pause playback
        /// </summary>
        public virtual void Pause()
        {
            isPaused = true;
        }

        /// <summary>
        /// Resume playback
        /// </summary>
        public virtual void Resume()
        {
            isPaused = false;
        }

        /// <summary>
        /// Stop playback and close the file
        /// </summary>
        public virtual void Stop()
        {
            isPlaying = false;
            CloseBinkFile();
        }

        protected virtual void OnDestroy()
        {
            Stop();
        }
    }
}
