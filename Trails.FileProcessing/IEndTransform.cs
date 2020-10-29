using System;
using System.Threading.Tasks;

namespace Trails.FileProcessing
{
    public interface IEndTransform<in TInput, in TContext>
    {

        void transform(TInput input, TContext context);
    }
}
