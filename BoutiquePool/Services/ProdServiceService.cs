using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiquePool.Services
{
    public class ProdServiceService
    {
        private Repositories.MongoDB.PersistentRepository<Entities.TableAux.ServEstabService> _servEstabRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.TableAux.EnquadraService> _enquadraRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.TableAux.ImageProdService> _imageProdServiceRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.ProdServ> _produtoServicoRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento> _tipoServicoEstabelecimentoRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.TipoOferta> _tipoOfertaRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Pilar> _pilarRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabItem> _serviceEstabItemRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabView> _serviceEstabViewRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.OpeningHours> _openingHoursRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Enquadramento> _enquadramentoRepository;

        private Repositories.MongoDB.PersistentRepository<Entities.Precificacao> _precificacaoRepository;

        private Repositories.MongoDB.PersistentRepository<Entities.Worker> _workerRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.ProdService> _prodServiceRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Address> _addressRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Other> _otherRepository;




        public ProdServiceService(Repositories.MongoDB.PersistentRepository<Entities.ProdService> prodServiceRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.Worker> workerRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.TableAux.ImageProdService> imageProdServiceRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.ProdServ> produtoServicoRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.TipoServicoEstabelecimento> tipoServicoEstabelecimentoRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.TableAux.ServEstabService> servEstabReposito,
                                  Repositories.MongoDB.PersistentRepository<Entities.TipoOferta> tipoOfertaRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.Pilar> pilarRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.TableAux.EnquadraService> enquadraRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabItem> serviceEstabItemRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.ServiceEstabView> serviceEstabViewRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.Address> addressRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.Precificacao> precificacaoRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.OpeningHours> openingHoursRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.Enquadramento> enquadramentoRepository,
                                  Repositories.MongoDB.PersistentRepository<Entities.Other> otherRepository
                                  )
        {
            _prodServiceRepository = prodServiceRepository;
            _servEstabRepository = servEstabReposito;
            _enquadraRepository = enquadraRepository;
            _imageProdServiceRepository = imageProdServiceRepository;
            _produtoServicoRepository = produtoServicoRepository;
            _tipoServicoEstabelecimentoRepository = tipoServicoEstabelecimentoRepository;
            _tipoOfertaRepository = tipoOfertaRepository;
            _pilarRepository = pilarRepository;
            _workerRepository = workerRepository;
            _serviceEstabItemRepository = serviceEstabItemRepository;
            _serviceEstabViewRepository = serviceEstabViewRepository;

            _addressRepository = addressRepository;
            _precificacaoRepository = precificacaoRepository;
            _openingHoursRepository = openingHoursRepository;
            _enquadramentoRepository = enquadramentoRepository;
            _otherRepository = otherRepository;





        }

        public Entities.ProdService Create(Models.ProdServ prodService)
        {
            var newProdService = new Entities.ProdService();

            newProdService.IdWorker = prodService.id_worker;
            newProdService.IdAddress = prodService.id_address;
            newProdService.IdCoreProdServ = prodService.id_core_prod_serv;
            newProdService.IdCorePilar = prodService.id_core_pilar;
            newProdService.IdCoreTipoOferta = prodService.id_core_tipo_oferta;
            newProdService.IdCorePrecificacao = prodService.id_core_precificacao;
            newProdService.Medida = prodService.medida;
            newProdService.Descricao = prodService.descricao;
            newProdService.Valor = prodService.valor;
            newProdService.Lat = prodService.lat;
            newProdService.Lon = prodService.lon;
            newProdService.DateRegister = DateTime.Now;
            newProdService.Active = true;
            newProdService.Status = "pending";


            var prodServiceCreate = _prodServiceRepository.Create(newProdService);


          


            for (int c = 0; c < prodService.itens_serv_estab.Count; c++)
            {
                var newItemServEstab = new Entities.TableAux.ServEstabService();
                newItemServEstab.IdProdService = prodServiceCreate.id;
                newItemServEstab.IdCoreTipoServicoEstabelecimento = prodService.itens_serv_estab[c];
                _servEstabRepository.Create(newItemServEstab);

          
               

            }

            for (int c = 0; c < prodService.itens_enquadra.Count; c++)
            {
                var newItemEnquadra = new Entities.TableAux.EnquadraService();
                newItemEnquadra.IdProdService = prodServiceCreate.id;
                newItemEnquadra.IdCoreEnquadramento = prodService.itens_enquadra[c];
                _enquadraRepository.Create(newItemEnquadra);

            

            }

            int verificationExistsOtherServEstab = 0;


            for (int c = 0; c < prodService.itens_serv_estab.Count; c++)
            {
                if(prodService.itens_serv_estab[c] == 14)
                    verificationExistsOtherServEstab = prodService.itens_serv_estab[c];
                
            }

            int verificationExistsOtherItenEnquadra = 0;


            for (int c = 0; c < prodService.itens_enquadra.Count; c++)
            {
                if (prodService.itens_serv_estab[c] == 13)
                    verificationExistsOtherItenEnquadra = prodService.itens_enquadra[c];

            }



            if (prodService.id_core_prod_serv == 18 || verificationExistsOtherServEstab == 14 || verificationExistsOtherItenEnquadra == 13)
            {
                var newOther = new Entities.Other();
                newOther.IdCoreTipoOferta = prodService.id_core_tipo_oferta;
                newOther.IdCorePilar = prodService.id_core_pilar;
                newOther.IdProdService = prodServiceCreate.id;


                newOther.CadProdServ = prodService.cad_prod_serv_other;
                newOther.CadOfferCategory = prodService.cad_offer_category_other;
                newOther.CadServiceGroup = prodService.cad_service_group_other;

                _otherRepository.Create(newOther);
            }





            return prodServiceCreate;
        }

        public List<Entities.ServiceEstabItem> GetServicesEstabs(int index)
        {
            var filter = index * 5;
            var itens = _serviceEstabItemRepository.GetXelements(filter);

            if (itens.Count() > 5)
            {
                if (itens.Count() % 5 == 0)
                {
                    var itensReturns = Enumerable.Reverse(itens).Take(5).Reverse().ToList();
                    return itensReturns.FindAll(i => i.Status == "active");
                }
                else
                {

                    var itensReturns = Enumerable.Reverse(itens).Take(itens.Count() % 5).Reverse().ToList();
                    return itensReturns.FindAll(i => i.Status == "active");

                }


            }


            return itens.FindAll(i => i.Status == "active");

        }

        public List<Entities.ServiceEstabItem> GetServicesEstabsPending()
        {

            return _serviceEstabItemRepository.Find(s => s.Status == "pending");

        }

        public void ActiveServicePending(string id)
        {
            var service = _serviceEstabItemRepository.FirstOrDefault(s => s.id == id);
            service.Status = "active";


            _serviceEstabItemRepository.Update(id, service);

        }


        public List<Entities.ServiceEstabItem> GetServicesEstabsFilter(Models.Filter filter)
        {

            var items = new List<Entities.ServiceEstabItem>();

            if (Array.Exists(filter.pilar, p => p == 1))
            {
                var itemsEnergias = new List<Entities.ServiceEstabItem>();

                if (Array.Exists(filter.type_offer, t => t == 1))
                {
                    var itemsServico = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Energias" && s.TypeOffer == "Serviço" && s.Status == "active");
                    itemsServico.ForEach((item) =>
                    {
                        itemsEnergias.Add(item);
                    });

                }

                if (Array.Exists(filter.type_offer, t => t == 2))
                {
                    var itemsEstabelecimento = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Energias" && s.TypeOffer == "Estabelecimento" && s.Status == "active");
                    itemsEstabelecimento.ForEach((item) =>
                    {
                        itemsEnergias.Add(item);
                    });

                }

                itemsEnergias.ForEach((item) =>
                {
                    items.Add(item);
                });

            }
            if (Array.Exists(filter.pilar, p => p == 2))
            {
                var itemsHidricos = new List<Entities.ServiceEstabItem>();

                if (Array.Exists(filter.type_offer, t => t == 1))
                {
                    var itemsServico = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Hídricos" && s.TypeOffer == "Serviço" && s.Status == "active");
                    itemsServico.ForEach((item) =>
                    {
                        itemsHidricos.Add(item);
                    });

                }

                if (Array.Exists(filter.type_offer, t => t == 2))
                {
                    var itemsEstabelecimento = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Hídricos" && s.TypeOffer == "Estabelecimento" && s.Status == "active");
                    itemsEstabelecimento.ForEach((item) =>
                    {
                        itemsHidricos.Add(item);
                    });

                }

                itemsHidricos.ForEach((item) =>
                {
                    items.Add(item);
                });
            }
            if (Array.Exists(filter.pilar, p => p == 3))
            {
                var itemsResiduos = new List<Entities.ServiceEstabItem>();

                if (Array.Exists(filter.type_offer, t => t == 1))
                {
                    var itemsServico = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Resíduos" && s.TypeOffer == "Serviço" && s.Status == "active");
                    itemsServico.ForEach((item) =>
                    {
                        itemsResiduos.Add(item);
                    });

                }

                if (Array.Exists(filter.type_offer, t => t == 2))
                {
                    var itemsEstabelecimento = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Resíduos" && s.TypeOffer == "Estabelecimento" && s.Status == "active");
                    itemsEstabelecimento.ForEach((item) =>
                    {
                        itemsResiduos.Add(item);
                    });

                }

                itemsResiduos.ForEach((item) =>
                {
                    items.Add(item);
                });
            }

            if (Array.Exists(filter.pilar, p => p == 4))
            {
                var itemsSustentaveis = new List<Entities.ServiceEstabItem>();

                if (Array.Exists(filter.type_offer, t => t == 1))
                {
                    var itemsServico = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Sustentáveis" && s.TypeOffer == "Serviço" && s.Status == "active");
                    itemsServico.ForEach((item) =>
                    {
                        itemsSustentaveis.Add(item);
                    });

                }

                if (Array.Exists(filter.type_offer, t => t == 2))
                {
                    var itemsEstabelecimento = _serviceEstabItemRepository.Find(s => s.Cornerstone == "Sustentáveis" && s.TypeOffer == "Estabelecimento" && s.Status == "active");
                    itemsEstabelecimento.ForEach((item) =>
                    {
                        itemsSustentaveis.Add(item);
                    });

                }

                itemsSustentaveis.ForEach((item) =>
                {
                    items.Add(item);
                });
            }

            if (filter.distance_filter != 0)
            {
                var itemsFilterDistance = new List<Entities.ServiceEstabItem>();

                items.ForEach((item) =>
                {
                    var d1 = filter.location_user.latitude * (Math.PI / 180.0);
                    var num1 = filter.location_user.longitude * (Math.PI / 180.0);
                    var d2 = item.Lat * (Math.PI / 180.0);
                    var num2 = item.Lon * (Math.PI / 180.0) - num1;
                    var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                        Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
                    var result = Convert.ToInt32((6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))) / 1000));

                    if (result < filter.distance_filter && item.Status == "active")
                        itemsFilterDistance.Add(item);

                });
                return itemsFilterDistance;


            }


            if (items == null)
                return _serviceEstabItemRepository.Find(s => s.Status == "active");

            return items;

        }

        public Entities.ServiceEstabView CreateServiceEstabView(Entities.ProdService serviceEstab, string idService)
        {
            Entities.ServiceEstabView serviceEstabView = new Entities.ServiceEstabView();

            //PRODUCT 
            serviceEstabView.IdService = idService;

            var images = _imageProdServiceRepository.Find(i => i.IdProdService == serviceEstab.id);
            List<string> listImages = new List<string>();

            if (images.Count != 0)
            {
                for (int c = 0; c < images.Count; c++)
                {
                    var url = images[c].Url;

                    listImages.Add(url);


                };
            }

            serviceEstabView.ImagesProducts = listImages;
            serviceEstabView.Medida = serviceEstab.Medida;

            var prodService = _produtoServicoRepository.FirstOrDefault(p => p.IdCoreProdutoServico == serviceEstab.IdCoreProdServ);
            serviceEstabView.NameServiceEstab = prodService.StCoreProdutoServico;
            serviceEstabView.Active = "Disponível";

            //TIPO
            var typeServiceEstab = _servEstabRepository.Find(s => s.IdProdService == serviceEstab.id);
            List<string> listItemsTypeServeEstab = new List<string>();

            for (int c = 0; c < typeServiceEstab.Count; c++)
            {
                var tipoServEstab = _tipoServicoEstabelecimentoRepository.FirstOrDefault(t => t.IdCoreTipoServicoEstabelecimento == typeServiceEstab[c].IdCoreTipoServicoEstabelecimento);
                listItemsTypeServeEstab.Add(tipoServEstab.StCoreTipoServicoEstabelecimento);
            }
            serviceEstabView.CategoryType = listItemsTypeServeEstab;

            //ATENDE
            var attendanceEstab = _enquadraRepository.Find(s => s.IdProdService == serviceEstab.id);
            List<string> listItemsAttendanceEstab = new List<string>();

            for (int c = 0; c < attendanceEstab.Count; c++)
            {
                var tipoAttendanceEstab = _enquadramentoRepository.FirstOrDefault(t => t.IdCoreEnquadramento == attendanceEstab[c].IdCoreEnquadramento);
                listItemsAttendanceEstab.Add(tipoAttendanceEstab.StCoreEnquadramento);
            }
            serviceEstabView.EnquadraType = listItemsAttendanceEstab;



            serviceEstabView.Price = serviceEstab.Valor;

            var precificacao = _precificacaoRepository.FirstOrDefault(p => p.IdCorePrecificacao == serviceEstab.IdCorePrecificacao);
            serviceEstabView.Precification = precificacao.StCorePrecificacao;

            var cornestone = _pilarRepository.FirstOrDefault(p => p.IdCorePilar == serviceEstab.IdCorePilar);
            serviceEstabView.Cornerstone = cornestone.StCorePilar;

            var typeOffer = _tipoOfertaRepository.FirstOrDefault(t => t.IdCoreTipoOferta == serviceEstab.IdCoreTipoOferta);
            serviceEstabView.TypeOffer = typeOffer.StCoreTipoOferta;


            //CONTACT
            var worker = _workerRepository.FirstOrDefault(w => w.id == serviceEstab.IdWorker);
            serviceEstabView.Facebook = worker.Facebook;
            serviceEstabView.Instagram = worker.Instagram;
            serviceEstabView.WhatsappNumber = worker.WhatsappNumber;
            serviceEstabView.Site = worker.Site;
            serviceEstabView.PhoneCorp = worker.PhoneCorp;
            serviceEstabView.CNPJ = worker.CPFCNPJ;


            //DESCRIPTION PRODUCT
            serviceEstabView.Description = serviceEstab.Descricao;

            //COMPANY NAME
            serviceEstabView.CompanyName = worker.SocialReason;
            serviceEstabView.CompanyDescription = worker.Description;

            var addressUser = _addressRepository.FirstOrDefault(a => a.IdWorker == worker.id);

            var openingHourUser = _openingHoursRepository.FirstOrDefault(o => o.IdAdress == addressUser.id);

            //ATTENDANCE
            serviceEstabView.AllDays = openingHourUser.AllDays;
            serviceEstabView.DayFriday = openingHourUser.DayFriday;
            serviceEstabView.DayMonday = openingHourUser.DayMonday;
            serviceEstabView.DaySaturday = openingHourUser.DaySaturday;
            serviceEstabView.DaySunday = openingHourUser.DaySunday;
            serviceEstabView.DayThursday = openingHourUser.DayThursday;
            serviceEstabView.DayTuesday = openingHourUser.DayTuesday;
            serviceEstabView.DayWednesday = openingHourUser.DayWednesday;
            serviceEstabView.HourEndFriday = openingHourUser.HourEndFriday;
            serviceEstabView.HourEndMonday = openingHourUser.HourEndMonday;
            serviceEstabView.HourEndSaturday = openingHourUser.HourEndSaturday;
            serviceEstabView.HourEndSunday = openingHourUser.HourEndSunday;
            serviceEstabView.HourEndThursday = openingHourUser.HourEndThursday;
            serviceEstabView.HourEndTuesday = openingHourUser.HourEndTuesday;
            serviceEstabView.HourEndWednesday = openingHourUser.HourEndWednesday;
            serviceEstabView.Hours24 = openingHourUser.Hours24;
            serviceEstabView.HourBeginFriday = openingHourUser.HourBeginFriday;
            serviceEstabView.HourBeginMonday = openingHourUser.HourBeginMonday;
            serviceEstabView.HourBeginSaturday = openingHourUser.HourBeginSaturday;
            serviceEstabView.HourBeginSunday = openingHourUser.HourBeginSunday;
            serviceEstabView.HourBeginThursday = openingHourUser.HourBeginThursday;
            serviceEstabView.HourBeginTuesday = openingHourUser.HourBeginTuesday;
            serviceEstabView.HourBeginWednesday = openingHourUser.HourBeginWednesday;
            serviceEstabView.MondayFriday = openingHourUser.MondayFriday;
            serviceEstabView.MondaySaturday = openingHourUser.MondaySaturday;
            serviceEstabView.SaturdaySunday = openingHourUser.SaturdaySunday;


            //address
            serviceEstabView.District = addressUser.District;
            serviceEstabView.Street = addressUser.Street;
            serviceEstabView.Number = addressUser.Number;
            serviceEstabView.CEP = addressUser.CEP;
            serviceEstabView.City = addressUser.City;
            serviceEstabView.State = addressUser.State;
            serviceEstabView.Complement = addressUser.Complement;

            //maps
            serviceEstabView.Lat = addressUser.Lat;
            serviceEstabView.Lon = addressUser.Lon;
            serviceEstabView.ImageCompany = worker.Image;







            return _serviceEstabViewRepository.Create(serviceEstabView);

        }

        public Entities.ServiceEstabView GetServiceEstabView(string idService)
        {
            return _serviceEstabViewRepository.FirstOrDefault(s => s.IdService == idService);

        }

        public Entities.ServiceEstabItem CreateServiceEstabItem(Entities.ProdService serviceEstab, string idService)
        {


            Entities.ServiceEstabItem serviceEstabItem = new Entities.ServiceEstabItem();

            serviceEstabItem.IdService = idService;
            var image = _imageProdServiceRepository.Find(i => i.IdProdService == serviceEstab.id);
            if (image.Count != 0)
                serviceEstabItem.Image = image[image.Count - 1].Url;

            var prodService = _produtoServicoRepository.FirstOrDefault(p => p.IdCoreProdutoServico == serviceEstab.IdCoreProdServ);
            serviceEstabItem.Name = prodService.StCoreProdutoServico;

            var worker = _workerRepository.FirstOrDefault(w => w.id == serviceEstab.IdWorker);
            serviceEstabItem.SocialReason = worker.SocialReason;

            var location = _addressRepository.FirstOrDefault(a => a.IdWorker == worker.id);
            serviceEstabItem.Lat = location.Lat;
            serviceEstabItem.Lon = location.Lon;



            var typeServiceEstab = _servEstabRepository.Find(s => s.IdProdService == serviceEstab.id);
            List<string> listItemsTypeServeEstab = new List<string>();


            for (int c = 0; c < typeServiceEstab.Count; c++)
            {



                var tipoServEstab = _tipoServicoEstabelecimentoRepository.FirstOrDefault(t => t.IdCoreTipoServicoEstabelecimento == typeServiceEstab[c].IdCoreTipoServicoEstabelecimento);
                listItemsTypeServeEstab.Add(tipoServEstab.StCoreTipoServicoEstabelecimento);


            }

            serviceEstabItem.CategoryType = listItemsTypeServeEstab;
            var typeOffer = _tipoOfertaRepository.FirstOrDefault(t => t.IdCoreTipoOferta == serviceEstab.IdCoreTipoOferta);
            serviceEstabItem.TypeOffer = typeOffer.StCoreTipoOferta;

            var cornestone = _pilarRepository.FirstOrDefault(p => p.IdCorePilar == serviceEstab.IdCorePilar);
            serviceEstabItem.Cornerstone = cornestone.StCorePilar;

            serviceEstabItem.Status = "pending";



            return _serviceEstabItemRepository.Create(serviceEstabItem);



        }




        public List<Entities.ProdService> GetProdService(string idWorker)
        {
            return _prodServiceRepository.Find(p => p.IdWorker == idWorker);
        }


    }
}
