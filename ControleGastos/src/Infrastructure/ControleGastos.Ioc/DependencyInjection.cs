using ControleGastos.Application.Handlers.Categorias;
using ControleGastos.Application.Handlers.Categorias.Interface;
using ControleGastos.Application.Handlers.Pessoas;
using ControleGastos.Application.Handlers.Pessoas.Interfaces;
using ControleGastos.Application.Handlers.Transacoes;
using ControleGastos.Application.Handlers.Transacoes.Interfaces;
using ControleGastos.Domain.Contexts.Categorias.Interfaces;
using ControleGastos.Domain.Contexts.Pessoas.Interfaces;
using ControleGastos.Domain.Contexts.Transacoes.Interfaces;
using ControleGastos.Infra.Data.Respositories.Categorias;
using ControleGastos.Infra.Data.Respositories.Pessoas;
using ControleGastos.Infra.Data.Respositories.Transacoes;
using ControleGastos.Ioc.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleGastos.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            //handlers
            services.AddScoped<IPessoaHandler, PessoaHandler>();
            services.AddScoped<ICategoriaHandler, CategoriaHandler>();
            services.AddScoped<ITransacaoHandler, TransacaoHandler>();

            //repositories
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();

            //dbcontext
            services.RegisterDbContext(configuration);

            return services;
        }
    }
}
