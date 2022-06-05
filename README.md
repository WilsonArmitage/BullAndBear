# BullAndBear

## Setup

This application uses the  Alpha Vantage API to retrieve stock related information.
While it is possible to use the key 'demo', possibly some functions may stop working.

Should any issues occur, a valid key can be inserted into the user secrets via:

 `dotnet user-secrets set "AlphaVantage:ServiceApiKey" "[INSERT KEY HERE]" --project PortfolioAPI`

 ## Application Diagram

```mermaid
  graph TD;
      Website-->Portfolio API;
      Website-->SQL Server Express;
      Portfolio API-->Alpha Vantage API;
      Portfolio API-->LiteDB;
```