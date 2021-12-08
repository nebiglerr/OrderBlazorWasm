using Blazored.LocalStorage;
using MealOrdering.Client.Utils;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.Dto;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MealOrdering.Client.Pages.Users
{
    public class UserListProcess : ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }
        protected List<UserDto> userList = new List<UserDto>();
        [Inject]
        ModalManager ModalManager { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadList();

        }
        protected void goCreateUserPage()
        {
            NavigationManager.NavigateTo("/users/add");
        }
        protected void goEditUserPage(Guid UserId)
        {
            NavigationManager.NavigateTo("/users/edit/"+UserId);
        }
        protected async Task DeleteUser(Guid UserId)
        {
         bool confirmed =   await ModalManager.ConfirmationAsync("Confirmation", "User will be deleted. Are you sure ?");
            if (!confirmed) return;
            try
            {
               bool deleted = await Client.PostGetServiceResponseAsync<bool, Guid>("api/user/delete", UserId,true);
                await LoadList();
            }
            catch (ApiException ex)
            {

                await ModalManager.ShowMessageAsync("User Deletion Error", ex.Message);
               
            }
            catch (Exception ex)
            {
                await ModalManager.ShowMessageAsync("Exptions", ex.Message);
            }
        }
        protected async Task LoadList()
        {
            try
            {
                userList = await Client.GetServiceResponseAsync<List<UserDto>>("api/User/Users", true);
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("Api Exptions", ex.Message);
            }
            catch (Exception ex)
            {
                await ModalManager.ShowMessageAsync("Exptions", ex.Message);
            }
        }
    }
}
