using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChangeed([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private double radius;

        public double Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                OnPropertyChangeed();
            }
        }

        private double lenCirc;

        public double LenCirc
        {
            get
            {
                return lenCirc;
            }
            set
            {
                lenCirc = value;
                OnPropertyChangeed();
            }
        }

        public ICommand AddCommand { get; }

        private void OnAddCommandExecute(object p)
        {
            LenCirc = Calc.CalcLenCirc(Radius);
        }

        private bool OnAddCanExecuted(object p)
        {
            return true;
        }

        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecute, OnAddCanExecuted);
        }
    }
}

