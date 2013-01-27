using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuroph.Util
{
    public interface IObserver
    {
        void Update(IObservable arg0, Object arg1);
    }
}
