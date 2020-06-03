using GalaSoft.MvvmLight;
using SmallTroupManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ElCommon.Util;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.Windows;

namespace SmallTroupManager.ViewModel
{
    public class UserViewModel: ViewModelBase
    {
       
        public UserViewModel()
        {
            
        }

        //public void UIElement_OnFocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
            
        //}

        //public void FocusChange()
        //{

        //}


        private ICommand _saveFileCommand;
        public ICommand SaveFileCommand
        {
            get => _saveFileCommand ?? (_saveFileCommand = new UtilRelayCommand(delegate (object obj)
            {
               
            }, pre =>
            {
                return true;
            }));
        }
        private ICommand _selectCommand;
        public ICommand SelectCommand
        {
            get => _selectCommand ?? (_selectCommand = new UtilRelayCommand(delegate (object obj)
            {
                var b = obj;
            }, pre =>
            {
                return true;
            }));
        }
        private ICommand _keyCommand;
        public ICommand KeyCommand
        {
            get => _keyCommand ?? (_keyCommand = new UtilRelayCommand(delegate (object obj)
            {
                var b = obj;
            }, pre =>
            {
                return true;
            }));
        }
    }
}
