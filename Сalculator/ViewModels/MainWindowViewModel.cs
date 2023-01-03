using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Сalculator.Models;

namespace Сalculator.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChangeed([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

       

        private string textInn;
        public string TextInn
        {
            get
            {
                return textInn;
            }
            set
            {
                textInn = value;
                OnPropertyChangeed();
            }
        }


        private ObservableCollection<MathExpression> observableCollection;
        public ObservableCollection<MathExpression> ObservableCollection
        {
            get
            {
                if (observableCollection == null)
                {
                    observableCollection = new ObservableCollection<MathExpression>();
                }
                return observableCollection;
            }
        }


        private MathExpression curMathException;
        public ICommand Command_Equals { get; }
        private void OnCommand_Equals_Execute(object p)
        {
            MathExpression curMathException = new MathExpression(TextInn);

            if (curMathException.ErrSatus)
            {
                MessageBox.Show(curMathException.ErrDescription);
            }
            else
            {
                TextInn = curMathException.MathExcpressionStr;
                observableCollection.Add(curMathException);
            }
        }


        public ICommand Command_Add_0 { get; }
        private void OnCommand_Add_0_Execute(object p) { TextInn += "0"; }
        public ICommand Command_Add_1 { get; }
        private void OnCommand_Add_1_Execute(object p) { TextInn += "1"; }
        public ICommand Command_Add_2 { get; }
        private void OnCommand_Add_2_Execute(object p) { TextInn += "2"; }
        public ICommand Command_Add_3 { get; }
        private void OnCommand_Add_3_Execute(object p) { TextInn += "3"; }
        public ICommand Command_Add_4 { get; }
        private void OnCommand_Add_4_Execute(object p) { TextInn += "4"; }
        public ICommand Command_Add_5 { get; }
        private void OnCommand_Add_5_Execute(object p) { TextInn += "5"; }
        public ICommand Command_Add_6 { get; }
        private void OnCommand_Add_6_Execute(object p) { TextInn += "6"; }
        public ICommand Command_Add_7 { get; }
        private void OnCommand_Add_7_Execute(object p) { TextInn += "7"; }
        public ICommand Command_Add_8 { get; }
        private void OnCommand_Add_8_Execute(object p) { TextInn += "8"; }
        public ICommand Command_Add_9 { get; }
        private void OnCommand_Add_9_Execute(object p) { TextInn += "9"; }

        public ICommand Command_Add_Plus { get; }
        private void OnCommand_Add_Plus_Execute(object p) { TextInn += "+"; }
        public ICommand Command_Add_Minus { get; }
        private void OnCommand_Add_Minus_Execute(object p) { TextInn += "-"; }
        public ICommand Command_Add_Mult { get; }
        private void OnCommand_Add_Mult_Execute(object p) { TextInn += "*"; }
        public ICommand Command_Add_Div { get; }
        private void OnCommand_Add_Div_Execute(object p) { TextInn += "/"; }

        public ICommand Command_Add_Comma { get; }
        private void OnCommand_Add_Bracket_CloseExecute(object p) { TextInn += ","; }
        public ICommand Command_Add_Bracket_Open { get; }
        private void OnCommand_Add_Bracket_Open_Execute(object p) { TextInn += "("; }
        public ICommand Command_Add_Bracket_Close { get; }
        private void OnCommand_Add_Bracket_Close_Execute(object p) { TextInn += ")"; }
        public ICommand Command_Add_Pow { get; }
        private void OnCommand_Add_Base_10_Pow_Execute(object p) { TextInn += "10^"; }
        public ICommand Command_Add_Pow_2 { get; }
        private void OnCommand_Add_Pow_2_Execute(object p) { TextInn += "^2"; }
        public ICommand Command_Square_Root { get; }
        private void OnCommand_Add_Pow_Execute(object p) { TextInn += "^"; }
        public ICommand Command_Add_Base_10_Pow { get; }
        private void OnCommand_Square_Root_Execute(object p) { TextInn += "^0,5"; }


        public ICommand Command_Clear { get; }
        private void On_Command_Clear_Execute(object p)
        {
            if (TextInn.Length > 0)
            {
                TextInn = TextInn.Remove(0);
            }
        }


        public ICommand Command_Delete { get; }
        private void On_Command_Delete_Execute(object p)
        {
            if (TextInn.Length > 0)
            {
                TextInn = TextInn.Remove(TextInn.Length - 1);
            }
        }


        private bool OnAddCanExecuted(object p)
        {
            return true;
        }


        public MainWindowViewModel()
        {
            Command_Equals = new RelayCommand(OnCommand_Equals_Execute, OnAddCanExecuted);

            Command_Add_0 = new RelayCommand(OnCommand_Add_0_Execute, OnAddCanExecuted);
            Command_Add_1 = new RelayCommand(OnCommand_Add_1_Execute, OnAddCanExecuted);
            Command_Add_2 = new RelayCommand(OnCommand_Add_2_Execute, OnAddCanExecuted);
            Command_Add_3 = new RelayCommand(OnCommand_Add_3_Execute, OnAddCanExecuted);
            Command_Add_4 = new RelayCommand(OnCommand_Add_4_Execute, OnAddCanExecuted);
            Command_Add_5 = new RelayCommand(OnCommand_Add_5_Execute, OnAddCanExecuted);
            Command_Add_6 = new RelayCommand(OnCommand_Add_6_Execute, OnAddCanExecuted);
            Command_Add_7 = new RelayCommand(OnCommand_Add_7_Execute, OnAddCanExecuted);
            Command_Add_8 = new RelayCommand(OnCommand_Add_8_Execute, OnAddCanExecuted);
            Command_Add_9 = new RelayCommand(OnCommand_Add_9_Execute, OnAddCanExecuted);

            Command_Add_Plus = new RelayCommand(OnCommand_Add_Plus_Execute, OnAddCanExecuted);
            Command_Add_Minus = new RelayCommand(OnCommand_Add_Minus_Execute, OnAddCanExecuted);
            Command_Add_Mult = new RelayCommand(OnCommand_Add_Mult_Execute, OnAddCanExecuted);
            Command_Add_Div = new RelayCommand(OnCommand_Add_Div_Execute, OnAddCanExecuted);

            Command_Add_Comma= new RelayCommand(OnCommand_Add_Bracket_CloseExecute, OnAddCanExecuted);
            Command_Add_Bracket_Open = new RelayCommand(OnCommand_Add_Bracket_Open_Execute, OnAddCanExecuted);
            Command_Add_Bracket_Close = new RelayCommand(OnCommand_Add_Bracket_Close_Execute, OnAddCanExecuted);
            Command_Add_Pow = new RelayCommand(OnCommand_Add_Pow_Execute, OnAddCanExecuted);
            Command_Add_Pow_2 = new RelayCommand(OnCommand_Add_Pow_2_Execute, OnAddCanExecuted);
            Command_Square_Root = new RelayCommand(OnCommand_Square_Root_Execute, OnAddCanExecuted);
            Command_Add_Base_10_Pow = new RelayCommand(OnCommand_Add_Base_10_Pow_Execute, OnAddCanExecuted);
            
            Command_Clear = new RelayCommand(On_Command_Clear_Execute, OnAddCanExecuted);
            Command_Delete = new RelayCommand(On_Command_Delete_Execute, OnAddCanExecuted);
        }
    }
}
