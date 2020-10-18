using System;
using System.Threading.Tasks;

namespace Trails.FileProcessing
{
    public interface ITransform<in TInput, out TOutput, in TContext>
    {

        TOutput transform(TInput input, TContext content);
    }
}
