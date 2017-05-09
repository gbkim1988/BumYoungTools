using BumYoungTools.Message;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Input;

namespace BumYoungTools.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }
        private ICommand closeWindowCommand;

        public ICommand CloseWindowCommand
        {
            get
            {
                if (closeWindowCommand == null)
                {
                    closeWindowCommand = new RelayCommand(() => this.CloseWindow(), null);
                }
                return closeWindowCommand;
            }
        }

        public static void HandleKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                MessageBox.Show("CTRL + SHIFT + TAB trapped");
            }
            else if (e.Key == Key.C && (Keyboard.Modifiers & (ModifierKeys.Control)) == ModifierKeys.Control) {
                MessageBox.Show("CTRL + C trapped");
            }
        }

        private void CloseWindow()
        {
            //Do your operations
            MessageBox.Show("Really?");
            // 이벤트를 등록하여 BumYoung1 ViewModel 이 정상적이 종료 로직을 수행하도록 해준다.
            Messenger.Default.Send(new ExitMessage { Exit = true });
        }
    }
}