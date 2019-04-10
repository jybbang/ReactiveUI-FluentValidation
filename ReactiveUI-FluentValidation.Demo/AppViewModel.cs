using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI_FluentValidation.Demo
{
    public class AppViewModel : ReactiveObject
    {
        private string fullname;
        private string surname;
        private string forename;
        public string Fullname
        {
            get => fullname;
            set
            {
                this.RaiseAndSetIfChanged(ref fullname, value);
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                this.RaiseAndSetIfChanged(ref surname, value);
            }
        }
        public string Forename
        {
            get => forename;
            set
            {
                this.RaiseAndSetIfChanged(ref forename, value);
            }
        }

        public ReactiveCommand<Unit, Unit> SomeAction { get; }

        public AppViewModel()
        {
            var isValid = this.WhenAnyValue(vm => vm.Surname, vm => vm.Forename)
                .Select(x => !String.IsNullOrWhiteSpace(x.Item1) && !String.IsNullOrWhiteSpace(x.Item2));

            SomeAction = ReactiveCommand.Create(() =>
            {
                Fullname = $"{Forename} {Surname}";
            }, isValid);
        }
    }
}
