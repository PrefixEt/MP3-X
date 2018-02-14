using System;
using System.IO;
using System.Collections.Generic;
using Un4seen.Bass;



namespace MP3_X.O
{
    public interface IFileMod
    {
        void OpenFile(string filePath);
        void PlayFile();
        void PauseFile();
        void StopFile();
        void Volume(int volume);
        void Back(int index);
        void Next(int index);
        void Delete(int index);
        double FileTime { get; set; }
        int Duration { get; }        
        bool MInitBass();
        string PlayListData { set; get; }
        void PlayListChek(int index);
        void PlayListFileCheck();
        string[] PlayListFile { get; }
        int IDPlayList { get; set; }
    }


    public class FileMod : IFileMod

    {

        int _stream;
        string _FilePath;
        bool InitBassDll;
        List<string> _PlayList = new List<string>();
        int _IDPlayList;
        public bool MInitBass()
        {

            if (!InitBassDll)
                InitBassDll = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            return InitBassDll;
        }


        public void PlayListChek(int index)
        {
            StopFile();
            if (index >= 0 && index <= _PlayList.Count - 1)
            {             
                _stream = Bass.BASS_StreamCreateFile(_PlayList[index], 0, 0, BASSFlag.BASS_DEFAULT);
                _FilePath = _PlayList[index];
                _IDPlayList = index;

            }
        }

    
        public void OpenFile(string filePath)
        {

            StopFile();
            if (filePath != null && InitBassDll)
            {
                _FilePath = filePath;
                _stream = Bass.BASS_StreamCreateFile(_FilePath, 0, 0, BASSFlag.BASS_DEFAULT);
                PlayListData = _FilePath;
            }

        }

        public void PlayFile() {

            if (InitBassDll)

            {
                Bass.BASS_ChannelPlay(_stream, false);

            }
        }


        public void PauseFile()
        {
            Bass.BASS_ChannelPause(_stream);
        }

        public void StopFile()
        {
            if (_stream != 0)
            {
                Bass.BASS_ChannelStop(_stream);
                Bass.BASS_StreamFree(_stream);

            }
        }


        public void Volume(int volume) {
            if (_stream != 0)
                Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, ((float)volume) / 100);
        }


        public void Back(int index)
        {   
            if (_FilePath != null)
                PlayListChek(index - 1);

        }

        public void Next(int index)
        {
            if (_FilePath != null)
                PlayListChek(index + 1);
        }

        public double FileTime {

            get { return Bass.BASS_ChannelBytes2Seconds(_stream, Bass.BASS_ChannelGetPosition(_stream)); }
            set {
                Bass.BASS_ChannelSetPosition(_stream, Bass.BASS_ChannelSeconds2Bytes(_stream, value), BASSMode.BASS_POS_BYTES);
            }
        }



        public int Duration { get {
                return (int)Bass.BASS_ChannelBytes2Seconds(_stream, Bass.BASS_ChannelGetLength(_stream));
            }
        }

        public string PlayListData
        {
            get
            {
                string[] SongName = _PlayList[_PlayList.Count - 1].Split('\\');
                return SongName[SongName.Length - 1];
            }
            set { _PlayList.Add(value); }
        }

        public void PlayListFileCheck()
        {

                if (!File.Exists(@".\PlayList.txt"))
                {
                    File.Open(@".\PlayList.txt", FileMode.OpenOrCreate).Dispose();

                }

                if (_PlayList.Count != 0 && !PlayListEquals(_PlayList.ToArray(), File.ReadAllLines(@".\PlayList.txt")))
                {
                File.WriteAllText(@".\PlayList.txt", "");
                File.WriteAllLines(@".\PlayList.txt", _PlayList);

                }


                if (File.ReadAllLines(@".\PlayList.txt").Length != 0 && _PlayList.Count == 0)
                {
                    foreach (string FilePathFile in File.ReadAllLines(@".\PlayList.txt"))
                        _PlayList.Add(FilePathFile);

                }
          
           
         }

        public int IDPlayList { get { return _IDPlayList; } set { _IDPlayList = value; } }

        public string[] PlayListFile
        {
            get { return _PlayList.ToArray(); }
        }

        bool PlayListEquals(string[] A, string[] B)
        {
            string[] a = A;
            string[] b = B;

            if (a.Length != b.Length) return false;

            for (int i = 0; i < a.Length - 1; i++)
                if (a[i] != b[i]) return false;

            return true;                      
        }

        public void Delete(int index)
        {   if (_PlayList.Count > 0 && index <= _PlayList.Count);
            _PlayList.RemoveAt(index);
        }


        }    
        

}
