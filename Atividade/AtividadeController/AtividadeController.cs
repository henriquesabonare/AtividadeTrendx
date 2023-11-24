using Atividade.Core.AtividadeModel.Repository;
using Atividade.Core.AtividadeModel.Service;
using AutoMapper;
using DinkToPdf.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sefaz.Client.Models;
using Sefaz.ECPF.API.ViewModels.Agendamentos;
using Sefaz.ECPF.API.ViewModels.LayoutsPDF;
using Sefaz.ECPF.Core.Aggregates.AdministraTextos.Services;
using Sefaz.ECPF.Core.Aggregates.Agendamentos;
using Sefaz.ECPF.Core.Aggregates.Agendamentos.DTO.EmissaoCertificado;
using Sefaz.ECPF.Core.Aggregates.Agendamentos.Repository;
using Sefaz.ECPF.Core.Aggregates.Agendamentos.Services;
using Sefaz.ECPF.Core.Aggregates.SendMail.DTO;
using Sefaz.ECPF.Core.Integration.WebServiceViaCEP.Services;
using Sefaz.Shared.Kernel.API.Controllers;
using Sefaz.Shared.Kernel.API.Services;
using Sefaz.Shared.Kernel.Data;
using Sefaz.Shared.Kernel.Domain.Events;
using Sefaz.Shared.Kernel.Mediator;

namespace Sefaz.ECPF.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class AtividadeController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IAtividadeRepository _repository;
        private readonly IAtividadeService _atividadeService;
        private readonly ILogger<AtividadeController> _logger;
        private IConverter _converter;

        public AgendamentoController(INotificationHandler<DomainNotificationEvent> notifications,
                                     IMediatorHandler mediator,
                                     IMapper mapper,
                                     IAtividadeRepository repository,
                                     IAtividadeService atividadeService,
                                     IUnitOfWork unitOfWork,
                                     ILogger<AtividadeController> logger,
                                     IConverter converter)
            : base(unitOfWork, notifications, mediator)
        {
            _mapper = mapper;
            _repository = repository;
            _atividadeService = atividadeService;
            _viewService = viewService;
            _logger = logger;
            _converter = converter;
        }

        #region Emissão de Certificado Digital

        [HttpGet]
        public async Task<IActionResult> obtemTodasAtividades()
        {
            //throw new Exception("Erro Aconteceu");
            _logger.LogInformation("Executando pesquisa api/tasks");
            return Response(await _repository.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> adicionaAtividade([FromBody] atividadeViewModel atividadeViewModel)
        {
            _logger.LogInformation($"Executando post api/tasks/{atividadeViewModel}");
            var atividade = _mapper.Map<Atividade>(atividadeViewModel);
            await _service.AddAsync(atividade);
            await CommitAsync();
            return Response(atividade);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> atualizaAtividade(int id)
        {
            _logger.LogInformation($"Executando put api/tasks/{id}");
            await _service.Update(id);
            await CommitAsync();
            return Response();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAgendamentoAsync(int id)
        {
            _logger.LogInformation($"Executando delete api/tasks/{id}");
            await _repository.RemoveAsync(id);
            await CommitAsync();
            return Response();
        }
        #endregion
    }
}

