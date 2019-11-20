using GalaSoft.MvvmLight;
using SmallTroupManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallTroupManager.ViewModel
{
    public class UserViewModel: ViewModelBase
    {
        private ObservableCollection<RepertoireItem> targetItems;

        public UserViewModel()
        {
            targetItems.Add(new RepertoireItem(1,"红动中国","4'32''","mi","红动中国","c:\\sdsda.mp3","local"));
        }

        public ObservableCollection<RepertoireItem> TargetItems { get => targetItems ?? (targetItems = new ObservableCollection<RepertoireItem>()); }








    }
}
