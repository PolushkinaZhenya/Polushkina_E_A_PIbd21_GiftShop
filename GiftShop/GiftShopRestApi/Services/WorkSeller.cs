using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiftShopServiceDAL.BindingModel;
using GiftShopServiceDAL.Interfaces;
using System.Threading;

namespace GiftShopRestApi.Services
{
    public class WorkSeller
    {
        private readonly IMainService _service;

        private readonly ISellerService _serviceSeller;

        private readonly int _sellerId;

        private readonly int _procedureId;

        // семафор
        static Semaphore _sem = new Semaphore(3, 3);

        Thread myThread;

        public WorkSeller(IMainService service, ISellerService
        serviceSeller, int sellerId, int procedureId)
        {
            _service = service;
            _serviceSeller = serviceSeller;
            _sellerId = sellerId;
            _procedureId = procedureId;
            try
            {
                _service.TakeProcedureInWork(new ProcedureBindingModel
                {
                    Id = _procedureId,
                    SellerId = _sellerId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            myThread = new Thread(Work);
            myThread.Start();
        }
        public void Work()
        {
            try
            {
                // забиваем мастерскую
                _sem.WaitOne();
                // Типа выполняем
                Thread.Sleep(10);
                _service.FinishProcedure(new ProcedureBindingModel
                {
                    Id = _procedureId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // освобождаем мастерскую
                _sem.Release();
            }
        }
    }
}