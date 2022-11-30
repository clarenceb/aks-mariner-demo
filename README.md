CBL-Mariner 2.0 use in AKS
==========================

Create AKS cluster with Mariner system nodepool
-----------------------------------------------

```sh
az group create --name MarinerTest --location australiasoutheast
az aks get-versions -l australiasoutheast -o table
az aks create --name testMarinerCluster --resource-group MarinerTest --os-sku mariner -c 3 -k 1.24.6 -a monitoring
az aks get-credentials --resource-group MarinerTest --name testMarinerCluster

kubectl get pods --all-namespaces
kubectl get nodes -o wide

# NAME                                STATUS   ROLES   AGE    VERSION   ...  OS-IMAGE            KERNEL-VERSION    CONTAINER-RUNTIME
# aks-nodepool1-67251971-vmss000000   Ready    agent   161m   v1.24.6   ...  CBL-Mariner/Linux   5.15.70.1-1.cm2   containerd://1.6.6
# ...
```

Create a .NET 6 app based on Mariner distroless image
-----------------------------------------------------

```sh
dotnet --version
# 6.0.100

dotnet new webapp -n webapp
cd webapp
```

Update file `webapp/Pages/Index.cshtml.cs` to log some info for ContainerInsights:

```C#
public void OnGet()
{
    _logger.LogInformation("IndexModel - OnGet accessed from {IP} ({UserAgent}",
        HttpContext.Connection.RemoteIpAddress,
        HttpContext.Request.Headers["User-Agent"]);
}
```

Test webapp locally first:

```sh
dotnet dev-certs https --trust
dotnet run
# CTRL+C
```

(Optional) Build Docker image locally and test it (requires Docker Desktop or alternative container engine)
-----------------------------------------------------------------------------------------------------------

```sh
cd webapp/
docker build -t webapp:mariner .
docker run --rm -ti -p 8080:8080 webapp:mariner
# Access http://localhost:8080 in your browser
```

Create an Azure Container Registry (ACR)
----------------------------------------

```sh
az acr create -n marineracrtest -g MarinerTest -l australiasoutheast --sku Standard
```

Publish image to ACR
--------------------

```sh

```

Grant access to ACR for the AKS cluster
---------------------------------------

Deploy webapp to AKS using Mariner image
----------------------------------------

Check logs are collected via Container Insights
-----------------------------------------------


References
----------

* https://learn.microsoft.com/EN-us/azure/aks/use-mariner
* https://learn.microsoft.com/EN-us/azure/aks/cluster-configuration#mariner-os
