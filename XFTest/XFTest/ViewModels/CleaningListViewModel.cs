using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using XFTest.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace XFTest.ViewModels
{
	public class CleaningListViewModel : BindableBase, INotifyPropertyChanged
    {
        readonly IList<CleaningList> source;

        public ObservableCollection<CleaningList> Monkeys { get; private set; }

        public ObservableCollection<CalendarModel> _calendarLists;
        public ObservableCollection<CalendarModel> CalendarLists
        {
            get
            {
                return _calendarLists;
            }
            set
            {
                SetProperty(ref _calendarLists, value);
            }
        }

        public string _selectedDateWithMonth="I DAG";
        public string SelectedDateWithMonth
        {
            get
            {
                return _selectedDateWithMonth;
            }
            set
            {
                SetProperty(ref _selectedDateWithMonth, value);
            }
        }

        public string _currentMonthWithYear;
        public string CurrentMonthWithYear
        {
            get
            {
                return _currentMonthWithYear;
            }
            set
            {
                SetProperty(ref _currentMonthWithYear, value);
            }
        }

        public int _currentYear;
        public int CurrentYear 
        {
            get
            {
                return _currentYear;
            }
            set
            {
                SetProperty(ref _currentYear, value);
            }
        }

        public int _currentMonth;
        public int CurrentMonth
        {
            get
            {
                return _currentMonth;
            }
            set
            {
                SetProperty(ref _currentMonth, value);
            }
        }

        public bool _isCalendar;
        public bool IsCalendar {
            get {
                return _isCalendar;
            }
            set {
                SetProperty(ref _isCalendar, value);
            }
        }

        public DateTime SelectedDate { get; set; }

        private DelegateCommand _monthBackwardCommand;
        public DelegateCommand MonthBackwardCommand =>
            _monthBackwardCommand ?? (_monthBackwardCommand = new DelegateCommand(ExecuteMonthBackwardCommand));

      
        private DelegateCommand _monthForwardCommand;
        public DelegateCommand MonthForwardCommand =>
            _monthForwardCommand ?? (_monthForwardCommand = new DelegateCommand(ExecuteMonthForwardCommand));

        
        private DelegateCommand _showDialogCommand;
        public DelegateCommand ShowDialogCommand =>
            _showDialogCommand ?? (_showDialogCommand = new DelegateCommand(ExecuteShowDialogCommand));

        private DelegateCommand<CalendarModel> _dateSelectionCommand;
        public DelegateCommand<CalendarModel> DateSelectionCommand =>
            _dateSelectionCommand ?? (_dateSelectionCommand = new DelegateCommand<CalendarModel>((item) =>ExecuteDateSelectionCommand(item)));

        private void ExecuteDateSelectionCommand(CalendarModel item)
        {
            var selectedDate = CalendarLists.Where(x => x.IsSelected == true).FirstOrDefault();
            if(selectedDate!=null)
            {
                selectedDate.IsSelected = false;
            }
            item.IsSelected = true;
            var selectedCurrentDate = new DateTime(CurrentYear, CurrentMonth, item.Date);
            GetSelectedDate(selectedCurrentDate);
        }

     

        private void ExecuteMonthBackwardCommand()
        {
            var fullDate = new DateTime(CurrentYear, CurrentMonth, DateTime.Now.Day);
            fullDate=fullDate.AddMonths(-1);
            SetCurrentMonth(fullDate);
        }

        private void ExecuteMonthForwardCommand()
        {
            var fullDate = new DateTime(CurrentYear, CurrentMonth, DateTime.Now.Day);
            fullDate=fullDate.AddMonths(1);
            SetCurrentMonth(fullDate);
        }

        public void GetSelectedDate(DateTime selectedCurrentDate)
        {
            SelectedDateWithMonth = selectedCurrentDate.Date == DateTime.Now.Date ? "I DAG" : selectedCurrentDate.ToString("dd MMM yyyy");
        }

        private void ExecuteShowDialogCommand()
        {
            IsCalendar = !IsCalendar;
        }

        public CleaningListViewModel( IDialogService dialogService, INavigationService navigationService)
        {
            source = new List<CleaningList>();
            SetCurrentMonth(DateTime.Now);
            CreateMonkeyCollection();
        }

        private void SetCurrentMonth(DateTime CurrentDate)
        {
            CurrentMonth = CurrentDate.Month;
            CurrentYear = CurrentDate.Year;
            CurrentMonthWithYear = CurrentDate.ToString("MMM yyyy").ToUpper();
            CalendarWithDay(CurrentYear, CurrentMonth);
        }

        private void CalendarWithDay(int year, int month)
        {
            CalendarLists = new ObservableCollection<CalendarModel>();
            var NoOfDays = DateTime.DaysInMonth(year,month);
            for (int Date = 1; Date <= NoOfDays; Date++)
            {
                var selection = new DateTime(CurrentYear,CurrentMonth, Date).Date.Day == DateTime.Now.Date.Day ? true : false;
                var day = new DateTime(CurrentYear,CurrentMonth, Date).ToString("ddd");
                CalendarLists.Add(new CalendarModel() { Date = Date, Day = day.Substring(0, 1), IsSelected = selection });
            }

            SelectedDate=new DateTime(year,month,CalendarLists.Where(x=>x.IsSelected==true).FirstOrDefault().Date);
            GetSelectedDate(SelectedDate);
        }

        void CreateMonkeyCollection()
        {
            source.Add(new CleaningList
            {
                Name = "Wash1",

            });

            source.Add(new CleaningList
            {
                Name = "Wash2",

            });

            source.Add(new CleaningList
            {
                Name = "Wash3",

            });

            source.Add(new CleaningList
            {
                Name = "Wash4",

            });

            source.Add(new CleaningList
            {
                Name = "Wash5",
            });

            Monkeys = new ObservableCollection<CleaningList>(source);
        }
    }
}
