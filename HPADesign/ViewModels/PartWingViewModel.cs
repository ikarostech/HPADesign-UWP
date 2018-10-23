using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using HPADesign.Models;

namespace HPADesign.ViewModels
{
    public class PartWingViewModel : IDisposable
    {
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public PartWing Model { get; }

        [Required]
        public ReactiveProperty<int> Length { get; }

        private Subject<Unit> CommitTrigger { get; } = new Subject<Unit>();

        private IObservable<Unit> CommitAsObservable => this.CommitTrigger
            .ToUnit();

        public PartWingViewModel(PartWing model)
        {
            this.Model = model;
            this.Length = this.Model
                .ObserveProperty(x => x.Length)
                .ToReactiveProperty()
                .SetValidateAttribute(() => this.Length)
                .AddTo(this.Disposable);
        }

        public void Commit() => this.CommitTrigger.OnNext(Unit.Default);

        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
