# insurance-api
.NET 6 Insurance API featuring 4 endpoints
- GET /api/claims/{id} - returns details of one claim with a property of how old the claim is in days
- PUT /api/claims/{id} - allows updating of a claim
- GET /api/company/{id} - returns single company with a property of whether the insurance is active
- GET /api/company/{id}/claims - returns list of claims for one company

## Getting Started
- Have SQL Server installed on your machine
- Change the 'server' in app.settings.json to your local one
- Using the package manager console run Update-Database. This will seed the db as well.
  - *Ensure Insurance.API is the startup project and Insurance.DataAccess is default project in package manager console for this to work*
- On completion of the above steps. Run the service and try it out.
