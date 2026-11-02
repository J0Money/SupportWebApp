# SupportWebApp

Blazor Web App til at oprette og se support-henvendelser, med Azure Cosmos DB (NoSQL) som datalager.

## Teknologier
- Blazor (ASP.NET Core)
- Azure Cosmos DB

## Opsætning af Cosmos DB
```bash
export RESGRP="IBasSupportRG"
export DBACCOUNT="ibas-db-account-$RANDOM"
export DATABASE="IBasSupportDB"
export CONTAINER="ibassupport"
export PK="/category"

az cosmosdb create --name $DBACCOUNT --resource-group $RESGRP --enable-free-tier true

az cosmosdb sql database create \
  --account-name $DBACCOUNT \
  --resource-group $RESGRP \
  --name $DATABASE

az cosmosdb sql container create \
  --account-name $DBACCOUNT \
  --resource-group $RESGRP \
  --database-name $DATABASE \
  --name $CONTAINER \
  --partition-key-path $PK
```

## Status
Appen kan oprette og vise supporthenvendelser, som gemmes i Cosmos DB.

**Mulige næste skridt:** autentificering (login/roller), samt søgning og filtrering i oversigten.
