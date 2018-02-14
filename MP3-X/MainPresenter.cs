using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MP3_X.O;

namespace MP3_X
{
    class MainPresenter
    {
        private readonly IMainForm _form;
        private readonly IFileMod _module;
        string _FilePath;
        /*Проброс событий*/
        public MainPresenter(IMainForm form, IFileMod module)
        {
            _form = form;
            _module = module;
            _form.AudioOpenClick += _form_AudioOpenClick;
            _form.PlayClick += _form_PlayClick;
            _form.PauseClick += _form_PauseClick;
            _form.StopClick += _form_StopClick;
            _form.BackClick += _form_BackClick;
            _form.NextClick += _form_NextClick;
            _form.VolumeChanged += _form_VolumeChanged;
            _form.Tick += _form_Tick;
            _form.TimeBarChange += _form_TimeBarChange;
            _form.PlayListChek += _form_PlayListChek;
            _form.ClosePlayer += _form_ClosePlayer;
            _form.OpenPlayer += _form_OpenPlayer;
            _form.DeletePlayListSong += _form_DeletePlayListSong;
        }
            /*Удаление песни из плейлиста*/
        private void _form_DeletePlayListSong(object sender, EventArgs e)
        {
            _module.Delete(_form.PlayListSelect);
            _form.PlayListClear();
            if (_module.PlayListFile != null)
            {

                foreach (string Songs in _module.PlayListFile)
                {
                    string[] Title = Songs.Split('\\');
                    _form.PlayList = Title[Title.Length - 1];
                }
               
            }
        }
        /*Открытие плеера, загрузка плейлиста из файла*/

        private void _form_OpenPlayer(object sender, EventArgs e)
        {
            _module.PlayListFileCheck();

            if (_module.PlayListFile != null)
            {
                
                foreach (string Songs in _module.PlayListFile)
                {
                    string[] Title = Songs.Split('\\');
                    _form.PlayList = Title[Title.Length - 1];
                }
                _module.MInitBass();
            }
        }

        /*закрытие плеера*/

        private void _form_ClosePlayer(object sender, EventArgs e)
        {
            _module.PlayListFileCheck();

        }

        /*Выбор из плейлиста кликом мышки*/

        private void _form_PlayListChek(object sender, EventArgs e)
        {
            _module.IDPlayList = _form.PlayListSelect;
            _module.PlayListChek(_form.PlayListSelect);
            _form.FilePathProperty = _module.PlayListFile[_form.PlayListSelect];
            _FilePath = _module.PlayListFile[_form.PlayListSelect];
            _form_PlayClick(this, EventArgs.Empty);
        }


        /*Изменение тайминга звуковой дорожки кликом по трекбару*/
        private void _form_TimeBarChange(object sender, EventArgs e)
        {
            _module.FileTime = (double)_form.TimeChange;
        }



       /*Тики таймера изменяют движение трекбара*/
        private void _form_Tick(object sender, EventArgs e)
        {
            _form.TimeChange = (int)_module.FileTime;

            if (_module.FileTime >= _module.Duration)
                _form_StopClick(this, EventArgs.Empty);
        }

        /*изменение громкости*/

        private void _form_VolumeChanged(object sender, EventArgs e)
        {
            _module.Volume(_form.Volume);
        }

        /*Следующий аудиофайл*/
        private void _form_NextClick(object sender, EventArgs e)
        {
            if (_module.PlayListFile.Length != 0 && _form.PlayListSelect < _module.PlayListFile.Length - 1)
            { 
            _module.Next(_form.PlayListSelect);
            _module.PlayFile();
            _form.PlayListSelect = _module.IDPlayList;
            }
        }


          /*Предыдущий аудиофайл*/
        private void _form_BackClick(object sender, EventArgs e)
        {
            if (_form.PlayListSelect > 0 && _module.PlayListFile.Length != 0)
            {
                _module.Back(_form.PlayListSelect);
                _module.PlayFile();
                _form.PlayListSelect = _module.IDPlayList;
            }
        }
                    /*Кнопка Стоп*/
        private void _form_StopClick(object sender, EventArgs e)
        {
            
                _FilePath = null;
                _form.FilePathProperty = null;
                _module.StopFile();
                _form.TimerStop();
           
         }
           /*Кнопка Пауза*/
        private void _form_PauseClick(object sender, EventArgs e)
        {
            _module.PauseFile();
            _form.TimerStop();

        }
             /*Кнопка Плей*/
        private void _form_PlayClick(object sender, EventArgs e)
        {
            if (_FilePath != null)
            {
                
                _module.PlayFile();
                _form.TimeDuration = _module.Duration;
                _form.TimerStart();
            }
            _FilePath = _form.FilePathProperty;
            }

                /*Кнопка открытия аудиофайла*/
        private void _form_AudioOpenClick(object sender, EventArgs e)
        {
            
             if(_form.FilePathProperty != null)
            {
                _FilePath = _form.FilePathProperty;
                _module.MInitBass();
                _module.OpenFile(_FilePath);
                _module.PlayFile();
                _form.TimeChange = (int)_module.FileTime;
                _form.TimeDuration = _module.Duration;
                _form.PlayList = _module.PlayListData;
                _form.TimerStart();
            }

        }
       
    
         
        
    }
}
