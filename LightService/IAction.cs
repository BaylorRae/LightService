using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightService {
    public interface IAction<T> {
        T Executed(T context);
    }
}
