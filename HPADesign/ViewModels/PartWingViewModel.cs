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

namespace HPADesign.ViewModels
{
    class PartWingViewModel : IDisposable
    {
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        private Subject<Unit> CommitTrigger { get; } = new Subject<Unit>();

        public void Commit() => this.CommitTrigger.OnNext(Unit.Default);

        public void Dispose()
        {
            this.Disposable.Dispose();
        }
    }
}
