using Blazored.Modal;
using Blazored.Modal.Services;
using MealOrdering.Client.CustomCompanents.ModalCompanents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Client.Utils
{
    public class ModalManager
    {
        private readonly IModalService _modalService;
        public ModalManager(IModalService ModalService)
        {
            _modalService = ModalService;
        }
        public async Task ShowMessageAsync(String Title, String Message,int Duration = 0)
        {
            ModalParameters mParams = new ModalParameters();
            mParams.Add("Message", Message);
            var modalRef = _modalService.Show<ShowMessagePopupComponent>(Title, mParams);
            if (Duration > 0)
            {
                await Task.Delay(Duration);
                modalRef.Close();
            }
           // await modalRef.Result;
        }
        public async Task<bool> ConfirmationAsync(String Title, String Message)
        {
            ModalParameters mParams = new ModalParameters();
            mParams.Add("Message", Message);
            var modalRef = _modalService.Show<ComfirmationPopupComponent>(Title, mParams);
            var res =await modalRef.Result;
            return !res.Cancelled;
        }
    }
}
