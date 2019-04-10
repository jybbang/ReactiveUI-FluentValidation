using FluentValidation;
using FluentValidation.Results;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace ReactiveUI.FluentValidation
{
    public class ReactiveValidationObject : ReactiveObject, INotifyDataErrorInfo
    {
        #region Events
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        #endregion

        #region Fields
        private readonly IValidator validator;
        private IEnumerable<ValidationFailure> Errors;
        private bool _hasErrors;
        private bool _isValid;

        protected readonly IObservable<bool> isValid;
        #endregion

        #region Properties
        public bool HasErrors { get => _hasErrors; private set => this.RaiseAndSetIfChanged(ref _hasErrors, value); }
        public bool IsValid { get => _isValid; private set => this.RaiseAndSetIfChanged(ref _isValid, value); }
        #endregion

        #region Constructors
        public ReactiveValidationObject(IValidator _validator)
        {
            validator = _validator ?? throw new ArgumentNullException(nameof(_validator));
            isValid = this.WhenAnyValue(x => x.IsValid)
                .Skip(1)
                .Select(_ => !HasErrors)
                .StartWith(false);
        }
        #endregion

        #region Public Methods
        public IEnumerable GetErrors(string propertyName)
            => Errors.Where(x => x.PropertyName == propertyName);

        protected void RaiseValidation(params string[] propertyName)
        {
            var ret = validator.Validate(this);
            Errors = ret.Errors;
            HasErrors = ret.Errors.Count > 0;
            IsValid = ret.IsValid;
            foreach (var item in propertyName)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(item));
            }
        }
        #endregion
    }
}