using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sport.ViewModels
{
    public class StopWatchViewModel : BaseViewModel
    {
        public Command RestartTimerCommand { get; }
        public Command StartTimerCommand { get; }
        public Command StopTimerCommand { get; }

        private bool timer = false, _isRestart = false, _isStart = true, _isStop = false;

        private int _h = 0, _mn = 0, _s = 0;

        private string _seconds = "00", _minutes = "00", _hours = "00";

        public bool IsRestart
        {
            get => _isRestart;
            set => SetProperty(ref _isRestart, value);
        }

        public bool IsStart
        {
            get => _isStart;
            set => SetProperty(ref _isStart, value);
        }

        public bool IsStop
        {
            get => _isStop;
            set => SetProperty(ref _isStop, value);
        }

        public string Hour
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        public string Minute
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        public string Second
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }


        public StopWatchViewModel()
        {
            RestartTimerCommand = new Command(OnRestart);
            StartTimerCommand = new Command(OnStart);
            StopTimerCommand = new Command(OnStop);
        }

        private void OnStart()
        {
            timer = true;
            EnabledButton(true, false, true);

            Device.StartTimer(new TimeSpan(0,0,0,1), () =>
            {
                _s++;
                Second = AssignSecond();
                Minute = AssignMinute();
                Hour = AssignHour();

                return timer;

            });
        }

        private void OnStop()
        {
            timer = false;
            EnabledButton(true,true,false);
        }

        private void OnRestart()
        {
            _s = 0;
            _h = 0;
            _mn = 0;

            Second = "00";
            Minute = "00";
            Hour = "00";
        }

        private void EnabledButton(bool restart, bool start, bool stop)
        {
            IsRestart = restart;
            IsStop = stop;
            IsStart = start;
        }

        private string AssignSecond()
        {
            if (_s < 10)
                return $"0{_s}";
            else if (_s < 60)
                return $"{_s}";
            else
            {
                _s++;
                return "00";
            }
        }

        private string AssignMinute()
        {
            if (_mn < 10)
                return $"0{_mn}";
            else if (_mn < 60)
                return $"{_mn}";
            else
            {
                _mn++;
                return "00";
            }
        }

        private string AssignHour()
        {
            if (_h < 10)
                return $"0{_h}";
            else if (_h < 24)
                return $"{_h}";
            else
            {
                OnStop();
                return "00";
            }
        }
    }
}
