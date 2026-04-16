# Internet Banking

## Internet Banking (IB)
This is the backend service for the Internet Banking application.
It provides secure APIs for account management, fund transfers, and transaction history.
Built using ASP.NET Core Web API, it is optimized for performance and easy integration with the React frontend.


## Features
Retrieve all bank accounts
Transfer funds between accounts
Real‑time balance updates
Validation rules:
Amount > 0
Maximum limit (25,000 AED)
Cannot transfer to the same account
Sufficient balance check
Stores and returns the latest 10 transactions
Clean JSON responses
CORS enabled for frontend communication


## Tech Stack
ASP.NET Core Web API
C#
.NET 8 / .NET 7
JWT Authentication
In‑memory or simple data storage

## Authentication & Authorization
Login returns a JWT token
Token must be included in Authorization: Bearer <token> header

## Installation
git clone https://github.com/MaheshMudike/Banking-backend.git
cd backend-app  Banking.Api
dotnet restore

## Build
Run `dotnet run` to run the project

## Author 
Mahesh Mudike
