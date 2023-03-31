This is a copy of the Northwind example from https://github.com/odata/Restier. This allows you to test and develop OData clients without access to the Internet.

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/dwhite8405/Microsoft.Restier.Samples.Northwind.AspNetCore)

OData is a queryable REST service specification. https://www.odata.org/.

To run this locally, make sure you have the .NET SDK version 6.0 installed. This is the `dotnet-sdk-6.0` package on Debian-based distros. 

If you're running on Windows, then you need to edit appsettings.json and substitute the slashes in the
database filename to double backslashes, i.e.::

        "NorthwindEntities": "Data Source=.\\Database\\Northwind.db;Mode=ReadOnly;"

Then:

    $ dotnet build
    $ dotnet run

You should see:

    info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:56488
    info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:56489
    info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/usbd/Microsoft.Restier.Samples.Northwind.AspNetCore

Connect your browser to either of those two URLs to view the OData service.

Credit to @jpwhite3 for providing the Northwind database in SQLite format at https://github.com/jpwhite3/northwind-SQLite3

# TODO

Implement the changes here:
https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-7.0&tabs=linux-ubuntu