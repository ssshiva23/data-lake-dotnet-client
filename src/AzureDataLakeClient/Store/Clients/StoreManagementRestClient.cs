using System.Collections.Generic;
using AzureDataLakeClient.Rm;
using Microsoft.Azure.Management.DataLake.Store;

namespace AzureDataLakeClient.Store.Clients
{
    public class StoreManagementRestClient
    {
        private Microsoft.Azure.Management.DataLake.Store.DataLakeStoreAccountManagementClient _rest_client;

        public StoreManagementRestClient(Microsoft.Azure.Management.DataLake.Store.DataLakeStoreAccountManagementClient _rest)
        {
            this._rest_client = _rest;
        }

        public IEnumerable<Microsoft.Azure.Management.DataLake.Store.Models.DataLakeStoreAccount> ListAccounts()
        {
            var page = this._rest_client.Account.List();
            foreach (var acc in RESTUtil.EnumItemsInPages(page,
                p => this._rest_client.Account.ListNext(p.NextPageLink)))
            {
                yield return acc;
            }
        }

        public IEnumerable<Microsoft.Azure.Management.DataLake.Store.Models.DataLakeStoreAccount> ListAccountsByResourceGroup(ResourceGroup resource_group)
        {
            var page = this._rest_client.Account.ListByResourceGroup(resource_group.Name);

            foreach (var acc in RESTUtil.EnumItemsInPages(page,
                p => this._rest_client.Account.ListByResourceGroupNext(p.NextPageLink)))
            {
                yield return acc;
            }
        }

        public Microsoft.Azure.Management.DataLake.Store.Models.DataLakeStoreAccount GetAccount(StoreAccount account)
        {
            return this._rest_client.Account.Get(account.ResourceGroup.Name, account.Name);
        }

        public void Update(StoreAccount account, Microsoft.Azure.Management.DataLake.Store.Models.DataLakeStoreAccountUpdateParameters parameters)
        {
            this._rest_client.Account.Update(account.ResourceGroup.Name, account.Name, parameters);
        }

        public void Delete(StoreAccount account)
        {
            this._rest_client.Account.Delete(account.ResourceGroup.Name, account.Name);
        }

        public bool Exists(StoreAccount account)
        {
            return this._rest_client.Account.Exists(account.ResourceGroup.Name, account.Name);
        }

    }
}