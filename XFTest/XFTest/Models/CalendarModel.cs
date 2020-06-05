using System;
using Prism.Mvvm;

namespace XFTest.Models
{
    public class CalendarModel:BindableBase
    {
        public int Date { get; set; }
        public string Day { get; set; }
        public bool _isSelected;
        public bool IsSelected { get { return _isSelected; } set { SetProperty(ref _isSelected, value); } }
    }
}
