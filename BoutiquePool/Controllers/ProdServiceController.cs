using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoutiquePool.Models.Configurations.AWS;
using BoutiquePool.Models.Configurations.MongoDB;
using BoutiquePool.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiquePool.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ProdServiceController : Controller
    {
        protected Repositories.MongoDB.PersistentRepository<Entities.Person> personRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.ProdService> prodServiceRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Address> addressRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.TableAux.ImageProdService> imageProdServiceRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.TableAux.ServEstabService> servEstabServiceRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.TableAux.EnquadraService> enquadraServiceRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento> tipoServicoEstabelecimentoRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.TipoOferta> tipoOfertaRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.Pilar> pilarRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.ProdServ> prodServRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabItem> serviceEstabItemRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabView> serviceEstabViewRepository;
        protected Repositories.MongoDB.PersistentRepository<Entities.OpeningHours> openingHoursRepository;


        protected Repositories.MongoDB.PersistentRepository<Entities.Precificacao> precificacaoRepository;






        protected Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository;

        protected ProdServiceService prodServiceService;
        protected MediaService mediaService;


        public ProdServiceController(DatabaseSettings databaseSettings, S3Configuration s3Configuration, Credentials credentials)
        {
            prodServiceRepository = new Repositories.MongoDB.PersistentRepository<Entities.ProdService>(databaseSettings, "prod_servico");
            workerRepository = new Repositories.MongoDB.PersistentRepository<Entities.Worker>(databaseSettings, "worker");
            tipoOfertaRepository = new Repositories.MongoDB.PersistentRepository<Entities.TipoOferta>(databaseSettings, "tipo_oferta");
            addressRepository = new Repositories.MongoDB.PersistentRepository<Entities.Address>(databaseSettings, "address");
            mediaRepository = new Repositories.MongoDB.PersistentRepository<Entities.Media>(databaseSettings, "media");
            personRepository = new Repositories.MongoDB.PersistentRepository<Entities.Person>(databaseSettings, "person");
            prodServRepository = new Repositories.MongoDB.PersistentRepository<Entities.ProdServ>(databaseSettings, "produto_servico");
            tipoServicoEstabelecimentoRepository = new Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento>(databaseSettings, "tipo_servico_estabelecimento");
            pilarRepository = new Repositories.MongoDB.PersistentRepository<Entities.Pilar>(databaseSettings, "pilar");
            serviceEstabItemRepository = new Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabItem>(databaseSettings, "service-estab-item");
            serviceEstabViewRepository = new Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabView>(databaseSettings, "service-estab-view");
            openingHoursRepository = new Repositories.MongoDB.PersistentRepository<Entities.OpeningHours>(databaseSettings, "opening_hours");


            precificacaoRepository = new Repositories.MongoDB.PersistentRepository<Entities.Precificacao>(databaseSettings, "precificacao");




            imageProdServiceRepository = new Repositories.MongoDB.PersistentRepository<Entities.TableAux.ImageProdService>(databaseSettings, "image_prod_service");
            servEstabServiceRepository = new Repositories.MongoDB.PersistentRepository<Entities.TableAux.ServEstabService>(databaseSettings, "serv_estab_service");
            enquadraServiceRepository = new Repositories.MongoDB.PersistentRepository<Entities.TableAux.EnquadraService>(databaseSettings, "enquadra_service");

            prodServiceService = new ProdServiceService(prodServiceRepository, workerRepository, imageProdServiceRepository, prodServRepository, tipoServicoEstabelecimentoRepository, servEstabServiceRepository, tipoOfertaRepository, pilarRepository, enquadraServiceRepository, serviceEstabItemRepository, serviceEstabViewRepository, addressRepository, precificacaoRepository, openingHoursRepository);
            mediaService = new MediaService(mediaRepository, credentials, s3Configuration);


        }

        [HttpPost("{idUser}")]
        public ActionResult<Entities.ProdService> Create(string idUser, Models.ProdServ prodService)
        {

            var worker = workerRepository.FirstOrDefault(w => w.IdUser == idUser);
            if (worker == null)
                return NotFound(new
                {
                    error = "O id inserido, não corresponde a nenhuma conta",
                    title = "PERSON_NOT_FOUND",
                    message = "O id inserido, não corresponde a nenhuma conta, então não conseguiremos cadastrar um novo serviço com esse id",
                    status = 404,
                    instance = "/prodService/{idUser}"
                });

            var address = addressRepository.FirstOrDefault(a => a.IdWorker == worker.id);
            if (address == null)
                return NotFound(new
                {
                    error = "O id worker inserido, não corresponde a nenhum endereço ",
                    title = "ADDRESS_NOT_FOUND",
                    message = "O id worker inserido, não corresponde a nenhum endereço, então não conseguiremos cadastrar um novo serviço com esse id worker",
                    status = 404,
                    instance = "/prodService/{idUser}"
                });



            prodService.id_worker = worker.id;
            prodService.id_address = address.id;
            prodService.lat = address.Lat;
            prodService.lon = address.Lon;

            var serviceCreate = prodServiceService.Create(prodService);



            return serviceCreate;
        }


        [HttpPost("item-service/{idService}")]
        public ActionResult<Entities.ServiceEstabItem> CreateServiceEstabItem(string idService)
        {
            var serviceEstab = prodServiceRepository.FirstOrDefault(p => p.id == idService);

            return prodServiceService.CreateServiceEstabItem(serviceEstab, idService);
        }



        [HttpPost("item-view/{idService}")]
        public ActionResult<Entities.ServiceEstabView> CreateServiceEstabView(string idService)
        {
            var serviceEstab = prodServiceRepository.FirstOrDefault(p => p.id == idService);

            return prodServiceService.CreateServiceEstabView(serviceEstab, idService);
        }

        [HttpGet("item-view/{idService}")]
        public ActionResult<Entities.ServiceEstabView> GetServiceEstabView(string idService)
        {
            var serviceEstabView = prodServiceService.GetServiceEstabView(idService);

            if (serviceEstabView == null)
            {
                var serviceEstab = prodServiceRepository.FirstOrDefault(p => p.id == idService);
                serviceEstabView = prodServiceService.CreateServiceEstabView(serviceEstab, idService);
                return serviceEstabView;
            }
            return serviceEstabView;
        }



        [HttpGet("{index}")]
        public ActionResult<List<Entities.ServiceEstabItem>> GetServicesEstabs(int index)
        {
            return prodServiceService.GetServicesEstabs(index);
        }
        [HttpGet("services-pending")]
        public ActionResult<List<Entities.ServiceEstabItem>> GetServicesEstabsPendings()
        {
            return prodServiceService.GetServicesEstabsPending();
        }

        [HttpPut("active-service-pending/{id}")]
        public ActionResult ActiveServicePending(string id)
        {
            prodServiceService.ActiveServicePending(id);

            return Ok();
        }



        [HttpPost("filter")]
        public ActionResult<List<Entities.ServiceEstabItem>> GetServicesEstabsFilter(Models.Filter filter)
        {
            return prodServiceService.GetServicesEstabsFilter(filter);
        }


      


        [HttpPost("send-picture/{idUser}/{idService}")]
        public ActionResult<Entities.Media> SendFile(string idUser, string idService, IFormFile file)
        {

            var worker = workerRepository.FirstOrDefault(w => w.IdUser == idUser);
            if (worker == null)
                return this.NotFound(new
                {
                    error = "O id inserido não corresponde a nenhuma conta",
                    title = "ACCOUNT_NOT_FOUND",
                    message = "O id inserido, não está associado a nenhuma conta, então não conseguiremos obter a conta",
                    status = 401,
                    instance = "/worker/send-picture/{idWorker}"
                });

            var newImageProdService = new Entities.TableAux.ImageProdService();



            var info = new FileInfo(file.FileName);
            var extension = info.Extension.ToLower().Replace(".", "");
            var type = mediaService.GetTypeByExtension(extension);
            if (type == Entities.Media.Types.Unknown)
            {
                return BadRequest("INVALID_EXTENSIONS");
            }


            var media = mediaService.Create(file.OpenReadStream(), type, extension, idService);

            newImageProdService.IdProdService = idService;
            newImageProdService.Url = media.Url;

            imageProdServiceRepository.Create(newImageProdService);


       
            return media;

        }


    }
}