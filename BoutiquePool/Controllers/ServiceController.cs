using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiquePool.Models.Configurations.MongoDB;
using BoutiquePool.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoutiquePool.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Pilar> pilarRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.TipoOferta> tipoOfertaRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.ProdServ> prodServRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Precificacao> precificacaoRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento> tipoServicoEstabelecimentoRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Enquadramento> enquadramentoRepository;




        protected ServiceService serviceService;

        public ServiceController(DatabaseSettings databaseSettings)
        {
            pilarRepository = new Repositories.MongoDB.PersistentRepository<Entities.Pilar>(databaseSettings, "core_branch");
            tipoOfertaRepository = new Repositories.MongoDB.PersistentRepository<Entities.TipoOferta>(databaseSettings, "core_offer_type");
            prodServRepository = new Repositories.MongoDB.PersistentRepository<Entities.ProdServ>(databaseSettings, "core_product_service");
            precificacaoRepository = new Repositories.MongoDB.PersistentRepository<Entities.Precificacao>(databaseSettings, "core_pricing");
            tipoServicoEstabelecimentoRepository = new Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento>(databaseSettings, "core_offer_category");
            enquadramentoRepository = new Repositories.MongoDB.PersistentRepository<Entities.Enquadramento>(databaseSettings, "core_service_group");

            serviceService = new ServiceService(pilarRepository, tipoOfertaRepository, prodServRepository, precificacaoRepository, tipoServicoEstabelecimentoRepository, enquadramentoRepository);

        }

        [HttpPost("pilar")]
        public ActionResult<Entities.Pilar> CreatePilar(Entities.Pilar pilar)
        {
            var newPilar = pilarRepository.FirstOrDefault(p => p.StCorePilar == pilar.StCorePilar);
            if(newPilar != null)
                return Unauthorized("PILAR_ALREADY_EXISTS");

            return serviceService.CreatePilar(pilar);
        }

        [HttpPost("tipo-oferta")]
        public ActionResult<Entities.TipoOferta> CreateTipoOferta(Entities.TipoOferta tipoOferta)
        {
            var newTipoOferta = tipoOfertaRepository.FirstOrDefault(t => t.IdCoreTipoOferta == tipoOferta.IdCoreTipoOferta);
            if (newTipoOferta != null)
                return Unauthorized("TIPO_OFERTA_ALREADY_EXISTS");

            return serviceService.CreateTipoOferta(tipoOferta);
        }

        [HttpPost("produto-servico")]
        public ActionResult<Entities.ProdServ> CreateProdutoServico(Entities.ProdServ produtoServico)
        {
            var newProdutoServico = prodServRepository.FirstOrDefault(p => p.IdCoreProdutoServico == produtoServico.IdCoreProdutoServico);
            if (newProdutoServico != null)
                return Unauthorized("PRODUTO_SERVICO_ALREADY_EXISTS");

            return serviceService.CreateProdutoServico(produtoServico);
        }

        [HttpPost("precificacao")]
        public ActionResult<Entities.Precificacao> CreatePrecificacao(Entities.Precificacao precificacao)
        {
            var newPrecificacao = precificacaoRepository.FirstOrDefault(p => p.IdCorePrecificacao == precificacao.IdCorePrecificacao);
            if (newPrecificacao != null)
                return Unauthorized("PRECIFICACAO_ALREADY_EXISTS");

            return serviceService.CreatePrecificacao(precificacao);
        }

        [HttpPost("tipo-servico-estabelecimento")]
        public ActionResult<Entities.TipoServicoEstabelecimento> CreateTipoServicoEstabelecimento(Entities.TipoServicoEstabelecimento tipoServicoEstabelecimento)
        {
            var newTipoServicoEstabelecimento = tipoServicoEstabelecimentoRepository.FirstOrDefault(t => t.IdCoreTipoServicoEstabelecimento == tipoServicoEstabelecimento.IdCoreTipoServicoEstabelecimento);
            if (newTipoServicoEstabelecimento != null)
                return Unauthorized("TIPO_SERVICO_ESTABELECIMENTO_ALREADY_EXISTS");

            return serviceService.CreateTipoServicoEstabelecimento(tipoServicoEstabelecimento);
        }

        [HttpPost("enquadramento")]
        public ActionResult<Entities.Enquadramento> CreateEnquadramento(Entities.Enquadramento enquadramento)
        {
            var newEnquadramento = enquadramentoRepository.FirstOrDefault(e => e.IdCoreEnquadramento == enquadramento.IdCoreEnquadramento);
            if (newEnquadramento != null)
                return Unauthorized("ENQUADRAMENTO_ALREADY_EXISTS");

            return serviceService.CreateEnquadramento(enquadramento);
        }

        [HttpGet("pilar")]
        public ActionResult<List<Entities.Pilar>> GetPilar()
        {

            return pilarRepository.Find(f => true);

        }
        [HttpGet("tipo-oferta")]
        public ActionResult<List<Entities.TipoOferta>> GetTipoOferta()
        {

            return tipoOfertaRepository.Find(f => true);

        }


        [HttpGet("produto-servico/{idCorePilar}/{tipoVenda}")]
        public ActionResult<List<Entities.ProdServ>> GetProdutoServico(int idCorePilar, int tipoVenda)
        {

            List<Entities.ProdServ> produtosServicos = null;

            switch (idCorePilar)
            {
                case 1:
                    produtosServicos = prodServRepository.Find(p => p.FgPilarEnergia == 1 && p.FgTipoVenda == tipoVenda);
                    break;                    
                case 2:
                    produtosServicos = prodServRepository.Find(p => p.FgPilarHidrico == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 3:
                    produtosServicos = prodServRepository.Find(p => p.FgPilarResiduos == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 4:
                    produtosServicos = prodServRepository.Find(p => p.FgPilarSustentavel == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                default:
                    
                    break;
            }

            return produtosServicos;


        }

        [HttpGet("precificacao/{idCorePilar}/{tipoVenda}")]
        public ActionResult<List<Entities.Precificacao>> GetPrecificacao(int idCorePilar, int tipoVenda)
        {

            List<Entities.Precificacao> precificacaos = null;

            switch (idCorePilar)
            {
                case 1:
                    precificacaos = precificacaoRepository.Find(p => p.FgPilarEnergia == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 2:
                    precificacaos = precificacaoRepository.Find(p => p.FgPilarHidrico == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 3:
                    precificacaos = precificacaoRepository.Find(p => p.FgPilarResiduos == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 4:
                    precificacaos = precificacaoRepository.Find(p => p.FgPilarSustentavel == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                default:

                    break;
            }

            return precificacaos;

        }

        [HttpGet("tipo-servico-estabelecimento/{idCorePilar}/{tipoVenda}")]
        public ActionResult<List<Entities.TipoServicoEstabelecimento>> GetTipoServicoEstabelecimento(int idCorePilar, int tipoVenda)
        {

            List<Entities.TipoServicoEstabelecimento> tipoServicoEstabelecimentos = null;

            switch (idCorePilar)
            {
                case 1:
                    tipoServicoEstabelecimentos = tipoServicoEstabelecimentoRepository.Find(p => p.FgPilarEnergia == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 2:
                    tipoServicoEstabelecimentos = tipoServicoEstabelecimentoRepository.Find(p => p.FgPilarHidrico == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 3:
                    tipoServicoEstabelecimentos = tipoServicoEstabelecimentoRepository.Find(p => p.FgPilarResiduos == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 4:
                    tipoServicoEstabelecimentos = tipoServicoEstabelecimentoRepository.Find(p => p.FgPilarSustentavel == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                default:

                    break;
            }

            return tipoServicoEstabelecimentos;

        }

        [HttpGet("enquadramento/{idCorePilar}/{tipoVenda}")]
        public ActionResult<List<Entities.Enquadramento>> GetEnquadramento(int idCorePilar, int tipoVenda)
        {

            List<Entities.Enquadramento> enquadramentos = null;

            switch (idCorePilar)
            {
                case 1:
                    enquadramentos = enquadramentoRepository.Find(p => p.FgPilarEnergia == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 2:
                    enquadramentos = enquadramentoRepository.Find(p => p.FgPilarHidrico == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 3:
                    enquadramentos = enquadramentoRepository.Find(p => p.FgPilarResiduos == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                case 4:
                    enquadramentos = enquadramentoRepository.Find(p => p.FgPilarSustentavel == 1 && p.FgTipoVenda == tipoVenda);
                    break;
                default:

                    break;
            }

            return enquadramentos;

        }



    }
}