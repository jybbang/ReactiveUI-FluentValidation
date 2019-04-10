using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;
using System.Reactive.Disposables;

namespace ReactiveUI_FluentValidation.Demo
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : ReactiveWindow<AppViewModelWithValidation>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new AppViewModelWithValidation();
            DataContext = ViewModel;

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel,
                    vm => vm.Fullname,
                    v => v.TextName.Text)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                    vm => vm.SomeAction,
                    v => v.buttonYourname)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
