# ReactiveValidationObject

It is a small library for [ReactiveUI](https://reactiveui.net/) to support XAML Binding validation using [\*FluentValidation](https://fluentvalidation.net/)

- ReactiveUI is one of the most powerful framworks for MVVM.
- It has a original validation library ([ReactiveUI.Validation](https://reactiveui.net/docs/handbook/user-input-validation/)), It is very simple to use and well functioned.
- But It does not support `INotifyDataErrorInfo` interface for XAML binding engine to display validation erros in your view.
- So, I wrap the base ViewModel (ReactiveObject) with a popular validation library [\*FluentValidation](https://fluentvalidation.net/)
- To XAML binding, we should initialize the DataContext of View. Andthen we use some traditional XAML markup bindings (only some properties that we want to validate).

> CAUTION
> If you HATE to use XAML markup bindings. It's NOT for you.

## Get Started

### 1. Installation

ReactiveValidationObject can be installed using the Nuget package manager or the dotnet CLI.

> Install-Package ReactiveValidationObject

Or you can just copy the `ReactiveValidationObject.cs` into your project.

After that you should get [\*FluentValidation](https://fluentvalidation.net/) pakage.

### 2. Create your validator

I use a popular validation library \*FluentValidation. Therefore you can learn how to validate - [https://fluentvalidation.net/](https://fluentvalidation.net/)

So this thime. I just show a how to use my library only.

this is our first ViewModel class:

```csharp
	using ReactiveUI;

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
```