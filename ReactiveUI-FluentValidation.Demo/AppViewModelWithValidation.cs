using FluentValidation;
using ReactiveUI;
using ReactiveUI.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI_FluentValidation.Demo
{
    public class AppViewModelValidator : AbstractValidator<AppViewModelWithValidation>
    {
        public AppViewModelValidator()
        {
            RuleFor(vm => vm.Surname).NotEmpty();
            RuleFor(vm => vm.Forename).NotEmpty();
        }
    }

    public class AppViewModelWithValidation : ReactiveValidationObject
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
                this.RaiseValidation(nameof(Surname));
            }
        }
        public string Forename
        {
            get => forename;
            set
            {
                this.RaiseAndSetIfChanged(ref forename, value);
                this.RaiseValidation(nameof(Forename));
            }
        }

        public ReactiveCommand<Unit, Unit> SomeAction { get; }

        public AppViewModelWithValidation() : base(new AppViewModelValidator())
        {
            SomeAction = ReactiveCommand.Create(() =>
            {
                Fullname = $"{Forename} {Surname}";
            }, isValid);
        }
    }
}
