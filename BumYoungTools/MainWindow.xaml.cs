using BumYoungTools.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace BumYoungTools
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 키보드 Press 제어 핸들러
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)MainViewModel.HandleKeyDownEvent);
        }
    }
}
