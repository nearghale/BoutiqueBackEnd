using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class ServiceService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.Pilar> _pilarRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.TipoOferta> _tipoOfertaRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.ProdServ> _produtoServicoRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Precificacao> _precificacaoRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento> _tipoServicoEstabelecimentoRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Enquadramento> _enquadramentoRepository;




        public ServiceService(Repositories.MongoDB.PersistentRepository<Entities.Pilar> pilarRepository,
                              Repositories.MongoDB.PersistentRepository<Entities.TipoOferta> tipoOfertaRepository,
                              Repositories.MongoDB.PersistentRepository<Entities.ProdServ> produtoServicoRepository,
                              Repositories.MongoDB.PersistentRepository<Entities.Precificacao> precificacaoRepository,
                              Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento> tipoServicoEstabelecimentoRepository,
                              Repositories.MongoDB.PersistentRepository<Entities.Enquadramento> enquadramentoRepository)
        {

            _pilarRepository = pilarRepository;
            _tipoOfertaRepository = tipoOfertaRepository;
            _produtoServicoRepository = produtoServicoRepository;
            _precificacaoRepository = precificacaoRepository;
            _tipoServicoEstabelecimentoRepository = tipoServicoEstabelecimentoRepository;
            _enquadramentoRepository = enquadramentoRepository;


        }

        public Entities.Pilar CreatePilar(Entities.Pilar pilar)
        {
            var newPilar = new Entities.Pilar();

            newPilar.IdCorePilar = pilar.IdCorePilar;
            newPilar.StCorePilar = pilar.StCorePilar;
            newPilar.Icon = pilar.Icon;
            newPilar.ColorPrimary = pilar.ColorPrimary;
            newPilar.ColorSecundary = pilar.ColorSecundary;

            return _pilarRepository.Create(newPilar);

        }

        public Entities.TipoOferta CreateTipoOferta(Entities.TipoOferta tipoOferta)
        {
            var newTipoOferta = new Entities.TipoOferta();

            newTipoOferta.IdCoreTipoOferta = tipoOferta.IdCoreTipoOferta;
            newTipoOferta.StCoreTipoOferta = tipoOferta.StCoreTipoOferta;

            return _tipoOfertaRepository.Create(newTipoOferta);

        }

        public Entities.ProdServ CreateProdutoServico(Entities.ProdServ produtoServico)
        {
            var newProdutoServico = new Entities.ProdServ();

            newProdutoServico.Active = true;
            newProdutoServico.FgTipoVenda = produtoServico.FgTipoVenda;
            newProdutoServico.FgPilarTudo = produtoServico.FgPilarTudo;
            newProdutoServico.FgPilarEnergia = produtoServico.FgPilarEnergia;
            newProdutoServico.FgPilarHidrico = produtoServico.FgPilarHidrico;
            newProdutoServico.FgPilarResiduos = produtoServico.FgPilarResiduos;
            newProdutoServico.FgPilarSustentavel = produtoServico.FgPilarSustentavel;
            newProdutoServico.IdCoreProdutoServico = produtoServico.IdCoreProdutoServico;
            newProdutoServico.StCoreProdutoServico = produtoServico.StCoreProdutoServico;


            return _produtoServicoRepository.Create(newProdutoServico);

        }

        public Entities.Precificacao CreatePrecificacao(Entities.Precificacao precificacao)
        {
            var newPrecificacao = new Entities.Precificacao();

            newPrecificacao.Active = true;
            newPrecificacao.FgTipoVenda = precificacao.FgTipoVenda;
            newPrecificacao.FgPilarTudo = precificacao.FgPilarTudo;
            newPrecificacao.FgPilarEnergia = precificacao.FgPilarEnergia;
            newPrecificacao.FgPilarHidrico = precificacao.FgPilarHidrico;
            newPrecificacao.FgPilarResiduos = precificacao.FgPilarResiduos;
            newPrecificacao.FgPilarSustentavel = precificacao.FgPilarSustentavel;
            newPrecificacao.IdCorePrecificacao = precificacao.IdCorePrecificacao;
            newPrecificacao.StCorePrecificacao = precificacao.StCorePrecificacao;


            return _precificacaoRepository.Create(newPrecificacao);

        }

        public Entities.TipoServicoEstabelecimento CreateTipoServicoEstabelecimento(Entities.TipoServicoEstabelecimento tipoServicoEstabelecimento)
        {
            var newTipoServicoEstabelecimento = new Entities.TipoServicoEstabelecimento();

            newTipoServicoEstabelecimento.Active = true;
            newTipoServicoEstabelecimento.FgTipoVenda = tipoServicoEstabelecimento.FgTipoVenda;
            newTipoServicoEstabelecimento.FgPilarTudo = tipoServicoEstabelecimento.FgPilarTudo;
            newTipoServicoEstabelecimento.FgPilarEnergia = tipoServicoEstabelecimento.FgPilarEnergia;
            newTipoServicoEstabelecimento.FgPilarHidrico = tipoServicoEstabelecimento.FgPilarHidrico;
            newTipoServicoEstabelecimento.FgPilarResiduos = tipoServicoEstabelecimento.FgPilarResiduos;
            newTipoServicoEstabelecimento.FgPilarSustentavel = tipoServicoEstabelecimento.FgPilarSustentavel;
            newTipoServicoEstabelecimento.IdCoreTipoServicoEstabelecimento = tipoServicoEstabelecimento.IdCoreTipoServicoEstabelecimento;
            newTipoServicoEstabelecimento.StCoreTipoServicoEstabelecimento = tipoServicoEstabelecimento.StCoreTipoServicoEstabelecimento;


            return _tipoServicoEstabelecimentoRepository.Create(newTipoServicoEstabelecimento);

        }

        public Entities.Enquadramento CreateEnquadramento(Entities.Enquadramento enquadramento)
        {
            var newEnquadramento = new Entities.Enquadramento();

            newEnquadramento.Active = true;
            newEnquadramento.FgTipoVenda = enquadramento.FgTipoVenda;
            newEnquadramento.FgPilarTudo = enquadramento.FgPilarTudo;
            newEnquadramento.FgPilarEnergia = enquadramento.FgPilarEnergia;
            newEnquadramento.FgPilarHidrico = enquadramento.FgPilarHidrico;
            newEnquadramento.FgPilarResiduos = enquadramento.FgPilarResiduos;
            newEnquadramento.FgPilarSustentavel = enquadramento.FgPilarSustentavel;
            newEnquadramento.IdCoreEnquadramento = enquadramento.IdCoreEnquadramento;
            newEnquadramento.StCoreEnquadramento = enquadramento.StCoreEnquadramento;


            return _enquadramentoRepository.Create(newEnquadramento);

        }




    }
}
