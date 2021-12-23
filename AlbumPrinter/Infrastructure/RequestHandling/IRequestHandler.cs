using System.Threading;
using System.Threading.Tasks;

namespace AlbumPrinter.Infrastructure.RequestHandling
{
    public interface IRequestHandler<TDto, TResponse>
    {
        public Task<TResponse> HandleAsync(TDto request, CancellationToken cancellationToken = default);
    }
}
