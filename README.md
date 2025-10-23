# Cosmos DB med Blazor WebApp

## Formål
En simpel Blazor Web App, der gemmer og henter support-henvendelser fra Azure Cosmos DB (NoSQL).



## Oprette ny CosmosDB til løsningen
export RESGRP="IBasSupportRG"
export DBACCOUNT="ibas-db-account-$RANDOM"
export DATABASE="IBasSupportDB"
export CONTAINER="ibassupport"
export PK="/category"

az cosmosdb create --name $ --resource-group $ --enable-free-tier true

az cosmosdb sql database create \
--account-name $DBACCOUNT \
--resource-group $RESGRP \
--name $DATABASE

az cosmosdb sql container create \
--account-name $DBACCOUNT \ 
--resource-group $RESGRP \
--database-name $DATABASE \
--name $CONTAINER \
--partition-key-path "//category"

## Status
### Det nåede jeg:

- Oprettet Blazor Web App, der kan oprette og vise supporthenvendelser
- Cosmos DB (NoSQL) oprettet med database og container
- Appen gemmer data  i Cosmos DB

### Det mangler:

- Der kunne have været brugt lidt mere tid på styling og brugergrænseflade (UI) for at gøre webappen mere lækker

### Næste trin måske:

- Tilføje autentificering (login/roller)
- TIlføje søgning og filtrering i oversigten