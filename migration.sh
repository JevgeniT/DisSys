#!/bin/sh
dotnet ef database update 0 --project DAL.App.EF --startup-project WebApp
dotnet ef migrations remove --project DAL.App.EF --startup-project WebApp
dotnet ef migrations add Init --project DAL.App.EF --startup-project WebApp
dotnet ef database update --project DAL.App.EF --startup-project WebApp
